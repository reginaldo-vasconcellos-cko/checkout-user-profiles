using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using UserProfiles.Common.Models.Entities;
using UserProfiles.Common.Models.Requests;

namespace UserProfiles.Data.Repository
{
    public class UserResourceIdentityRepository : IUserResourceIdentityRepository
    {
        private string _connectionString;

        public UserResourceIdentityRepository() => _connectionString = "Server=.;Database=Hub;Trusted_Connection=True;";

        public IDbConnection Connection => new SqlConnection(_connectionString);

        public bool VerifyUserResourceIdentityPermission(VerifyUserResouceIdentityPermissionRequest request)
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

        public void InsertUserResourceIdentity(int userId, int resourceId)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();

                    dbConnection.Execute("InsertUserResouceIdentity",
                        new { UserId = userId, ResourceIdentityId = resourceId },
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ResetUserResources(int userId)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();

                    dbConnection.Execute("delete from [dbo].[UserResourceIdentity] where [UserId] = @UserId", new { UserId = userId });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
