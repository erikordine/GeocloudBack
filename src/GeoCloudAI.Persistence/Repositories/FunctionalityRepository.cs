using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using System.Linq;

namespace GeoCloudAI.Persistence.Repositories
{
    public class FunctionalityRepository: IFunctionalityRepository
    {
        private DbSession _db;

        public FunctionalityRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(Functionality functionality)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (functionality.TypeId == 0) { return 0; }
                    string command = @"INSERT INTO FUNCTIONALITY(typeId, name)
                                        VALUES(@typeId, @name); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: functionality);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(Functionality functionality)
        {
            try
            {
                var conn = _db.Connection;
                if (functionality.TypeId == 0) { return 0; }
                string command = @"UPDATE FUNCTIONALITY SET 
                                    typeId    = @typeId,
                                    name      = @name
                                    WHERE id  = @id";
                var result = await conn.ExecuteAsync(sql: command, param: functionality);
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
                string command = @"DELETE FROM FUNCTIONALITY WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Functionality>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT F.*, 'split', T.*
                                FROM Functionality F 
                                INNER JOIN FunctionalityType T ON F.typeId = T.id";
                if (term != ""){
                     query = query + "WHERE F.name LIKE '%" + term + "%' " +
                                     "OR    T.Name LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<Functionality>(
                    query, 
                    new[] {
                        typeof(Functionality),
                        typeof(FunctionalityType)
                    },
                    objects => {
                        Functionality     functionality = objects[0] as Functionality;
                        FunctionalityType type          = objects[1] as FunctionalityType;
                        //Dependency required
                        functionality.Type = type;
                        //Return
                        return functionality;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<Functionality>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
        public async Task<Functionality> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT F.*, 'split', T.*
                                FROM Functionality F 
                                INNER JOIN FunctionalityType T ON F.typeId = T.id
                                WHERE F.Id = @id";
                var res = await conn.QueryAsync<Functionality>(
                    query, 
                    new[] {
                        typeof(Functionality),
                        typeof(FunctionalityType)
                    },
                    objects => {
                        Functionality     functionality = objects[0] as Functionality;
                        FunctionalityType type          = objects[1] as FunctionalityType;
                        //Dependency required
                        functionality.Type = type;
                        //Return
                        return functionality;
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