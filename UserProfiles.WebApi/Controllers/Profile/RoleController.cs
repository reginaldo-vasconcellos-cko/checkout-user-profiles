using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserProfiles.Api.Services;
using UserProfiles.Common.Models.Requests;
using UserProfiles.Security.Attributes;

namespace UserProfiles.WebApi.Controllers.Profile
{
    [Route("api/[controller]/[action]")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        [RequirePermission("role.create")]
        public async Task<IActionResult> Create([FromBody]CreateRoleRequest request)
        {
            if (await _roleService.VerifyExistsAsync(request.Role))
            {
                ModelState.AddModelError("", "Role already exists!");

                return BadRequest(ModelState);
            }

            await _roleService.CreateAsync(request.Role);

            return Ok();
        }

        [HttpGet]
        [RequirePermission("role.get")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _roleService.GetAsync());
        }

        [HttpPost]
        [RequirePermission("role.assignClaim")]
        public async Task<IActionResult> AssignClaim([FromBody] AssignClaimToRoleRequest request)
        {
            await _roleService.AssignClaimAsync(request);

            return Ok();
        }
    }
}
