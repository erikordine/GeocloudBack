using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Repositories
{
    public class UnitTypeRepository: IUnitTypeRepository
    {
        private DbSession _db;

        public UnitTypeRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(UnitType unitType)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    string command = @"INSERT INTO UNITTYPE(name) VALUES(@name); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: unitType);
                    scope.Complete();
                    return result;
                }

                 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(UnitType unitType)
        {
            try
            {
                var conn = _db.Connection;
                string command = @"UPDATE UNITTYPE SET 
                                    name     = @name
                                    WHERE ID = @id";
                var result = await conn.ExecuteAsync(sql: command, param: unitType);
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
                string command = @"DELETE FROM UNITTYPE WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<UnitType>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT * FROM UNITTYPE ";
                if (term != "")
                    query = query + "WHERE name LIKE '%" + term + "%' ";
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                IEnumerable<UnitType> ages = (await conn.QueryAsync<UnitType>(sql: query, param: new {})).ToArray();
                return await PageList<UnitType>.CreateAsync(ages, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UnitType> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = "SELECT * FROM UNITTYPE WHERE id = @id";
                UnitType? age = await conn.QueryFirstOrDefaultAsync<UnitType>(sql: query, param: new { id });
                return age!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}