using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Repositories
{
    public class MineRepository: IMineRepository
    {
        private DbSession _db;
        
        public MineRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(Mine mine)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    //Required
                    if (mine.DepositId == 0) { return 0; }
                    if (mine.UserId    == 0) { return 0; }
                    //Not Required
                    var sizeId = "null";
                    if (mine.SizeId > 0) { 
                        sizeId = mine.SizeId.ToString(); }
                    var statusId = "null";
                    if (mine.StatusId > 0) { 
                        statusId = mine.StatusId.ToString(); }
                    var statusPreviousId = "null";
                    if (mine.StatusPreviousId > 0) { 
                        statusPreviousId = mine.StatusPreviousId.ToString(); }
                    string command = @"INSERT INTO MINE(   
                                            depositId, name, latitude, longitude, startYear, endYear, 
                                            resource, reserve, oreMined, comments, sizeId, statusId, statusPreviousId, 
                                            imgTypeProfile, imgTypeCover, userId, register) 
                                        VALUES(@depositId, @name, @latitude, @longitude, @startYear, @endYear, @resource, 
                                            @reserve, @oreMined, @comments, " + sizeId + ", " + statusId + ", " +
                                            statusPreviousId + ", " + "@imgTypeProfile, @imgTypeCover, @userId, @register ); " +
                                        "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: mine);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(Mine mine)
        {
            try
            {
                var conn = _db.Connection;
                //Required
                if (mine.DepositId == 0) { return 0; }
                if (mine.UserId    == 0) { return 0; }
                //Not Required
                var sizeId = "null";
                if (mine.SizeId > 0) { 
                    sizeId = mine.SizeId.ToString(); }
                var statusId = "null";
                if (mine.StatusId > 0) { 
                    statusId = mine.StatusId.ToString(); }
                var statusPreviousId = "null";
                if (mine.StatusPreviousId > 0) { 
                    statusPreviousId = mine.StatusPreviousId.ToString(); }
                string command = @"UPDATE MINE SET 
                                    depositId           = @depositId,
                                    name                = @name, 
                                    latitude            = @latitude, 
                                    longitude           = @longitude, 
                                    startYear           = @startYear, 
                                    endYear             = @endYear, 
                                    resource            = @resource, 
                                    reserve             = @reserve,  
                                    oreMined            = @oreMined,
                                    comments            = @comments,
                                    sizeId              = " + sizeId + @", 
                                    statusId            = " + statusId + @", 
                                    statusPreviousId    = " + statusPreviousId + @",
                                    imgTypeProfile      = @imgTypeProfile,
                                    imgTypeCover        = @imgTypeCover  
                                    WHERE id            = @id";
                var result = await conn.ExecuteAsync(sql: command, param: mine);
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
                string command = @"DELETE FROM MINE WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Mine>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT M.*, 
                                    (SELECT COUNT(MA.Id) FROM MineArea MA
                                     WHERE MA.MineId = M.Id) AS QttMineAreas,
                                    (SELECT COUNT(DH.Id) FROM DrillHole DH
                                     WHERE DH.MineId = M.Id) AS QttDrillHoles,
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
                                     INNER JOIN DrillHole DH ON DB.DrillHoleId = DH.Id
                                     WHERE DH.MineId = M.Id) AS QttDrillBoxes,
                                'split', D.*, 'split', R.*, 'split', 
                                S.*, 'split', T.*, 'split', P.*, 'split', U.*
                                FROM Mine M  
                                INNER JOIN Deposit      D   ON M.DepositId         = D.Id
                                INNER JOIN Region       R   ON D.RegionId          = R.Id
                                LEFT  JOIN MineSize     S   ON M.SizeId            = S.Id
                                LEFT  JOIN MineStatus   T   ON M.StatusId          = T.Id
                                LEFT  JOIN MineStatus   P   ON M.StatusPreviousId  = P.Id
                                INNER JOIN User         U   ON M.UserId            = U.Id"; 
                if (term != ""){
                    query = query + "WHERE M.Name LIKE '%" + term + "%' " +
                                    "OR    D.Name LIKE '%" + term + "%' " +
                                    "OR    R.Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Mine>(
                    query, 
                    new[] {
                        typeof(Mine),
                        typeof(Deposit),
                        typeof(Region),
                        typeof(MineSize),
                        typeof(MineStatus),
                        typeof(MineStatus),
                        typeof(User),
                    },
                    objects => {
                        Mine        mine           = objects[0] as Mine;
                        Deposit     deposit        = objects[1] as Deposit;
                        Region      region         = objects[2] as Region;
                        MineSize    size           = objects[3] as MineSize;
                        MineStatus  status         = objects[4] as MineStatus;
                        MineStatus  statusPrevious = objects[5] as MineStatus;
                        User        user           = objects[6] as User;
                        //Dependency required
                        deposit.Region  = region;
                        mine.Deposit    = deposit;
                        mine.User       = user;
                        //Dependency not required
                        if (size.Id > 0)           { mine.Size           = size; }
                        if (status.Id > 0)         { mine.Status         = status; }
                        if (statusPrevious.Id > 0) { mine.StatusPrevious = statusPrevious; }
                       //Return
                        return mine;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<Mine>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Mine>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT M.*, 
                                    (SELECT COUNT(MA.Id) FROM MineArea MA
                                     WHERE MA.MineId = M.Id) AS QttMineAreas,
                                    (SELECT COUNT(DH.Id) FROM DrillHole DH
                                     WHERE DH.MineId = M.Id) AS QttDrillHoles,
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
                                     INNER JOIN DrillHole DH ON DB.DrillHoleId = DH.Id
                                     WHERE DH.MineId = M.Id) AS QttDrillBoxes,
                                'split', D.*, 'split', R.*, 'split', 
                                S.*, 'split', T.*, 'split', P.*, 'split', U.*
                                FROM Mine M  
                                INNER JOIN Deposit      D   ON M.DepositId         = D.Id
                                INNER JOIN Region       R   ON D.RegionId          = R.Id
                                LEFT  JOIN MineSize     S   ON M.SizeId            = S.Id
                                LEFT  JOIN MineStatus   T   ON M.StatusId          = T.Id
                                LEFT  JOIN MineStatus   P   ON M.StatusPreviousId  = P.Id
                                INNER JOIN User         U   ON M.UserId            = U.Id
                                WHERE R.AccountId = @accountId "; 
                if (term != ""){
                    query = query + "AND (M.Name LIKE '%" + term + "%' " +
                                    "OR   D.Name LIKE '%" + term + "%' " +
                                    "OR   R.Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Mine>(
                    query, 
                    new[] {
                        typeof(Mine),
                        typeof(Deposit),
                        typeof(Region),
                        typeof(MineSize),
                        typeof(MineStatus),
                        typeof(MineStatus),
                        typeof(User),
                    },
                    objects => {
                        Mine        mine           = objects[0] as Mine;
                        Deposit     deposit        = objects[1] as Deposit;
                        Region      region         = objects[2] as Region;
                        MineSize    size           = objects[3] as MineSize;
                        MineStatus  status         = objects[4] as MineStatus;
                        MineStatus  statusPrevious = objects[5] as MineStatus;
                        User        user           = objects[6] as User;
                        //Dependency required
                        deposit.Region  = region;
                        mine.Deposit    = deposit;
                        mine.User       = user;
                        //Dependency not required
                        if (size.Id > 0)           { mine.Size           = size; }
                        if (status.Id > 0)         { mine.Status         = status; }
                        if (statusPrevious.Id > 0) { mine.StatusPrevious = statusPrevious; }
                       //Return
                        return mine;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<Mine>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Mine>> GetByRegion(int regionId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT M.*, 
                                    (SELECT COUNT(MA.Id) FROM MineArea MA
                                     WHERE MA.MineId = M.Id) AS QttMineAreas,
                                    (SELECT COUNT(DH.Id) FROM DrillHole DH
                                     WHERE DH.MineId = M.Id) AS QttDrillHoles,
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
                                     INNER JOIN DrillHole DH ON DB.DrillHoleId = DH.Id
                                     WHERE DH.MineId = M.Id) AS QttDrillBoxes,
                                'split', D.*, 'split', R.*, 'split', 
                                S.*, 'split', T.*, 'split', P.*, 'split', U.*
                                FROM Mine M  
                                INNER JOIN Deposit      D   ON M.DepositId         = D.Id
                                INNER JOIN Region       R   ON D.RegionId          = R.Id
                                LEFT  JOIN MineSize     S   ON M.SizeId            = S.Id
                                LEFT  JOIN MineStatus   T   ON M.StatusId          = T.Id
                                LEFT  JOIN MineStatus   P   ON M.StatusPreviousId  = P.Id
                                INNER JOIN User         U   ON M.UserId            = U.Id
                                WHERE R.Id = @regionId "; 
                if (term != ""){
                    query = query + "AND (M.Name LIKE '%" + term + "%' " +
                                    "OR   D.Name LIKE '%" + term + "%' " +
                                    "OR   R.Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Mine>(
                    query, 
                    new[] {
                        typeof(Mine),
                        typeof(Deposit),
                        typeof(Region),
                        typeof(MineSize),
                        typeof(MineStatus),
                        typeof(MineStatus),
                        typeof(User),
                    },
                    objects => {
                        Mine        mine           = objects[0] as Mine;
                        Deposit     deposit        = objects[1] as Deposit;
                        Region      region         = objects[2] as Region;
                        MineSize    size           = objects[3] as MineSize;
                        MineStatus  status         = objects[4] as MineStatus;
                        MineStatus  statusPrevious = objects[5] as MineStatus;
                        User        user           = objects[6] as User;
                        //Dependency required
                        deposit.Region  = region;
                        mine.Deposit    = deposit;
                        mine.User       = user;
                        //Dependency not required
                        if (size.Id > 0)           { mine.Size           = size; }
                        if (status.Id > 0)         { mine.Status         = status; }
                        if (statusPrevious.Id > 0) { mine.StatusPrevious = statusPrevious; }
                       //Return
                        return mine;
                    },
                    splitOn: "split",
                    param: new { regionId });
                return await PageList<Mine>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Mine>> GetByDeposit(int depositId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT M.*, 
                                    (SELECT COUNT(MA.Id) FROM MineArea MA
                                     WHERE MA.MineId = M.Id) AS QttMineAreas,
                                    (SELECT COUNT(DH.Id) FROM DrillHole DH
                                     WHERE DH.MineId = M.Id) AS QttDrillHoles,
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
                                     INNER JOIN DrillHole DH ON DB.DrillHoleId = DH.Id
                                     WHERE DH.MineId = M.Id) AS QttDrillBoxes,
                                'split', D.*, 'split', R.*, 'split', 
                                S.*, 'split', T.*, 'split', P.*, 'split', U.*
                                FROM Mine M  
                                INNER JOIN Deposit      D   ON M.DepositId         = D.Id
                                INNER JOIN Region       R   ON D.RegionId          = R.Id
                                LEFT  JOIN MineSize     S   ON M.SizeId            = S.Id
                                LEFT  JOIN MineStatus   T   ON M.StatusId          = T.Id
                                LEFT  JOIN MineStatus   P   ON M.StatusPreviousId  = P.Id
                                INNER JOIN User         U   ON M.UserId            = U.Id
                                WHERE D.Id = @depositId "; 
                if (term != ""){
                    query = query + "AND (M.Name LIKE '%" + term + "%' " +
                                    "OR   D.Name LIKE '%" + term + "%' " +
                                    "OR   R.Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Mine>(
                    query, 
                    new[] {
                        typeof(Mine),
                        typeof(Deposit),
                        typeof(Region),
                        typeof(MineSize),
                        typeof(MineStatus),
                        typeof(MineStatus),
                        typeof(User),
                    },
                    objects => {
                        Mine        mine           = objects[0] as Mine;
                        Deposit     deposit        = objects[1] as Deposit;
                        Region      region         = objects[2] as Region;
                        MineSize    size           = objects[3] as MineSize;
                        MineStatus  status         = objects[4] as MineStatus;
                        MineStatus  statusPrevious = objects[5] as MineStatus;
                        User        user           = objects[6] as User;
                        //Dependency required
                        deposit.Region  = region;
                        mine.Deposit    = deposit;
                        mine.User       = user;
                        //Dependency not required
                        if (size.Id > 0)           { mine.Size           = size; }
                        if (status.Id > 0)         { mine.Status         = status; }
                        if (statusPrevious.Id > 0) { mine.StatusPrevious = statusPrevious; }
                       //Return
                        return mine;
                    },
                    splitOn: "split",
                    param: new { depositId });
                return await PageList<Mine>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Mine> GetById(int id)
        {
            try
            {
                var conn = _db.Connection; 
                string query = @"SELECT M.*, 
                                    (SELECT COUNT(MA.Id) FROM MineArea MA
                                     WHERE MA.MineId = M.Id) AS QttMineAreas,
                                    (SELECT COUNT(DH.Id) FROM DrillHole DH
                                     WHERE DH.MineId = M.Id) AS QttDrillHoles,
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
                                     INNER JOIN DrillHole DH ON DB.DrillHoleId = DH.Id
                                     WHERE DH.MineId = M.Id) AS QttDrillBoxes,
                                'split', D.*, 'split', R.*, 'split', 
                                S.*, 'split', T.*, 'split', P.*, 'split', U.*
                                FROM Mine M  
                                INNER JOIN Deposit      D   ON M.DepositId         = D.Id
                                INNER JOIN Region       R   ON D.RegionId          = R.Id
                                LEFT  JOIN MineSize     S   ON M.SizeId            = S.Id
                                LEFT  JOIN MineStatus   T   ON M.StatusId          = T.Id
                                LEFT  JOIN MineStatus   P   ON M.StatusPreviousId  = P.Id
                                INNER JOIN User         U   ON M.UserId            = U.Id
                                WHERE M.Id = @id";
                var res = await conn.QueryAsync<Mine>(
                    query, 
                    new[] {
                        typeof(Mine),
                        typeof(Deposit),
                        typeof(Region),
                        typeof(MineSize),
                        typeof(MineStatus),
                        typeof(MineStatus),
                        typeof(User),
                    },
                    objects => {
                        Mine        mine           = objects[0] as Mine;
                        Deposit     deposit        = objects[1] as Deposit;
                        Region      region         = objects[2] as Region;
                        MineSize    size           = objects[3] as MineSize;
                        MineStatus  status         = objects[4] as MineStatus;
                        MineStatus  statusPrevious = objects[5] as MineStatus;
                        User        user           = objects[6] as User;
                        //Dependency required
                        deposit.Region  = region;
                        mine.Deposit    = deposit;
                        mine.User       = user;
                        //Dependency not required
                        if (size.Id > 0)           { mine.Size           = size; }
                        if (status.Id > 0)         { mine.Status         = status; }
                        if (statusPrevious.Id > 0) { mine.StatusPrevious = statusPrevious; }
                       //Return
                        return mine;
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