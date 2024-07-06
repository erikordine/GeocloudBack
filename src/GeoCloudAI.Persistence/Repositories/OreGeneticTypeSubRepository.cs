using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using System.Linq;

namespace GeoCloudAI.Persistence.Repositories
{
    public class OreGeneticTypeSubRepository: IOreGeneticTypeSubRepository
    {
        private DbSession _db;

        public OreGeneticTypeSubRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(OreGeneticTypeSub oreGeneticTypeSub)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (oreGeneticTypeSub.OreGeneticTypeId == 0) { return 0; }
                    string command = @"INSERT INTO OREGENETICTYPESUB(oreGeneticTypeId, name)
                                        VALUES(@oreGeneticTypeId, @name); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: oreGeneticTypeSub);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(OreGeneticTypeSub oreGeneticTypeSub)
        {
            try
            {
                var conn = _db.Connection;
                if (oreGeneticTypeSub.OreGeneticTypeId == 0) { return 0; }
                string command = @"UPDATE OREGENETICTYPESUB SET 
                                    oreGeneticTypeId = @oreGeneticTypeId,
                                    name             = @name
                                    WHERE id         = @id";
                var result = await conn.ExecuteAsync(sql: command, param: oreGeneticTypeSub);
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
                string command = @"DELETE FROM OREGENETICTYPESUB WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<OreGeneticTypeSub>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT OS.*, 'split', O.*, 'split', D.*, 'split', A.*
                                FROM OreGeneticTypeSub OS 
                                INNER JOIN OreGeneticType O ON OS.oreGeneticTypeId = O.id 
                                INNER JOIN DepositType    D ON O.depositTypeId     = D.id 
                                INNER JOIN Account        A ON D.accountId         = A.id ";
                if (term != ""){
                     query = query + "WHERE OS.name LIKE '%" + term + "%' " +
                                     "OR    O.Name  LIKE '%" + term + "%' " +
                                     "OR    D.Name  LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<OreGeneticTypeSub>(
                    query, 
                    new[] {
                        typeof(OreGeneticTypeSub),
                        typeof(OreGeneticType),
                        typeof(DepositType),
                        typeof(Account)
                    },
                    objects => {
                        OreGeneticTypeSub oreGeneticTypeSub = objects[0] as OreGeneticTypeSub;
                        OreGeneticType    oreGeneticType    = objects[1] as OreGeneticType;
                        DepositType       depositType       = objects[2] as DepositType;
                        Account           account           = objects[3] as Account;
                        //Dependency required
                        depositType.Account              = account;
                        oreGeneticType.DepositType       = depositType;
                        oreGeneticTypeSub.OreGeneticType = oreGeneticType;
                        //Return
                        return oreGeneticTypeSub;
                    },
                    splitOn: "split",
                    param: new {});
                return await PageList<OreGeneticTypeSub>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<OreGeneticTypeSub>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT OS.*, 'split', O.*, 'split', D.*, 'split', A.*
                                FROM OreGeneticTypeSub OS 
                                INNER JOIN OreGeneticType O ON OS.oreGeneticTypeId = O.id 
                                INNER JOIN DepositType    D ON O.depositTypeId     = D.id 
                                INNER JOIN Account        A ON D.accountId         = A.id 
                                WHERE A.id= @accountId "; 
                if (term != ""){
                     query = query + "AND (OS.name LIKE '%" + term + "%' " +
                                     "OR   O.Name  LIKE '%" + term + "%' " +
                                     "OR   D.Name  LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<OreGeneticTypeSub>(
                    query, 
                    new[] {
                        typeof(OreGeneticTypeSub),
                        typeof(OreGeneticType),
                        typeof(DepositType),
                        typeof(Account)
                    },
                    objects => {
                        OreGeneticTypeSub oreGeneticTypeSub = objects[0] as OreGeneticTypeSub;
                        OreGeneticType    oreGeneticType    = objects[1] as OreGeneticType;
                        DepositType       depositType       = objects[2] as DepositType;
                        Account           account           = objects[3] as Account;
                        //Dependency required
                        depositType.Account              = account;
                        oreGeneticType.DepositType       = depositType;
                        oreGeneticTypeSub.OreGeneticType = oreGeneticType;
                        //Return
                        return oreGeneticTypeSub;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<OreGeneticTypeSub>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<OreGeneticTypeSub>> GetByOreGeneticType(int oreGeneticTypeId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT OS.*, 'split', O.*, 'split', D.*, 'split', A.*
                                FROM OreGeneticTypeSub OS 
                                INNER JOIN OreGeneticType O ON OS.oreGeneticTypeId = O.id 
                                INNER JOIN DepositType    D ON O.depositTypeId     = D.id 
                                INNER JOIN Account        A ON D.accountId         = A.id 
                                WHERE O.id= @oreGeneticTypeId "; 
                if (term != ""){
                     query = query + "AND (OS.name LIKE '%" + term + "%' " +
                                     "OR   O.Name  LIKE '%" + term + "%' " +
                                     "OR   D.Name  LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<OreGeneticTypeSub>(
                    query, 
                    new[] {
                        typeof(OreGeneticTypeSub),
                        typeof(OreGeneticType),
                        typeof(DepositType),
                        typeof(Account)
                    },
                    objects => {
                        OreGeneticTypeSub oreGeneticTypeSub = objects[0] as OreGeneticTypeSub;
                        OreGeneticType    oreGeneticType    = objects[1] as OreGeneticType;
                        DepositType       depositType       = objects[2] as DepositType;
                        Account           account           = objects[3] as Account;
                        //Dependency required
                        depositType.Account              = account;
                        oreGeneticType.DepositType       = depositType;
                        oreGeneticTypeSub.OreGeneticType = oreGeneticType;
                        //Return
                        return oreGeneticTypeSub;
                    },
                    splitOn: "split",
                    param: new { oreGeneticTypeId });
                return await PageList<OreGeneticTypeSub>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OreGeneticTypeSub> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT OS.*, 'split', O.*, 'split', D.*, 'split', A.*
                                FROM OreGeneticTypeSub OS 
                                INNER JOIN OreGeneticType O ON OS.oreGeneticTypeId = O.id 
                                INNER JOIN DepositType    D ON O.depositTypeId     = D.id 
                                INNER JOIN Account        A ON D.accountId         = A.id 
                                WHERE OS.ID = @id";
                var res = await conn.QueryAsync<OreGeneticTypeSub>(
                    query, 
                    new[] {
                        typeof(OreGeneticTypeSub),
                        typeof(OreGeneticType),
                        typeof(DepositType),
                        typeof(Account)
                    },
                    objects => {
                        OreGeneticTypeSub oreGeneticTypeSub = objects[0] as OreGeneticTypeSub;
                        OreGeneticType    oreGeneticType    = objects[1] as OreGeneticType;
                        DepositType       depositType       = objects[2] as DepositType;
                        Account           account           = objects[3] as Account;
                        //Dependency required
                        depositType.Account              = account;
                        oreGeneticType.DepositType       = depositType;
                        oreGeneticTypeSub.OreGeneticType = oreGeneticType;
                        //Return
                        return oreGeneticTypeSub;
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