using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using System.Linq;

namespace GeoCloudAI.Persistence.Repositories
{
    public class OreGeneticTypeRepository: IOreGeneticTypeRepository
    {
        private DbSession _db;

        public OreGeneticTypeRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(OreGeneticType oreGeneticType)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (oreGeneticType.DepositTypeId == 0) { return 0; }
                    string command = @"INSERT INTO OREGENETICTYPE(depositTypeId, name)
                                        VALUES(@depositTypeId, @name); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: oreGeneticType);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(OreGeneticType oreGeneticType)
        {
            try
            {
                var conn = _db.Connection;
                if (oreGeneticType.DepositTypeId == 0) { return 0; }
                string command = @"UPDATE OREGENETICTYPE SET 
                                    depositTypeId = @depositTypeId,
                                    name          = @name
                                    WHERE id      = @id";
                var result = await conn.ExecuteAsync(sql: command, param: oreGeneticType);
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
                string command = @"DELETE FROM OREGENETICTYPE WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<OreGeneticType>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT O.*, 'split', D.*, 'split', A.*
                                FROM OreGeneticType O 
                                INNER JOIN DepositType D  ON O.depositTypeId = D.id 
                                INNER JOIN Account A      ON D.AccountId     = A.id ";
                if (term != ""){
                     query = query + "WHERE O.name LIKE '%" + term + "%' " +
                                     "OR    D.Name LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<OreGeneticType>(
                    query, 
                    new[] {
                        typeof(OreGeneticType),
                        typeof(DepositType),
                        typeof(Account)
                    },
                    objects => {
                        OreGeneticType oreGeneticType = objects[0] as OreGeneticType;
                        DepositType    depositType    = objects[1] as DepositType;
                        Account        account        = objects[2] as Account;
                        //Dependency required
                        depositType.Account        = account;
                        oreGeneticType.DepositType = depositType;
                        //Return
                        return oreGeneticType;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<OreGeneticType>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<OreGeneticType>> GetByAccount(int accountId,PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT O.*, 'split', D.*, 'split', A.*
                                FROM OreGeneticType O 
                                INNER JOIN DepositType D  ON O.depositTypeId = D.id 
                                INNER JOIN Account A      ON D.AccountId     = A.id 
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
                var res = await conn.QueryAsync<OreGeneticType>(
                    query, 
                    new[] {
                        typeof(OreGeneticType),
                        typeof(DepositType),
                        typeof(Account)
                    },
                    objects => {
                        OreGeneticType oreGeneticType = objects[0] as OreGeneticType;
                        DepositType    depositType    = objects[1] as DepositType;
                        Account        account        = objects[2] as Account;
                        //Dependency required
                        depositType.Account        = account;
                        oreGeneticType.DepositType = depositType;
                        //Return
                        return oreGeneticType;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<OreGeneticType>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<OreGeneticType>> GetByDepositType(int depositTypeId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT O.*, 'split', D.*, 'split', A.*
                                FROM OreGeneticType O 
                                INNER JOIN DepositType D  ON O.depositTypeId = D.id 
                                INNER JOIN Account A      ON D.AccountId     = A.id 
                                WHERE D.id = @depositTypeId "; 
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
                var res = await conn.QueryAsync<OreGeneticType>(
                    query, 
                    new[] {
                        typeof(OreGeneticType),
                        typeof(DepositType),
                        typeof(Account)
                    },
                    objects => {
                        OreGeneticType oreGeneticType = objects[0] as OreGeneticType;
                        DepositType    depositType    = objects[1] as DepositType;
                        Account        account        = objects[2] as Account;
                        //Dependency required
                        depositType.Account        = account;
                        oreGeneticType.DepositType = depositType;
                        //Return
                        return oreGeneticType;
                    },
                    splitOn: "split",
                    param: new { depositTypeId });
                return await PageList<OreGeneticType>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OreGeneticType> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT O.*, 'split', D.*, 'split', A.*
                                FROM OreGeneticType O 
                                INNER JOIN DepositType D  ON O.depositTypeId = D.id 
                                INNER JOIN Account A      ON D.AccountId     = A.id 
                                WHERE O.ID = @id";
                var res = await conn.QueryAsync<OreGeneticType>(
                    query, 
                    new[] {
                        typeof(OreGeneticType),
                        typeof(DepositType),
                        typeof(Account)
                    },
                    objects => {
                        OreGeneticType oreGeneticType = objects[0] as OreGeneticType;
                        DepositType    depositType    = objects[1] as DepositType;
                        Account        account        = objects[2] as Account;
                        //Dependency required
                        depositType.Account        = account;
                        oreGeneticType.DepositType = depositType;
                        //Return
                        return oreGeneticType;
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