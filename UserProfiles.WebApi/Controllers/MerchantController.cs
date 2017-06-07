using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserProfiles.Api.Services;
using UserProfiles.Common.Models.Enums;
using UserProfiles.Common.Models.Requests;
using UserProfiles.Security.Attributes;
using UserProfiles.Security.Requirements;

namespace UserProfiles.WebApi.Controllers
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
