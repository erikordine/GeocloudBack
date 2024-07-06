using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Repositories
{
    public class RegionRepository: IRegionRepository
    {
        private DbSession _db;
        
        public RegionRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(Region region)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    //Required
                    if (region.AccountId == 0) { return 0; }
                    if (region.CountryId == 0) { return 0; }
                    if (region.UserId    == 0) { return 0; }
                    string command = @"INSERT INTO REGION(
                                            accountId, name, countryId, state, city, latitude, longitude, 
                                            comments, imgTypeProfile, imgTypeCover, userId, register)
                                         VALUES(@accountId, @name, @countryId, @state, @city, @latitude, @longitude, 
                                            @comments, @imgTypeProfile, @imgTypeCover, @userId, @register ); " +
                                        "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: region);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(Region region)
        {
            try
            {
                var conn = _db.Connection;
                //Required
                if (region.AccountId == 0) { return 0; }
                if (region.CountryId == 0) { return 0; }
                if (region.UserId    == 0) { return 0; }
                string command = @"UPDATE REGION SET 
                                    accountId       = @accountId,
                                    name            = @name, 
                                    countryId       = @countryId, 
                                    state           = @state, 
                                    city            = @city, 
                                    latitude        = @latitude, 
                                    longitude       = @longitude, 
                                    comments        = @comments,
                                    imgTypeProfile  = @imgTypeProfile,
                                    imgTypeCover    = @imgTypeCover  
                                    WHERE id        = @id";
                var result = await conn.ExecuteAsync(sql: command, param: region);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                var conn = _db.Connection;
                string command = @"DELETE FROM REGION WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Region>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT R.*, 
                                    (SELECT COUNT(D.Id) FROM Deposit D 
                                     WHERE D.RegionId = R.Id) AS QttDeposits,
                                    (SELECT COUNT(M.Id) FROM Mine M 
                                     INNER JOIN Deposit D ON M.DepositId = D.Id 
                                     WHERE D.RegionId = R.Id) AS QttMines,
                                    (SELECT COUNT(MA.Id) FROM MineArea MA 
                                     INNER JOIN Mine M ON MA.MineId = M.Id
                                     INNER JOIN Deposit D ON M.DepositId = D.Id
                                     WHERE D.RegionId = R.Id) AS QttMineAreas,
                                    (SELECT COUNT(DH.Id) FROM DrillHole DH
                                     WHERE DH.RegionId = R.Id) AS QttDrillHoles,
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
                                     INNER JOIN DrillHole DH ON DB.DrillHoleId = DH.Id
                                     WHERE DH.RegionId = R.Id) AS QttDrillBoxes, 
                                'split', A.*, 'split', C.*, 'split', U.*
                                FROM Region R  
                                INNER JOIN Account  A   ON R.AccountId = A.Id
                                INNER JOIN Country  C   ON R.CountryId = C.Id
                                INNER JOIN User     U   ON R.UserId    = U.Id ";
                if (term != ""){
                    query = query + "WHERE (R.Name  LIKE '%" + term + "%' " +
                                    "OR     C.Name  LIKE '%" + term + "%' " +
                                    "OR     R.State LIKE '%" + term + "%' " +
                                    "OR     R.City  LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Region>(
                    query, 
                    new[] {
                        typeof(Region),
                        typeof(Account),
                        typeof(Country),
                        typeof(User),
                    },
                    objects => {
                        Region   region  = objects[0] as Region;
                        Account  account = objects[1] as Account;
                        Country  country = objects[2] as Country;
                        User     user    = objects[3] as User;
                        //Dependency required
                        region.User     = user;
                        region.Account  = account;
                        region.Country  = country;
                        //Return
                        return region;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<Region>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Region>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT R.*, 
                                    (SELECT COUNT(D.Id) FROM Deposit D 
                                     WHERE D.RegionId = R.Id) AS QttDeposits,
                                    (SELECT COUNT(M.Id) FROM Mine M 
                                     INNER JOIN Deposit D ON M.DepositId = D.Id 
                                     WHERE D.RegionId = R.Id) AS QttMines,
                                    (SELECT COUNT(MA.Id) FROM MineArea MA 
                                     INNER JOIN Mine M ON MA.MineId = M.Id
                                     INNER JOIN Deposit D ON M.DepositId = D.Id
                                     WHERE D.RegionId = R.Id) AS QttMineAreas,
                                    (SELECT COUNT(DH.Id) FROM DrillHole DH
                                     WHERE DH.RegionId = R.Id) AS QttDrillHoles,
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
                                     INNER JOIN DrillHole DH ON DB.DrillHoleId = DH.Id
                                     WHERE DH.RegionId = R.Id) AS QttDrillBoxes, 
                                'split', A.*, 'split', C.*, 'split', U.*
                                FROM Region R  
                                INNER JOIN Account  A   ON R.AccountId = A.Id
                                INNER JOIN Country  C   ON R.CountryId = C.Id
                                INNER JOIN User     U   ON R.UserId    = U.Id
                                WHERE A.Id = @accountId "; 
                if (term != ""){
                    query = query + "AND (R.Name  LIKE '%" + term + "%' " +
                                    "OR   C.Name  LIKE '%" + term + "%' " +
                                    "OR   R.State LIKE '%" + term + "%' " +
                                    "OR   R.City  LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Region>(
                    query, 
                    new[] {
                        typeof(Region),
                        typeof(Account),
                        typeof(Country),
                        typeof(User),
                    },
                    objects => {
                        Region   region  = objects[0] as Region;
                        Account  account = objects[1] as Account;
                        Country  country = objects[2] as Country;
                        User     user    = objects[3] as User;
                        //Dependency required
                        region.User     = user;
                        region.Account  = account;
                        region.Country  = country;
                        //Return
                        return region;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<Region>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Region> GetById(int id)
        {
            try
            {
                var conn = _db.Connection; 
                string query = @"SELECT R.*, 
                                    (SELECT COUNT(D.Id) FROM Deposit D 
                                     WHERE D.RegionId = R.Id) AS QttDeposits,
                                    (SELECT COUNT(M.Id) FROM Mine M 
                                     INNER JOIN Deposit D ON M.DepositId = D.Id 
                                     WHERE D.RegionId = R.Id) AS QttMines,
                                    (SELECT COUNT(MA.Id) FROM MineArea MA 
                                     INNER JOIN Mine M ON MA.MineId = M.Id
                                     INNER JOIN Deposit D ON M.DepositId = D.Id
                                     WHERE D.RegionId = R.Id) AS QttMineAreas,
                                    (SELECT COUNT(DH.Id) FROM DrillHole DH
                                     WHERE DH.RegionId = R.Id) AS QttDrillHoles,
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB 
                                     INNER JOIN DrillHole DH ON DB.DrillHoleId = DH.Id
                                     WHERE DH.RegionId = R.Id) AS QttDrillBoxes,
                                'split', A.*, 'split', C.*, 'split', U.*
                                FROM Region R  
                                INNER JOIN Account  A   ON R.AccountId = A.Id
                                INNER JOIN Country  C   ON R.CountryId = C.Id
                                INNER JOIN User     U   ON R.UserId    = U.Id
                                WHERE R.Id = @id";
                var res = await conn.QueryAsync<Region>(
                    query, 
                    new[] {
                        typeof(Region),
                        typeof(Account),
                        typeof(Country),
                        typeof(User),
                    },
                    objects => {
                        Region   region  = objects[0] as Region;
                        Account  account = objects[1] as Account;
                        Country  country = objects[2] as Country;
                        User     user    = objects[3] as User;
                        //Dependency required
                        region.User     = user;
                        region.Account  = account;
                        region.Country  = country;
                        //Return
                        return region;
                    },
                    splitOn: "split",
                    param: new { id });
                if (res.Count() == 0) return null; 
                return res.First();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}