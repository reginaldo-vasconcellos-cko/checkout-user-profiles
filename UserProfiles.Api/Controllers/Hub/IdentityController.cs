using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserProfiles.Api.Security.Attributes;
using UserProfiles.Api.Services;

namespace UserProfiles.Api.Controllers.Hub
{
    [Produces("application/json")]
    public class IdentityController : Controller
    {
        private readonly IUserService _userService;

        public IdentityController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        [RequirePermission("identity.get")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _userService.GetDetailsByIdAsync(id));
        }

        [HttpGet]
        //[RequirePermission("identity.getRoles")]
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