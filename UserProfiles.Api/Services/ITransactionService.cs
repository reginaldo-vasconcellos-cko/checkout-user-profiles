using System.Collections.Generic;
using UserProfiles.Api.Models.Entities;

namespace UserProfiles.Api.Services
{
    public interface ITransactionService
    {
        List<Transaction> Get(int userId);

        List<Transaction> GetByMerchantId(int id, int userId);

        List<Transaction> GetByBusinessId(int id);
    }
}
