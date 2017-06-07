using System.Collections.Generic;
using System.Threading.Tasks;
using UserProfiles.Common.Models.Entities;

namespace UserProfiles.Data.Repository
{
    public interface IResourceIdentityRepository
    {
        Task<IEnumerable<ResourceIdentityDto>> ListAsync();
    }
}
