using System.Collections.Generic;
using UserProfiles.Api.Models.Entities;
using UserProfiles.Api.Models.Requests;

namespace UserProfiles.Api.Repository
{
    public interface IMerchantRepository
    {
        List<Merchant> Get();

        Merchant GetById(int id);

        void Update(UpdateAccountRequest request);
    }
}
