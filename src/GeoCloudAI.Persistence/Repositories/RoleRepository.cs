using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using System.Linq;

namespace GeoCloudAI.Persistence.Repositories
{
    public class RoleRepository: IRoleRepository
    {
        private DbSession _db;

        public RoleRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(Role role)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (role.AccountId == 0) { return 0; }
                    string command = @"INSERT INTO ROLE(accountId, name, imgType)
                                        VALUES(@accountId, @name, @imgType); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: role);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(Role role)
        {
            try
            {
                var conn = _db.Connection;
                if (role.AccountId == 0) { return 0; }
                string command = @"UPDATE ROLE SET 
                                    accountId = @accountId,
                                    name      = @name,
                                    imgType   = @imgType
                                    WHERE id  = @id";
                var result = await conn.ExecuteAsync(sql: command, param: role);
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
                string command = @"DELETE FROM ROLE WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        public async Task<PageList<Role>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT R.*, 'split', A.*
                                FROM ROLE R 
                                INNER JOIN Account A ON R.accountId = A.id ";
                if (term != ""){
                     query = query + "WHERE R.name    LIKE '%" + term + "%' " +
                                     "OR    A.id      LIKE '%" + term + "%' " +
                                     "OR    A.company LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Role, Account, Role>(
                    sql: query,
                    map: (role, account) => {
                        role.Account = account;
                        return role;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<Role>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Role>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT R.*, 'split', A.*
                                FROM ROLE R 
                                INNER JOIN Account A ON R.accountId = A.id  
                                WHERE A.id = @accountId "; 
                if (term != ""){
                     query = query + "AND (R.name    LIKE '%"    + term + "%' " +
                                     "OR   A.id      LIKE '%" + term + "%' " +
                                     "OR   A.company LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Role, Account, Role>(
                    sql: query,
                    map: (role, account) => {
                        role.Account = account;
                        return role;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<Role>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Role> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT R.*, 'split', A.*
                                FROM ROLE R 
                                INNER JOIN Account A ON R.accountId = A.id
                                WHERE R.ID = @id";
                var res =  await conn.QueryAsync<Role, Account, Role>(
                    sql: query,
                    map: (role, account) => {
                        role.Account = account;
                        return role;
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