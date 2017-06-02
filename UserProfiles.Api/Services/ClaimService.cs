using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserProfiles.Api.Models.Entities;
using UserProfiles.Api.Repository;

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

        public async Task<IEnumerable<ClaimBase>> ListAsync()
        {
            return await _claimRepository.ListAsync();
        }
    }
}
