using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using UserProfiles.Api.Models.Entities;
using UserProfiles.Api.Models.Requests;
using UserProfiles.Api.Models.Responses;

namespace UserProfiles.Api.Repository
{
    public class UserResouceIdentityRepository : IUserResouceIdentityRepository
    {
        private string _connectionString;

        public UserResouceIdentityRepository() => _connectionString = Startup.ConnectionString;

        public IDbConnection Connection => new SqlConnection(_connectionString);

        public bool VerifyUserResouceIdentityPermission(VerifyUserResouceIdentityPermissionRequest request)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();

                    var result = dbConnection.Query<UserResourceIdentity>("VerifyUserResouceIdentityPermission", 
                                new { UserId = request.UserId, IdentityType = request.IdentityType, IdentityId = request.IdentityId },
                                commandType: CommandType.StoredProcedure).FirstOrDefault();

                    return result != null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async void InsertUserResouceIdentity(AssignResourceToUserRequest request)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();

                    await dbConnection.ExecuteAsync("InsertUserResouceIdentity",
                        new { UserId = request.UserId, IdentityType = request.IdentityType, IdentityId = request.IdentityId },
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
