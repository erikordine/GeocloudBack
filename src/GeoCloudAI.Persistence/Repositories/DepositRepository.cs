using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Repositories
{
    public class DepositRepository: IDepositRepository
    {
        private DbSession _db;
        
        public DepositRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(Deposit deposit)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    //Required
                    if (deposit.RegionId == 0) { return 0; }
                    if (deposit.UserId   == 0) { return 0; }
                    //Not Required
                    var depositTypeId = "null";
                    if (deposit.DepositTypeId > 0) { 
                        depositTypeId = deposit.DepositTypeId.ToString(); }
                    var oreGeneticTypeId = "null";
                    if (deposit.OreGeneticTypeId > 0) { 
                        oreGeneticTypeId = deposit.OreGeneticTypeId.ToString(); }
                    var oreGeneticTypeSubId = "null";
                    if (deposit.OreGeneticTypeSubId > 0) { 
                        oreGeneticTypeSubId = deposit.OreGeneticTypeSubId.ToString(); }
                    var metalGroupId = "null";
                    if (deposit.MetalGroupId > 0) { 
                        metalGroupId = deposit.MetalGroupId.ToString(); }
                    var metalGroupSubId = "null";
                    if (deposit.MetalGroupSubId > 0) { 
                        metalGroupSubId = deposit.MetalGroupSubId.ToString(); }
                    string command = @"INSERT INTO DEPOSIT(
                                            regionId, name, alternativeNames, state, city, latitude, longitude, 
                                            geologicalDistrict, discoveryBy, discoveryYear, resource, reserve, comments,
                                            depositTypeId, oregeneticTypeId, oregeneticTypeSubId, metalGroupId, 
                                            metalGroupSubId, imgTypeProfile, imgTypeCover, userId, register) 
                                        VALUES(@regionId, @name, @alternativeNames, @state, @city, " +
                                            "@latitude, @longitude, @geologicalDistrict, @discoveryBy, @discoveryYear, @resource, " +
                                            "@reserve, @comments, " + depositTypeId + ", " + oreGeneticTypeId + ", " +
                                            oreGeneticTypeSubId + ", " + metalGroupId + ", " + metalGroupSubId + ", " + 
                                            "@imgTypeProfile, @imgTypeCover, @userId, @register ); " +
                                        "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: deposit);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(Deposit deposit)
        {
            try
            {
                var conn = _db.Connection;
                //Required
                if (deposit.RegionId == 0) { return 0; }
                if (deposit.UserId   == 0) { return 0; }
                //Not Required
                var depositTypeId = "null";
                if (deposit.DepositTypeId > 0) { 
                    depositTypeId = deposit.DepositTypeId.ToString(); }
                var oreGeneticTypeId = "null";
                if (deposit.OreGeneticTypeId > 0) { 
                    oreGeneticTypeId = deposit.OreGeneticTypeId.ToString(); }
                var oreGeneticTypeSubId = "null";
                if (deposit.OreGeneticTypeSubId > 0) { 
                    oreGeneticTypeSubId = deposit.OreGeneticTypeSubId.ToString(); }
                var metalGroupId = "null";
                if (deposit.MetalGroupId > 0) { 
                    metalGroupId = deposit.MetalGroupId.ToString(); }
                var metalGroupSubId = "null";
                if (deposit.MetalGroupSubId > 0) { 
                    metalGroupSubId = deposit.MetalGroupSubId.ToString(); }
                string command = @"UPDATE DEPOSIT SET 
                                        regionId            = @regionId,
                                        name                = @name, 
                                        alternativeNames    = @alternativeNames, 
                                        state               = @state, 
                                        city                = @city, 
                                        latitude            = @latitude, 
                                        longitude           = @longitude, 
                                        geologicalDistrict  = @geologicalDistrict, 
                                        discoveryBy         = @discoveryBy, 
                                        discoveryYear       = @discoveryYear, 
                                        resource            = @resource, 
                                        reserve             = @reserve, 
                                        comments            = @comments,
                                        depositTypeId       = " + depositTypeId + @", 
                                        oreGeneticTypeId    = " + oreGeneticTypeId + @", 
                                        oreGeneticTypeSubId = " + oreGeneticTypeSubId + @",
                                        metalGroupId        = " + metalGroupId + @", 
                                        metalGroupSubId     = " + metalGroupSubId + @",
                                        imgTypeProfile      = @imgTypeProfile,
                                        imgTypeCover        = @imgTypeCover  
                                        WHERE id            = @id";
                var result = await conn.ExecuteAsync(sql: command, param: deposit);
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
                string command = @"DELETE FROM DEPOSIT WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Deposit>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT D.*, 
                                    (SELECT COUNT(MX.Id) FROM Mine MX
                                     WHERE MX.DepositId = D.Id) AS QttMines,
                                    (SELECT COUNT(MA.Id) FROM MineArea MA
                                     INNER JOIN Mine MX ON MA.MineId = MX.Id 
                                     WHERE MX.DepositId = D.Id) AS QttMineAreas, 
                                    (SELECT COUNT(DH.Id) FROM DrillHole DH
                                     WHERE DH.DepositId = D.Id) AS QttDrillHoles,
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
                                     INNER JOIN DrillHole DH ON DB.DrillHoleId = DH.Id
                                     WHERE DH.DepositId = D.Id) AS QttDrillBoxes,
                                'split', R.*, 'split', T.*, 'split', O.*, 'split', 
                                OS.*, 'split', M.*, 'split', MS.*, 'split', U.*
                                FROM Deposit D  
                                INNER JOIN Region            R   ON D.RegionId             = R.Id
                                LEFT  JOIN DepositType       T   ON D.DepositTypeId        = T.Id
                                LEFT  JOIN OreGeneticType    O   ON D.OreGeneticTypeId     = O.Id
                                LEFT  JOIN OreGeneticTypeSub OS  ON D.OreGeneticTypeSubId  = OS.Id
                                LEFT  JOIN MetalGroup        M   ON D.MetalGroupId         = M.Id
                                LEFT  JOIN MetalGroupSub     MS  ON D.MetalGroupSubId      = MS.Id
                                INNER JOIN User              U   ON D.UserId               = U.Id";
                if (term != ""){
                    query = query + "WHERE (D.Name   LIKE '%" + term + "%' " +
                                    "OR     R.Name   LIKE '%" + term + "%' " +
                                    "OR     T.Name   LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Deposit>(
                    query, 
                    new[] {
                        typeof(Deposit),
                        typeof(Region),
                        typeof(DepositType),
                        typeof(OreGeneticType),
                        typeof(OreGeneticTypeSub),
                        typeof(MetalGroup),
                        typeof(MetalGroupSub),
                        typeof(User),
                    },
                    objects => {
                        Deposit           deposit           = objects[0] as Deposit;
                        Region            region            = objects[1] as Region;
                        DepositType       depositType       = objects[2] as DepositType;
                        OreGeneticType    oreGeneticType    = objects[3] as OreGeneticType;
                        OreGeneticTypeSub oreGeneticTypeSub = objects[4] as OreGeneticTypeSub;
                        MetalGroup        metalGroup        = objects[5] as MetalGroup;
                        MetalGroupSub     metalGroupSub     = objects[6] as MetalGroupSub;
                        User              user              = objects[7] as User;
                        //Dependency required
                        deposit.Region = region;
                        deposit.User   = user;
                        //Dependency not required
                        if (depositType.Id > 0) { 
                            deposit.DepositType = depositType; 
                        }
                        if (oreGeneticType.Id > 0) { 
                            deposit.OreGeneticType = oreGeneticType; 
                        }
                        if (oreGeneticTypeSub.Id > 0) { 
                            deposit.OreGeneticTypeSub = oreGeneticTypeSub; 
                        }
                        if (metalGroup.Id > 0) { 
                            deposit.MetalGroup = metalGroup; 
                        }
                        if (metalGroupSub.Id > 0) { 
                            deposit.MetalGroupSub = metalGroupSub; 
                        }
                        //Return
                        return deposit;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<Deposit>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Deposit>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT D.*, 
                                    (SELECT COUNT(MX.Id) FROM Mine MX
                                     WHERE MX.DepositId = D.Id) AS QttMines,
                                    (SELECT COUNT(MA.Id) FROM MineArea MA
                                     INNER JOIN Mine MX ON MA.MineId = MX.Id 
                                     WHERE MX.DepositId = D.Id) AS QttMineAreas, 
                                    (SELECT COUNT(DH.Id) FROM DrillHole DH
                                     WHERE DH.DepositId = D.Id) AS QttDrillHoles,
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
                                     INNER JOIN DrillHole DH ON DB.DrillHoleId = DH.Id
                                     WHERE DH.DepositId = D.Id) AS QttDrillBoxes,
                                'split', R.*, 'split', T.*, 'split', O.*, 'split', 
                                OS.*, 'split', M.*, 'split', MS.*, 'split', U.*
                                FROM Deposit D  
                                INNER JOIN Region            R   ON D.RegionId             = R.Id
                                LEFT  JOIN DepositType       T   ON D.DepositTypeId        = T.Id
                                LEFT  JOIN OreGeneticType    O   ON D.OreGeneticTypeId     = O.Id
                                LEFT  JOIN OreGeneticTypeSub OS  ON D.OreGeneticTypeSubId  = OS.Id
                                LEFT  JOIN MetalGroup        M   ON D.MetalGroupId         = M.Id
                                LEFT  JOIN MetalGroupSub     MS  ON D.MetalGroupSubId      = MS.Id
                                INNER JOIN User              U   ON D.UserId               = U.Id
                                WHERE R.AccountId = @accountId "; 
                if (term != ""){
                    query = query + "AND (D.Name   LIKE '%" + term + "%' " +
                                    "OR   R.Name   LIKE '%" + term + "%' " +
                                    "OR   T.Name   LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Deposit>(
                    query, 
                    new[] {
                        typeof(Deposit),
                        typeof(Region),
                        typeof(DepositType),
                        typeof(OreGeneticType),
                        typeof(OreGeneticTypeSub),
                        typeof(MetalGroup),
                        typeof(MetalGroupSub),
                        typeof(User),
                    },
                    objects => {
                        Deposit           deposit           = objects[0] as Deposit;
                        Region            region            = objects[1] as Region;
                        DepositType       depositType       = objects[2] as DepositType;
                        OreGeneticType    oreGeneticType    = objects[3] as OreGeneticType;
                        OreGeneticTypeSub oreGeneticTypeSub = objects[4] as OreGeneticTypeSub;
                        MetalGroup        metalGroup        = objects[5] as MetalGroup;
                        MetalGroupSub     metalGroupSub     = objects[6] as MetalGroupSub;
                        User              user              = objects[7] as User;
                        //Dependency required
                        deposit.Region = region;
                        deposit.User   = user;
                        //Dependency not required
                        if (depositType.Id > 0) { 
                            deposit.DepositType = depositType; 
                        }
                        if (oreGeneticType.Id > 0) { 
                            deposit.OreGeneticType = oreGeneticType; 
                        }
                        if (oreGeneticTypeSub.Id > 0) { 
                            deposit.OreGeneticTypeSub = oreGeneticTypeSub; 
                        }
                        if (metalGroup.Id > 0) { 
                            deposit.MetalGroup = metalGroup; 
                        }
                        if (metalGroupSub.Id > 0) { 
                            deposit.MetalGroupSub = metalGroupSub; 
                        }
                        //Return
                        return deposit;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<Deposit>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Deposit>> GetByRegion(int regionId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT D.*, 
                                    (SELECT COUNT(MX.Id) FROM Mine MX
                                     WHERE MX.DepositId = D.Id) AS QttMines,
                                    (SELECT COUNT(MA.Id) FROM MineArea MA
                                     INNER JOIN Mine MX ON MA.MineId = MX.Id 
                                     WHERE MX.DepositId = D.Id) AS QttMineAreas, 
                                    (SELECT COUNT(DH.Id) FROM DrillHole DH
                                     WHERE DH.DepositId = D.Id) AS QttDrillHoles,
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
                                     INNER JOIN DrillHole DH ON DB.DrillHoleId = DH.Id
                                     WHERE DH.DepositId = D.Id) AS QttDrillBoxes,
                                'split', R.*, 'split', T.*, 'split', O.*, 'split', 
                                OS.*, 'split', M.*, 'split', MS.*, 'split', U.*
                                FROM Deposit D  
                                INNER JOIN Region            R   ON D.RegionId             = R.Id
                                LEFT  JOIN DepositType       T   ON D.DepositTypeId        = T.Id
                                LEFT  JOIN OreGeneticType    O   ON D.OreGeneticTypeId     = O.Id
                                LEFT  JOIN OreGeneticTypeSub OS  ON D.OreGeneticTypeSubId  = OS.Id
                                LEFT  JOIN MetalGroup        M   ON D.MetalGroupId         = M.Id
                                LEFT  JOIN MetalGroupSub     MS  ON D.MetalGroupSubId      = MS.Id
                                INNER JOIN User              U   ON D.UserId               = U.Id
                                WHERE R.Id = @regionId "; 
                if (term != ""){
                    query = query + "AND (D.Name   LIKE '%" + term + "%' " +
                                    "OR   R.Name   LIKE '%" + term + "%' " +
                                    "OR   T.Name   LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Deposit>(
                    query, 
                    new[] {
                        typeof(Deposit),
                        typeof(Region),
                        typeof(DepositType),
                        typeof(OreGeneticType),
                        typeof(OreGeneticTypeSub),
                        typeof(MetalGroup),
                        typeof(MetalGroupSub),
                        typeof(User),
                    },
                    objects => {
                        Deposit           deposit           = objects[0] as Deposit;
                        Region            region            = objects[1] as Region;
                        DepositType       depositType       = objects[2] as DepositType;
                        OreGeneticType    oreGeneticType    = objects[3] as OreGeneticType;
                        OreGeneticTypeSub oreGeneticTypeSub = objects[4] as OreGeneticTypeSub;
                        MetalGroup        metalGroup        = objects[5] as MetalGroup;
                        MetalGroupSub     metalGroupSub     = objects[6] as MetalGroupSub;
                        User              user              = objects[7] as User;
                        //Dependency required
                        deposit.Region = region;
                        deposit.User   = user;
                        //Dependency not required
                        if (depositType.Id > 0) { 
                            deposit.DepositType = depositType; 
                        }
                        if (oreGeneticType.Id > 0) { 
                            deposit.OreGeneticType = oreGeneticType; 
                        }
                        if (oreGeneticTypeSub.Id > 0) { 
                            deposit.OreGeneticTypeSub = oreGeneticTypeSub; 
                        }
                        if (metalGroup.Id > 0) { 
                            deposit.MetalGroup = metalGroup; 
                        }
                        if (metalGroupSub.Id > 0) { 
                            deposit.MetalGroupSub = metalGroupSub; 
                        }
                        //Return
                        return deposit;
                    },
                    splitOn: "split",
                    param: new { regionId });
                return await PageList<Deposit>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Deposit> GetById(int id)
        {
            try
            {
                var conn = _db.Connection; 
                string query = @"SELECT D.*, 
                                    (SELECT COUNT(MX.Id) FROM Mine MX
                                     WHERE MX.DepositId = D.Id) AS QttMines,
                                    (SELECT COUNT(MA.Id) FROM MineArea MA
                                     INNER JOIN Mine MX ON MA.MineId = MX.Id 
                                     WHERE MX.DepositId = D.Id) AS QttMineAreas, 
                                    (SELECT COUNT(DH.Id) FROM DrillHole DH
                                     WHERE DH.DepositId = D.Id) AS QttDrillHoles,
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
                                     INNER JOIN DrillHole DH ON DB.DrillHoleId = DH.Id
                                     WHERE DH.DepositId = D.Id) AS QttDrillBoxes,
                                'split', R.*, 'split', T.*, 'split', O.*, 'split', 
                                OS.*, 'split', M.*, 'split', MS.*, 'split', U.*
                                FROM Deposit D  
                                INNER JOIN Region            R   ON D.RegionId             = R.Id
                                LEFT  JOIN DepositType       T   ON D.DepositTypeId        = T.Id
                                LEFT  JOIN OreGeneticType    O   ON D.OreGeneticTypeId     = O.Id
                                LEFT  JOIN OreGeneticTypeSub OS  ON D.OreGeneticTypeSubId  = OS.Id
                                LEFT  JOIN MetalGroup        M   ON D.MetalGroupId         = M.Id
                                LEFT  JOIN MetalGroupSub     MS  ON D.MetalGroupSubId      = MS.Id
                                INNER JOIN User              U   ON D.UserId               = U.Id
                                WHERE D.Id = @id";
                var res = await conn.QueryAsync<Deposit>(
                    query, 
                    new[] {
                        typeof(Deposit),
                        typeof(Region),
                        typeof(DepositType),
                        typeof(OreGeneticType),
                        typeof(OreGeneticTypeSub),
                        typeof(MetalGroup),
                        typeof(MetalGroupSub),
                        typeof(User),
                    },
                    objects => {
                        Deposit           deposit           = objects[0] as Deposit;
                        Region            region            = objects[1] as Region;
                        DepositType       depositType       = objects[2] as DepositType;
                        OreGeneticType    oreGeneticType    = objects[3] as OreGeneticType;
                        OreGeneticTypeSub oreGeneticTypeSub = objects[4] as OreGeneticTypeSub;
                        MetalGroup        metalGroup        = objects[5] as MetalGroup;
                        MetalGroupSub     metalGroupSub     = objects[6] as MetalGroupSub;
                        User              user              = objects[7] as User;
                        //Dependency required
                        deposit.Region = region;
                        deposit.User   = user;
                        //Dependency not required
                        if (depositType.Id > 0) { 
                            deposit.DepositType = depositType; 
                        }
                        if (oreGeneticType.Id > 0) { 
                            deposit.OreGeneticType = oreGeneticType; 
                        }
                        if (oreGeneticTypeSub.Id > 0) { 
                            deposit.OreGeneticTypeSub = oreGeneticTypeSub; 
                        }
                        if (metalGroup.Id > 0) { 
                            deposit.MetalGroup = metalGroup; 
                        }
                        if (metalGroupSub.Id > 0) { 
                            deposit.MetalGroupSub = metalGroupSub; 
                        }
                        //Return
                        return deposit;
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