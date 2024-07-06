using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Repositories
{
    public class ProjectStatusRepository: IProjectStatusRepository
    {
        private DbSession _db;

        public ProjectStatusRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(ProjectStatus projectStatus)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (projectStatus.AccountId == 0) { return 0; }
                    string command = @"INSERT INTO PROJECTSTATUS(accountId, name, imgType)
                                        VALUES(@accountId, @name, @imgType); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: projectStatus);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(ProjectStatus projectStatus)
        {
            try
            {
                var conn = _db.Connection;
                if (projectStatus.AccountId == 0) { return 0; }
                string command = @"UPDATE PROJECTSTATUS SET 
                                    accountId = @accountId,
                                    name      = @name,
                                    imgType   = @imgType
                                    WHERE id  = @id";
                var result = await conn.ExecuteAsync(sql: command, param: projectStatus);
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
                string command = @"DELETE FROM PROJECTSTATUS WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        public async Task<PageList<ProjectStatus>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT P.*, 'split', A.*
                                FROM ProjectStatus P 
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
                var res = await conn.QueryAsync<ProjectStatus, Account, ProjectStatus>(
                    sql: query,
                    map: (projectStatus, account) => {
                        projectStatus.Account = account;
                        return projectStatus;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<ProjectStatus>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<ProjectStatus>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT P.*, 'split', A.*
                                FROM ProjectStatus P 
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
                var res = await conn.QueryAsync<ProjectStatus, Account, ProjectStatus>(
                    sql: query,
                    map: (projectStatus, account) => {
                        projectStatus.Account = account;
                        return projectStatus;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<ProjectStatus>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProjectStatus> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT P.*, 'split', A.*
                                FROM ProjectStatus P 
                                INNER JOIN Account A ON P.accountId = A.id
                                WHERE P.ID = @id";
                var res =  await conn.QueryAsync<ProjectStatus, Account, ProjectStatus>(
                    sql: query,
                    map: (projectStatus, account) => {
                        projectStatus.Account = account;
                        return projectStatus;
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