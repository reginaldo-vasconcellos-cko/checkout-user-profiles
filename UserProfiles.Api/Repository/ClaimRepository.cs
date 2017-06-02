using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using UserProfiles.Api.Models.Entities;

namespace UserProfiles.Api.Repository
{
    public class ClaimRepository : IClaimRepository
    {
        private string _connectionString;

        public ClaimRepository() => _connectionString = Startup.IdentityConnectionString;

        public IDbConnection Connection => new SqlConnection(_connectionString);

        public async Task CreateAsync(ClaimBase claim)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"insert into [dbo].[Claims] ([Type], [Value]) values (@Type, @Value)";

                dbConnection.Open();

                await dbConnection.ExecuteAsync(sQuery, claim);
            }
        }

        public async Task<IEnumerable<ClaimBase>> ListAsync()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select [Id], [Type], [Value] from [dbo].[Claims]";

                dbConnection.Open();

                var result = await dbConnection.QueryAsync<ClaimBase>(sQuery);

                return result;
            }
        }
    }
}
