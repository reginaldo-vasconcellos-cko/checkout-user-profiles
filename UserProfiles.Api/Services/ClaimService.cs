using System.Collections.Generic;
using System.Threading.Tasks;
using UserProfiles.Common.Models.Entities;
using UserProfiles.Data.Repository;

namespace UserProfiles.Api.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _claimRepository;

        public ClaimService(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task CreateAsync(ClaimBase claim)
        {
            await _claimRepository.CreateAsync(claim);
        }

        public async Task EditAsync(ClaimBase claim)
        {
            await _claimRepository.EditAsync(claim);
        }

        public async Task<IEnumerable<ClaimBase>> ListAsync()
        {
            return await _claimRepository.ListAsync();
        }

        public async Task<ClaimBase> GetByIdAsync(int id)
        {
            return await _claimRepository.GetByIdAsync(id);
        }
    }
}
