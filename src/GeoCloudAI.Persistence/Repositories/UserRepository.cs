using Dapper;

using System.Transactions;
using GeoCloudAI.Domain.Classes;
using GeoCloudAI.Persistence.Data;
using GeoCloudAI.Persistence.Contracts;
using GeoCloudAI.Persistence.Models;

namespace GeoCloudAI.Persistence.Repositories
{
    public class UserRepository: IUserRepository
    {
        private DbSession _db;
        
        public UserRepository(DbSession dbSession)
        {
            _db = dbSession;
        }

        public async Task<int> Add(User user)
        {
            try
            {
                var conn = _db.Connection;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (user.ProfileId == 0) { return 0; }
                    if (user.CountryId == 0) { return 0; }
                    string command = @"INSERT INTO USER(profileId, firstName, lastName, phone, email, password, countryId, 
                                                        state, city, access, attempts, blocked, 
                                                        imgTypeProfile, imgTypeCover, register) 
                                        VALUES(@profileId, @firstName, @lastName, @phone, @email, @password,  
                                               @countryId , @state, @city, @access, @attempts, @blocked, 
                                               @imgTypeProfile, @imgTypeCover,  @register); " +
                                    "SELECT LAST_INSERT_ID();";
                    var result = conn.ExecuteScalar<int>(sql: command, param: user);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Update(User user)
        {
            try
            {
                var conn = _db.Connection;
                if (user.ProfileId == 0) { return 0; }
                if (user.CountryId == 0) { return 0; }
                string command = @"UPDATE USER SET 
                                        profileId      = @profileId,
                                        firstName      = @firstName, 
                                        lastName       = @lastName, 
                                        phone          = @phone, 
                                        email          = @email, 
                                        password       = @password, 
                                        countryId      = @countryId, 
                                        state          = @state, 
                                        city           = @city, 
                                        access         = @access, 
                                        attempts       = @attempts, 
                                        blocked        = @blocked,
                                        imgTypeProfile = @imgTypeProfile,
                                        imgTypeCover   = @imgTypeCover, 
                                        register       = @register
                                        WHERE ID       = @id";
                var result = await conn.ExecuteAsync(sql: command, param: user);
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
                string command = @"DELETE FROM USER WHERE id = @id";
                var resultado = await conn.ExecuteAsync(sql: command, param: new { id });
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<User>> Get(PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT U.*, 'split', P.*, 'split', A.*, 'split', C.*
                                FROM User U  
                                INNER JOIN Profile P ON U.profileId = P.id
                                INNER JOIN Account A ON P.AccountId = A.id
                                INNER JOIN Country C ON U.CountryId = C.id ";
                if (term != ""){
                    query = query + "AND (U.firstName LIKE '%" + term + "%' " +
                                    "OR   U.lastName  LIKE '%" + term + "%' " +
                                    "OR   P.name      LIKE '%" + term + "%' " +
                                    "OR   A.company   LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<User, Profile, Account, Country, User>(
                    sql: query,
                    map: (user, profile, account, country) => {
                        profile.Account = account;
                        user.Profile = profile;
                        user.Country = country;
                        return user;
                    },
                    splitOn: "split",
                    param: new { });
                return await PageList<User>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<User>> GetByAccount(int accountId, PageParams pageParams)
        {
            try
            {
                var conn = _db.Connection;
                var term         = pageParams.Term;
                var orderField   = pageParams.OrderField;
                var orderReverse = pageParams.OrderReverse;
                string query = @"SELECT U.*, 'split', P.*, 'split', A.*, 'split', C.*
                                FROM User U  
                                INNER JOIN Profile P ON U.profileId = P.id
                                INNER JOIN Account A ON P.AccountId = A.id
                                INNER JOIN Country C ON U.CountryId = C.id 
                                WHERE A.id = @accountId "; 
                if (term != ""){
                    query = query + "AND (U.firstName LIKE '%" + term + "%' " +
                                    "OR   U.lastName  LIKE '%" + term + "%' " +
                                    "OR   P.name      LIKE '%" + term + "%' " +
                                    "OR   A.company   LIKE '%" + term + "%') ";
                }
                if (orderField != ""){
                    query = query + " ORDER BY " + orderField;
                    if (orderReverse) {
                        query = query + " DESC ";
                    }
                }
                var res = await conn.QueryAsync<User, Profile, Account, Country, User>(
                    sql: query,
                    map: (user, profile, account, country) => {
                        profile.Account = account;
                        user.Profile = profile;
                        user.Country = country;
                        return user;
                    },
                    splitOn: "split",
                    param: new { accountId });
                return await PageList<User>.CreateAsync(res, pageParams.PageNumber, pageParams.pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetById(int id)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT U.*, 'split', P.*, 'split', A.*, 'split', C.*
                                FROM User U  
                                INNER JOIN Profile P ON U.profileId = P.id
                                INNER JOIN Account A ON P.AccountId = A.id
                                INNER JOIN Country C ON U.CountryId = C.id  
                                WHERE U.ID = @id";
                var res = await conn.QueryAsync<User, Profile, Account, Country, User>(
                    sql: query,
                    map: (user, profile, account, country) => {
                        profile.Account = account;
                        user.Profile = profile;
                        user.Country = country;
                        return user;
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

        public async Task<User> GetByEmail(string email)
        {
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT U.*, 'split', P.*, 'split', A.*, 'split', C.*
                                FROM User U  
                                INNER JOIN Profile P ON U.profileId = P.id
                                INNER JOIN Account A ON P.AccountId = A.id
                                INNER JOIN Country C ON U.CountryId = C.id  
                                WHERE U.email = @email";
                var res = await conn.QueryAsync<User, Profile, Account, Country, User>(
                    sql: query,
                    map: (user, profile, account, country) => {
                        profile.Account = account;
                        user.Profile = profile;
                        user.Country = country;
                        return user;
                    },
                    splitOn: "split",
                    param: new { email });
                if (res.Count() == 0) return null;
                return res.First();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> Login(string email, string password){
            try
            {
                var conn = _db.Connection;
                string query = @"SELECT U.*, 'split', P.*, 'split', A.*, 'split', C.*
                                FROM User U  
                                INNER JOIN Profile P ON U.profileId = P.id
                                INNER JOIN Account A ON P.AccountId = A.id
                                INNER JOIN Country C ON U.CountryId = C.id  
                                WHERE U.email    = @email
                                AND   U.password = @password";
                var res = await conn.QueryAsync<User, Profile, Account, Country, User>(
                    sql: query,
                    map: (user, profile, account, country) => {
                        profile.Account = account;
                        user.Profile = profile;
                        user.Country = country;
                        return user;
                    },
                    splitOn: "split",
                    param: new { email, password });
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

                