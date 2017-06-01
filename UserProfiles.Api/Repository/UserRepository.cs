using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using UserProfiles.Api.Models.Entities;
using UserProfiles.Api.Models.Responses;

namespace UserProfiles.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private string _connectionString;

        public UserRepository() => _connectionString = Startup.ConnectionString;

        public IDbConnection Connection => new SqlConnection(_connectionString);

        public void Add(User user)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    string sQuery = "INSERT INTO [dbo].[User] (GuidRef)"
                                    + " VALUES(@GuidRef)";

                    dbConnection.Open();
                    dbConnection.Execute(sQuery, user);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public void Update(User user)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    string sQuery = "UPDATE [dbo].[User] set GuidRef = @GuidRef " +
                                    "where Id = @Id";

                    dbConnection.Open();
                    dbConnection.Execute(sQuery, user);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public GetUserPermissionsResponse GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select u.Id, a.UserName, " //User
                                + "c.[Name], d.ClaimType, d.ClaimValue, " //Role
                                + "e.ClaimType as Type, e.ClaimValue as Value " //Claim
                                + "from [Hub].[dbo].[User] u inner join [Hub.Identity].[dbo].[AspNetUsers] a on u.GuidRef = a.Id collate SQL_Latin1_General_CP1_CI_AS "
                                + "left join[Hub.Identity].[dbo].[AspNetUserRoles] b on a.id = b.UserId "
                                + "left join[Hub.Identity].[dbo].[AspNetRoles] c on b.RoleId = c.Id "
                                + "left join[Hub.Identity].[dbo].[AspNetRoleClaims] d on c.Id = d.RoleId "
                                + "left join[Hub.Identity].[dbo].[AspNetUserClaims] e on a.id = e.UserId"
                                + " WHERE u.Id = " +  id;

                dbConnection.Open();

                var lookup = new Dictionary<int, GetUserPermissionsResponse>();

                dbConnection
                    .Query<GetUserPermissionsResponse, Role, ClaimBase, GetUserPermissionsResponse>(sQuery,
                        (user, role, claim) =>
                        {
                            GetUserPermissionsResponse response;

                            if (!lookup.TryGetValue(user.Id, out response))
                                lookup.Add(user.Id, response = user);

                            if (response.Roles == null)
                                response.Roles = new List<Role>();

                            if(!response.Roles.Contains(role))
                                response.Roles.Add(role);

                            if(response.Claims == null)
                                response.Claims = new List<ClaimBase>();

                            if (!response.Claims.Contains(claim))
                                response.Claims.Add(claim);

                            return response;
                        }, splitOn: "Name,Type").AsQueryable();

                return lookup.Values.FirstOrDefault();
            }
        }

        public User GetByRefId(string id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select a.Id, a.GuidRef from [Hub].[dbo].[User] a inner join [Hub.Identity].[dbo].[AspNetUsers] b on a.GuidRef = b.Id collate SQL_Latin1_General_CP1_CI_AS"
                                + " where b.Id = @Id";

                dbConnection.Open();

                var result = dbConnection.Query<User>(sQuery, new { Id = id}).FirstOrDefault();

                return result;
            }
        }
    }
}
