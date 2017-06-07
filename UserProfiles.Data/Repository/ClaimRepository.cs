using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using UserProfiles.Common.Models.Entities;

namespace UserProfiles.Data.Repository
{
    public class ClaimRepository : IClaimRepository
    {
        private string _connectionString;

        public ClaimRepository() => _connectionString = "Server=.;Database=Hub.Identity;Trusted_Connection=True;";

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

        public async Task EditAsync(ClaimBase claim)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"update [dbo].[Claims] set [Type] = @Type, [Value] = @Value where Id = @Id";

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

        public async Task<ClaimBase> GetByIdAsync(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select [Id], [Type], [Value] from [dbo].[Claims] where Id = @Id";

                dbConnection.Open();

                var result = await dbConnection.QueryAsync<ClaimBase>(sQuery, new { Id = id });

                return result.FirstOrDefault();
            }
        }
    }
}
