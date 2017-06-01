using System.Collections.Generic;
using UserProfiles.Api.Models.Entities;
using UserProfiles.Api.Models.Requests;
using UserProfiles.Api.Repository;

namespace UserProfiles.Api.Services
{
    public class MerchantService : IMerchantService
    {
        private readonly IMerchantRepository _merchantRepository;

        public MerchantService(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        public List<Merchant> Get()
        {
            return _merchantRepository.Get();
        }

        public Merchant GetById(int id)
        {
            return _merchantRepository.GetById(id);
        }

        public void Update(UpdateAccountRequest request)
        {
            _merchantRepository.Update(request);
        }
    }
}
