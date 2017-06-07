using System.Collections.Generic;
using UserProfiles.Common.Models.Entities;
using UserProfiles.Common.Models.Requests;

namespace UserProfiles.Data.Repository
{
    public interface IMerchantRepository
    {
        List<Merchant> Get();

        Merchant GetById(int id);

        void Update(UpdateAccountRequest request);
    }
}
