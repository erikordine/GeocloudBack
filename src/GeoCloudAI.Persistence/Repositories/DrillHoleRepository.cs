using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Repositories
{
    public class DrillHoleRepository: IDrillHoleRepository
    {
        private DbSession _db;
        
        public DrillHoleRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(DrillHole drillHole)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    //Required
                    if (drillHole.RegionId == 0) { return 0; }
                    if (drillHole.UserId   == 0) { return 0; }
                    //Not Required
                    var depositId = "null";
                    if (drillHole.DepositId > 0) { 
                        depositId = drillHole.DepositId.ToString(); }
                   var mineId = "null";
                    if (drillHole.MineId > 0) { 
                        mineId = drillHole.MineId.ToString(); }
                    var mineAreaId = "null";
                    if (drillHole.MineAreaId > 0) { 
                        mineAreaId = drillHole.MineAreaId.ToString(); }
                    var typeId = "null";
                    if (drillHole.TypeId > 0) { 
                        typeId = drillHole.TypeId.ToString(); }
                    var drillingTypeId = "null";
                    if (drillHole.DrillingTypeId > 0) { 
                        drillingTypeId = drillHole.DrillingTypeId.ToString(); }
                    var contractorId = "null";
                    if (drillHole.ContractorId > 0) { 
                        contractorId = drillHole.ContractorId.ToString(); }
                    var drillerId = "null";
                    if (drillHole.DrillerId > 0) { 
                        drillerId = drillHole.DrillerId.ToString(); }
                    string command = @"INSERT INTO DRILLHOLE( 
                                            regionId, depositId, mineId, mineAreaId, name, latitude, longitude, elevation,
                                            length, comments, typeId, drillingTypeId, contractorId, drillerId,
                                            startDate, endDate, userId, register) 
                                        
                                        VALUES(@regionId, " + depositId + ", " + mineId + ", " + mineAreaId + ", " + 
                                            "@name, @latitude, @longitude, @elevation, @length, @comments, " +
                                            typeId + ", " + drillingTypeId + ", " + contractorId + ", " + drillerId + ", " +
                                            "@startDate, @endDate, @userId, @register); " +
                                        "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: drillHole);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(DrillHole drillHole)
        {
            try
            {
                var conn = _db.Connection;
                //Required
                if (drillHole.RegionId == 0) { return 0; }
                if (drillHole.UserId   == 0) { return 0; }
                //Not Required
                var depositId = "null";
                if (drillHole.DepositId > 0) { 
                    depositId = drillHole.DepositId.ToString(); }
                var mineId = "null";
                if (drillHole.MineId > 0) { 
                    mineId = drillHole.MineId.ToString(); }
                var mineAreaId = "null";
                if (drillHole.MineAreaId > 0) { 
                    mineAreaId = drillHole.MineAreaId.ToString(); }
                var typeId = "null";
                if (drillHole.TypeId > 0) { 
                    typeId = drillHole.TypeId.ToString(); }
                var drillingTypeId = "null";
                if (drillHole.DrillingTypeId > 0) { 
                    drillingTypeId = drillHole.DrillingTypeId.ToString(); }
                var contractorId = "null";
                if (drillHole.ContractorId > 0) { 
                    contractorId = drillHole.ContractorId.ToString(); }
                var drillerId = "null";
                if (drillHole.DrillerId > 0) { 
                    drillerId = drillHole.DrillerId.ToString(); }
                string command = @"UPDATE DRILLHOLE SET 
                                    regionId            = @regionId,
                                    depositId           = " + depositId + @",
                                    mineId              = " + mineId + @",
                                    mineAreaId          = " + mineAreaId + @",
                                    name                = @name, 
                                    latitude            = @latitude, 
                                    longitude           = @longitude,
                                    elevation           = @elevation, 
                                    length              = @length,
                                    comments            = @comments,
                                    typeId              = " + typeId + @", 
                                    drillingTypeId      = " + drillingTypeId + @",
                                    contractorId        = " + contractorId + @", 
                                    drillerId           = " + drillerId + @",
                                    startDate           = @startDate,
                                    endDate             = @endDate
                                    WHERE id            = @id";
              var result = await conn.ExecuteAsync(sql: command, param: drillHole);
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
                string command = @"DELETE FROM DRILLHOLE WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillHole>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT DH.*, 
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
	                                 WHERE DB.DrillHoleId = DH.Id) AS QttDrillBoxes,
                                'split', R.*, 'split', D.*, 'split', M.*, 'split', MA.*, 'split', 
                                TY.*, 'split', DT.*, 'split', CC.*, 'split', CD.*, 'split', U.*
                                FROM DrillHole DH  
                                INNER JOIN Region        R   ON DH.RegionId       = R.Id
                                LEFT  JOIN Deposit       D   ON DH.depositId      = D.Id
                                LEFT  JOIN Mine          M   ON DH.mineId         = M.Id
                                LEFT  JOIN MineArea      MA  ON DH.mineAreaId     = MA.Id
                                LEFT  JOIN DrillHoleType TY  ON DH.typeId         = TY.Id
                                LEFT  JOIN DrillingType  DT  ON DH.drillingTypeId = DT.Id
                                LEFT  JOIN Company       CC  ON DH.contractorId   = CC.Id
                                LEFT  JOIN Company       CD  ON DH.drillerId      = CD.Id
                                INNER JOIN User          U   ON DH.UserId         = U.Id"; 
                if (term != ""){
                    query = query + "WHERE DH.Name LIKE '%" + term + "%' " +
                                    "OR    R.Name  LIKE '%" + term + "%' " +
                                    "OR    D.Name  LIKE '%" + term + "%' " +
                                    "OR    M.Name  LIKE '%" + term + "%' " +
                                    "OR    MA.Name LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillHole>(
                    query, 
                    new[] {
                        typeof(DrillHole),
                        typeof(Region),
                        typeof(Deposit),
                        typeof(Mine),
                        typeof(MineArea),
                        typeof(DrillHoleType),
                        typeof(DrillingType),
                        typeof(Company),
                        typeof(Company),
                        typeof(User),
                    },
                    objects => {
                        DrillHole     drillHole      = objects[0] as DrillHole;
                        Region        region         = objects[1] as Region;
                        Deposit       deposit        = objects[2] as Deposit;
                        Mine          mine           = objects[3] as Mine;
                        MineArea      mineArea       = objects[4] as MineArea;
                        DrillHoleType type           = objects[5] as DrillHoleType;
                        DrillingType  drillingType   = objects[6] as DrillingType;
                        Company       contractor     = objects[7] as Company;
                        Company       driller        = objects[8] as Company;
                        User          user           = objects[9] as User;
                        //Dependency required
                        drillHole.Region = region;
                        drillHole.User   = user;
                        //Dependency not required
                        if (deposit.Id > 0)      { drillHole.Deposit     = deposit; }
                        if (mine.Id > 0)         { drillHole.Mine         = mine; }
                        if (mineArea.Id > 0)     { drillHole.MineArea     = mineArea; }
                        if (type.Id > 0)         { drillHole.Type         = type; }
                        if (drillingType.Id > 0) { drillHole.DrillingType = drillingType; }
                        if (contractor.Id > 0)   { drillHole.Contractor   = contractor; }
                        if (driller.Id > 0)      { drillHole.Driller      = driller; }
                        //Return
                        return drillHole;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<DrillHole>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillHole>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT DH.*, 
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
	                                 WHERE DB.DrillHoleId = DH.Id) AS QttDrillBoxes,
                                'split', R.*, 'split', D.*, 'split', M.*, 'split', MA.*, 'split', 
                                TY.*, 'split', DT.*, 'split', CC.*, 'split', CD.*, 'split', U.*
                                FROM DrillHole DH  
                                INNER JOIN Region        R   ON DH.RegionId       = R.Id
                                LEFT  JOIN Deposit       D   ON DH.depositId      = D.Id
                                LEFT  JOIN Mine          M   ON DH.mineId         = M.Id
                                LEFT  JOIN MineArea      MA  ON DH.mineAreaId     = MA.Id
                                LEFT  JOIN DrillHoleType TY  ON DH.typeId         = TY.Id
                                LEFT  JOIN DrillingType  DT  ON DH.drillingTypeId = DT.Id
                                LEFT  JOIN Company       CC  ON DH.contractorId   = CC.Id
                                LEFT  JOIN Company       CD  ON DH.drillerId      = CD.Id
                                INNER JOIN User          U   ON DH.UserId         = U.Id
                                WHERE R.AccountId = @accountId "; 
                if (term != ""){
                    query = query + "AND  (DH.Name LIKE '%" + term + "%' " +
                                    "OR    R.Name  LIKE '%" + term + "%' " +
                                    "OR    D.Name  LIKE '%" + term + "%' " +
                                    "OR    M.Name  LIKE '%" + term + "%' " +
                                    "OR    MA.Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillHole>(
                    query, 
                    new[] {
                        typeof(DrillHole),
                        typeof(Region),
                        typeof(Deposit),
                        typeof(Mine),
                        typeof(MineArea),
                        typeof(DrillHoleType),
                        typeof(DrillingType),
                        typeof(Company),
                        typeof(Company),
                        typeof(User),
                    },
                    objects => {
                        DrillHole     drillHole      = objects[0] as DrillHole;
                        Region        region         = objects[1] as Region;
                        Deposit       deposit        = objects[2] as Deposit;
                        Mine          mine           = objects[3] as Mine;
                        MineArea      mineArea       = objects[4] as MineArea;
                        DrillHoleType type           = objects[5] as DrillHoleType;
                        DrillingType  drillingType   = objects[6] as DrillingType;
                        Company       contractor     = objects[7] as Company;
                        Company       driller        = objects[8] as Company;
                        User          user           = objects[9] as User;
                        //Dependency required
                        drillHole.Region = region;
                        drillHole.User   = user;
                        //Dependency not required
                        if (deposit.Id > 0)      { drillHole.Deposit      = deposit; }
                        if (mine.Id > 0)         { drillHole.Mine         = mine; }
                        if (mineArea.Id > 0)     { drillHole.MineArea     = mineArea; }
                        if (type.Id > 0)         { drillHole.Type         = type; }
                        if (drillingType.Id > 0) { drillHole.DrillingType = drillingType; }
                        if (contractor.Id > 0)   { drillHole.Contractor   = contractor; }
                        if (driller.Id > 0)      { drillHole.Driller      = driller; }
                        //Return
                        return drillHole;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<DrillHole>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillHole>> GetByRegion(int regionId, bool direct, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT DH.*, 
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
	                                 WHERE DB.DrillHoleId = DH.Id) AS QttDrillBoxes,
                                'split', R.*, 'split', D.*, 'split', M.*, 'split', MA.*, 'split', 
                                TY.*, 'split', DT.*, 'split', CC.*, 'split', CD.*, 'split', U.*
                                FROM DrillHole DH  
                                INNER JOIN Region        R   ON DH.RegionId       = R.Id
                                LEFT  JOIN Deposit       D   ON DH.depositId      = D.Id
                                LEFT  JOIN Mine          M   ON DH.mineId         = M.Id
                                LEFT  JOIN MineArea      MA  ON DH.mineAreaId     = MA.Id
                                LEFT  JOIN DrillHoleType TY  ON DH.typeId         = TY.Id
                                LEFT  JOIN DrillingType  DT  ON DH.drillingTypeId = DT.Id
                                LEFT  JOIN Company       CC  ON DH.contractorId   = CC.Id
                                LEFT  JOIN Company       CD  ON DH.drillerId      = CD.Id
                                INNER JOIN User          U   ON DH.UserId         = U.Id
                                WHERE R.Id = @regionId "; 
                if (direct) { 
                    query = query + "AND D.Id is null ";
                }
                if (term != ""){
                    query = query + "AND  (DH.Name LIKE '%" + term + "%' " +
                                    "OR    R.Name  LIKE '%" + term + "%' " +
                                    "OR    D.Name  LIKE '%" + term + "%' " +
                                    "OR    M.Name  LIKE '%" + term + "%' " +
                                    "OR    MA.Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillHole>(
                    query, 
                    new[] {
                        typeof(DrillHole),
                        typeof(Region),
                        typeof(Deposit),
                        typeof(Mine),
                        typeof(MineArea),
                        typeof(DrillHoleType),
                        typeof(DrillingType),
                        typeof(Company),
                        typeof(Company),
                        typeof(User),
                    },
                    objects => {
                        DrillHole     drillHole      = objects[0] as DrillHole;
                        Region        region         = objects[1] as Region;
                        Deposit       deposit        = objects[2] as Deposit;
                        Mine          mine           = objects[3] as Mine;
                        MineArea      mineArea       = objects[4] as MineArea;
                        DrillHoleType type           = objects[5] as DrillHoleType;
                        DrillingType  drillingType   = objects[6] as DrillingType;
                        Company       contractor     = objects[7] as Company;
                        Company       driller        = objects[8] as Company;
                        User          user           = objects[9] as User;
                        //Dependency required
                        drillHole.Region = region;
                        drillHole.User   = user;
                        //Dependency not required
                        if (deposit.Id > 0)      { drillHole.Deposit     = deposit; }
                        if (mine.Id > 0)         { drillHole.Mine         = mine; }
                        if (mineArea.Id > 0)     { drillHole.MineArea     = mineArea; }
                        if (type.Id > 0)         { drillHole.Type         = type; }
                        if (drillingType.Id > 0) { drillHole.DrillingType = drillingType; }
                        if (contractor.Id > 0)   { drillHole.Contractor   = contractor; }
                        if (driller.Id > 0)      { drillHole.Driller      = driller; }
                        //Return
                        return drillHole;
                    },
                    splitOn: "split",
                    param: new { regionId });
                return await PageList<DrillHole>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillHole>> GetByDeposit(int depositId, bool direct, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT DH.*, 
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
	                                 WHERE DB.DrillHoleId = DH.Id) AS QttDrillBoxes,
                                'split', R.*, 'split', D.*, 'split', M.*, 'split', MA.*, 'split', 
                                TY.*, 'split', DT.*, 'split', CC.*, 'split', CD.*, 'split', U.*
                                FROM DrillHole DH  
                                INNER JOIN Region        R   ON DH.RegionId       = R.Id
                                LEFT  JOIN Deposit       D   ON DH.depositId      = D.Id
                                LEFT  JOIN Mine          M   ON DH.mineId         = M.Id
                                LEFT  JOIN MineArea      MA  ON DH.mineAreaId     = MA.Id
                                LEFT  JOIN DrillHoleType TY  ON DH.typeId         = TY.Id
                                LEFT  JOIN DrillingType  DT  ON DH.drillingTypeId = DT.Id
                                LEFT  JOIN Company       CC  ON DH.contractorId   = CC.Id
                                LEFT  JOIN Company       CD  ON DH.drillerId      = CD.Id
                                INNER JOIN User          U   ON DH.UserId         = U.Id
                                WHERE D.Id = @depositId "; 
                if (direct) { 
                    query = query + "AND M.Id is null ";
                }
                if (term != ""){
                    query = query + "AND  (DH.Name LIKE '%" + term + "%' " +
                                    "OR    R.Name  LIKE '%" + term + "%' " +
                                    "OR    D.Name  LIKE '%" + term + "%' " +
                                    "OR    M.Name  LIKE '%" + term + "%' " +
                                    "OR    MA.Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillHole>(
                    query, 
                    new[] {
                        typeof(DrillHole),
                        typeof(Region),
                        typeof(Deposit),
                        typeof(Mine),
                        typeof(MineArea),
                        typeof(DrillHoleType),
                        typeof(DrillingType),
                        typeof(Company),
                        typeof(Company),
                        typeof(User),
                    },
                    objects => {
                        DrillHole     drillHole      = objects[0] as DrillHole;
                        Region        region         = objects[1] as Region;
                        Deposit       deposit        = objects[2] as Deposit;
                        Mine          mine           = objects[3] as Mine;
                        MineArea      mineArea       = objects[4] as MineArea;
                        DrillHoleType type           = objects[5] as DrillHoleType;
                        DrillingType  drillingType   = objects[6] as DrillingType;
                        Company       contractor     = objects[7] as Company;
                        Company       driller        = objects[8] as Company;
                        User          user           = objects[9] as User;
                        //Dependency required
                        drillHole.Region = region;
                        drillHole.User   = user;
                        //Dependency not required
                        if (deposit.Id > 0)      { drillHole.Deposit     = deposit; }
                        if (mine.Id > 0)         { drillHole.Mine         = mine; }
                        if (mineArea.Id > 0)     { drillHole.MineArea     = mineArea; }
                        if (type.Id > 0)         { drillHole.Type         = type; }
                        if (drillingType.Id > 0) { drillHole.DrillingType = drillingType; }
                        if (contractor.Id > 0)   { drillHole.Contractor   = contractor; }
                        if (driller.Id > 0)      { drillHole.Driller      = driller; }
                        //Return
                        return drillHole;
                    },
                    splitOn: "split",
                    param: new { depositId });
                return await PageList<DrillHole>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillHole>> GetByMine(int mineId, bool direct, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT DH.*, 
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
	                                 WHERE DB.DrillHoleId = DH.Id) AS QttDrillBoxes,
                                'split', R.*, 'split', D.*, 'split', M.*, 'split', MA.*, 'split', 
                                TY.*, 'split', DT.*, 'split', CC.*, 'split', CD.*, 'split', U.*
                                FROM DrillHole DH  
                                INNER JOIN Region        R   ON DH.RegionId       = R.Id
                                LEFT  JOIN Deposit       D   ON DH.depositId      = D.Id
                                LEFT  JOIN Mine          M   ON DH.mineId         = M.Id
                                LEFT  JOIN MineArea      MA  ON DH.mineAreaId     = MA.Id
                                LEFT  JOIN DrillHoleType TY  ON DH.typeId         = TY.Id
                                LEFT  JOIN DrillingType  DT  ON DH.drillingTypeId = DT.Id
                                LEFT  JOIN Company       CC  ON DH.contractorId   = CC.Id
                                LEFT  JOIN Company       CD  ON DH.drillerId      = CD.Id
                                INNER JOIN User          U   ON DH.UserId         = U.Id
                                WHERE M.Id = @mineId "; 
                if (direct) { 
                    query = query + "AND MA.Id is null ";
                }
                if (term != ""){
                    query = query + "AND  (DH.Name LIKE '%" + term + "%' " +
                                    "OR    R.Name  LIKE '%" + term + "%' " +
                                    "OR    D.Name  LIKE '%" + term + "%' " +
                                    "OR    M.Name  LIKE '%" + term + "%' " +
                                    "OR    MA.Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillHole>(
                    query, 
                    new[] {
                        typeof(DrillHole),
                        typeof(Region),
                        typeof(Deposit),
                        typeof(Mine),
                        typeof(MineArea),
                        typeof(DrillHoleType),
                        typeof(DrillingType),
                        typeof(Company),
                        typeof(Company),
                        typeof(User),
                    },
                    objects => {
                        DrillHole     drillHole      = objects[0] as DrillHole;
                        Region        region         = objects[1] as Region;
                        Deposit       deposit        = objects[2] as Deposit;
                        Mine          mine           = objects[3] as Mine;
                        MineArea      mineArea       = objects[4] as MineArea;
                        DrillHoleType type           = objects[5] as DrillHoleType;
                        DrillingType  drillingType   = objects[6] as DrillingType;
                        Company       contractor     = objects[7] as Company;
                        Company       driller        = objects[8] as Company;
                        User          user           = objects[9] as User;
                        //Dependency required
                        drillHole.Region = region;
                        drillHole.User   = user;
                        //Dependency not required
                        if (deposit.Id > 0)      { drillHole.Deposit     = deposit; }
                        if (mine.Id > 0)         { drillHole.Mine         = mine; }
                        if (mineArea.Id > 0)     { drillHole.MineArea     = mineArea; }
                        if (type.Id > 0)         { drillHole.Type         = type; }
                        if (drillingType.Id > 0) { drillHole.DrillingType = drillingType; }
                        if (contractor.Id > 0)   { drillHole.Contractor   = contractor; }
                        if (driller.Id > 0)      { drillHole.Driller      = driller; }
                        //Return
                        return drillHole;
                    },
                    splitOn: "split",
                    param: new { mineId });
                return await PageList<DrillHole>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillHole>> GetByMineArea(int mineAreaId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT DH.*, 
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
	                                 WHERE DB.DrillHoleId = DH.Id) AS QttDrillBoxes,
                                'split', R.*, 'split', D.*, 'split', M.*, 'split', MA.*, 'split', 
                                TY.*, 'split', DT.*, 'split', CC.*, 'split', CD.*, 'split', U.*
                                FROM DrillHole DH  
                                INNER JOIN Region        R   ON DH.RegionId       = R.Id
                                LEFT  JOIN Deposit       D   ON DH.depositId      = D.Id
                                LEFT  JOIN Mine          M   ON DH.mineId         = M.Id
                                LEFT  JOIN MineArea      MA  ON DH.mineAreaId     = MA.Id
                                LEFT  JOIN DrillHoleType TY  ON DH.typeId         = TY.Id
                                LEFT  JOIN DrillingType  DT  ON DH.drillingTypeId = DT.Id
                                LEFT  JOIN Company       CC  ON DH.contractorId   = CC.Id
                                LEFT  JOIN Company       CD  ON DH.drillerId      = CD.Id
                                INNER JOIN User          U   ON DH.UserId         = U.Id
                                WHERE MA.Id = @mineAreaId "; 
                if (term != ""){
                    query = query + "AND  (DH.Name LIKE '%" + term + "%' " +
                                    "OR    R.Name  LIKE '%" + term + "%' " +
                                    "OR    D.Name  LIKE '%" + term + "%' " +
                                    "OR    M.Name  LIKE '%" + term + "%' " +
                                    "OR    MA.Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillHole>(
                    query, 
                    new[] {
                        typeof(DrillHole),
                        typeof(Region),
                        typeof(Deposit),
                        typeof(Mine),
                        typeof(MineArea),
                        typeof(DrillHoleType),
                        typeof(DrillingType),
                        typeof(Company),
                        typeof(Company),
                        typeof(User),
                    },
                    objects => {
                        DrillHole     drillHole      = objects[0] as DrillHole;
                        Region        region         = objects[1] as Region;
                        Deposit       deposit        = objects[2] as Deposit;
                        Mine          mine           = objects[3] as Mine;
                        MineArea      mineArea       = objects[4] as MineArea;
                        DrillHoleType type           = objects[5] as DrillHoleType;
                        DrillingType  drillingType   = objects[6] as DrillingType;
                        Company       contractor     = objects[7] as Company;
                        Company       driller        = objects[8] as Company;
                        User          user           = objects[9] as User;
                        //Dependency required
                        drillHole.Region = region;
                        drillHole.User   = user;
                        //Dependency not required
                        if (deposit.Id > 0)      { drillHole.Deposit     = deposit; }
                        if (mine.Id > 0)         { drillHole.Mine         = mine; }
                        if (mineArea.Id > 0)     { drillHole.MineArea     = mineArea; }
                        if (type.Id > 0)         { drillHole.Type         = type; }
                        if (drillingType.Id > 0) { drillHole.DrillingType = drillingType; }
                        if (contractor.Id > 0)   { drillHole.Contractor   = contractor; }
                        if (driller.Id > 0)      { drillHole.Driller      = driller; }
                        //Return
                        return drillHole;
                    },
                    splitOn: "split",
                    param: new { mineAreaId });
                return await PageList<DrillHole>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DrillHole> GetById(int id)
        {
            try
            {
                var conn = _db.Connection; 
                string query = @"SELECT DH.*, 
                                    (SELECT COUNT(DB.Id) FROM DrillBox DB
	                                 WHERE DB.DrillHoleId = DH.Id) AS QttDrillBoxes,
                                'split', R.*, 'split', D.*, 'split', M.*, 'split', MA.*, 'split', 
                                TY.*, 'split', DT.*, 'split', CC.*, 'split', CD.*, 'split', U.*
                                FROM DrillHole DH  
                                INNER JOIN Region        R   ON DH.RegionId       = R.Id
                                LEFT  JOIN Deposit       D   ON DH.depositId      = D.Id
                                LEFT  JOIN Mine          M   ON DH.mineId         = M.Id
                                LEFT  JOIN MineArea      MA  ON DH.mineAreaId     = MA.Id
                                LEFT  JOIN DrillHoleType TY  ON DH.typeId         = TY.Id
                                LEFT  JOIN DrillingType  DT  ON DH.drillingTypeId = DT.Id
                                LEFT  JOIN Company       CC  ON DH.contractorId   = CC.Id
                                LEFT  JOIN Company       CD  ON DH.drillerId      = CD.Id
                                INNER JOIN User          U   ON DH.UserId         = U.Id
                                WHERE DH.Id = @id";
                var res = await conn.QueryAsync<DrillHole>(
                    query, 
                    new[] {
                        typeof(DrillHole),
                        typeof(Region),
                        typeof(Deposit),
                        typeof(Mine),
                        typeof(MineArea),
                        typeof(DrillHoleType),
                        typeof(DrillingType),
                        typeof(Company),
                        typeof(Company),
                        typeof(User),
                    },
                    objects => {
                        DrillHole     drillHole      = objects[0] as DrillHole;
                        Region        region         = objects[1] as Region;
                        Deposit       deposit        = objects[2] as Deposit;
                        Mine          mine           = objects[3] as Mine;
                        MineArea      mineArea       = objects[4] as MineArea;
                        DrillHoleType type           = objects[5] as DrillHoleType;
                        DrillingType  drillingType   = objects[6] as DrillingType;
                        Company       contractor     = objects[7] as Company;
                        Company       driller        = objects[8] as Company;
                        User          user           = objects[9] as User;
                        //Dependency required
                        drillHole.Region = region;
                        drillHole.User   = user;
                        //Dependency not required
                        if (deposit.Id > 0)      { drillHole.Deposit     = deposit; }
                        if (mine.Id > 0)         { drillHole.Mine         = mine; }
                        if (mineArea.Id > 0)     { drillHole.MineArea     = mineArea; }
                        if (type.Id > 0)         { drillHole.Type         = type; }
                        if (drillingType.Id > 0) { drillHole.DrillingType = drillingType; }
                        if (contractor.Id > 0)   { drillHole.Contractor   = contractor; }
                        if (driller.Id > 0)      { drillHole.Driller      = driller; }
                        //Return
                        return drillHole;
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