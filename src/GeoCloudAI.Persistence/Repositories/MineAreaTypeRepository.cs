using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using System.Linq;

namespace GeoCloudAI.Persistence.Repositories
{
    public class MineAreaTypeRepository: IMineAreaTypeRepository
    {
        private DbSession _db;

        public MineAreaTypeRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(MineAreaType mineAreaType)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (mineAreaType.AccountId == 0) { return 0; }
                    string command = @"INSERT INTO MINEAREATYPE(accountId, name, imgType)
                                        VALUES(@accountId, @name, @imgType); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: mineAreaType);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(MineAreaType mineAreaType)
        {
            try
            {
                var conn = _db.Connection;
                if (mineAreaType.AccountId == 0) { return 0; }
                string command = @"UPDATE MINEAREATYPE SET 
                                    accountId = @accountId,
                                    name      = @name,
                                    imgType   = @imgType
                                    WHERE id  = @id";
                var result = await conn.ExecuteAsync(sql: command, param: mineAreaType);
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
                string command = @"DELETE FROM MINEAREATYPE WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<MineAreaType>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT M.*, 'split', A.*
                                FROM MineAreaType M 
                                INNER JOIN Account A ON M.accountId = A.id ";
                if (term != ""){
                     query = query + "WHERE M.name    LIKE '%" + term + "%' " +
                                     "OR    A.id      LIKE '%" + term + "%' " +
                                     "OR    A.company LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<MineAreaType, Account, MineAreaType>(
                    sql: query,
                    map: (mineAreaType, account) => {
                        mineAreaType.Account = account;
                        return mineAreaType;
                    },
                    splitOn: "split",
                    param: new {});
                return await PageList<MineAreaType>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<MineAreaType>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT M.*, 'split', A.*
                                FROM MineAreaType M 
                                INNER JOIN Account A ON M.accountId = A.id 
                                WHERE A.id = @accountId "; 
                if (term != ""){
                     query = query + "AND (M.name LIKE '%"    + term + "%' " +
                                     "OR   A.id      LIKE '%" + term + "%' " +
                                     "OR   A.company LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<MineAreaType, Account, MineAreaType>(
                    sql: query,
                    map: (mineAreaType, account) => {
                        mineAreaType.Account = account;
                        return mineAreaType;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<MineAreaType>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MineAreaType> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT M.*, 'split', A.*
                                FROM MineAreaType M 
                                INNER JOIN Account A ON M.accountId = A.id
                                WHERE M.ID = @id";
                var res =  await conn.QueryAsync<MineAreaType, Account, MineAreaType>(
                    sql: query,
                    map: (mineAreaType, account) => {
                        mineAreaType.Account = account;
                        return mineAreaType;
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