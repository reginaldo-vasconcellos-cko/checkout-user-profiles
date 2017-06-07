using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using UserProfiles.Common.Models.Entities;

namespace UserProfiles.Data.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private string _connectionString;

        public TransactionRepository() => _connectionString = "Server=.;Database=Hub;Trusted_Connection=True;";

        public IDbConnection Connection => new SqlConnection(_connectionString);

        public List<Transaction> Get(int userId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select a.Id, TransactionDate, Amount, Currency, a.MerchantId as [Id], f.[Name], BusinessId as [Id], g.[Name]
                                from [dbo].[Transaction] a
                                inner join [dbo].[ResourceIdentity] d on d.IdentityType = 2 and a.BusinessId = d.IdentityId
                                inner join [dbo].[UserResourceIdentity] e on e.ResourceIdentityId = d.Id
								inner join [dbo].[Merchant] f on f.Id = a.MerchantId
								inner join [dbo].[Business] g on g.Id = a.BusinessId
                                where userId = @UserId";

                dbConnection.Open();

                var result = dbConnection
                    .Query<Transaction, Merchant, Business, Transaction>(sQuery,
                        (transaction, merchant, business) =>
                        {
                            transaction.Merchant = merchant;
                            business.MerchantId = merchant.Id;
                            transaction.Business = business;

                            return transaction;
                        }, new { UserId = userId }).ToList();

                return result;
            }
        }

        public List<Transaction> GetByMerchantId(int merchantId, int userId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select a.Id, TransactionDate, Amount, Currency, a.MerchantId as [Id], f.[Name], BusinessId as [Id], g.[Name]
                                from [dbo].[Transaction] a
                                inner join [dbo].[ResourceIdentity] d on d.IdentityType = 2 and a.BusinessId = d.IdentityId
                                inner join [dbo].[UserResourceIdentity] e on e.ResourceIdentityId = d.Id
								inner join [dbo].[Merchant] f on f.Id = a.MerchantId
								inner join [dbo].[Business] g on g.Id = a.BusinessId
                                where a.MerchantId = @MerchantId and userId = @UserId";

                dbConnection.Open();

                var result = dbConnection
                    .Query<Transaction, Merchant, Business, Transaction>(sQuery,
                        (transaction, merchant, business) =>
                        {
                            transaction.Merchant = merchant;
                            business.MerchantId = merchant.Id;
                            transaction.Business = business;

                            return transaction;
                        }, new { MerchantId = merchantId, UserId = userId }).ToList();

                return result;
            }
        }

        public List<Transaction> GetByBusinessId(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"select a.Id, TransactionDate, Amount, Currency, a.MerchantId as [Id], f.[Name], BusinessId as [Id], g.[Name]
                                from [dbo].[Transaction] a
								inner join [dbo].[Merchant] f on f.Id = a.MerchantId
								inner join [dbo].[Business] g on g.Id = a.BusinessId
                                where BusinessId = @BusinessId";

                dbConnection.Open();

                var result = dbConnection
                    .Query<Transaction, Merchant, Business, Transaction>(sQuery,
                        (transaction, merchant, business) =>
                        {
                            transaction.Merchant = merchant;
                            business.MerchantId = merchant.Id;
                            transaction.Business = business;

                            return transaction;
                        }, new { BusinessId = id}).ToList();

                return result;
            }
        }
    }
}
