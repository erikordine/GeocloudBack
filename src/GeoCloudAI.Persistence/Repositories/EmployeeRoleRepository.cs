using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using System.Linq;

namespace GeoCloudAI.Persistence.Repositories
{
    public class EmployeeRoleRepository: IEmployeeRoleRepository
    {
        private DbSession _db;

        public EmployeeRoleRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(EmployeeRole employeeRole)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (employeeRole.AccountId == 0) { return 0; }
                    string command = @"INSERT INTO EMPLOYEEROLE(accountId, name, imgType)
                                        VALUES(@accountId, @name, @imgType); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: employeeRole);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(EmployeeRole employeeRole)
        {
            try
            {
                var conn = _db.Connection;
                if (employeeRole.AccountId == 0) { return 0; }
                string command = @"UPDATE EMPLOYEEROLE SET 
                                    accountId = @accountId,
                                    name      = @name,
                                    imgType   = @imgType
                                    WHERE id  = @id";
                var result = await conn.ExecuteAsync(sql: command, param: employeeRole);
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
                string command = @"DELETE FROM EMPLOYEEROLE WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        public async Task<PageList<EmployeeRole>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT D.*, 'split', A.*
                                FROM EMPLOYEEROLE D 
                                INNER JOIN Account A ON D.accountId = A.id ";
                if (term != ""){
                     query = query + "WHERE D.name    LIKE '%" + term + "%' " +
                                     "OR    A.id      LIKE '%" + term + "%' " +
                                     "OR    A.company LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<EmployeeRole, Account, EmployeeRole>(
                    sql: query,
                    map: (employeeRole, account) => {
                        employeeRole.Account = account;
                        return employeeRole;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<EmployeeRole>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<EmployeeRole>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT D.*, 'split', A.*
                                FROM EMPLOYEEROLE D 
                                INNER JOIN Account A ON D.accountId = A.id  
                                WHERE A.id = @accountId "; 
                if (term != ""){
                     query = query + "AND (D.name LIKE '%"    + term + "%' " +
                                     "OR   A.id      LIKE '%" + term + "%' " +
                                     "OR   A.company LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<EmployeeRole, Account, EmployeeRole>(
                    sql: query,
                    map: (employeeRole, account) => {
                        employeeRole.Account = account;
                        return employeeRole;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<EmployeeRole>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeRole> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT D.*, 'split', A.*
                                FROM EMPLOYEEROLE D 
                                INNER JOIN Account A ON D.accountId = A.id
                                WHERE D.ID = @id";
                var res =  await conn.QueryAsync<EmployeeRole, Account, EmployeeRole>(
                    sql: query,
                    map: (employeeRole, account) => {
                        employeeRole.Account = account;
                        return employeeRole;
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