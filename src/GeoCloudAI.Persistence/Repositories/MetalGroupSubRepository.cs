using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using System.Linq;

namespace GeoCloudAI.Persistence.Repositories
{
    public class MetalGroupSubRepository: IMetalGroupSubRepository
    {
        private DbSession _db;

        public MetalGroupSubRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(MetalGroupSub metalGroupSub)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (metalGroupSub.MetalGroupId == 0) { return 0; }
                    string command = @"INSERT INTO METALGROUPSUB(metalGroupId, name)
                                        VALUES(@metalGroupId, @name); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: metalGroupSub);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(MetalGroupSub metalGroupSub)
        {
            try
            {
                var conn = _db.Connection;
                if (metalGroupSub.MetalGroupId == 0) { return 0; }
                string command = @"UPDATE METALGROUPSUB SET 
                                    metalGroupId = @metalGroupId,
                                    name         = @name
                                    WHERE id     = @id";
                var result = await conn.ExecuteAsync(sql: command, param: metalGroupSub);
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
                string command = @"DELETE FROM METALGROUPSUB WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<MetalGroupSub>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT MS.*, 'split', M.*, 'split', O.*, 'split', D.*, 'split', A.*
                                FROM MetalGroupSub MS 
                                INNER JOIN MetalGroup     M ON MS.metalGroupId    = M.id
                                INNER JOIN OreGeneticType O ON M.oregenetictypeId = O.id
                                INNER JOIN DepositType    D ON O.depositTypeId    = D.id 
                                INNER JOIN Account        A ON D.accountId        = A.id ";
                if (term != ""){
                     query = query + "WHERE MS.name LIKE '%" + term + "%' " +
                                     "OR    M.Name  LIKE '%" + term + "%' " +
                                     "OR    O.Name  LIKE '%" + term + "%' " +
                                     "OR    D.Name  LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<MetalGroupSub>(
                    query, 
                    new[] {
                        typeof(MetalGroupSub),
                        typeof(MetalGroup),
                        typeof(OreGeneticType),
                        typeof(DepositType),
                        typeof(Account)
                    },
                    objects => {
                        MetalGroupSub     metalGroupSub     = objects[0] as MetalGroupSub;
                        MetalGroup        metalGroup        = objects[1] as MetalGroup;
                        OreGeneticType    oreGeneticType    = objects[2] as OreGeneticType;
                        DepositType       depositType       = objects[3] as DepositType;
                        Account           account           = objects[4] as Account;
                        //Dependency required
                        depositType.Account         = account;
                        oreGeneticType.DepositType  = depositType;
                        metalGroup.OreGeneticType   = oreGeneticType;
                        metalGroupSub.MetalGroup    = metalGroup;
                        //Return
                        return metalGroupSub;
                    },
                    splitOn: "split",
                    param: new {});
                return await PageList<MetalGroupSub>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<MetalGroupSub>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT MS.*, 'split', M.*, 'split', O.*, 'split', D.*, 'split', A.*
                                FROM MetalGroupSub MS 
                                INNER JOIN MetalGroup     M ON MS.metalGroupId    = M.id
                                INNER JOIN OreGeneticType O ON M.oregenetictypeId = O.id
                                INNER JOIN DepositType    D ON O.depositTypeId    = D.id 
                                INNER JOIN Account        A ON D.accountId        = A.id 
                                WHERE A.id = @accountId "; 
                if (term != ""){
                     query = query + "AND  (MS.name LIKE '%" + term + "%' " +
                                     "OR    M.Name  LIKE '%" + term + "%' " +
                                     "OR    O.Name  LIKE '%" + term + "%' " +
                                     "OR    D.Name  LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<MetalGroupSub>(
                    query, 
                    new[] {
                        typeof(MetalGroupSub),
                        typeof(MetalGroup),
                        typeof(OreGeneticType),
                        typeof(DepositType),
                        typeof(Account)
                    },
                    objects => {
                        MetalGroupSub     metalGroupSub     = objects[0] as MetalGroupSub;
                        MetalGroup        metalGroup        = objects[1] as MetalGroup;
                        OreGeneticType    oreGeneticType    = objects[2] as OreGeneticType;
                        DepositType       depositType       = objects[3] as DepositType;
                        Account           account           = objects[4] as Account;
                        //Dependency required
                        depositType.Account         = account;
                        oreGeneticType.DepositType  = depositType;
                        metalGroup.OreGeneticType   = oreGeneticType;
                        metalGroupSub.MetalGroup    = metalGroup;
                        //Return
                        return metalGroupSub;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<MetalGroupSub>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<MetalGroupSub>> GetByMetalGroup(int metalGroupId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT MS.*, 'split', M.*, 'split', O.*, 'split', D.*, 'split', A.*
                                FROM MetalGroupSub MS 
                                INNER JOIN MetalGroup     M ON MS.metalGroupId    = M.id
                                INNER JOIN OreGeneticType O ON M.oregenetictypeId = O.id
                                INNER JOIN DepositType    D ON O.depositTypeId    = D.id 
                                INNER JOIN Account        A ON D.accountId        = A.id 
                                WHERE M.id = @metalGroupId "; 
                if (term != ""){
                     query = query + "AND  (MS.name LIKE '%" + term + "%' " +
                                     "OR    M.Name  LIKE '%" + term + "%' " +
                                     "OR    O.Name  LIKE '%" + term + "%' " +
                                     "OR    D.Name  LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<MetalGroupSub>(
                    query, 
                    new[] {
                        typeof(MetalGroupSub),
                        typeof(MetalGroup),
                        typeof(OreGeneticType),
                        typeof(DepositType),
                        typeof(Account)
                    },
                    objects => {
                        MetalGroupSub     metalGroupSub     = objects[0] as MetalGroupSub;
                        MetalGroup        metalGroup        = objects[1] as MetalGroup;
                        OreGeneticType    oreGeneticType    = objects[2] as OreGeneticType;
                        DepositType       depositType       = objects[3] as DepositType;
                        Account           account           = objects[4] as Account;
                        //Dependency required
                        depositType.Account         = account;
                        oreGeneticType.DepositType  = depositType;
                        metalGroup.OreGeneticType   = oreGeneticType;
                        metalGroupSub.MetalGroup    = metalGroup;
                        //Return
                        return metalGroupSub;
                    },
                    splitOn: "split",
                    param: new { metalGroupId });
                return await PageList<MetalGroupSub>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MetalGroupSub> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                 string query = @"SELECT MS.*, 'split', M.*, 'split', O.*, 'split', D.*, 'split', A.*
                                FROM MetalGroupSub MS 
                                INNER JOIN MetalGroup     M ON MS.metalGroupId    = M.id
                                INNER JOIN OreGeneticType O ON M.oregenetictypeId = O.id
                                INNER JOIN DepositType    D ON O.depositTypeId    = D.id 
                                INNER JOIN Account        A ON D.accountId        = A.id  
                                WHERE MS.id = @id ";
                var res = await conn.QueryAsync<MetalGroupSub, MetalGroup, OreGeneticType, DepositType, Account, MetalGroupSub>(
                    sql: query,
                    map: (metalgroupsub, metalgroup, oregenetictype, deposittype, account) => {
                          deposittype.Account        = account;
                          oregenetictype.DepositType = deposittype;
                          metalgroup.OreGeneticType  = oregenetictype;
                          metalgroupsub.MetalGroup   = metalgroup;
                          return metalgroupsub;
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