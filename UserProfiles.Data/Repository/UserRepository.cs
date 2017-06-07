using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using UserProfiles.Common.Models.Entities;
using UserProfiles.Common.Models.Responses;

namespace UserProfiles.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private string _connectionString;

        public UserRepository() => _connectionString = "Server=.;Database=Hub;Trusted_Connection=True;";

        public IDbConnection Connection => new SqlConnection(_connectionString);

        public async Task<int> Add(User user)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    string sQuery = @"INSERT INTO [dbo].[User] (GuidRef)
                                    VALUES(@GuidRef); select @@identity";

                    dbConnection.Open();

                    return await dbConnection.ExecuteScalarAsync<int>(sQuery, user);
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

        public async Task<List<GetUserPermissionsResponse>> GetDetailsByIdAsync(int? id = null)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select u.Id, a.UserName, a.Email,
                                c.[Name], d.ClaimType, d.ClaimValue, 
                                e.ClaimType as Type, e.ClaimValue as Value 
                                from [Hub].[dbo].[User] u inner join [Hub.Identity].[dbo].[AspNetUsers] a on u.GuidRef = a.Id collate SQL_Latin1_General_CP1_CI_AS 
                                left join[Hub.Identity].[dbo].[AspNetUserRoles] b on a.id = b.UserId 
                                left join[Hub.Identity].[dbo].[AspNetRoles] c on b.RoleId = c.Id 
                                left join[Hub.Identity].[dbo].[AspNetRoleClaims] d on c.Id = d.RoleId 
                                left join[Hub.Identity].[dbo].[AspNetUserClaims] e on a.id = e.UserId ";

                if (id.HasValue)
                    sQuery += "where u.Id = " + id;

                dbConnection.Open();

                var lookup = new Dictionary<int, GetUserPermissionsResponse>();

                await dbConnection
                    .QueryAsync<GetUserPermissionsResponse, RoleBase, ClaimBase, GetUserPermissionsResponse>(sQuery,
                        (user, role, claim) =>
                        {
                            GetUserPermissionsResponse response;

                            if (!lookup.TryGetValue(user.Id, out response))
                                lookup.Add(user.Id, response = user);

                            if (role != null)
                            {
                                if (response.Roles == null)
                                    response.Roles = new List<RoleDto>();

                                if (!response.Roles.Any(c => c.Name.Equals(role.Name)))
                                    response.Roles.Add(new RoleDto
                                    {
                                        Name = role.Name,
                                        Claims = new List<ClaimBase>
                                        {
                                            new ClaimBase {Type = role.ClaimType, Value = role.ClaimValue}
                                        }
                                    });
                                else
                                {
                                    var editRole = response.Roles.Find(c => c.Name.Equals(role.Name));

                                    if (!string.IsNullOrEmpty(role.ClaimValue) && !editRole.Claims.Any(c => c.Type.Equals(role.ClaimType) &&
                                                                  c.Value.Equals(role.ClaimValue)))
                                    {
                                        editRole.Claims.Add(new ClaimBase
                                        {
                                            Type = role.ClaimType,
                                            Value = role.ClaimValue
                                        });
                                    }
                                }
                            }

                            if (claim != null)
                            {
                                if (response.Claims == null)
                                    response.Claims = new List<ClaimBase>();

                                if (!response.Claims.Any(c => c.Type.Equals(claim.Type) && c.Value.Equals(claim.Value)))
                                    response.Claims.Add(claim);
                            }

                            return response;
                        }, splitOn: "Name,Type");

                var userDetailsList = lookup.Values.ToList();

                string sResourceAccessQuery = @"select u.Id, g.Id,
		                    case IdentityType when 1 then 'Merchant' else 'Business' end as [Type],
		                    isnull(h.Name, i.Name) as Name
                            from [Hub].[dbo].[User] u
                            inner join [Hub.Identity].[dbo].[AspNetUsers] a on u.GuidRef = a.Id collate SQL_Latin1_General_CP1_CI_AS
                            inner join [Hub].[dbo].[UserResourceIdentity] f on u.Id = f.UserId
                            inner join [Hub].[dbo].[ResourceIdentity] g on f.ResourceIdentityId = g.Id
                            left join [Hub].[dbo].[Merchant] h on g.IdentityType = 1 and h.Id = g.IdentityId
                            left join [Hub].[dbo].[Business] i on g.IdentityType = 2 and i.Id = g.IdentityId ";

                if (id.HasValue)
                    sResourceAccessQuery += "where u.Id = " + id;

                await dbConnection
                    .QueryAsync<int, ResourceIdentityDto, ResourceIdentityDto>(sResourceAccessQuery,
                        (userId, access) =>
                        {
                            var userDetails = userDetailsList.Find(c => c.Id == userId);

                            if (userDetails.ResourceAccesses == null)
                                userDetails.ResourceAccesses = new List<ResourceIdentityDto>();

                            userDetails.ResourceAccesses.Add(access);

                            return access;
                        });

                return userDetailsList;
            }
        }

        public User GetByRefId(string id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select a.Id, a.GuidRef from [Hub].[dbo].[User] a inner join [Hub.Identity].[dbo].[AspNetUsers] b on a.GuidRef = b.Id collate SQL_Latin1_General_CP1_CI_AS"
                                + " where b.Id = @Id";

                dbConnection.Open();

                var result = dbConnection.Query<User>(sQuery, new { Id = id }).FirstOrDefault();

                return result;
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select Id, GuidRef from [Hub].[dbo].[User] where Id = @Id";

                dbConnection.Open();

                var result = await dbConnection.QueryAsync<User>(sQuery, new { Id = id });

                return result.FirstOrDefault();
            }
        }
    }
}
