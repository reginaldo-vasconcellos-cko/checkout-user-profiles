using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using UserProfiles.Common.Models.Entities;

namespace UserProfiles.Data.Repository
{
    public class ResourceIdentityRepository : IResourceIdentityRepository
    {
        private string _connectionString;

        public ResourceIdentityRepository() => _connectionString = "Server=.;Database=Hub;Trusted_Connection=True;";

        public IDbConnection Connection => new SqlConnection(_connectionString);
        
        public async Task<IEnumerable<ResourceIdentityDto>> ListAsync()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select g.Id, 
	                            g.IdentityType as [Type],
	                            isnull(h.[Name], i.[Name]) as [Name] 
                                from [Hub].[dbo].[ResourceIdentity] g 
                                left join [Hub].[dbo].[Merchant] h on g.IdentityType = 1 and h.Id = g.IdentityId
                                left join [Hub].[dbo].[Business] i on g.IdentityType = 2 and i.Id = g.IdentityId";

                dbConnection.Open();

                var result = await dbConnection.QueryAsync<ResourceIdentityDto>(sQuery);

                return result;
            }
        }
    }
}
