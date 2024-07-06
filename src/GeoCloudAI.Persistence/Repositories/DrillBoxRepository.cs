using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Repositories
{
    public class DrillBoxRepository: IDrillBoxRepository
    {
        private DbSession _db;
        
        public DrillBoxRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(DrillBox drillBox)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    //Required
                    if (drillBox.DrillHoleId == 0) { return 0; }
                    if (drillBox.UserId      == 0) { return 0; }
                    //Not Required
                    var typeId = "null";
                    if (drillBox.TypeId > 0) { 
                        typeId = drillBox.TypeId.ToString(); }
                    var statusId = "null";
                    if (drillBox.StatusId > 0) { 
                        statusId = drillBox.StatusId.ToString(); }
                    var materialId = "null";
                    if (drillBox.MaterialId > 0) { 
                        materialId = drillBox.MaterialId.ToString(); }
                    var coreShedId = "null";
                    if (drillBox.CoreShedId > 0) { 
                        coreShedId = drillBox.CoreShedId.ToString(); }
                    string command = @"INSERT INTO DRILLBOX( 
                                            drillHoleId, number, amountCores, code, uuid, startDepth, endDepth, description,
                                            comments, typeId, statusId, materialId, coreShedId, shelves, imgType, userId, register) 
                                        VALUES(@drillHoleId, @number, @amountCores, @code, @uuid, @startDepth, @endDepth, 
                                               @description, @comments, " + typeId + ", " + statusId + ", " + materialId + ", " + 
                                               coreShedId + ", " + "@shelves, @imgType, @userId, @register); " +
                                       "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: drillBox);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(DrillBox drillBox)
        {
            try
            {
                var conn = _db.Connection;
                //Required
                if (drillBox.DrillHoleId == 0) { return 0; }
                if (drillBox.UserId      == 0) { return 0; }
                //Not Required
                var typeId = "null";
                if (drillBox.TypeId > 0) { 
                    typeId = drillBox.TypeId.ToString(); }
                var statusId = "null";
                if (drillBox.StatusId > 0) { 
                    statusId = drillBox.StatusId.ToString(); }
                var materialId = "null";
                if (drillBox.MaterialId > 0) { 
                    materialId = drillBox.MaterialId.ToString(); }
                var coreShedId = "null";
                if (drillBox.CoreShedId > 0) { 
                    coreShedId = drillBox.CoreShedId.ToString(); }
                string command = @"UPDATE DRILLBOX SET 
                                    drillHoleId  = @drillHoleId,
                                    number       = @number, 
                                    amountCores  = @amountCores, 
                                    code         = @code,
                                    uuid         = @uuid,
                                    startDepth   = @startDepth,
                                    endDepth     = @endDepth, 
                                    description  = @description,
                                    comments     = @comments,
                                    typeId       = " + typeId + @", 
                                    statusId     = " + statusId + @",
                                    materialId   = " + materialId + @", 
                                    coreShedId   = " + coreShedId + @",
                                    shelves      = @shelves,
                                    imgType      = @imgType
                                    WHERE id     = @id";
              var result = await conn.ExecuteAsync(sql: command, param: drillBox);
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
                string command = @"DELETE FROM DRILLBOX WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillBox>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT B.*, 'split',  H.*,  'split',  R.*,  'split', D.*,  'split', M.*,  'split', 
                                        MA.*, 'split', BT.*, 'split', BS.*, 'split', BM.*, 'split', C.*,  'split', U.*
                                FROM DrillBox B
                                INNER JOIN DrillHole         H  ON B.drillHoleId = H.Id
                                INNER JOIN Region            R  ON H.RegionId    = R.Id
                                LEFT  JOIN Deposit           D  ON H.depositId   = D.Id
                                LEFT  JOIN Mine              M  ON H.mineId      = M.Id
                                LEFT  JOIN MineArea          MA ON H.mineAreaId  = MA.Id
                                LEFT  JOIN DrillBoxType      BT ON B.typeId      = BT.Id
                                LEFT  JOIN DrillBoxStatus    BS ON B.statusId    = BS.Id
                                LEFT  JOIN DrillBoxMaterial  BM ON B.materialId  = BM.Id
                                LEFT  JOIN CoreShed          C  ON B.coreShedId  = C.Id
                                INNER JOIN User              U  ON B.UserId      = U.Id "; 
                if (term != ""){
                    query = query + "WHERE B.number     LIKE '%" + term + "%' " +
                                    "OR    B.code       LIKE '%" + term + "%' " +
                                    "OR    B.startDepth LIKE '%" + term + "%' " +
                                    "OR    B.endDepth   LIKE '%" + term + "%' " +
                                    "OR    H.Name       LIKE '%" + term + "%' " +
                                    "OR    BT.Name      LIKE '%" + term + "%' " +
                                    "OR    BS.Name      LIKE '%" + term + "%' " +
                                    "OR    C.Name       LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillBox>(
                    query, 
                    new[] {
                        typeof(DrillBox),
                        typeof(DrillHole),
                        typeof(Region),
                        typeof(Deposit),
                        typeof(Mine),
                        typeof(MineArea),
                        typeof(DrillBoxType),
                        typeof(DrillBoxStatus),
                        typeof(DrillBoxMaterial),
                        typeof(CoreShed),
                        typeof(User),
                    },
                    objects => {
                        DrillBox         drillBox   = objects[0]  as DrillBox;
                        DrillHole        drillHole  = objects[1]  as DrillHole;
                        Region           region     = objects[2]  as Region;
                        Deposit          deposit    = objects[3]  as Deposit;
                        Mine             mine       = objects[4]  as Mine;
                        MineArea         mineArea   = objects[5]  as MineArea;
                        DrillBoxType     type       = objects[6]  as DrillBoxType;
                        DrillBoxStatus   status     = objects[7]  as DrillBoxStatus;
                        DrillBoxMaterial material   = objects[8]  as DrillBoxMaterial;
                        CoreShed         coreShed   = objects[9]  as CoreShed;
                        User             user       = objects[10] as User;
                        //Dependency required
                        drillHole.Region   = region;
                        drillBox.DrillHole = drillHole;
                        drillBox.User      = user;
                        //Dependency not required
                        if (deposit.Id > 0)   { drillHole.Deposit  = deposit; }
                        if (mine.Id > 0)      { drillHole.Mine     = mine; }
                        if (mineArea.Id > 0)  { drillHole.MineArea = mineArea; }
                        if (type.Id > 0)      { drillBox.Type      = type; }
                        if (status.Id > 0)    { drillBox.Status    = status; }
                        if (material.Id > 0)  { drillBox.Material  = material; }
                        if (coreShed.Id > 0)  { drillBox.CoreShed  = coreShed; }
                        //Return
                        return drillBox;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<DrillBox>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillBox>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT B.*, 'split',  H.*,  'split',  R.*,  'split', D.*,  'split', M.*,  'split', 
                                        MA.*, 'split', BT.*, 'split', BS.*, 'split', BM.*, 'split', C.*,  'split', U.*
                                FROM DrillBox B
                                INNER JOIN DrillHole         H  ON B.drillHoleId = H.Id
                                INNER JOIN Region            R  ON H.RegionId    = R.Id
                                LEFT  JOIN Deposit           D  ON H.depositId   = D.Id
                                LEFT  JOIN Mine              M  ON H.mineId      = M.Id
                                LEFT  JOIN MineArea          MA ON H.mineAreaId  = MA.Id
                                LEFT  JOIN DrillBoxType      BT ON B.typeId      = BT.Id
                                LEFT  JOIN DrillBoxStatus    BS ON B.statusId    = BS.Id
                                LEFT  JOIN DrillBoxMaterial  BM ON B.materialId  = BM.Id
                                LEFT  JOIN CoreShed          C  ON B.coreShedId  = C.Id
                                INNER JOIN User              U  ON B.UserId      = U.Id
                                WHERE R.AccountId = @accountId "; 
                if (term != ""){
                    query = query + "AND  (B.number     LIKE '%" + term + "%' " +
                                    "OR    B.code       LIKE '%" + term + "%' " +
                                    "OR    B.startDepth LIKE '%" + term + "%' " +
                                    "OR    B.endDepth   LIKE '%" + term + "%' " +
                                    "OR    H.Name       LIKE '%" + term + "%' " +
                                    "OR    BT.Name      LIKE '%" + term + "%' " +
                                    "OR    BS.Name      LIKE '%" + term + "%' " +
                                    "OR    C.Name       LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillBox>(
                    query, 
                    new[] {
                        typeof(DrillBox),
                        typeof(DrillHole),
                        typeof(Region),
                        typeof(Deposit),
                        typeof(Mine),
                        typeof(MineArea),
                        typeof(DrillBoxType),
                        typeof(DrillBoxStatus),
                        typeof(DrillBoxMaterial),
                        typeof(CoreShed),
                        typeof(User),
                    },
                    objects => {
                        DrillBox         drillBox   = objects[0]  as DrillBox;
                        DrillHole        drillHole  = objects[1]  as DrillHole;
                        Region           region     = objects[2]  as Region;
                        Deposit          deposit    = objects[3]  as Deposit;
                        Mine             mine       = objects[4]  as Mine;
                        MineArea         mineArea   = objects[5]  as MineArea;
                        DrillBoxType     type       = objects[6]  as DrillBoxType;
                        DrillBoxStatus   status     = objects[7]  as DrillBoxStatus;
                        DrillBoxMaterial material   = objects[8]  as DrillBoxMaterial;
                        CoreShed         coreShed   = objects[9]  as CoreShed;
                        User             user       = objects[10] as User;
                        //Dependency required
                        drillHole.Region   = region;
                        drillBox.DrillHole = drillHole;
                        drillBox.User      = user;
                        //Dependency not required
                        if (deposit.Id > 0)   { drillHole.Deposit  = deposit; }
                        if (mine.Id > 0)      { drillHole.Mine     = mine; }
                        if (mineArea.Id > 0)  { drillHole.MineArea = mineArea; }
                        if (type.Id > 0)      { drillBox.Type      = type; }
                        if (status.Id > 0)    { drillBox.Status    = status; }
                        if (material.Id > 0)  { drillBox.Material  = material; }
                        if (coreShed.Id > 0)  { drillBox.CoreShed  = coreShed; }
                        //Return
                        return drillBox;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<DrillBox>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillBox>> GetByRegion(int regionId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT B.*, 'split',  H.*,  'split',  R.*,  'split', D.*,  'split', M.*,  'split', 
                                        MA.*, 'split', BT.*, 'split', BS.*, 'split', BM.*, 'split', C.*,  'split', U.*
                                FROM DrillBox B
                                INNER JOIN DrillHole         H  ON B.drillHoleId = H.Id
                                INNER JOIN Region            R  ON H.RegionId    = R.Id
                                LEFT  JOIN Deposit           D  ON H.depositId   = D.Id
                                LEFT  JOIN Mine              M  ON H.mineId      = M.Id
                                LEFT  JOIN MineArea          MA ON H.mineAreaId  = MA.Id
                                LEFT  JOIN DrillBoxType      BT ON B.typeId      = BT.Id
                                LEFT  JOIN DrillBoxStatus    BS ON B.statusId    = BS.Id
                                LEFT  JOIN DrillBoxMaterial  BM ON B.materialId  = BM.Id
                                LEFT  JOIN CoreShed          C  ON B.coreShedId  = C.Id
                                INNER JOIN User              U  ON B.UserId      = U.Id
                                WHERE R.Id = @regionId "; 
                if (term != ""){
                    query = query + "AND  (B.number     LIKE '%" + term + "%' " +
                                    "OR    B.code       LIKE '%" + term + "%' " +
                                    "OR    B.startDepth LIKE '%" + term + "%' " +
                                    "OR    B.endDepth   LIKE '%" + term + "%' " +
                                    "OR    H.Name       LIKE '%" + term + "%' " +
                                    "OR    BT.Name      LIKE '%" + term + "%' " +
                                    "OR    BS.Name      LIKE '%" + term + "%' " +
                                    "OR    C.Name       LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillBox>(
                    query, 
                    new[] {
                        typeof(DrillBox),
                        typeof(DrillHole),
                        typeof(Region),
                        typeof(Deposit),
                        typeof(Mine),
                        typeof(MineArea),
                        typeof(DrillBoxType),
                        typeof(DrillBoxStatus),
                        typeof(DrillBoxMaterial),
                        typeof(CoreShed),
                        typeof(User),
                    },
                    objects => {
                        DrillBox         drillBox   = objects[0]  as DrillBox;
                        DrillHole        drillHole  = objects[1]  as DrillHole;
                        Region           region     = objects[2]  as Region;
                        Deposit          deposit    = objects[3]  as Deposit;
                        Mine             mine       = objects[4]  as Mine;
                        MineArea         mineArea   = objects[5]  as MineArea;
                        DrillBoxType     type       = objects[6]  as DrillBoxType;
                        DrillBoxStatus   status     = objects[7]  as DrillBoxStatus;
                        DrillBoxMaterial material   = objects[8]  as DrillBoxMaterial;
                        CoreShed         coreShed   = objects[9]  as CoreShed;
                        User             user       = objects[10] as User;
                        //Dependency required
                        drillHole.Region   = region;
                        drillBox.DrillHole = drillHole;
                        drillBox.User      = user;
                        //Dependency not required
                        if (deposit.Id > 0)   { drillHole.Deposit  = deposit; }
                        if (mine.Id > 0)      { drillHole.Mine     = mine; }
                        if (mineArea.Id > 0)  { drillHole.MineArea = mineArea; }
                        if (type.Id > 0)      { drillBox.Type      = type; }
                        if (status.Id > 0)    { drillBox.Status    = status; }
                        if (material.Id > 0)  { drillBox.Material  = material; }
                        if (coreShed.Id > 0)  { drillBox.CoreShed  = coreShed; }
                        //Return
                        return drillBox;
                    },
                    splitOn: "split",
                    param: new { regionId });
                return await PageList<DrillBox>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillBox>> GetByDeposit(int depositId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT B.*, 'split',  H.*,  'split',  R.*,  'split', D.*,  'split', M.*,  'split', 
                                        MA.*, 'split', BT.*, 'split', BS.*, 'split', BM.*, 'split', C.*,  'split', U.*
                                FROM DrillBox B
                                INNER JOIN DrillHole         H  ON B.drillHoleId = H.Id
                                INNER JOIN Region            R  ON H.RegionId    = R.Id
                                LEFT  JOIN Deposit           D  ON H.depositId   = D.Id
                                LEFT  JOIN Mine              M  ON H.mineId      = M.Id
                                LEFT  JOIN MineArea          MA ON H.mineAreaId  = MA.Id
                                LEFT  JOIN DrillBoxType      BT ON B.typeId      = BT.Id
                                LEFT  JOIN DrillBoxStatus    BS ON B.statusId    = BS.Id
                                LEFT  JOIN DrillBoxMaterial  BM ON B.materialId  = BM.Id
                                LEFT  JOIN CoreShed          C  ON B.coreShedId  = C.Id
                                INNER JOIN User              U  ON B.UserId      = U.Id
                                WHERE D.Id = @depositId "; 
                if (term != ""){
                    query = query + "AND  (B.number     LIKE '%" + term + "%' " +
                                    "OR    B.code       LIKE '%" + term + "%' " +
                                    "OR    B.startDepth LIKE '%" + term + "%' " +
                                    "OR    B.endDepth   LIKE '%" + term + "%' " +
                                    "OR    H.Name       LIKE '%" + term + "%' " +
                                    "OR    BT.Name      LIKE '%" + term + "%' " +
                                    "OR    BS.Name      LIKE '%" + term + "%' " +
                                    "OR    C.Name       LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillBox>(
                    query, 
                    new[] {
                        typeof(DrillBox),
                        typeof(DrillHole),
                        typeof(Region),
                        typeof(Deposit),
                        typeof(Mine),
                        typeof(MineArea),
                        typeof(DrillBoxType),
                        typeof(DrillBoxStatus),
                        typeof(DrillBoxMaterial),
                        typeof(CoreShed),
                        typeof(User),
                    },
                    objects => {
                        DrillBox         drillBox   = objects[0]  as DrillBox;
                        DrillHole        drillHole  = objects[1]  as DrillHole;
                        Region           region     = objects[2]  as Region;
                        Deposit          deposit    = objects[3]  as Deposit;
                        Mine             mine       = objects[4]  as Mine;
                        MineArea         mineArea   = objects[5]  as MineArea;
                        DrillBoxType     type       = objects[6]  as DrillBoxType;
                        DrillBoxStatus   status     = objects[7]  as DrillBoxStatus;
                        DrillBoxMaterial material   = objects[8]  as DrillBoxMaterial;
                        CoreShed         coreShed   = objects[9]  as CoreShed;
                        User             user       = objects[10] as User;
                        //Dependency required
                        drillHole.Region   = region;
                        drillBox.DrillHole = drillHole;
                        drillBox.User      = user;
                        //Dependency not required
                        if (deposit.Id > 0)   { drillHole.Deposit  = deposit; }
                        if (mine.Id > 0)      { drillHole.Mine     = mine; }
                        if (mineArea.Id > 0)  { drillHole.MineArea = mineArea; }
                        if (type.Id > 0)      { drillBox.Type      = type; }
                        if (status.Id > 0)    { drillBox.Status    = status; }
                        if (material.Id > 0)  { drillBox.Material  = material; }
                        if (coreShed.Id > 0)  { drillBox.CoreShed  = coreShed; }
                        //Return
                        return drillBox;
                    },
                    splitOn: "split",
                    param: new { depositId });
                return await PageList<DrillBox>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillBox>> GetByMine(int mineId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT B.*, 'split',  H.*,  'split',  R.*,  'split', D.*,  'split', M.*,  'split', 
                                        MA.*, 'split', BT.*, 'split', BS.*, 'split', BM.*, 'split', C.*,  'split', U.*
                                FROM DrillBox B
                                INNER JOIN DrillHole         H  ON B.drillHoleId = H.Id
                                INNER JOIN Region            R  ON H.RegionId    = R.Id
                                LEFT  JOIN Deposit           D  ON H.depositId   = D.Id
                                LEFT  JOIN Mine              M  ON H.mineId      = M.Id
                                LEFT  JOIN MineArea          MA ON H.mineAreaId  = MA.Id
                                LEFT  JOIN DrillBoxType      BT ON B.typeId      = BT.Id
                                LEFT  JOIN DrillBoxStatus    BS ON B.statusId    = BS.Id
                                LEFT  JOIN DrillBoxMaterial  BM ON B.materialId  = BM.Id
                                LEFT  JOIN CoreShed          C  ON B.coreShedId  = C.Id
                                INNER JOIN User              U  ON B.UserId      = U.Id
                                WHERE M.Id = @mineId "; 
                if (term != ""){
                    query = query + "AND  (B.number     LIKE '%" + term + "%' " +
                                    "OR    B.code       LIKE '%" + term + "%' " +
                                    "OR    B.startDepth LIKE '%" + term + "%' " +
                                    "OR    B.endDepth   LIKE '%" + term + "%' " +
                                    "OR    H.Name       LIKE '%" + term + "%' " +
                                    "OR    BT.Name      LIKE '%" + term + "%' " +
                                    "OR    BS.Name      LIKE '%" + term + "%' " +
                                    "OR    C.Name       LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillBox>(
                    query, 
                    new[] {
                        typeof(DrillBox),
                        typeof(DrillHole),
                        typeof(Region),
                        typeof(Deposit),
                        typeof(Mine),
                        typeof(MineArea),
                        typeof(DrillBoxType),
                        typeof(DrillBoxStatus),
                        typeof(DrillBoxMaterial),
                        typeof(CoreShed),
                        typeof(User),
                    },
                    objects => {
                        DrillBox         drillBox   = objects[0]  as DrillBox;
                        DrillHole        drillHole  = objects[1]  as DrillHole;
                        Region           region     = objects[2]  as Region;
                        Deposit          deposit    = objects[3]  as Deposit;
                        Mine             mine       = objects[4]  as Mine;
                        MineArea         mineArea   = objects[5]  as MineArea;
                        DrillBoxType     type       = objects[6]  as DrillBoxType;
                        DrillBoxStatus   status     = objects[7]  as DrillBoxStatus;
                        DrillBoxMaterial material   = objects[8]  as DrillBoxMaterial;
                        CoreShed         coreShed   = objects[9]  as CoreShed;
                        User             user       = objects[10] as User;
                        //Dependency required
                        drillHole.Region   = region;
                        drillBox.DrillHole = drillHole;
                        drillBox.User      = user;
                        //Dependency not required
                        if (deposit.Id > 0)   { drillHole.Deposit  = deposit; }
                        if (mine.Id > 0)      { drillHole.Mine     = mine; }
                        if (mineArea.Id > 0)  { drillHole.MineArea = mineArea; }
                        if (type.Id > 0)      { drillBox.Type      = type; }
                        if (status.Id > 0)    { drillBox.Status    = status; }
                        if (material.Id > 0)  { drillBox.Material  = material; }
                        if (coreShed.Id > 0)  { drillBox.CoreShed  = coreShed; }
                        //Return
                        return drillBox;
                    },
                    splitOn: "split",
                    param: new { mineId });
                return await PageList<DrillBox>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillBox>> GetByMineArea(int mineAreaId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT B.*, 'split',  H.*,  'split',  R.*,  'split', D.*,  'split', M.*,  'split', 
                                        MA.*, 'split', BT.*, 'split', BS.*, 'split', BM.*, 'split', C.*,  'split', U.*
                                FROM DrillBox B
                                INNER JOIN DrillHole         H  ON B.drillHoleId = H.Id
                                INNER JOIN Region            R  ON H.RegionId    = R.Id
                                LEFT  JOIN Deposit           D  ON H.depositId   = D.Id
                                LEFT  JOIN Mine              M  ON H.mineId      = M.Id
                                LEFT  JOIN MineArea          MA ON H.mineAreaId  = MA.Id
                                LEFT  JOIN DrillBoxType      BT ON B.typeId      = BT.Id
                                LEFT  JOIN DrillBoxStatus    BS ON B.statusId    = BS.Id
                                LEFT  JOIN DrillBoxMaterial  BM ON B.materialId  = BM.Id
                                LEFT  JOIN CoreShed          C  ON B.coreShedId  = C.Id
                                INNER JOIN User              U  ON B.UserId      = U.Id
                                WHERE MA.Id = @mineAreaId "; 
                if (term != ""){
                    query = query + "AND  (B.number     LIKE '%" + term + "%' " +
                                    "OR    B.code       LIKE '%" + term + "%' " +
                                    "OR    B.startDepth LIKE '%" + term + "%' " +
                                    "OR    B.endDepth   LIKE '%" + term + "%' " +
                                    "OR    H.Name       LIKE '%" + term + "%' " +
                                    "OR    BT.Name      LIKE '%" + term + "%' " +
                                    "OR    BS.Name      LIKE '%" + term + "%' " +
                                    "OR    C.Name       LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillBox>(
                    query, 
                    new[] {
                        typeof(DrillBox),
                        typeof(DrillHole),
                        typeof(Region),
                        typeof(Deposit),
                        typeof(Mine),
                        typeof(MineArea),
                        typeof(DrillBoxType),
                        typeof(DrillBoxStatus),
                        typeof(DrillBoxMaterial),
                        typeof(CoreShed),
                        typeof(User),
                    },
                    objects => {
                        DrillBox         drillBox   = objects[0]  as DrillBox;
                        DrillHole        drillHole  = objects[1]  as DrillHole;
                        Region           region     = objects[2]  as Region;
                        Deposit          deposit    = objects[3]  as Deposit;
                        Mine             mine       = objects[4]  as Mine;
                        MineArea         mineArea   = objects[5]  as MineArea;
                        DrillBoxType     type       = objects[6]  as DrillBoxType;
                        DrillBoxStatus   status     = objects[7]  as DrillBoxStatus;
                        DrillBoxMaterial material   = objects[8]  as DrillBoxMaterial;
                        CoreShed         coreShed   = objects[9]  as CoreShed;
                        User             user       = objects[10] as User;
                        //Dependency required
                        drillHole.Region   = region;
                        drillBox.DrillHole = drillHole;
                        drillBox.User      = user;
                        //Dependency not required
                        if (deposit.Id > 0)   { drillHole.Deposit  = deposit; }
                        if (mine.Id > 0)      { drillHole.Mine     = mine; }
                        if (mineArea.Id > 0)  { drillHole.MineArea = mineArea; }
                        if (type.Id > 0)      { drillBox.Type      = type; }
                        if (status.Id > 0)    { drillBox.Status    = status; }
                        if (material.Id > 0)  { drillBox.Material  = material; }
                        if (coreShed.Id > 0)  { drillBox.CoreShed  = coreShed; }
                        //Return
                        return drillBox;
                    },
                    splitOn: "split",
                    param: new { mineAreaId });
                return await PageList<DrillBox>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillBox>> GetByDrillHole(int drillHoleId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT B.*, 'split',  H.*,  'split',  R.*,  'split', D.*,  'split', M.*,  'split', 
                                        MA.*, 'split', BT.*, 'split', BS.*, 'split', BM.*, 'split', C.*,  'split', U.*
                                FROM DrillBox B
                                INNER JOIN DrillHole         H  ON B.drillHoleId = H.Id
                                INNER JOIN Region            R  ON H.RegionId    = R.Id
                                LEFT  JOIN Deposit           D  ON H.depositId   = D.Id
                                LEFT  JOIN Mine              M  ON H.mineId      = M.Id
                                LEFT  JOIN MineArea          MA ON H.mineAreaId  = MA.Id
                                LEFT  JOIN DrillBoxType      BT ON B.typeId      = BT.Id
                                LEFT  JOIN DrillBoxStatus    BS ON B.statusId    = BS.Id
                                LEFT  JOIN DrillBoxMaterial  BM ON B.materialId  = BM.Id
                                LEFT  JOIN CoreShed          C  ON B.coreShedId  = C.Id
                                INNER JOIN User              U  ON B.UserId      = U.Id
                                WHERE H.Id = @drillHoleId "; 
                if (term != ""){
                    query = query + "AND  (B.number     LIKE '%" + term + "%' " +
                                    "OR    B.code       LIKE '%" + term + "%' " +
                                    "OR    B.startDepth LIKE '%" + term + "%' " +
                                    "OR    B.endDepth   LIKE '%" + term + "%' " +
                                    "OR    H.Name       LIKE '%" + term + "%' " +
                                    "OR    BT.Name      LIKE '%" + term + "%' " +
                                    "OR    BS.Name      LIKE '%" + term + "%' " +
                                    "OR    C.Name       LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillBox>(
                    query, 
                    new[] {
                        typeof(DrillBox),
                        typeof(DrillHole),
                        typeof(Region),
                        typeof(Deposit),
                        typeof(Mine),
                        typeof(MineArea),
                        typeof(DrillBoxType),
                        typeof(DrillBoxStatus),
                        typeof(DrillBoxMaterial),
                        typeof(CoreShed),
                        typeof(User),
                    },
                    objects => {
                        DrillBox         drillBox   = objects[0]  as DrillBox;
                        DrillHole        drillHole  = objects[1]  as DrillHole;
                        Region           region     = objects[2]  as Region;
                        Deposit          deposit    = objects[3]  as Deposit;
                        Mine             mine       = objects[4]  as Mine;
                        MineArea         mineArea   = objects[5]  as MineArea;
                        DrillBoxType     type       = objects[6]  as DrillBoxType;
                        DrillBoxStatus   status     = objects[7]  as DrillBoxStatus;
                        DrillBoxMaterial material   = objects[8]  as DrillBoxMaterial;
                        CoreShed         coreShed   = objects[9]  as CoreShed;
                        User             user       = objects[10] as User;
                        //Dependency required
                        drillHole.Region   = region;
                        drillBox.DrillHole = drillHole;
                        drillBox.User      = user;
                        //Dependency not required
                        if (deposit.Id > 0)   { drillHole.Deposit  = deposit; }
                        if (mine.Id > 0)      { drillHole.Mine     = mine; }
                        if (mineArea.Id > 0)  { drillHole.MineArea = mineArea; }
                        if (type.Id > 0)      { drillBox.Type      = type; }
                        if (status.Id > 0)    { drillBox.Status    = status; }
                        if (material.Id > 0)  { drillBox.Material  = material; }
                        if (coreShed.Id > 0)  { drillBox.CoreShed  = coreShed; }
                        //Return
                        return drillBox;
                    },
                    splitOn: "split",
                    param: new { drillHoleId });
                return await PageList<DrillBox>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DrillBox> GetById(int id)
        {
            try
            {
                var conn = _db.Connection; 
                string query = @"SELECT B.*, 'split',  H.*,  'split',  R.*,  'split', D.*,  'split', M.*,  'split', 
                                        MA.*, 'split', BT.*, 'split', BS.*, 'split', BM.*, 'split', C.*,  'split', U.*
                                FROM DrillBox B
                                INNER JOIN DrillHole         H  ON B.drillHoleId = H.Id
                                INNER JOIN Region            R  ON H.RegionId    = R.Id
                                LEFT  JOIN Deposit           D  ON H.depositId   = D.Id
                                LEFT  JOIN Mine              M  ON H.mineId      = M.Id
                                LEFT  JOIN MineArea          MA ON H.mineAreaId  = MA.Id
                                LEFT  JOIN DrillBoxType      BT ON B.typeId      = BT.Id
                                LEFT  JOIN DrillBoxStatus    BS ON B.statusId    = BS.Id
                                LEFT  JOIN DrillBoxMaterial  BM ON B.materialId  = BM.Id
                                LEFT  JOIN CoreShed          C  ON B.coreShedId  = C.Id
                                INNER JOIN User              U  ON B.UserId      = U.Id
                                WHERE B.Id = @id";
                var res = await conn.QueryAsync<DrillBox>(
                    query, 
                    new[] {
                        typeof(DrillBox),
                        typeof(DrillHole),
                        typeof(Region),
                        typeof(Deposit),
                        typeof(Mine),
                        typeof(MineArea),
                        typeof(DrillBoxType),
                        typeof(DrillBoxStatus),
                        typeof(DrillBoxMaterial),
                        typeof(CoreShed),
                        typeof(User),
                    },
                    objects => {
                        DrillBox         drillBox   = objects[0]  as DrillBox;
                        DrillHole        drillHole  = objects[1]  as DrillHole;
                        Region           region     = objects[2]  as Region;
                        Deposit          deposit    = objects[3]  as Deposit;
                        Mine             mine       = objects[4]  as Mine;
                        MineArea         mineArea   = objects[5]  as MineArea;
                        DrillBoxType     type       = objects[6]  as DrillBoxType;
                        DrillBoxStatus   status     = objects[7]  as DrillBoxStatus;
                        DrillBoxMaterial material   = objects[8]  as DrillBoxMaterial;
                        CoreShed         coreShed   = objects[9]  as CoreShed;
                        User             user       = objects[10] as User;
                        //Dependency required
                        drillHole.Region   = region;
                        drillBox.DrillHole = drillHole;
                        drillBox.User      = user;
                        //Dependency not required
                        if (deposit.Id > 0)   { drillHole.Deposit  = deposit; }
                        if (mine.Id > 0)      { drillHole.Mine     = mine; }
                        if (mineArea.Id > 0)  { drillHole.MineArea = mineArea; }
                        if (type.Id > 0)      { drillBox.Type      = type; }
                        if (status.Id > 0)    { drillBox.Status    = status; }
                        if (material.Id > 0)  { drillBox.Material  = material; }
                        if (coreShed.Id > 0)  { drillBox.CoreShed  = coreShed; }
                        //Return
                        return drillBox;
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