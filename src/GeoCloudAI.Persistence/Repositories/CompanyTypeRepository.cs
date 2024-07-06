using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using System.Linq;

namespace GeoCloudAI.Persistence.Repositories
{
    public class CompanyTypeRepository: ICompanyTypeRepository
    {
        private DbSession _db;

        public CompanyTypeRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(CompanyType companyType)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (companyType.AccountId == 0) { return 0; }
                    string command = @"INSERT INTO COMPANYTYPE(accountId, name, imgType)
                                        VALUES(@accountId, @name, @imgType); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: companyType);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(CompanyType companyType)
        {
            try
            {
                var conn = _db.Connection;
                if (companyType.AccountId == 0) { return 0; }
                string command = @"UPDATE COMPANYTYPE SET 
                                    accountId = @accountId,
                                    name      = @name,
                                    imgType   = @imgType
                                    WHERE id  = @id";
                var result = await conn.ExecuteAsync(sql: command, param: companyType);
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
                string command = @"DELETE FROM COMPANYTYPE WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        public async Task<PageList<CompanyType>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT C.*, 'split', A.*
                                FROM COMPANYTYPE C 
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
                var res = await conn.QueryAsync<CompanyType, Account, CompanyType>(
                    sql: query,
                    map: (companyType, account) => {
                        companyType.Account = account;
                        return companyType;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<CompanyType>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<CompanyType>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT C.*, 'split', A.*
                                FROM COMPANYTYPE C 
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
                var res = await conn.QueryAsync<CompanyType, Account, CompanyType>(
                    sql: query,
                    map: (companyType, account) => {
                        companyType.Account = account;
                        return companyType;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<CompanyType>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CompanyType> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT C.*, 'split', A.*
                                FROM COMPANYTYPE C 
                                INNER JOIN Account A ON C.accountId = A.id
                                WHERE C.ID = @id";
                var res =  await conn.QueryAsync<CompanyType, Account, CompanyType>(
                    sql: query,
                    map: (companyType, account) => {
                        companyType.Account = account;
                        return companyType;
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