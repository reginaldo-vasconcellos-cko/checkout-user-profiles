using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserProfiles.Api.Models.Entities;

namespace UserProfiles.Api.Services
{
    public interface IClaimService
    {
        Task CreateAsync(ClaimBase claim);

        Task<IEnumerable<ClaimBase>> ListAsync();
    }
}
