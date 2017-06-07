using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using UserProfiles.Common.Models.Entities;
using UserProfiles.Common.Models.Requests;

namespace UserProfiles.Data.Repository
{
    public class MerchantRepository : IMerchantRepository
    {
        private string _connectionString;

        public MerchantRepository() => _connectionString = "Server=.;Database=Hub;Trusted_Connection=True;";

        public IDbConnection Connection => new SqlConnection(_connectionString);

        public List<Merchant> Get()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select Id, Name from [dbo].[Merchant]";

                dbConnection.Open();

                var result = dbConnection.Query<Merchant>(sQuery).ToList();

                return result;
            }
        }

        public Merchant GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select Id, Name from [dbo].[Merchant] where Id = @Id";

                dbConnection.Open();

                var result = dbConnection.Query<Merchant>(sQuery, new { Id = id }).FirstOrDefault();

                return result;
            }
        }

        public void Update(UpdateAccountRequest request)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"update [dbo].[Merchant] set [Name] = @Name where Id = @Id";

                dbConnection.Open();

                dbConnection.Execute(sQuery, request);
            }
        }
    }
}
