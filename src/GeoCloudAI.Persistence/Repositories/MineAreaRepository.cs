using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Repositories
{
    public class MineAreaRepository: IMineAreaRepository
    {
        private DbSession _db;
        
        public MineAreaRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(MineArea mineArea)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    //Required
                    if (mineArea.MineId == 0) { return 0; }
                    if (mineArea.UserId == 0) { return 0; }
                    //Not Required
                    var typeId = "null";
                    if (mineArea.TypeId > 0){ 
                        typeId = mineArea.TypeId.ToString(); }
                    var statusId = "null";
                    if (mineArea.StatusId > 0) { 
                        statusId = mineArea.StatusId.ToString(); }
                    var shapeId = "null";
                    if (mineArea.ShapeId > 0) { 
                        shapeId = mineArea.ShapeId.ToString(); }
                    string command = @"INSERT INTO MINEAREA(   
                                            mineId, name, latitude, longitude, startYear, endYear, 
                                            resource, reserve, oreMined, comments, typeId, statusId, shapeId, 
                                            imgTypeProfile, imgTypeCover, userId, register) 
                                        VALUES(@mineId, @name, @latitude, @longitude, @startYear, @endYear, @resource, 
                                            @reserve, @oreMined, @comments, " + typeId + ", " + statusId + ", " +
                                            shapeId + ", " + "@imgTypeProfile, @imgTypeCover, @userId, @register ); " +
                                        "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: mineArea);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(MineArea mineArea)
        {
            try
            {
                var conn = _db.Connection;
                //Required
                if (mineArea.MineId == 0) { return 0; }
                if (mineArea.UserId == 0) { return 0; }
                //Not Required
                var typeId = "null";
                if (mineArea.TypeId > 0){ 
                    typeId = mineArea.TypeId.ToString(); }
                var statusId = "null";
                if (mineArea.StatusId > 0) { 
                    statusId = mineArea.StatusId.ToString(); }
                var shapeId = "null";
                if (mineArea.ShapeId > 0) { 
                    shapeId = mineArea.ShapeId.ToString(); }
                string command = @"UPDATE MINEAREA SET 
                                    mineId              = @mineId, 
                                    name                = @name, 
                                    latitude            = @latitude, 
                                    longitude           = @longitude, 
                                    startYear           = @startYear, 
                                    endYear             = @endYear, 
                                    resource            = @resource, 
                                    reserve             = @reserve,  
                                    oreMined            = @oreMined,
                                    comments            = @comments,
                                    typeId              = " + typeId + @", 
                                    statusId            = " + statusId + @", 
                                    shapeId             = " + shapeId + @",
                                    imgTypeProfile      = @imgTypeProfile,
                                    imgTypeCover        = @imgTypeCover  
                                    WHERE id            = @id";
                var result = await conn.ExecuteAsync(sql: command, param: mineArea);
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
                string command = @"DELETE FROM MINEAREA WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<MineArea>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT MA.*, 
                                    (SELECT COUNT(DH.Id) FROM DrillHole DH
                                     WHERE DH.MineAreaId = MA.Id) AS QttDrillHoles,
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
                                     INNER JOIN DrillHole DH ON DB.DrillHoleId = DH.Id
                                     WHERE DH.MineAreaId = MA.Id) AS QttDrillBoxes,
                                'split', M.*, 'split', D.*, 'split', R.*, 'split',
                                T.*, 'split', S.*, 'split', H.*, 'split', U.*
                                FROM MineArea MA  
                                INNER JOIN Mine             M   ON MA.MineId    = M.Id
                                INNER JOIN Deposit          D   ON M.DepositId  = D.Id
                                INNER JOIN Region           R   ON D.RegionId   = R.Id
                                LEFT  JOIN MineAreaType     T   ON MA.TypeId    = T.Id
                                LEFT  JOIN MineAreaStatus   S   ON MA.StatusId  = S.Id
                                LEFT  JOIN MineAreaShape    H   ON MA.ShapeId   = H.Id
                                INNER JOIN User             U   ON MA.UserId    = U.Id"; 
                if (term != ""){
                    query = query + "WHERE MA.Name LIKE '%" + term + "%' " +
                                    "OR    M.Name  LIKE '%" + term + "%' " +
                                    "OR    D.Name  LIKE '%" + term + "%' " +
                                    "OR    R.Name  LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<MineArea>(
                    query, 
                    new[] {
                        typeof(MineArea),
                        typeof(Mine),
                        typeof(Deposit),
                        typeof(Region),
                        typeof(MineAreaType),
                        typeof(MineAreaStatus),
                        typeof(MineAreaShape),
                        typeof(User),
                    },
                    objects => {
                        MineArea        mineArea    = objects[0] as MineArea;
                        Mine            mine        = objects[1] as Mine;
                        Deposit         deposit     = objects[2] as Deposit;
                        Region          region      = objects[3] as Region;
                        MineAreaType    type        = objects[4] as MineAreaType;
                        MineAreaStatus  status      = objects[5] as MineAreaStatus;
                        MineAreaShape   shape       = objects[6] as MineAreaShape;
                        User            user        = objects[7] as User;
                        //Dependency required
                        deposit.Region   = region;
                        mine.Deposit     = deposit;
                        mineArea.Mine    = mine;
                        mineArea.User    = user;
                        //Dependency not required
                        if (type.Id > 0)    { mineArea.Type   = type; }
                        if (status.Id > 0)  { mineArea.Status = status; }
                        if (shape.Id > 0)   { mineArea.Shape  = shape; }
                        //Return
                        return mineArea;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<MineArea>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<MineArea>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT MA.*, 
                                    (SELECT COUNT(DH.Id) FROM DrillHole DH
                                     WHERE DH.MineAreaId = MA.Id) AS QttDrillHoles,
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
                                     INNER JOIN DrillHole DH ON DB.DrillHoleId = DH.Id
                                     WHERE DH.MineAreaId = MA.Id) AS QttDrillBoxes,
                                'split', M.*, 'split', D.*, 'split', R.*, 'split',
                                T.*, 'split', S.*, 'split', H.*, 'split', U.*
                                FROM MineArea MA  
                                INNER JOIN Mine             M   ON MA.MineId    = M.Id
                                INNER JOIN Deposit          D   ON M.DepositId  = D.Id
                                INNER JOIN Region           R   ON D.RegionId   = R.Id
                                LEFT  JOIN MineAreaType     T   ON MA.TypeId    = T.Id
                                LEFT  JOIN MineAreaStatus   S   ON MA.StatusId  = S.Id
                                LEFT  JOIN MineAreaShape    H   ON MA.ShapeId   = H.Id
                                INNER JOIN User             U   ON MA.UserId    = U.Id
                                WHERE R.AccountId = @accountId "; 
                if (term != ""){
                    query = query + "AND (MA.Name LIKE '%" + term + "%' " +
                                    "OR   M.Name  LIKE '%" + term + "%' " +
                                    "OR   D.Name  LIKE '%" + term + "%' " +
                                    "OR   R.Name  LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<MineArea>(
                    query, 
                    new[] {
                        typeof(MineArea),
                        typeof(Mine),
                        typeof(Deposit),
                        typeof(Region),
                        typeof(MineAreaType),
                        typeof(MineAreaStatus),
                        typeof(MineAreaShape),
                        typeof(User),
                    },
                    objects => {
                        MineArea        mineArea    = objects[0] as MineArea;
                        Mine            mine        = objects[1] as Mine;
                        Deposit         deposit     = objects[2] as Deposit;
                        Region          region      = objects[3] as Region;
                        MineAreaType    type        = objects[4] as MineAreaType;
                        MineAreaStatus  status      = objects[5] as MineAreaStatus;
                        MineAreaShape   shape       = objects[6] as MineAreaShape;
                        User            user        = objects[7] as User;
                        //Dependency required
                        deposit.Region   = region;
                        mine.Deposit     = deposit;
                        mineArea.Mine    = mine;
                        mineArea.User    = user;
                        //Dependency not required
                        if (type.Id > 0)    { mineArea.Type   = type; }
                        if (status.Id > 0)  { mineArea.Status = status; }
                        if (shape.Id > 0)   { mineArea.Shape  = shape; }
                        //Return
                        return mineArea;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<MineArea>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<MineArea>> GetByRegion(int regionId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT MA.*, 
                                    (SELECT COUNT(DH.Id) FROM DrillHole DH
                                     WHERE DH.MineAreaId = MA.Id) AS QttDrillHoles,
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
                                     INNER JOIN DrillHole DH ON DB.DrillHoleId = DH.Id
                                     WHERE DH.MineAreaId = MA.Id) AS QttDrillBoxes,
                                'split', M.*, 'split', D.*, 'split', R.*, 'split',
                                T.*, 'split', S.*, 'split', H.*, 'split', U.*
                                FROM MineArea MA  
                                INNER JOIN Mine             M   ON MA.MineId    = M.Id
                                INNER JOIN Deposit          D   ON M.DepositId  = D.Id
                                INNER JOIN Region           R   ON D.RegionId   = R.Id
                                LEFT  JOIN MineAreaType     T   ON MA.TypeId    = T.Id
                                LEFT  JOIN MineAreaStatus   S   ON MA.StatusId  = S.Id
                                LEFT  JOIN MineAreaShape    H   ON MA.ShapeId   = H.Id
                                INNER JOIN User             U   ON MA.UserId    = U.Id
                                WHERE R.Id = @regionId "; 
                if (term != ""){
                    query = query + "AND (MA.Name LIKE '%" + term + "%' " +
                                    "OR   M.Name  LIKE '%" + term + "%' " +
                                    "OR   D.Name  LIKE '%" + term + "%' " +
                                    "OR   R.Name  LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<MineArea>(
                    query, 
                    new[] {
                        typeof(MineArea),
                        typeof(Mine),
                        typeof(Deposit),
                        typeof(Region),
                        typeof(MineAreaType),
                        typeof(MineAreaStatus),
                        typeof(MineAreaShape),
                        typeof(User),
                    },
                    objects => {
                        MineArea        mineArea    = objects[0] as MineArea;
                        Mine            mine        = objects[1] as Mine;
                        Deposit         deposit     = objects[2] as Deposit;
                        Region          region      = objects[3] as Region;
                        MineAreaType    type        = objects[4] as MineAreaType;
                        MineAreaStatus  status      = objects[5] as MineAreaStatus;
                        MineAreaShape   shape       = objects[6] as MineAreaShape;
                        User            user        = objects[7] as User;
                        //Dependency required
                        deposit.Region   = region;
                        mine.Deposit     = deposit;
                        mineArea.Mine    = mine;
                        mineArea.User    = user;
                        //Dependency not required
                        if (type.Id > 0)    { mineArea.Type   = type; }
                        if (status.Id > 0)  { mineArea.Status = status; }
                        if (shape.Id > 0)   { mineArea.Shape  = shape; }
                        //Return
                        return mineArea;
                    },
                    splitOn: "split",
                    param: new { regionId });
                return await PageList<MineArea>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<MineArea>> GetByDeposit(int depositId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT MA.*, 
                                    (SELECT COUNT(DH.Id) FROM DrillHole DH
                                     WHERE DH.MineAreaId = MA.Id) AS QttDrillHoles,
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
                                     INNER JOIN DrillHole DH ON DB.DrillHoleId = DH.Id
                                     WHERE DH.MineAreaId = MA.Id) AS QttDrillBoxes,
                                'split', M.*, 'split', D.*, 'split', R.*, 'split',
                                T.*, 'split', S.*, 'split', H.*, 'split', U.*
                                FROM MineArea MA  
                                INNER JOIN Mine             M   ON MA.MineId    = M.Id
                                INNER JOIN Deposit          D   ON M.DepositId  = D.Id
                                INNER JOIN Region           R   ON D.RegionId   = R.Id
                                LEFT  JOIN MineAreaType     T   ON MA.TypeId    = T.Id
                                LEFT  JOIN MineAreaStatus   S   ON MA.StatusId  = S.Id
                                LEFT  JOIN MineAreaShape    H   ON MA.ShapeId   = H.Id
                                INNER JOIN User             U   ON MA.UserId    = U.Id
                                WHERE D.Id = @depositId "; 
                if (term != ""){
                    query = query + "AND (MA.Name LIKE '%" + term + "%' " +
                                    "OR   M.Name  LIKE '%" + term + "%' " +
                                    "OR   D.Name  LIKE '%" + term + "%' " +
                                    "OR   R.Name  LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<MineArea>(
                    query, 
                    new[] {
                        typeof(MineArea),
                        typeof(Mine),
                        typeof(Deposit),
                        typeof(Region),
                        typeof(MineAreaType),
                        typeof(MineAreaStatus),
                        typeof(MineAreaShape),
                        typeof(User),
                    },
                    objects => {
                        MineArea        mineArea    = objects[0] as MineArea;
                        Mine            mine        = objects[1] as Mine;
                        Deposit         deposit     = objects[2] as Deposit;
                        Region          region      = objects[3] as Region;
                        MineAreaType    type        = objects[4] as MineAreaType;
                        MineAreaStatus  status      = objects[5] as MineAreaStatus;
                        MineAreaShape   shape       = objects[6] as MineAreaShape;
                        User            user        = objects[7] as User;
                        //Dependency required
                        deposit.Region   = region;
                        mine.Deposit     = deposit;
                        mineArea.Mine    = mine;
                        mineArea.User    = user;
                        //Dependency not required
                        if (type.Id > 0)    { mineArea.Type   = type; }
                        if (status.Id > 0)  { mineArea.Status = status; }
                        if (shape.Id > 0)   { mineArea.Shape  = shape; }
                        //Return
                        return mineArea;
                    },
                    splitOn: "split",
                    param: new { depositId });
                return await PageList<MineArea>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<MineArea>> GetByMine(int mineId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT MA.*, 
                                    (SELECT COUNT(DH.Id) FROM DrillHole DH
                                     WHERE DH.MineAreaId = MA.Id) AS QttDrillHoles,
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
                                     INNER JOIN DrillHole DH ON DB.DrillHoleId = DH.Id
                                     WHERE DH.MineAreaId = MA.Id) AS QttDrillBoxes,
                                'split', M.*, 'split', D.*, 'split', R.*, 'split',
                                T.*, 'split', S.*, 'split', H.*, 'split', U.*
                                FROM MineArea MA  
                                INNER JOIN Mine             M   ON MA.MineId    = M.Id
                                INNER JOIN Deposit          D   ON M.DepositId  = D.Id
                                INNER JOIN Region           R   ON D.RegionId   = R.Id
                                LEFT  JOIN MineAreaType     T   ON MA.TypeId    = T.Id
                                LEFT  JOIN MineAreaStatus   S   ON MA.StatusId  = S.Id
                                LEFT  JOIN MineAreaShape    H   ON MA.ShapeId   = H.Id
                                INNER JOIN User             U   ON MA.UserId    = U.Id
                                WHERE M.Id = @mineId "; 
                if (term != ""){
                    query = query + "AND (MA.Name LIKE '%" + term + "%' " +
                                    "OR   M.Name  LIKE '%" + term + "%' " +
                                    "OR   D.Name  LIKE '%" + term + "%' " +
                                    "OR   R.Name  LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<MineArea>(
                    query, 
                    new[] {
                        typeof(MineArea),
                        typeof(Mine),
                        typeof(Deposit),
                        typeof(Region),
                        typeof(MineAreaType),
                        typeof(MineAreaStatus),
                        typeof(MineAreaShape),
                        typeof(User),
                    },
                    objects => {
                        MineArea        mineArea    = objects[0] as MineArea;
                        Mine            mine        = objects[1] as Mine;
                        Deposit         deposit     = objects[2] as Deposit;
                        Region          region      = objects[3] as Region;
                        MineAreaType    type        = objects[4] as MineAreaType;
                        MineAreaStatus  status      = objects[5] as MineAreaStatus;
                        MineAreaShape   shape       = objects[6] as MineAreaShape;
                        User            user        = objects[7] as User;
                        //Dependency required
                        deposit.Region   = region;
                        mine.Deposit     = deposit;
                        mineArea.Mine    = mine;
                        mineArea.User    = user;
                        //Dependency not required
                        if (type.Id > 0)    { mineArea.Type   = type; }
                        if (status.Id > 0)  { mineArea.Status = status; }
                        if (shape.Id > 0)   { mineArea.Shape  = shape; }
                        //Return
                        return mineArea;
                    },
                    splitOn: "split",
                    param: new { mineId });
                return await PageList<MineArea>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MineArea> GetById(int id)
        {
            try
            {
                var conn = _db.Connection; 
                string query = @"SELECT MA.*, 
                                    (SELECT COUNT(DH.Id) FROM DrillHole DH
                                     WHERE DH.MineAreaId = MA.Id) AS QttDrillHoles,
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
                                     INNER JOIN DrillHole DH ON DB.DrillHoleId = DH.Id
                                     WHERE DH.MineAreaId = MA.Id) AS QttDrillBoxes,
                                'split', M.*, 'split', D.*, 'split', R.*, 'split',
                                T.*, 'split', S.*, 'split', H.*, 'split', U.*
                                FROM MineArea MA  
                                INNER JOIN Mine             M   ON MA.MineId    = M.Id
                                INNER JOIN Deposit          D   ON M.DepositId  = D.Id
                                INNER JOIN Region           R   ON D.RegionId   = R.Id
                                LEFT  JOIN MineAreaType     T   ON MA.TypeId    = T.Id
                                LEFT  JOIN MineAreaStatus   S   ON MA.StatusId  = S.Id
                                LEFT  JOIN MineAreaShape    H   ON MA.ShapeId   = H.Id
                                INNER JOIN User             U   ON MA.UserId    = U.Id
                                WHERE MA.Id = @id";
                var res = await conn.QueryAsync<MineArea>(
                    query, 
                    new[] {
                        typeof(MineArea),
                        typeof(Mine),
                        typeof(Deposit),
                        typeof(Region),
                        typeof(MineAreaType),
                        typeof(MineAreaStatus),
                        typeof(MineAreaShape),
                        typeof(User),
                    },
                    objects => {
                        MineArea        mineArea    = objects[0] as MineArea;
                        Mine            mine        = objects[1] as Mine;
                        Deposit         deposit     = objects[2] as Deposit;
                        Region          region      = objects[3] as Region;
                        MineAreaType    type        = objects[4] as MineAreaType;
                        MineAreaStatus  status      = objects[5] as MineAreaStatus;
                        MineAreaShape   shape       = objects[6] as MineAreaShape;
                        User            user        = objects[7] as User;
                        //Dependency required
                        deposit.Region   = region;
                        mine.Deposit     = deposit;
                        mineArea.Mine    = mine;
                        mineArea.User    = user;
                        //Dependency not required
                        if (type.Id > 0)    { mineArea.Type   = type; }
                        if (status.Id > 0)  { mineArea.Status = status; }
                        if (shape.Id > 0)   { mineArea.Shape  = shape; }
                        //Return
                        return mineArea;
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