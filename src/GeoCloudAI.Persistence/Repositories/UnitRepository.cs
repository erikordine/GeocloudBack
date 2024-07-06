using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Repositories
{
    public class UnitRepository: IUnitRepository
    {
        private DbSession _db;

        public UnitRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(Unit unit)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    string command = @"INSERT INTO UNIT(typeId, name) VALUES(@typeId, @name); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: unit);
                    scope.Complete();
                    return result;
                }

                 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(Unit unit)
        {
            try
            {
                var conn = _db.Connection;
                string command = @"UPDATE UNIT SET 
                                    typeId   = @typeId,
                                    name     = @name
                                    WHERE ID = @id";
                var result = await conn.ExecuteAsync(sql: command, param: unit);
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
                string command = @"DELETE FROM UNIT WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Unit>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT U.*, 'split', T.*
                                FROM UNIT U 
                                INNER JOIN UnitType T ON U.typeId = T.id ";
                if (term != "")
                    query = query + "WHERE U.name LIKE '%" + term + "%' " +
                                    "OR    T.name LIKE '%" + term + "%' ";
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Unit, UnitType, Unit>(
                    sql: query,
                    map: (unit, unitType) => {
                        unit.Type = unitType;
                        return unit;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<Unit>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Unit>> GetByUnitType(int unitTypeId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT U.*, 'split', T.*
                                FROM UNIT U 
                                INNER JOIN UnitType T ON U.typeId = T.id  
                                WHERE T.id = @unitTypeId "; 
                if (term != ""){
                     query = query + "AND (U.name LIKE '%" + term + "%' " +
                                     "OR   T.name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Unit, UnitType, Unit>(
                    sql: query,
                    map: (unit, unitType) => {
                        unit.Type = unitType;
                        return unit;
                    },
                    splitOn: "split",
                    param: new { unitTypeId });
                return await PageList<Unit>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Unit> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT U.*, 'split', T.*
                                FROM UNIT U 
                                INNER JOIN UnitType T ON U.typeId = T.id
                                WHERE U.ID = @id";
                var res = await conn.QueryAsync<Unit, UnitType, Unit>(
                    sql: query,
                    map: (unit, unitType) => {
                        unit.Type = unitType;
                        return unit;
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