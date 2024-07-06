using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using System.Linq;

namespace GeoCloudAI.Persistence.Repositories
{
    public class CoreShedRepository: ICoreShedRepository
    {
        private DbSession _db;

        public CoreShedRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(CoreShed coreShed)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (coreShed.AccountId == 0) { return 0; }
                    string command = @"INSERT INTO CORESHED(accountId, name, imgType)
                                        VALUES(@accountId, @name, @imgType); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: coreShed);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(CoreShed coreShed)
        {
            try
            {
                var conn = _db.Connection;
                if (coreShed.AccountId == 0) { return 0; }
                string command = @"UPDATE CORESHED SET 
                                    accountId = @accountId,
                                    name      = @name,
                                    imgType   = @imgType
                                    WHERE id  = @id";
                var result = await conn.ExecuteAsync(sql: command, param: coreShed);
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
                string command = @"DELETE FROM CORESHED WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        public async Task<PageList<CoreShed>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT C.*, 'split', A.*
                                FROM CORESHED C 
                                INNER JOIN Account A ON C.accountId = A.id ";
                if (term != ""){
                     query = query + "WHERE C.name    LIKE '%" + term + "%' " +
                                     "OR    A.id      LIKE '%" + term + "%' " +
                                     "OR    A.company LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<CoreShed, Account, CoreShed>(
                    sql: query,
                    map: (coreShed, account) => {
                        coreShed.Account = account;
                        return coreShed;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<CoreShed>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<CoreShed>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT C.*, 'split', A.*
                                FROM CORESHED C 
                                INNER JOIN Account A ON C.accountId = A.id  
                                WHERE A.id = @accountId "; 
                if (term != ""){
                     query = query + "AND (C.name    LIKE '%"    + term + "%' " +
                                     "OR   A.id      LIKE '%" + term + "%' " +
                                     "OR   A.company LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<CoreShed, Account, CoreShed>(
                    sql: query,
                    map: (coreShed, account) => {
                        coreShed.Account = account;
                        return coreShed;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<CoreShed>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CoreShed> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT C.*, 'split', A.*
                                FROM CORESHED C 
                                INNER JOIN Account A ON C.accountId = A.id
                                WHERE C.ID = @id";
                var res =  await conn.QueryAsync<CoreShed, Account, CoreShed>(
                    sql: query,
                    map: (coreShed, account) => {
                        coreShed.Account = account;
                        return coreShed;
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