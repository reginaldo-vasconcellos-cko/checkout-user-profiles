using System.Collections.Generic;
using UserProfiles.Common.Models.Entities;
using UserProfiles.Data.Repository;

namespace UserProfiles.Api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public List<Transaction> Get(int userId)
        {
            return _transactionRepository.Get(userId);
        }

        public List<Transaction> GetByMerchantId(int id, int userId)
        {
            return _transactionRepository.GetByMerchantId(id, userId);
        }

        public List<Transaction> GetByBusinessId(int id)
        {
            return _transactionRepository.GetByBusinessId(id);
        }
    }
}
