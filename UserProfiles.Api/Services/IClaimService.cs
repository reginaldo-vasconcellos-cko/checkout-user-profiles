using System.Collections.Generic;
using System.Threading.Tasks;
using UserProfiles.Common.Models.Entities;

namespace UserProfiles.Api.Services
{
    public interface IClaimService
    {
        Task CreateAsync(ClaimBase claim);

        Task EditAsync(ClaimBase claim);

        Task<IEnumerable<ClaimBase>> ListAsync();

        Task<ClaimBase> GetByIdAsync(int id);
    }
}
