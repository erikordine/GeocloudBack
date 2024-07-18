using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Repositories
{
    public class DrillHoleRunRepository: IDrillHoleRunRepository
    {
        private DbSession _db;
        
        public DrillHoleRunRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(DrillHoleRun drillHoleRun)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    //Required
                    if (drillHoleRun.DrillHoleId == 0) { return 0; }
                    if (drillHoleRun.UserId      == 0) { return 0; }
                    //Not Required
                    string command = @"INSERT INTO DRILLHOLERUN( 
                                            drillHoleId, startDepth, endDepth, startTime, endTime,userId, register) 
                                        VALUES(@drillHoleId, @startDepth, @endDepth, @startTime, @endTime,
                                               @userId, @register); " +
                                        "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: drillHoleRun);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(DrillHoleRun drillHoleRun)
        {
            try
            {
                var conn = _db.Connection;
                //Required
                if (drillHoleRun.DrillHoleId == 0) { return 0; }
                if (drillHoleRun.UserId      == 0) { return 0; }
                string command = @"UPDATE DRILLHOLERUN SET 
                                    drillHoleId  = @drillHoleId,
                                    startDepth   = @startDepth,
                                    endDepth     = @endDepth,
                                    startTime    = @startTime,
                                    endTime      = @endTime
                                    WHERE id     = @id";
              var result = await conn.ExecuteAsync(sql: command, param: drillHoleRun);
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
                string command = @"DELETE FROM DRILLHOLERUN WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillHoleRun>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT R.*,'split',  H.*,  'split', U.*
                                FROM  DrillHoleRun R
                                INNER JOIN DrillHole         H  ON R.drillHoleId = H.Id
                                INNER JOIN User              U  ON R.UserId      = U.Id "; 
                if (term != ""){
                    query = query + "WHERE R.number     LIKE '%" + term + "%' " +
                                    "OR    R.startDepth LIKE '%" + term + "%' " +
                                    "OR    R.endDepth   LIKE '%" + term + "%' " +
                                    "OR    R.startTime  LIKE '%" + term + "%' " +
                                    "OR    R.endTime    LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillHoleRun>(
                    query, 
                    new[] {
                        typeof(DrillHoleRun),
                        typeof(DrillHole),
                        typeof(User),
                    },
                    objects => {
                        DrillHoleRun     drillHoleRun   = objects[0]  as DrillHoleRun;
                        DrillHole        drillHole      = objects[1]  as DrillHole;
                        User             user           = objects[2] as User;
                        //Dependency required
                        drillHoleRun.DrillHole = drillHole;
                        drillHoleRun.User      = user;
                        //Return
                        return drillHoleRun;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<DrillHoleRun>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillHoleRun>> GetByDrillHole(int drillHoleId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT R.*,  'split',  H.*,  'split', U.*
                                FROM DrillHoleRun R
                                INNER JOIN DrillHole         H  ON R.drillHoleId = H.Id
                                INNER JOIN User              U  ON R.UserId      = U.Id
                                WHERE H.Id = @drillHoleId "; 
                if (term != ""){
                    query = query + "AND  (R.startDepth LIKE '%" + term + "%' " +
                                    "OR    R.endDepth   LIKE '%" + term + "%' " +
                                    "OR    R.startTime  LIKE '%" + term + "%' " +
                                    "OR    R.endTime    LIKE '%" + term + "%'  ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillHoleRun>(
                    query, 
                    new[] {
                        typeof(DrillHoleRun),
                        typeof(DrillHole),
                        typeof(User),
                    },
                    objects => {
                        DrillHoleRun     drillHoleRun   = objects[0]  as DrillHoleRun;
                        DrillHole        drillHole      = objects[1]  as DrillHole;
                        User             user           = objects[2] as User;
                        //Dependency required
                        drillHoleRun.DrillHole = drillHole;
                        drillHoleRun.User      = user;
                        //Return
                        return drillHoleRun;
                    },
                    splitOn: "split",
                    param: new { drillHoleId });
                return await PageList<DrillHoleRun>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DrillHoleRun> GetById(int id)
        {
            try
            {
                var conn = _db.Connection; 
                string query = @"SELECT R.*, 'split',  H.*,  'split', U.*
                                FROM DrillHoleRun R
                                INNER JOIN DrillHole         H  ON R.drillHoleId = H.Id
                                INNER JOIN User              U  ON R.UserId      = U.Id
                                WHERE R.Id = @id";
                var res = await conn.QueryAsync<DrillHoleRun>(
                    query, 
                    new[] {
                        typeof(DrillHoleRun),
                        typeof(DrillHole),
                        typeof(User),
                    },
                    objects => {
                        DrillHoleRun     drillHoleRun   = objects[0]  as DrillHoleRun;
                        DrillHole        drillHole      = objects[1]  as DrillHole;
                        User             user           = objects[2] as User;
                        //Dependency required
                        drillHoleRun.DrillHole = drillHole;
                        drillHoleRun.User      = user;
                        //Return
                        return drillHoleRun;
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