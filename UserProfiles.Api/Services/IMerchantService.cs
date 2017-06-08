using System.Collections.Generic;
using UserProfiles.Common.Models.Entities;
using UserProfiles.Common.Models.Requests;

namespace UserProfiles.Api.Services
{
    public interface IMerchantService
    {
        List<Merchant> Get();

        Merchant GetById(int id);

        void Update(UpdateAccountRequest request);
    }
}
