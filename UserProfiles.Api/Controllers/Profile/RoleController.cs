using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using UserProfiles.Api.Models;
using UserProfiles.Api.Models.Requests;
using UserProfiles.Api.Security.Attributes;
using UserProfiles.Api.Services;

namespace UserProfiles.Api.Controllers.Profile
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
