using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Repositories
{
    public class CompanyRepository: ICompanyRepository
    {
        private DbSession _db;
        
        public CompanyRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(Company company)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    //Required
                    if (company.AccountId == 0) { return 0; }
                    if (company.UserId    == 0) { return 0; }
                    //Not Required
                    var typeId = "null";
                    if (company.TypeId > 0) { 
                        typeId = company.TypeId.ToString(); }
                    string command = @"INSERT INTO COMPANY(   
                                            accountId, name, typeId, comments, imgTypeProfile, imgTypeCover, userId, register) 
                                        VALUES(@accountId, @name, " + typeId + ", " + 
                                            "@comments, @imgTypeProfile, @imgTypeCover, @userId, @register ); " +
                                        "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: company);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(Company company)
        {
            try
            {
                var conn = _db.Connection;
                //Required
                if (company.AccountId == 0) { return 0; }
                if (company.UserId    == 0) { return 0; }
                //Not Required
                var typeId = "null";
                if (company.TypeId > 0) { 
                    typeId = company.TypeId.ToString(); }
                string command = @"UPDATE COMPANY SET 
                                    accountId           = @accountId,
                                    name                = @name, 
                                    typeId              = " + typeId + @",
                                    comments            = @comments, 
                                    imgTypeProfile      = @imgTypeProfile,
                                    imgTypeCover        = @imgTypeCover  
                                    WHERE id            = @id";
                var result = await conn.ExecuteAsync(sql: command, param: company);
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
                string command = @"DELETE FROM COMPANY WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Company>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT C.*, 'split', A.*, 'split', T.*, 'split', U.*
                                FROM Company C  
                                INNER JOIN Account     A   ON C.AccountId = A.Id
                                LEFT  JOIN CompanyType T   ON C.TypeId    = T.Id
                                INNER JOIN User        U   ON C.UserId    = U.Id"; 
                if (term != ""){
                    query = query + "WHERE C.Name LIKE '%" + term + "%' " +
                                    "OR    T.Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Company>(
                    query, 
                    new[] {
                        typeof(Company),
                        typeof(Account),
                        typeof(CompanyType),
                        typeof(User),
                    },
                    objects => {
                        Company     company = objects[0] as Company;
                        Account     account = objects[1] as Account;
                        CompanyType type    = objects[2] as CompanyType;
                        User        user    = objects[3] as User;
                        //Dependency required
                        company.Account = account;
                        company.User    = user;
                        //Dependency not required
                        if (type.Id > 0) { company.Type = type; }
                        //Return
                        return company;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<Company>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Company>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT C.*, 'split', A.*, 'split', T.*, 'split', U.*
                                FROM Company C  
                                INNER JOIN Account     A   ON C.AccountId = A.Id
                                LEFT  JOIN CompanyType T   ON C.TypeId    = T.Id
                                INNER JOIN User        U   ON C.UserId    = U.Id
                                WHERE C.AccountId = @accountId "; 
                if (term != ""){
                    query = query + "AND (C.Name LIKE '%" + term + "%' " +
                                    "OR   T.Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Company>(
                    query, 
                    new[] {
                        typeof(Company),
                        typeof(Account),
                        typeof(CompanyType),
                        typeof(User),
                    },
                    objects => {
                        Company     company = objects[0] as Company;
                        Account     account = objects[1] as Account;
                        CompanyType type    = objects[2] as CompanyType;
                        User        user    = objects[3] as User;
                        //Dependency required
                        company.Account = account;
                        company.User    = user;
                        //Dependency not required
                        if (type.Id > 0) { company.Type = type; }
                        //Return
                        return company;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<Company>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Company> GetById(int id)
        {
            try
            {
                var conn = _db.Connection; 
                string query = @"SELECT C.*, 'split', A.*, 'split', T.*, 'split', U.*
                                FROM Company C  
                                INNER JOIN Account     A   ON C.AccountId = A.Id
                                LEFT  JOIN CompanyType T   ON C.TypeId    = T.Id
                                INNER JOIN User        U   ON C.UserId    = U.Id
                                WHERE C.Id = @id";
                var res = await conn.QueryAsync<Company>(
                    query, 
                    new[] {
                        typeof(Company),
                        typeof(Account),
                        typeof(CompanyType),
                        typeof(User),
                    },
                    objects => {
                        Company     company = objects[0] as Company;
                        Account     account = objects[1] as Account;
                        CompanyType type    = objects[2] as CompanyType;
                        User        user    = objects[3] as User;
                        //Dependency required
                        company.Account = account;
                        company.User    = user;
                        //Dependency not required
                        if (type.Id > 0) { company.Type = type; }
                        //Return
                        return company;
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