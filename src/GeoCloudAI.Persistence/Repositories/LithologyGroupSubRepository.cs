using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Repositories
{
    public class LithologyGroupSubRepository: ILithologyGroupSubRepository
    {
        private DbSession _db;

        public LithologyGroupSubRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(LithologyGroupSub lithologyGroupSub)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (lithologyGroupSub.GroupId == 0) { return 0; }
                    string command = @"INSERT INTO LITHOLOGYGROUPSUB(groupId, name)
                                        VALUES(@groupId, @name); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: lithologyGroupSub);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(LithologyGroupSub lithologyGroupSub)
        {
            try
            {
                var conn = _db.Connection;
                if (lithologyGroupSub.GroupId == 0) { return 0; }
                string command = @"UPDATE LITHOLOGYGROUPSUB SET 
                                    groupId       = @groupId,
                                    name          = @name
                                    WHERE id      = @id";
                var result = await conn.ExecuteAsync(sql: command, param: lithologyGroupSub);
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
                string command = @"DELETE FROM LITHOLOGYGROUPSUB WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<LithologyGroupSub>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT LS.*, 'split', G.*, 'split', A.*
                                FROM LithologyGroupSub LS 
                                INNER JOIN LithologyGroup   G        ON LS.groupId      = G.id 
                                INNER JOIN Account          A        ON G.AccountId     = A.id ";
                if (term != ""){
                     query = query + "WHERE LS.name LIKE '%" + term + "%' " +
                                     "OR    G.Name LIKE  '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<LithologyGroupSub>(
                    query, 
                    new[] {
                        typeof(LithologyGroupSub),
                        typeof(LithologyGroup),
                        typeof(Account)
                    },
                    objects => {
                        LithologyGroupSub lithologyGroupSub = objects[0] as LithologyGroupSub;
                        LithologyGroup    group             = objects[1] as LithologyGroup;
                        Account           account           = objects[2] as Account;
                        //Dependency required
                        group.Account           = account;
                        lithologyGroupSub.Group = group;
                        //Return
                        return lithologyGroupSub;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<LithologyGroupSub>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<LithologyGroupSub>> GetByAccount(int accountId,PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT LS.*, 'split', G.*, 'split', A.*
                                FROM LithologyGroupSub LS 
                                INNER JOIN LithologyGroup    G   ON    LS.groupId   = G.id 
                                INNER JOIN Account           A   ON    G.AccountId  = A.id 
                                WHERE A.id= @accountId "; 
                if (term != ""){
                     query = query + "AND (O.name LIKE '%" + term + "%' " +
                                     "OR   D.Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<LithologyGroupSub>(
                    query, 
                    new[] {
                        typeof(LithologyGroupSub),
                        typeof(LithologyGroup),
                        typeof(Account)
                    },
                    objects => {
                        LithologyGroupSub lithologyGroupSub = objects[0] as LithologyGroupSub;
                        LithologyGroup    group             = objects[1] as LithologyGroup;
                        Account           account           = objects[2] as Account;
                        //Dependency required
                        group.Account        = account;
                        lithologyGroupSub.Group = group;
                        //Return
                        return lithologyGroupSub;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<LithologyGroupSub>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<LithologyGroupSub>> GetByLithologyGroup(int groupId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT LS.*, 'split', G.*, 'split', A.*
                                FROM LithologyGroupSub LS 
                                INNER JOIN LithologyGroup    G   ON    LS.groupId   = G.id 
                                INNER JOIN Account           A   ON    G.AccountId  = A.id 
                                WHERE G.id = @groupId "; 
                if (term != ""){
                     query = query + "AND (LS.name LIKE '%" + term + "%' " +
                                     "OR   G .Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<LithologyGroupSub>(
                    query, 
                    new[] {
                        typeof(LithologyGroupSub),
                        typeof(LithologyGroup),
                        typeof(Account)
                    },
                    objects => {
                        LithologyGroupSub lithologyGroupSub = objects[0] as LithologyGroupSub;
                        LithologyGroup    group             = objects[1] as LithologyGroup;
                        Account           account           = objects[2] as Account;
                        //Dependency required
                        group.Account        = account;
                        lithologyGroupSub.Group = group;
                        //Return
                        return lithologyGroupSub;
                    },
                    splitOn: "split",
                    param: new { groupId });
                return await PageList<LithologyGroupSub>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LithologyGroupSub> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT LS.*, 'split', G.*, 'split', A.*
                                FROM LithologyGroupSub LS 
                                INNER JOIN LithologyGroup    G   ON   LS.groupId      = G.id 
                                INNER JOIN Account           A   ON   G.AccountId     = A.id 
                                WHERE LS.ID = @id";
                var res = await conn.QueryAsync<LithologyGroupSub>(
                    query, 
                    new[] {
                        typeof(LithologyGroupSub),
                        typeof(LithologyGroup),
                        typeof(Account)
                    },
                    objects => {
                        LithologyGroupSub lithologyGroupSub = objects[0] as LithologyGroupSub;
                        LithologyGroup    group             = objects[1] as LithologyGroup;
                        Account           account           = objects[2] as Account;
                        //Dependency required
                        group.Account        = account;
                        lithologyGroupSub.Group = group;
                        //Return
                        return lithologyGroupSub;
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