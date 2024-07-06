using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using System.Linq;

namespace GeoCloudAI.Persistence.Repositories
{
    public class DrillBoxActivityTypeRepository: IDrillBoxActivityTypeRepository
    {
        private DbSession _db;

        public DrillBoxActivityTypeRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(DrillBoxActivityType drillBoxActivityType)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (drillBoxActivityType.AccountId == 0) { return 0; }
                    string command = @"INSERT INTO DRILLBOXACTIVITYTYPE(accountId, name, imgType)
                                        VALUES(@accountId, @name, @imgType); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: drillBoxActivityType);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(DrillBoxActivityType drillBoxActivityType)
        {
            try
            {
                var conn = _db.Connection;
                if (drillBoxActivityType.AccountId == 0) { return 0; }
                string command = @"UPDATE DRILLBOXACTIVITYTYPE SET 
                                    accountId = @accountId,
                                    name      = @name,
                                    imgType   = @imgType
                                    WHERE id  = @id";
                var result = await conn.ExecuteAsync(sql: command, param: drillBoxActivityType);
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
                string command = @"DELETE FROM DRILLBOXACTIVITYTYPE WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        public async Task<PageList<DrillBoxActivityType>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT D.*, 'split', A.*
                                FROM DRILLBOXACTIVITYTYPE D 
                                INNER JOIN Account A ON D.accountId = A.id ";
                if (term != ""){
                     query = query + "WHERE D.name    LIKE '%" + term + "%' " +
                                     "OR    A.id      LIKE '%" + term + "%' " +
                                     "OR    A.drillBoxActivity LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillBoxActivityType, Account, DrillBoxActivityType>(
                    sql: query,
                    map: (drillBoxActivityType, account) => {
                        drillBoxActivityType.Account = account;
                        return drillBoxActivityType;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<DrillBoxActivityType>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillBoxActivityType>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT D.*, 'split', A.*
                                FROM DRILLBOXACTIVITYTYPE D 
                                INNER JOIN Account A ON D.accountId = A.id  
                                WHERE A.id = @accountId "; 
                if (term != ""){
                     query = query + "AND (D.name LIKE '%"    + term + "%' " +
                                     "OR   A.id      LIKE '%" + term + "%' " +
                                     "OR   A.drillBoxActivity LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillBoxActivityType, Account, DrillBoxActivityType>(
                    sql: query,
                    map: (drillBoxActivityType, account) => {
                        drillBoxActivityType.Account = account;
                        return drillBoxActivityType;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<DrillBoxActivityType>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DrillBoxActivityType> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT D.*, 'split', A.*
                                FROM DRILLBOXACTIVITYTYPE D 
                                INNER JOIN Account A ON D.accountId = A.id
                                WHERE D.ID = @id";
                var res =  await conn.QueryAsync<DrillBoxActivityType, Account, DrillBoxActivityType>(
                    sql: query,
                    map: (drillBoxActivityType, account) => {
                        drillBoxActivityType.Account = account;
                        return drillBoxActivityType;
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