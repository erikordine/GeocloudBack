using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private DbSession _db;
        
        public EmployeeRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(Employee employee)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    //Required
                    if (employee.CompanyId == 0) { return 0; }
                    if (employee.UserId    == 0) { return 0; }
                    //Not Required
                    var roleId = "null";
                    if (employee.RoleId > 0) { 
                        roleId = employee.RoleId.ToString(); }
                    string command = @"INSERT INTO EMPLOYEE(   
                                            companyId, name, roleId, imgType, userId, register) 
                                        VALUES(@companyId, @name, " + roleId + ", " + 
                                            "@imgType, @userId, @register ); " +
                                        "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: employee);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(Employee employee)
        {
            try
            {
                var conn = _db.Connection;
                //Required
                if (employee.CompanyId == 0) { return 0; }
                if (employee.UserId    == 0) { return 0; }
                //Not Required
                var roleId = "null";
                if (employee.RoleId > 0) { 
                    roleId = employee.RoleId.ToString(); }
                string command = @"UPDATE EMPLOYEE SET 
                                    companyId = @companyId,
                                    name      = @name, 
                                    roleId    = " + roleId + @", 
                                    imgType   = @imgType  
                                    WHERE id  = @id";
                var result = await conn.ExecuteAsync(sql: command, param: employee);
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
                string command = @"DELETE FROM EMPLOYEE WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Employee>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT E.*, 'split', C.*, 'split', R.*, 'split', U.*
                                FROM Employee E  
                                INNER JOIN Company      C   ON E.CompanyId = C.Id
                                LEFT  JOIN EmployeeRole R   ON E.RoleId    = R.Id
                                INNER JOIN User         U   ON E.UserId    = U.Id"; 
                if (term != ""){
                    query = query + "WHERE E.Name LIKE '%" + term + "%' " +
                                    "OR    C.Name LIKE '%" + term + "%' " +
                                    "OR    R.Name LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Employee>(
                    query, 
                    new[] {
                        typeof(Employee),
                        typeof(Company),
                        typeof(EmployeeRole),
                        typeof(User),
                    },
                    objects => {
                        Employee     employee = objects[0] as Employee;
                        Company      company  = objects[1] as Company;
                        EmployeeRole role     = objects[2] as EmployeeRole;
                        User         user     = objects[3] as User;
                        //Dependency required
                        employee.Company = company;
                        employee.User    = user;
                        //Dependency not required
                        if (role.Id > 0) { employee.Role = role; }
                        //Return
                        return employee;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<Employee>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Employee>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT E.*, 'split', C.*, 'split', R.*, 'split', U.*
                                FROM Employee E  
                                INNER JOIN Company      C   ON E.CompanyId = C.Id
                                LEFT  JOIN EmployeeRole R   ON E.RoleId    = R.Id
                                INNER JOIN User         U   ON E.UserId    = U.Id
                                WHERE C.AccountId = @accountId "; 
                if (term != ""){
                    query = query + "AND (E.Name LIKE '%" + term + "%' " +
                                    "OR   C.Name LIKE '%" + term + "%' " +
                                    "OR   R.Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Employee>(
                    query, 
                    new[] {
                        typeof(Employee),
                        typeof(Company),
                        typeof(EmployeeRole),
                        typeof(User),
                    },
                    objects => {
                        Employee     employee = objects[0] as Employee;
                        Company      company  = objects[1] as Company;
                        EmployeeRole role     = objects[2] as EmployeeRole;
                        User         user     = objects[3] as User;
                        //Dependency required
                        employee.Company = company;
                        employee.User    = user;
                        //Dependency not required
                        if (role.Id > 0) { employee.Role = role; }
                        //Return
                        return employee;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<Employee>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Employee>> GetByCompany(int companyId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT E.*, 'split', C.*, 'split', R.*, 'split', U.*
                                FROM Employee E  
                                INNER JOIN Company      C   ON E.CompanyId = C.Id
                                LEFT  JOIN EmployeeRole R   ON E.RoleId    = R.Id
                                INNER JOIN User         U   ON E.UserId    = U.Id
                                WHERE C.Id = @companyId "; 
                if (term != ""){
                    query = query + "AND (E.Name LIKE '%" + term + "%' " +
                                    "OR   C.Name LIKE '%" + term + "%' " +
                                    "OR   R.Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Employee>(
                    query, 
                    new[] {
                        typeof(Employee),
                        typeof(Company),
                        typeof(EmployeeRole),
                        typeof(User),
                    },
                    objects => {
                        Employee     employee = objects[0] as Employee;
                        Company      company  = objects[1] as Company;
                        EmployeeRole role     = objects[2] as EmployeeRole;
                        User         user     = objects[3] as User;
                        //Dependency required
                        employee.Company = company;
                        employee.User    = user;
                        //Dependency not required
                        if (role.Id > 0) { employee.Role = role; }
                        //Return
                        return employee;
                    },
                    splitOn: "split",
                    param: new { companyId });
                return await PageList<Employee>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Employee> GetById(int id)
        {
            try
            {
                var conn = _db.Connection; 
                string query = @"SELECT E.*, 'split', C.*, 'split', R.*, 'split', U.*
                                FROM Employee E  
                                INNER JOIN Company      C   ON E.CompanyId = C.Id
                                LEFT  JOIN EmployeeRole R   ON E.RoleId    = R.Id
                                INNER JOIN User         U   ON E.UserId    = U.Id
                                WHERE E.Id = @id";
                var res = await conn.QueryAsync<Employee>(
                    query, 
                    new[] {
                        typeof(Employee),
                        typeof(Company),
                        typeof(EmployeeRole),
                        typeof(User),
                    },
                    objects => {
                        Employee     employee = objects[0] as Employee;
                        Company      company  = objects[1] as Company;
                        EmployeeRole role     = objects[2] as EmployeeRole;
                        User         user     = objects[3] as User;
                        //Dependency required
                        employee.Company = company;
                        employee.User    = user;
                        //Dependency not required
                        if (role.Id > 0) { employee.Role = role; }
                        //Return
                        return employee;
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