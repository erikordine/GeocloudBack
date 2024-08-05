using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Repositories
{
    public class DrillBoxActivityRepository: IDrillBoxActivityRepository
    {
        private DbSession _db;
        
        public DrillBoxActivityRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(DrillBoxActivity drillBoxActivity)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    //Required
                    if (drillBoxActivity.DrillBoxId == 0) { return 0; }
                    if (drillBoxActivity.UserId    == 0) { return 0; }
                    //Not Required
                    var activityId = "null";
                    if (drillBoxActivity.ActivityId > 0) { 
                        activityId = drillBoxActivity.ActivityId.ToString(); }
                    string command = @"INSERT INTO DRILLBOXACTIVITY(   
                                            drillBoxId, activityId, startTime, endTime, userId, register) 
                                        VALUES(@drillBoxId, " + activityId + ", " + 
                                            "@startTime, @endTime, @userId, @register ); " +
                                        "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: drillBoxActivity);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(DrillBoxActivity drillBoxActivity)
        {
            try
            {
                var conn = _db.Connection;
                //Required
                if (drillBoxActivity.DrillBoxId == 0) { return 0; }
                if (drillBoxActivity.UserId    == 0) { return 0; }
                //Not Required
                var activityId = "null";
                if (drillBoxActivity.ActivityId > 0) { 
                    activityId = drillBoxActivity.ActivityId.ToString(); }
                string command = @"UPDATE DRILLBOXACTIVITY SET 
                                    drillBoxId   = @drillBoxId,
                                    activityId   = " + activityId + @", 
                                    startTime    = @startTime,
                                    endTime      = @endTime 
                                    WHERE id     = @id";
                var result = await conn.ExecuteAsync(sql: command, param: drillBoxActivity);
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
                string command = @"DELETE FROM DRILLBOXACTIVITY WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillBoxActivity>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT DA.*, 'split', D.*, 'split', DT.*, 'split', U.*
                                FROM DrillBoxActivity DA  
                                INNER JOIN DrillBox                 D   ON DA.DrillBoxId = D.Id
                                LEFT  JOIN DrillBoxActivityType     DT  ON DA.ActivityId = DT.Id
                                INNER JOIN User                     U   ON DA.UserId     = U.Id"; 
                if (term != ""){
                    query = query + "WHERE D.code        LIKE '%" + term + "%' " +
                                    "OR    DT.Name       LIKE '%" + term + "%' " +
                                    "OR    DA.endDepth   LIKE '%" + term + "%' " +
                                    "OR    DA.startTime  LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillBoxActivity>(
                    query, 
                    new[] {
                        typeof(DrillBoxActivity),
                        typeof(DrillBox),
                        typeof(DrillBoxActivityType),
                        typeof(User),
                    },
                    objects => {
                        DrillBoxActivity        drillBoxActivity = objects[0] as DrillBoxActivity;
                        DrillBox                drillBox         = objects[1] as DrillBox;
                        DrillBoxActivityType    activity         = objects[2] as DrillBoxActivityType;
                        User                    user             = objects[3] as User;
                        //Dependency required
                        drillBoxActivity.DrillBox = drillBox;
                        drillBoxActivity.User    = user;
                        //Dependency not required
                        if (activity.Id > 0) { drillBoxActivity.Activity = activity; }
                        //Return
                        return drillBoxActivity;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<DrillBoxActivity>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillBoxActivity>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT DA.*, 'split', D.*, 'split', DT.*, 'split', U.*
                                FROM DrillBoxActivity DA  
                                INNER JOIN DrillBox                     D   ON DA.DrillBoxId = D.Id
                                LEFT  JOIN DrillBoxActivityType         DT  ON DA.ActivityId = DT.Id
                                INNER JOIN User                         U   ON DA.UserId     = U.Id
                                WHERE DT.AccountId = @accountId "; 
                if (term != ""){
                    query = query + "AND  (D.code        LIKE '%" + term + "%' " +
                                    "OR    DT.name       LIKE '%" + term + "%' " +
                                    "OR    DA.startTime  LIKE '%" + term + "%' " +
                                    "OR    DA.endTime    LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillBoxActivity>(
                    query, 
                    new[] {
                        typeof(DrillBoxActivity),
                        typeof(DrillBox),
                        typeof(DrillBoxActivityType),
                        typeof(User),
                    },
                    objects => {
                        DrillBoxActivity        drillBoxActivity = objects[0] as DrillBoxActivity;
                        DrillBox                drillBox         = objects[1] as DrillBox;
                        DrillBoxActivityType    activity         = objects[2] as DrillBoxActivityType;
                        User                    user             = objects[3] as User;
                        //Dependency required
                        drillBoxActivity.DrillBox = drillBox;
                        drillBoxActivity.User    = user;
                        //Dependency not required
                        if (activity.Id > 0) { drillBoxActivity.Activity = activity; }
                        //Return
                        return drillBoxActivity;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<DrillBoxActivity>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillBoxActivity>> GetByDrillBox(int drillBoxId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT DA.*, 'split', D.*, 'split', DT.*, 'split', U.*
                                FROM DrillBoxActivity DA  
                                INNER JOIN DrillBox                 D   ON DA.DrillBoxId   = D.Id
                                LEFT  JOIN DrillBoxActivityType     DT  ON DA.ActivityId   = DT.Id
                                INNER JOIN User                     U   ON DA.UserId       = U.Id
                                WHERE D.Id = @drillBoxId "; 
                if (term != ""){
                        query = query + "AND  (D.code        LIKE '%" + term + "%' " +
                                        "OR    DT.Name       LIKE '%" + term + "%' " +
                                        "OR    DA.endDepth   LIKE '%" + term + "%' " +
                                        "OR    DA.startTime  LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillBoxActivity>(
                    query, 
                    new[] {
                        typeof(DrillBoxActivity),
                        typeof(DrillBox),
                        typeof(DrillBoxActivityType),
                        typeof(User),
                    },
                    objects => {
                        DrillBoxActivity        drillBoxActivity = objects[0] as DrillBoxActivity;
                        DrillBox                drillBox         = objects[1] as DrillBox;
                        DrillBoxActivityType    activity         = objects[2] as DrillBoxActivityType;
                        User                    user             = objects[3] as User;
                        //Dependency required
                        drillBoxActivity.DrillBox = drillBox;
                        drillBoxActivity.User     = user;
                        //Dependency not required
                        if (activity.Id > 0) { drillBoxActivity.Activity = activity; }
                        //Return
                        return drillBoxActivity;
                    },
                    splitOn: "split",
                    param: new { drillBoxId });
                return await PageList<DrillBoxActivity>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DrillBoxActivity> GetById(int id)
        {
            try
            {
                var conn = _db.Connection; 
                string query = @"SELECT DA.*, 'split', D.*, 'split', DT.*, 'split', U.*
                                FROM DrillBoxActivity DA  
                                INNER JOIN DrillBox                 D   ON DA.DrillBoxId = D.Id
                                LEFT  JOIN DrillBoxActivityType     DT  ON DA.ActivityId = DT.Id
                                INNER JOIN User                     U   ON DA.UserId     = U.Id
                                WHERE DA.Id = @id";
                var res = await conn.QueryAsync<DrillBoxActivity>(
                    query, 
                    new[] {
                        typeof(DrillBoxActivity),
                        typeof(DrillBox),
                        typeof(DrillBoxActivityType),
                        typeof(User),
                    },
                    objects => {
                        DrillBoxActivity     drillBoxActivity = objects[0] as DrillBoxActivity;
                        DrillBox             drillBox         = objects[1] as DrillBox;
                        DrillBoxActivityType activity         = objects[2] as DrillBoxActivityType;
                        User                 user             = objects[3] as User;
                        //Dependency required
                        drillBoxActivity.DrillBox = drillBox;
                        drillBoxActivity.User     = user;
                        //Dependency not required
                        if (activity.Id > 0) { drillBoxActivity.Activity = activity; }
                        //Return
                        return drillBoxActivity;
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