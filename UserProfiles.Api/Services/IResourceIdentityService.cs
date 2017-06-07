using System.Collections.Generic;
using System.Threading.Tasks;
using UserProfiles.Common.Models.Entities;

namespace UserProfiles.Api.Services
{
    public interface IResourceIdentityService
    {
        Task<IEnumerable<ResourceIdentityDto>> ListAsync();
    }
}
