using System.Collections.Generic;
using System.Threading.Tasks;
using UserProfiles.Common.Models.Entities;
using UserProfiles.Data.Repository;

namespace UserProfiles.Api.Services
{
    public class ResourceIdentityService : IResourceIdentityService
    {
        private readonly IResourceIdentityRepository _resourceIdentityRepository;

        public ResourceIdentityService(IResourceIdentityRepository resourceIdentityRepository)
        {
            _resourceIdentityRepository = resourceIdentityRepository;
        }

        public async Task<IEnumerable<ResourceIdentityDto>> ListAsync()
        {
            return await _resourceIdentityRepository.ListAsync();
        }
    }
}
