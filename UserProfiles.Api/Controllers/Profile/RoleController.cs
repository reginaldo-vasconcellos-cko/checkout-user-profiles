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

namespace UserProfiles.Api.Controllers.Profile
{
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateRoleRequest request)
        {
            if (await _roleManager.RoleExistsAsync(request.Role))
            {
                ModelState.AddModelError("", "Role already exists!");

                return BadRequest(ModelState);
            }

            var role = new IdentityRole { Name = request.Role };

            _roleManager.CreateAsync(role).Wait();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AssignClaim([FromBody] AssignClaimToRoleRequest request)
        {
            var role = await _roleManager.FindByNameAsync(request.Role);

            await _roleManager.AddClaimAsync(role, new Claim(request.Claim.Type, request.Claim.Value));

            return Ok();
        }
    }
}
