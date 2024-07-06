using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Repositories
{
    public class FunctionalityTypeRepository: IFunctionalityTypeRepository
    {
        private DbSession _db;

        public FunctionalityTypeRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(FunctionalityType functionalityType)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    string command = @"INSERT INTO FUNCTIONALITYTYPE(name) VALUES(@name); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: functionalityType);
                    scope.Complete();
                    return result;
                }

                 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(FunctionalityType functionalityType)
        {
            try
            {
                var conn = _db.Connection;
                string command = @"UPDATE FUNCTIONALITYTYPE SET 
                                    name     = @name
                                    WHERE ID = @id";
                var result = await conn.ExecuteAsync(sql: command, param: functionalityType);
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
                string command = @"DELETE FROM FUNCTIONALITYTYPE WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<FunctionalityType>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT * FROM FUNCTIONALITYTYPE ";
                if (term != "")
                    query = query + "WHERE name LIKE '%" + term + "%' ";
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                IEnumerable<FunctionalityType> ages = (await conn.QueryAsync<FunctionalityType>(sql: query, param: new {})).ToArray();
                return await PageList<FunctionalityType>.CreateAsync(ages, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FunctionalityType> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = "SELECT * FROM FUNCTIONALITYTYPE WHERE id = @id";
                FunctionalityType? age = await conn.QueryFirstOrDefaultAsync<FunctionalityType>(sql: query, param: new { id });
                return age!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}