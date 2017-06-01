using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using UserProfiles.Api.Models.Entities;
using UserProfiles.Api.Models.Enums;
using UserProfiles.Api.Models.Requests;
using UserProfiles.Api.Repository;
using UserProfiles.Api.Security.Attributes;
using UserProfiles.Api.Security.Requirements;
using UserProfiles.Api.Services;

namespace UserProfiles.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class MerchantController : Controller
    {
        private readonly IMerchantService _merchantService;
        private readonly IAuthorizationService _authorizationService;

        public MerchantController(IAuthorizationService authorizationService, 
            IMerchantService merchantService)
        {
            _authorizationService = authorizationService;
            _merchantService = merchantService;
        }

        [HttpGet]
        [RequirePermission("merchant.list")]
        public async Task<IActionResult> Get()
        {
            return Ok(_merchantService.Get());
        }

        [HttpGet("{id}")]
        [RequirePermission("merchant.get")]
        public async Task<IActionResult> Get(int id)
        {
            if (!await _authorizationService.AuthorizeAsync(User, null, new ResourceAccessRequirement(id, IdentityType.Merchant)))
                return new ChallengeResult();

            return Ok(_merchantService.GetById(id));
        }

        [HttpPut]
        [RequirePermission("merchant.update")]
        public async Task<IActionResult> Update([FromBody]UpdateAccountRequest request)
        {
            if (!await _authorizationService.AuthorizeAsync(User, null, new ResourceAccessRequirement(request.Id, IdentityType.Merchant)))
                return new ChallengeResult();

            _merchantService.Update(request);

            return NoContent();
        }
    }
}
