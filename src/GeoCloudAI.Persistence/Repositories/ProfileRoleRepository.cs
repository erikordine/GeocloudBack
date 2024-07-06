using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;
using System.Linq;

namespace GeoCloudAI.Persistence.Repositories
{
    public class ProfileRoleRepository: IProfileRoleRepository
    {
        private DbSession _db;

        public ProfileRoleRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(ProfileRole profileRole)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    string command = @"INSERT INTO PROFILEROLE(profileId, roleId)
                                       VALUES(@profileId, @roleId); " +
                                      "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: profileRole);
                    scope.Complete();
                    return result;
                }
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
                string command = @"DELETE FROM PROFILEROLE WHERE id = @id";
                var result = await conn.ExecuteAsync(sql: command, param: new { id } );
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<ProfileRole>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT PD.*, 'split', P.*, 'split', R.*
                                FROM ProfileRole PD 
                                INNER JOIN Profile P ON PD.profileId = P.id 
                                INNER JOIN Role R    ON PD.roleId    = R.id ";
                if (term != ""){
                     query = query + "WHERE P.name LIKE '%" + term + "%' " +
                                     "OR    R.name LIKE '%" + term + "%' ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<ProfileRole>(
                    query, 
                    new[] {
                        typeof(ProfileRole),
                        typeof(Profile),
                        typeof(Role),
                    },
                    objects => {
                        ProfileRole profileRole = objects[0] as ProfileRole;
                        Profile     profile     = objects[1] as Profile;
                        Role        role        = objects[2] as Role;
                        //Dependency required
                        profileRole.Profile = profile;
                        profileRole.Role    = role;
                        //Return
                        return profileRole;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<ProfileRole>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<ProfileRole>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT PD.*, 'split', P.*, 'split', R.*
                                FROM ProfileRole PD 
                                INNER JOIN Profile P ON PD.profileId = P.id 
                                INNER JOIN Role R    ON PD.roleId    = R.id 
                                WHERE P.Accountid = @accountId "; 
                if (term != ""){
                     query = query + "AND (P.name LIKE '%" + term + "%' " +
                                     "OR   R.name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<ProfileRole>(
                    query, 
                    new[] {
                        typeof(ProfileRole),
                        typeof(Profile),
                        typeof(Role),
                    },
                    objects => {
                        ProfileRole profileRole = objects[0] as ProfileRole;
                        Profile     profile     = objects[1] as Profile;
                        Role        role        = objects[2] as Role;
                        //Dependency required
                        profileRole.Profile = profile;
                        profileRole.Role    = role;
                        //Return
                        return profileRole;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<ProfileRole>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<ProfileRole>> GetByProfile(int profileId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT PD.*, 'split', P.*, 'split', R.*
                                FROM ProfileRole PD 
                                INNER JOIN Profile P ON PD.profileId = P.id 
                                INNER JOIN Role R    ON PD.roleId    = R.id 
                                WHERE P.Id = @profileId "; 
                if (term != ""){
                     query = query + "AND (P.name LIKE '%" + term + "%' " +
                                     "OR   R.name LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + "ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<ProfileRole>(
                    query, 
                    new[] {
                        typeof(ProfileRole),
                        typeof(Profile),
                        typeof(Role),
                    },
                    objects => {
                        ProfileRole profileRole = objects[0] as ProfileRole;
                        Profile     profile     = objects[1] as Profile;
                        Role        role        = objects[2] as Role;
                        //Dependency required
                        profileRole.Profile = profile;
                        profileRole.Role    = role;
                        //Return
                        return profileRole;
                    },
                    splitOn: "split",
                    param: new { profileId });
                return await PageList<ProfileRole>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProfileRole> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT PD.*, 'split', P.*, 'split', R.*
                                FROM ProfileRole PD 
                                INNER JOIN Profile P ON PD.profileId = P.id 
                                INNER JOIN Role R    ON PD.roleId    = R.id 
                                WHERE PD.Id = @id "; 
                var res = await conn.QueryAsync<ProfileRole>(
                    query, 
                    new[] {
                        typeof(ProfileRole),
                        typeof(Profile),
                        typeof(Role),
                    },
                    objects => {
                        ProfileRole profileRole = objects[0] as ProfileRole;
                        Profile     profile     = objects[1] as Profile;
                        Role        role        = objects[2] as Role;
                        //Dependency required
                        profileRole.Profile = profile;
                        profileRole.Role    = role;
                        //Return
                        return profileRole;
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