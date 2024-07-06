using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using System.Linq;

namespace GeoCloudAI.Persistence.Repositories
{
    public class MetalGroupRepository: IMetalGroupRepository
    {
        private DbSession _db;

        public MetalGroupRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(MetalGroup metalGroup)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (metalGroup.OreGeneticTypeId == 0) { return 0; }
                    string command = @"INSERT INTO METALGROUP(oreGeneticTypeId, name)
                                        VALUES(@oreGeneticTypeId, @name); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: metalGroup);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(MetalGroup metalGroup)
        {
            try
            {
                var conn = _db.Connection;
                if (metalGroup.OreGeneticTypeId == 0) { return 0; }
                string command = @"UPDATE METALGROUP SET 
                                    oreGeneticTypeId = @oreGeneticTypeId,
                                    name             = @name
                                    WHERE id         = @id";
                var result = await conn.ExecuteAsync(sql: command, param: metalGroup);
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
                string command = @"DELETE FROM METALGROUP WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<MetalGroup>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT M.*, 'split', O.*, 'split', D.*, 'split', A.*
                                FROM MetalGroup M 
                                INNER JOIN OreGeneticType O ON M.oreGeneticTypeId = O.id 
                                INNER JOIN DepositType    D ON O.depositTypeId    = D.id 
                                INNER JOIN Account        A ON D.accountId        = A.id ";
                if (term != ""){
                     query = query + "WHERE M.name LIKE '%" + term + "%' " +
                                     "OR    O.Name LIKE '%" + term + "%' " +
                                     "OR    D.Name LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<MetalGroup>(
                    query, 
                    new[] {
                        typeof(MetalGroup),
                        typeof(OreGeneticType),
                        typeof(DepositType),
                        typeof(Account)
                    },
                    objects => {
                        MetalGroup        metalGroup        = objects[0] as MetalGroup;
                        OreGeneticType    oreGeneticType    = objects[1] as OreGeneticType;
                        DepositType       depositType       = objects[2] as DepositType;
                        Account           account           = objects[3] as Account;
                        //Dependency required
                        depositType.Account         = account;
                        oreGeneticType.DepositType  = depositType;
                        metalGroup.OreGeneticType   = oreGeneticType;
                        //Return
                        return metalGroup;
                    },
                    splitOn: "split",
                    param: new {});
                return await PageList<MetalGroup>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<MetalGroup>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT M.*, 'split', O.*, 'split', D.*, 'split', A.*
                                FROM MetalGroup M 
                                INNER JOIN OreGeneticType O ON M.oreGeneticTypeId = O.id 
                                INNER JOIN DepositType    D ON O.depositTypeId    = D.id 
                                INNER JOIN Account        A ON D.accountId        = A.id 
                                WHERE A.id = @accountId "; 
                if (term != ""){
                     query = query + "AND (M.name LIKE '%" + term + "%' " +
                                     "OR   O.Name LIKE '%" + term + "%' " +
                                     "OR   D.Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<MetalGroup>(
                    query, 
                    new[] {
                        typeof(MetalGroup),
                        typeof(OreGeneticType),
                        typeof(DepositType),
                        typeof(Account)
                    },
                    objects => {
                        MetalGroup        metalGroup        = objects[0] as MetalGroup;
                        OreGeneticType    oreGeneticType    = objects[1] as OreGeneticType;
                        DepositType       depositType       = objects[2] as DepositType;
                        Account           account           = objects[3] as Account;
                        //Dependency required
                        depositType.Account         = account;
                        oreGeneticType.DepositType  = depositType;
                        metalGroup.OreGeneticType   = oreGeneticType;
                        //Return
                        return metalGroup;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<MetalGroup>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<MetalGroup>> GetByOreGeneticType(int oreGeneticTypeId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT M.*, 'split', O.*, 'split', D.*, 'split', A.*
                                FROM MetalGroup M 
                                INNER JOIN OreGeneticType O ON M.oreGeneticTypeId = O.id 
                                INNER JOIN DepositType    D ON O.depositTypeId    = D.id 
                                INNER JOIN Account        A ON D.accountId        = A.id 
                                WHERE O.id = @oreGeneticTypeId "; 
                if (term != ""){
                     query = query + "AND (M.name LIKE '%" + term + "%' " +
                                     "OR   O.Name LIKE '%" + term + "%' " +
                                     "OR   D.Name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<MetalGroup>(
                    query, 
                    new[] {
                        typeof(MetalGroup),
                        typeof(OreGeneticType),
                        typeof(DepositType),
                        typeof(Account)
                    },
                    objects => {
                        MetalGroup        metalGroup        = objects[0] as MetalGroup;
                        OreGeneticType    oreGeneticType    = objects[1] as OreGeneticType;
                        DepositType       depositType       = objects[2] as DepositType;
                        Account           account           = objects[3] as Account;
                        //Dependency required
                        depositType.Account         = account;
                        oreGeneticType.DepositType  = depositType;
                        metalGroup.OreGeneticType   = oreGeneticType;
                        //Return
                        return metalGroup;
                    },
                    splitOn: "split",
                    param: new { oreGeneticTypeId });
                return await PageList<MetalGroup>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MetalGroup> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT M.*, 'split', O.*, 'split', D.*, 'split', A.*
                                FROM MetalGroup M 
                                INNER JOIN OreGeneticType O ON M.oreGeneticTypeId = O.id 
                                INNER JOIN DepositType    D ON O.depositTypeId    = D.id 
                                INNER JOIN Account        A ON D.accountId        = A.id 
                                WHERE M.id = @id";
                var res = await conn.QueryAsync<MetalGroup>(
                    query, 
                    new[] {
                        typeof(MetalGroup),
                        typeof(OreGeneticType),
                        typeof(DepositType),
                        typeof(Account)
                    },
                    objects => {
                        MetalGroup        metalGroup        = objects[0] as MetalGroup;
                        OreGeneticType    oreGeneticType    = objects[1] as OreGeneticType;
                        DepositType       depositType       = objects[2] as DepositType;
                        Account           account           = objects[3] as Account;
                        //Dependency required
                        depositType.Account         = account;
                        oreGeneticType.DepositType  = depositType;
                        metalGroup.OreGeneticType   = oreGeneticType;
                        //Return
                        return metalGroup;
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