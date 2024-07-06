using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using System.Linq;

namespace GeoCloudAI.Persistence.Repositories
{
    public class DrillHoleTypeRepository: IDrillHoleTypeRepository
    {
        private DbSession _db;

        public DrillHoleTypeRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(DrillHoleType drillHoleType)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (drillHoleType.AccountId == 0) { return 0; }
                    string command = @"INSERT INTO DRILLHOLETYPE(accountId, name, diameter)
                                        VALUES(@accountId, @name, @diameter); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: drillHoleType);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(DrillHoleType drillHoleType)
        {
            try
            {
                var conn = _db.Connection;
                if (drillHoleType.AccountId == 0) { return 0; }
                string command = @"UPDATE DRILLHOLETYPE SET 
                                    accountId = @accountId,
                                    name      = @name,
                                    diameter  = @diameter
                                    WHERE id  = @id";
                var result = await conn.ExecuteAsync(sql: command, param: drillHoleType);
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
                string command = @"DELETE FROM DRILLHOLETYPE WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        public async Task<PageList<DrillHoleType>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT D.*, 'split', A.*
                                FROM DRILLHOLETYPE D 
                                INNER JOIN Account A ON D.accountId = A.id ";
                if (term != ""){
                     query = query + "WHERE D.name     LIKE '%" + term + "%' " +
                                     "OR    D.diameter LIKE '%" + term + "%' " +
                                     "OR    A.id       LIKE '%" + term + "%' " +
                                     "OR    A.company  LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillHoleType, Account, DrillHoleType>(
                    sql: query,
                    map: (drillHoleType, account) => {
                        drillHoleType.Account = account;
                        return drillHoleType;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<DrillHoleType>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DrillHoleType>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT D.*, 'split', A.*
                                FROM DRILLHOLETYPE D 
                                INNER JOIN Account A ON D.accountId = A.id  
                                WHERE A.id = @accountId "; 
                if (term != ""){
                     query = query + "AND (D.name     LIKE '%" + term + "%' " +
                                     "OR   D.diameter LIKE '%" + term + "%' " +
                                     "OR   A.id       LIKE '%" + term + "%' " +
                                     "OR   A.company  LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<DrillHoleType, Account, DrillHoleType>(
                    sql: query,
                    map: (drillHoleType, account) => {
                        drillHoleType.Account = account;
                        return drillHoleType;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<DrillHoleType>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DrillHoleType> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT D.*, 'split', A.*
                                FROM DRILLHOLETYPE D 
                                INNER JOIN Account A ON D.accountId = A.id
                                WHERE D.ID = @id";
                var res =  await conn.QueryAsync<DrillHoleType, Account, DrillHoleType>(
                    sql: query,
                    map: (drillHoleType, account) => {
                        drillHoleType.Account = account;
                        return drillHoleType;
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