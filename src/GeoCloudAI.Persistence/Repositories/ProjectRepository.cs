using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using System.Linq;

namespace GeoCloudAI.Persistence.Repositories
{
    public class ProjectRepository: IProjectRepository
    {
        private DbSession _db;

        public ProjectRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(Project project)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    //Required
                    if (project.AccountId == 0) { return 0; }
                    if (project.UserId    == 0) { return 0; }
                    //Not Required
                    var typeId = "null";
                    if (project.TypeId > 0) { 
                        typeId = project.TypeId.ToString(); }
                    var statusId = "null";
                    if (project.StatusId > 0) { 
                        statusId = project.StatusId.ToString(); }
                    string command = @"INSERT INTO PROJECT(accountId, name, startDate, endDate, summary, 
                                                           comments, typeId, statusId, userId, register)
                                        VALUES(@accountId, @name, @startDate, @endDate, @summary, @comments," + 
                                               typeId + ", " + statusId + ", @userId, @register); " +
                                       "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: project);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(Project project)
        {
            try
            {
                var conn = _db.Connection;
                //Required
                if (project.AccountId == 0) { return 0; }
                if (project.UserId    == 0) { return 0; }
                //Not Required
                var typeId = "null";
                if (project.TypeId > 0) { 
                    typeId = project.TypeId.ToString(); }
                var statusId = "null";
                if (project.StatusId > 0) { 
                    statusId = project.StatusId.ToString(); }
                string command = @"UPDATE PROJECT SET 
                                    accountId       = @accountId, 
                                    name            = @name, 
                                    startDate       = @startDate, 
                                    endDate         = @endDate, 
                                    summary         = @summary, 
                                    comments        = @comments,
                                    typeId          = " + typeId + @", 
                                    statusId        = " + statusId + @" 
                                    WHERE id        = @id ";
                var result = await conn.ExecuteAsync(sql: command, param: project);
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
                string command = @"DELETE FROM PROJECT WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Project>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT P.*, 'split', A.*, 'split', T.*, 'split', S.*, 'split', U.*
                                FROM Project P  
                                INNER JOIN Account A       ON P.AccountId = A.id
                                LEFT  JOIN ProjectType T   ON P.TypeId    = T.id
                                LEFT  JOIN ProjectStatus S ON P.StatusId  = S.id
                                INNER JOIN User U          ON P.UserId    = U.id ";
                if (term != ""){
                    query = query + "WHERE P.Name LIKE '%" + term + "%' " +
                                    "OR    T.Name LIKE '%" + term + "%' " +
                                    "OR    S.Name LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Project>(
                    query, 
                    new[] {
                        typeof(Project),
                        typeof(Account),
                        typeof(ProjectType),
                        typeof(ProjectStatus),
                        typeof(User),
                    },
                    objects => {
                        Project       project = objects[0] as Project;
                        Account       account = objects[1] as Account;
                        ProjectType   type    = objects[2] as ProjectType;
                        ProjectStatus status  = objects[3] as ProjectStatus;
                        User          user    = objects[4] as User;
                        //Dependency required
                        project.Account = account;
                        project.User    = user;
                        //Dependency not required
                        if (type.Id > 0)   { project.Type  = type; }
                        if (status.Id > 0) { project.Status = status; }
                        //Return
                        return project;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<Project>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Project>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT P.*, 'split', A.*, 'split', T.*, 'split', S.*, 'split', U.*
                                FROM Project P  
                                INNER JOIN Account A       ON P.AccountId = A.id
                                LEFT  JOIN ProjectType T   ON P.TypeId    = T.id
                                LEFT  JOIN ProjectStatus S ON P.StatusId  = S.id
                                INNER JOIN User U          ON P.UserId    = U.id 
                                WHERE A.id = @accountId "; 
                if (term != ""){
                    query = query + "AND  (P.Name LIKE '%" + term + "%' " +
                                    "OR    T.Name LIKE '%" + term + "%' " +
                                    "OR    S.Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Project>(
                    query, 
                    new[] {
                        typeof(Project),
                        typeof(Account),
                        typeof(ProjectType),
                        typeof(ProjectStatus),
                        typeof(User),
                    },
                    objects => {
                        Project       project = objects[0] as Project;
                        Account       account = objects[1] as Account;
                        ProjectType   type    = objects[2] as ProjectType;
                        ProjectStatus status  = objects[3] as ProjectStatus;
                        User          user    = objects[4] as User;
                        //Dependency required
                        project.Account = account;
                        project.User    = user;
                        //Dependency not required
                        if (type.Id > 0)   { project.Type  = type; }
                        if (status.Id > 0) { project.Status = status; }
                        //Return
                        return project;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<Project>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Project> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT P.*, 'split', A.*, 'split', T.*, 'split', S.*, 'split', U.*
                                FROM Project P  
                                INNER JOIN Account A       ON P.AccountId = A.id
                                LEFT  JOIN ProjectType T   ON P.TypeId    = T.id
                                LEFT  JOIN ProjectStatus S ON P.StatusId  = S.id
                                INNER JOIN User U          ON P.UserId    = U.id
                                WHERE P.ID = @id";
                var res = await conn.QueryAsync<Project>(
                    query, 
                    new[] {
                        typeof(Project),
                        typeof(Account),
                        typeof(ProjectType),
                        typeof(ProjectStatus),
                        typeof(User),
                    },
                    objects => {
                        Project       project = objects[0] as Project;
                        Account       account = objects[1] as Account;
                        ProjectType   type    = objects[2] as ProjectType;
                        ProjectStatus status  = objects[3] as ProjectStatus;
                        User          user    = objects[4] as User;
                        //Dependency required
                        project.Account = account;
                        project.User    = user;
                        //Dependency not required
                        if (type.Id > 0)   { project.Type  = type; }
                        if (status.Id > 0) { project.Status = status; }
                        //Return
                        return project;
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