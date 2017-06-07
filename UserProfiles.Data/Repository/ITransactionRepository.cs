using System.Collections.Generic;
using UserProfiles.Common.Models.Entities;

namespace UserProfiles.Data.Repository
{
    public interface ITransactionRepository
    {
        List<Transaction> Get(int userId);
                                    
        List<Transaction> GetByMerchantId(int id, int userId);

        List<Transaction> GetByBusinessId(int id);
    }
}
