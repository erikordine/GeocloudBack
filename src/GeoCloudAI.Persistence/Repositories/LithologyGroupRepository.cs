using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using System.Linq;

namespace GeoCloudAI.Persistence.Repositories
{
    public class LithologyGroupRepository: ILithologyGroupRepository
    {
        private DbSession _db;

        public LithologyGroupRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(LithologyGroup lithologyGroup)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (lithologyGroup.AccountId == 0) { return 0; }
                    string command = @"INSERT INTO LITHOLOGYGROUP(accountId, name)
                                        VALUES(@accountId, @name); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: lithologyGroup);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(LithologyGroup lithologyGroup)
        {
            try
            {
                var conn = _db.Connection;
                if (lithologyGroup.AccountId == 0) { return 0; }
                string command = @"UPDATE LITHOLOGYGROUP SET 
                                    accountId = @accountId,
                                    name      = @name,
                                    WHERE id  = @id";
                var result = await conn.ExecuteAsync(sql: command, param: lithologyGroup);
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
                string command = @"DELETE FROM LITHOLOGYGROUP WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        public async Task<PageList<LithologyGroup>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT LG.*, 'split', A.*
                                FROM LITHOLOGYGROUP LG 
                                INNER JOIN Account A ON LG.accountId = A.id ";
                if (term != ""){
                     query = query + "WHERE LG.name   LIKE '%" + term + "%' " +
                                     "OR    A.id      LIKE '%" + term + "%' " +
                                     "OR    A.company LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<LithologyGroup, Account, LithologyGroup>(
                    sql: query,
                    map: (lithologyGroup, account) => {
                        lithologyGroup.Account = account;
                        return lithologyGroup;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<LithologyGroup>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<LithologyGroup>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT LG.*, 'split', A.*
                                FROM LITHOLOGYGROUP LG 
                                INNER JOIN Account A ON LG.accountId = A.id  
                                WHERE A.id = @accountId "; 
                if (term != ""){
                     query = query + "AND (LG.name LIKE '%"    + term + "%' " +
                                     "OR   A.id      LIKE '%" + term + "%' " +
                                     "OR   A.company LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<LithologyGroup, Account, LithologyGroup>(
                    sql: query,
                    map: (lithologyGroup, account) => {
                        lithologyGroup.Account = account;
                        return lithologyGroup;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<LithologyGroup>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LithologyGroup> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT LG.*, 'split', A.*
                                FROM LITHOLOGYGROUP LG 
                                INNER JOIN Account A ON LG.accountId = A.id
                                WHERE LG.ID = @id";
                var res =  await conn.QueryAsync<LithologyGroup, Account, LithologyGroup>(
                    sql: query,
                    map: (lithologyGroup, account) => {
                        lithologyGroup.Account = account;
                        return lithologyGroup;
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