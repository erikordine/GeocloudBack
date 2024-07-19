using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using System.Linq;

namespace GeoCloudAI.Persistence.Repositories
{
    public class LithologyMethodRepository: ILithologyMethodRepository
    {
        private DbSession _db;

        public LithologyMethodRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(LithologyMethod lithologyMethod)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (lithologyMethod.AccountId == 0) { return 0; }
                    string command = @"INSERT INTO LITHOLOGYMETHOD(accountId, name)
                                        VALUES(@accountId, @name); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: lithologyMethod);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(LithologyMethod lithologyMethod)
        {
            try
            {
                var conn = _db.Connection;
                if (lithologyMethod.AccountId == 0) { return 0; }
                string command = @"UPDATE LITHOLOGYMETHOD SET 
                                    accountId = @accountId,
                                    name      = @name
                                    WHERE id  = @id";
                var result = await conn.ExecuteAsync(sql: command, param: lithologyMethod);
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
                string command = @"DELETE FROM LITHOLOGYMETHOD WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        public async Task<PageList<LithologyMethod>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT LM.*, 'split', A.*
                                FROM LITHOLOGYMETHOD LM 
                                INNER JOIN Account A ON LM.accountId = A.id ";
                if (term != ""){
                     query = query + "WHERE LM.name   LIKE '%" + term + "%' " +
                                     "OR    A.id      LIKE '%" + term + "%' " +
                                     "OR    A.company LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<LithologyMethod, Account, LithologyMethod>(
                    sql: query,
                    map: (lithologyMethod, account) => {
                        lithologyMethod.Account = account;
                        return lithologyMethod;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<LithologyMethod>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<LithologyMethod>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT LM.*, 'split', A.*
                                FROM LITHOLOGYMETHOD LM 
                                INNER JOIN Account A ON LM.accountId = A.id  
                                WHERE A.id = @accountId "; 
                if (term != ""){
                     query = query + "AND (LM.name   LIKE '%"    + term + "%' " +
                                     "OR   A.id      LIKE '%" + term + "%' " +
                                     "OR   A.company LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<LithologyMethod, Account, LithologyMethod>(
                    sql: query,
                    map: (lithologyMethod, account) => {
                        lithologyMethod.Account = account;
                        return lithologyMethod;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<LithologyMethod>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LithologyMethod> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT LM.*, 'split', A.*
                                FROM LITHOLOGYMETHOD LM 
                                INNER JOIN Account A ON LM.accountId = A.id
                                WHERE LM.ID = @id";
                var res =  await conn.QueryAsync<LithologyMethod, Account, LithologyMethod>(
                    sql: query,
                    map: (lithologyMethod, account) => {
                        lithologyMethod.Account = account;
                        return lithologyMethod;
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