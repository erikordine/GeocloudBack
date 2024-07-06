using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using System.Linq;

namespace GeoCloudAI.Persistence.Repositories
{
    public class ProjectTypeRepository: IProjectTypeRepository
    {
        private DbSession _db;

        public ProjectTypeRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(ProjectType projectType)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (projectType.AccountId == 0) { return 0; }
                    string command = @"INSERT INTO PROJECTTYPE(accountId, name, imgType)
                                        VALUES(@accountId, @name, @imgType); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: projectType);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(ProjectType projectType)
        {
            try
            {
                var conn = _db.Connection;
                if (projectType.AccountId == 0) { return 0; }
                string command = @"UPDATE PROJECTTYPE SET 
                                    accountId = @accountId,
                                    name      = @name,
                                    imgType   = @imgType
                                    WHERE id  = @id";
                var result = await conn.ExecuteAsync(sql: command, param: projectType);
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
                string command = @"DELETE FROM PROJECTTYPE WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        public async Task<PageList<ProjectType>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT P.*, 'split', A.*
                                FROM ProjectType P 
                                INNER JOIN Account A ON P.accountId = A.id ";
                if (term != ""){
                     query = query + "WHERE P.name    LIKE '%" + term + "%' " +
                                     "OR    A.company LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<ProjectType, Account, ProjectType>(
                    sql: query,
                    map: (projectType, account) => {
                        projectType.Account = account;
                        return projectType;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<ProjectType>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<ProjectType>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT P.*, 'split', A.*
                                FROM ProjectType P 
                                INNER JOIN Account A ON P.accountId = A.id  
                                WHERE A.id = @accountId "; 
                if (term != ""){
                     query = query + "AND (P.name LIKE '%"    + term + "%' " +
                                     "OR   A.company LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<ProjectType, Account, ProjectType>(
                    sql: query,
                    map: (projectType, account) => {
                        projectType.Account = account;
                        return projectType;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<ProjectType>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProjectType> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT P.*, 'split', A.*
                                FROM ProjectType P 
                                INNER JOIN Account A ON P.accountId = A.id
                                WHERE P.ID = @id";
                var res =  await conn.QueryAsync<ProjectType, Account, ProjectType>(
                    sql: query,
                    map: (projectType, account) => {
                        projectType.Account = account;
                        return projectType;
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