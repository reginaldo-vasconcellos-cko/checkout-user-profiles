using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserProfiles.Api.Services;
using UserProfiles.Security.Attributes;

namespace UserProfiles.WebApi.Controllers.Hub
{
    [Produces("application/json")]
    public class IdentityController : Controller
    {
        private readonly IUserService _userService;

        public IdentityController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("api/identity/{id}")]
        [RequirePermission("identity.list")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _userService.GetDetailsByIdAsync(id));
        }

        [HttpGet]
        [RequirePermission("identity.getRoles")]
        [Route("api/identity/{id}/roles")]
        public async Task<IActionResult> GetRoles(int id)
        {
            return Ok(await _userService.GetRolesByIdAsync(id));
        }

        [HttpGet]
        [RequirePermission("identity.getPermissions")]
        [Route("api/identity/{id}/permissions")]
        public async Task<IActionResult> GetPermissions(int id)
        {
            return Ok(await _userService.GetClaimsByIdAsync(id));
        }
    }
}