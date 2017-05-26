using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using UserProfiles.Api.Models.Entities;

namespace UserProfiles.Api.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private string _connectionString;

        public TransactionRepository() => _connectionString = Startup.ConnectionString;

        public IDbConnection Connection => new SqlConnection(_connectionString);

        public List<Transaction> Get(int userId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select a.Id, MerchantId, BusinessId, TransactionDate, Amount, Currency
                                from [dbo].[Transaction] a
                                inner join [dbo].[ResourceIdentity] d on d.IdentityType = 2 and a.BusinessId = d.IdentityId
                                inner join [dbo].[UserResourceIdentity] e on e.ResourceIdentityId = d.Id
                                where userId = @UserId";

                dbConnection.Open();

                var result = dbConnection.Query<Transaction>(sQuery, new { UserId = userId }).ToList();

                return result;
            }
        }

        public List<Transaction> GetByMerchantId(int merchantId, int userId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select a.Id, MerchantId, BusinessId, TransactionDate, Amount, Currency
                                from [dbo].[Transaction] a
                                inner join [dbo].[ResourceIdentity] d on d.IdentityType = 2 and a.BusinessId = d.IdentityId
                                inner join [dbo].[UserResourceIdentity] e on e.ResourceIdentityId = d.Id
                                where a.MerchantId = @MerchantId and userId = @UserId";

                dbConnection.Open();

                var result = dbConnection.Query<Transaction>(sQuery, new { MerchantId = merchantId, UserId = userId }).ToList();

                return result;
            }
        }

        public List<Transaction> GetByBusinessId(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select Id, MerchantId, BusinessId, TransactionDate, Amount, Currency from [dbo].[Transaction] where BusinessId = @Id";

                dbConnection.Open();

                var result = dbConnection.Query<Transaction>(sQuery, new { Id = id }).ToList();

                return result;
            }
        }
    }
}
