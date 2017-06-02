using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserProfiles.Api.Models.Entities;
using UserProfiles.Api.Security.Attributes;
using UserProfiles.Api.Services;

namespace UserProfiles.Api.Controllers.Profile
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ClaimController : Controller
    {
        private readonly IClaimService _claimService;

        public ClaimController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        [HttpPost]
        [RequirePermission("claim.create")]
        public async Task<IActionResult> Create([FromBody]ClaimBase claim)
        {
            await _claimService.CreateAsync(claim);

            return Ok();
        }

        [HttpGet]
        [RequirePermission("claim.list")]
        public async Task<IActionResult> List()
        {
            return Ok(await _claimService.ListAsync());
        }
    }
}