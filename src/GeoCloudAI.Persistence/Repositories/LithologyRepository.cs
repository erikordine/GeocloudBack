using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using System.Linq;

namespace GeoCloudAI.Persistence.Repositories
{
    public class LithologyRepository: ILithologyRepository
    {
        private DbSession _db;

        public LithologyRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(Lithology lithology)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (lithology.GroupSubId == 0) { return 0; }
                    string command = @"INSERT INTO LITHOLOGY(groupSubId, name)
                                        VALUES(@groupSubId, @name); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: lithology);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(Lithology lithology)
        {
            try
            {
                var conn = _db.Connection;
                if (lithology.GroupSubId == 0) { return 0; }
                string command = @"UPDATE LITHOLOGY SET 
                                    groupSubId       = @groupSubId,
                                    name             = @name
                                    WHERE id         = @id";
                var result = await conn.ExecuteAsync(sql: command, param: lithology);
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
                string command = @"DELETE FROM LITHOLOGY WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Lithology>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT L.*, 'split', LS.*, 'split', LG.*, 'split', A.*
                                FROM Lithology L 
                                INNER JOIN LithologyGroupSub LS ON L.groupSubId    = LS.id 
                                INNER JOIN LithologyGroup    LG ON LS.groupId      = LG.id 
                                INNER JOIN Account           A  ON LG.accountId    = A.id ";
                if (term != ""){
                     query = query + "WHERE L.name  LIKE '%" + term + "%' " +
                                     "OR    LS.Name LIKE '%" + term + "%' " +
                                     "OR    LG.Name LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Lithology>(
                    query, 
                    new[] {
                        typeof(Lithology),
                        typeof(LithologyGroupSub),
                        typeof(LithologyGroup),
                        typeof(Account)
                    },
                    objects => {
                        Lithology            lithology       = objects[0] as Lithology;
                        LithologyGroupSub    groupSub        = objects[1] as LithologyGroupSub;
                        LithologyGroup       group           = objects[2] as LithologyGroup;
                        Account              account         = objects[3] as Account;
                        //Dependency required
                        group.Account         = account;
                        groupSub.Group        = group;
                        lithology.GroupSub    = groupSub;
                        //Return
                        return lithology;
                    },
                    splitOn: "split",
                    param: new {});
                return await PageList<Lithology>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Lithology>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT L.*, 'split', LS.*, 'split', LG.*, 'split', A.*
                                FROM Lithology L 
                                INNER JOIN LithologyGroupSub LS ON L.groupSubId  = LS.id 
                                INNER JOIN LithologyGroup    LG ON LS.groupId    = LG.id 
                                INNER JOIN Account           A  ON LG.accountId  = A.id 
                                WHERE A.id = @accountId "; 
                if (term != ""){
                     query = query + "AND (L.name  LIKE '%" + term + "%' " +
                                     "OR   LS.Name LIKE '%" + term + "%' " +
                                     "OR   LG.Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Lithology>(
                    query, 
                    new[] {
                        typeof(Lithology),
                        typeof(LithologyGroupSub),
                        typeof(LithologyGroup),
                        typeof(Account)
                    },
                    objects => {
                        Lithology            lithology        = objects[0] as Lithology;
                        LithologyGroupSub    groupSub         = objects[1] as LithologyGroupSub;
                        LithologyGroup       group            = objects[2] as LithologyGroup;
                        Account              account          = objects[3] as Account;
                        //Dependency required
                        group.Account         = account;
                        groupSub.Group        = group;
                        lithology.GroupSub    = groupSub;
                        //Return
                        return lithology;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<Lithology>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Lithology>> GetByLithologyGroupSub(int groupSubId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT L.*, 'split', LS.*, 'split', LG.*, 'split', A.*
                                FROM Lithology L 
                                INNER JOIN LithologyGroupSub LS ON L.groupSubId   = LS.id 
                                INNER JOIN LithologyGroup    LG ON LS.groupId     = LG.id 
                                INNER JOIN Account           A  ON LG.accountId   = A.id 
                                WHERE LS.id = @groupSubId "; 
                if (term != ""){
                     query = query + "AND (L.name  LIKE '%" + term + "%' " +
                                     "OR   LS.Name LIKE '%" + term + "%' " +
                                     "OR   LG.Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Lithology>(
                    query, 
                    new[] {
                        typeof(Lithology),
                        typeof(LithologyGroupSub),
                        typeof(LithologyGroup),
                        typeof(Account)
                    },
                    objects => {
                        Lithology            lithology        = objects[0] as Lithology;
                        LithologyGroupSub    groupSub         = objects[1] as LithologyGroupSub;
                        LithologyGroup       group            = objects[2] as LithologyGroup;
                        Account              account          = objects[3] as Account;
                        //Dependency required
                        group.Account         = account;
                        groupSub.Group        = group;
                        lithology.GroupSub    = groupSub;
                        //Return
                        return lithology;
                    },
                    splitOn: "split",
                    param: new { groupSubId });
                return await PageList<Lithology>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Lithology> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT L.*, 'split', LS.*, 'split', LG.*, 'split', A.*
                                FROM Lithology L 
                                INNER JOIN LithologyGroupSub LS ON L.groupSubId  = LS.id 
                                INNER JOIN LithologyGroup    LG ON LS.groupId    = LG.id 
                                INNER JOIN Account           A  ON LG.accountId  = A.id 
                                WHERE L.id = @id";
                var res = await conn.QueryAsync<Lithology>(
                    query, 
                    new[] {
                        typeof(Lithology),
                        typeof(LithologyGroupSub),
                        typeof(LithologyGroup),
                        typeof(Account)
                    },
                    objects => {
                        Lithology            lithology        = objects[0] as Lithology;
                        LithologyGroupSub    groupSub         = objects[1] as LithologyGroupSub;
                        LithologyGroup       group            = objects[2] as LithologyGroup;
                        Account              account          = objects[3] as Account;
                        //Dependency required
                        group.Account         = account;
                        groupSub.Group        = group;
                        lithology.GroupSub    = groupSub;
                        //Return
                        return lithology;
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





// using Dapper;

// using System.Transactions;
// using GeoCloudAI.Domain.Classes;
// using GeoCloudAI.Persistence.Data;
// using GeoCloudAI.Persistence.Contracts;
// using GeoCloudAI.Persistence.Models;

// namespace GeoCloudAI.Persistence.Repositories
// {
//     public class LithologyRepository: ILithologyRepository
//     {
//         private DbSession _db;

//         public LithologyRepository(DbSession dbSession)
//         {
//             _db = dbSession;
//         }

//         public async Task<int> Add(Lithology lithology)
//         {
//             try
//             {
//                 var conn = _db.Connection;
//                 using (TransactionScope scope = new TransactionScope())
//                 {
//                     if (lithology.GroupSubId == 0) { return 0; }
//                     string command = @"INSERT INTO LITHOLOGY(groupSubId, name)
//                                         VALUES(@groupSubId, @name); " +
//                                     "SELECT LAST_INSERT_ID();";
//                     var result = conn.ExecuteScalar<int>(sql: command, param: lithology);
//                     scope.Complete();
//                     return result;
//                 }
//             }
//             catch (Exception ex)
//             {
//                 throw new Exception(ex.Message);
//             }
//         }

//         public async Task<int> Update(Lithology lithology)
//         {
//             try
//             {
//                 var conn = _db.Connection;
//                 if (lithology.GroupSubId == 0) { return 0; }
//                 string command = @"UPDATE LITHOLOGY SET 
//                                     groupSubId      = @groupSubId,
//                                     name            = @name
//                                     WHERE id        = @id";
//                 var result = await conn.ExecuteAsync(sql: command, param: lithology);
//                 return result;
//             }
//             catch (Exception ex)
//             {
//                 throw new Exception(ex.Message);
//             }
//         }

//         public async Task<int> Delete(int id)
//         {
//             try
//             {
//                 var conn = _db.Connection;
//                 string command = @"DELETE FROM LITHOLOGY WHERE id = @id";
//                 var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
//                 return resultado;
//             }
//             catch (Exception ex)
//             {
//                 throw new Exception(ex.Message);
//             }
//         }

//         public async Task<PageList<Lithology>> Get(PageParams pageParams)
//         {
//             try
//             {
//                 var conn = _db.Connection;
//                 var term         = pageParams.Term;
//                 var orderField   = pageParams.OrderField;
//                 var orderReverse = pageParams.OrderReverse;
//                 string query = @"SELECT L.*, 'split', LS.*, 'split', LG.*, 'split', A.*
//                                 FROM Lithology L 
//                                 INNER JOIN LithologyGroupSub LS ON L.groupSubId        = LS.id
//                                 INNER JOIN LithologyGroup    LG ON LS.groupId          = LG.id
//                                 INNER JOIN Account           A  ON LG.accountId        = A.id ";
//                 if (term != ""){
//                      query = query + "WHERE L.name   LIKE '%" + term + "%' " +
//                                      "OR    LS.Name  LIKE '%" + term + "%' " +
//                                      "OR    LG.Name  LIKE '%" + term + "%' ";
//                 }
//                 if (orderField != ""){
//                     query = query + "ORDER BY " + orderField;
//                     if (orderReverse) {
//                         query = query + " DESC ";
//                     }
//                 }
//                 var res = await conn.QueryAsync<Lithology>(
//                     query, 
//                     new[] {
//                         typeof(Lithology),
//                         typeof(LithologyGroupSub),
//                         typeof(LithologyGroup),
//                         typeof(Account)
//                     },
//                     objects => {
//                         Lithology           lithology       = objects[0] as Lithology;
//                         LithologyGroupSub   groupSub        = objects[1] as LithologyGroupSub;
//                         LithologyGroup      group           = objects[2] as LithologyGroup;
//                         Account             account         = objects[3] as Account;
//                         //Dependency required
//                         group.Account                = account;
//                         groupSub.Group               = group;
//                         lithology.GroupSub           = groupSub;
//                         //Return
//                         return lithology;
//                     },
//                     splitOn: "split",
//                     param: new {});
//                 return await PageList<Lithology>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
//             }
//             catch (Exception ex)
//             {
//                 throw new Exception(ex.Message);
//             }
//         }

//         public async Task<PageList<Lithology>> GetByAccount(int accountId, PageParams pageParams)
//         {
//             try
//             {
//                 var conn = _db.Connection;
//                 var term         = pageParams.Term;
//                 var orderField   = pageParams.OrderField;
//                 var orderReverse = pageParams.OrderReverse;
//                 string query = @"SELECT L.*, 'split', LS.*, 'split', LG.*, 'split', A.*
//                                 FROM Lithology L 
//                                 INNER JOIN LithologyGroupSub     LS ON L.groupSubId        = LS.id
//                                 INNER JOIN LithologyGroup        LG ON LS.groupId          = LG.id
//                                 INNER JOIN Account               A  ON LG.accountId        = A.id 
//                                 WHERE A.id = @accountId "; 
//                 if (term != ""){
//                      query = query + "AND  (L.name   LIKE '%" + term + "%' " +
//                                      "OR    LS.Name  LIKE '%" + term + "%' " +
//                                      "OR    LG.Name  LIKE '%" + term + "%' ";
//                 }
//                 if (orderField != ""){
//                     query = query + "ORDER BY " + orderField;
//                     if (orderReverse) {
//                         query = query + " DESC ";
//                     }
//                 }
//                 var res = await conn.QueryAsync<Lithology>(
//                     query, 
//                     new[] {
//                         typeof(Lithology),
//                         typeof(LithologyGroupSub),
//                         typeof(LithologyGroup),
//                         typeof(Account)
//                     },
//                     objects => {
//                         Lithology               lithology       = objects[0] as Lithology;
//                         LithologyGroupSub       groupSub        = objects[1] as LithologyGroupSub;
//                         LithologyGroup          group           = objects[2] as LithologyGroup;
//                         Account                 account         = objects[3] as Account;
//                         //Dependency required
//                         group.Account           = account;
//                         groupSub.Group          = group;
//                         lithology.GroupSub      = groupSub;
//                         //Return
//                         return lithology;
//                     },
//                     splitOn: "split",
//                     param: new { accountId });
//                 return await PageList<Lithology>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
//             }
//             catch (Exception ex)
//             {
//                 throw new Exception(ex.Message);
//             }
//         }

//         public async Task<PageList<Lithology>> GetByLithologyGroupSub(int groupSubId, PageParams pageParams)
//         {
//             try
//             {
//                 var conn = _db.Connection;
//                 var term         = pageParams.Term;
//                 var orderField   = pageParams.OrderField;
//                 var orderReverse = pageParams.OrderReverse;
//                 string query = @"SELECT L.*, 'split', LS.*, 'split', LG.*, 'split', A.*
//                                 FROM Lithology L 
//                                 INNER JOIN LithologyGroupSub    LS ON L.groupSubId          = LS.id
//                                 INNER JOIN LithologyGroup       LG ON LS.groupId            = LG.id
//                                 INNER JOIN Account              A  ON LG.accountId          = A.id 
//                                 WHERE LG.id = @groupId "; 
//                 if (term != ""){
//                      query = query + "AND  (L.name   LIKE '%" + term + "%' " +
//                                      "OR    LS.Name  LIKE '%" + term + "%' " +
//                                      "OR    LG.Name  LIKE '%" + term + "%') ";
//                 }
//                 if (orderField != ""){
//                     query = query + "ORDER BY " + orderField;
//                     if (orderReverse) {
//                         query = query + " DESC ";
//                     }
//                 }
//                 var res = await conn.QueryAsync<Lithology>(
//                     query, 
//                     new[] {
//                         typeof(Lithology),
//                         typeof(LithologyGroupSub),
//                         typeof(LithologyGroup),
//                         typeof(Account)
//                     },
//                     objects => {
//                         Lithology             lithology       = objects[0] as Lithology;
//                         LithologyGroupSub     groupSub        = objects[1] as LithologyGroupSub;
//                         LithologyGroup        group           = objects[2] as LithologyGroup;
//                         Account               account         = objects[3] as Account;
//                         //Dependency required
//                         group.Account           = account;
//                         groupSub.Group          = group;
//                         lithology.GroupSub      = groupSub;
//                         //Return
//                         return lithology;
//                     },
//                     splitOn: "split",
//                     param: new { groupSubId });
//                 return await PageList<Lithology>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
//             }
//             catch (Exception ex)
//             {
//                 throw new Exception(ex.Message);
//             }
//         }

//         public async Task<Lithology> GetById(int id)
//         {
//             try
//             {
//                 var conn = _db.Connection;
//                  string query = @"SELECT L.*, 'split', LS.*, 'split', LG.*, 'split', A.*
//                                 FROM Lithology L 
//                                 INNER JOIN LithologyGroupSub     LS ON L.groupSubId    = LS.id
//                                 INNER JOIN LithologyGroup        LG ON LS.groupId      = LG.id
//                                 INNER JOIN Account               A  ON LG.accountId    = A.id  
//                                 WHERE L.id = @id ";
//                 var res = await conn.QueryAsync<Lithology, LithologyGroupSub, LithologyGroup, Account, Lithology>(
//                     sql: query,
//                     map: (lithology, lithologyGroupSub, lithologyGroup, account) => {
//                           lithologyGroup.Account            = account;
//                           lithologyGroupSub.Group           = lithologyGroup;
//                           lithology.GroupSub                = lithologyGroupSub;
//                           return lithology;
//                     },
//                     splitOn: "split",
//                     param: new { id });
//                 if (res.Count() == 0) return null;
//                 return res.First();
//             }
//             catch (Exception ex)
//             {
//                 throw new Exception(ex.Message);
//             }
//         }

//     }
// }