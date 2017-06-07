using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserProfiles.Api.Services;
using UserProfiles.Common.Models.Entities;
using UserProfiles.Mvc.Models;
using UserProfiles.Security.Attributes;

namespace UserProfiles.Mvc.Controllers
{
    [Route("api/[controller]/[action]")]
    public class RolesController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IClaimService _claimService;
        private readonly IMapper _mapper;

        public RolesController(IRoleService roleService, 
            IMapper mapper, 
            IClaimService claimService)
        {
            _roleService = roleService;
            _mapper = mapper;
            _claimService = claimService;
        }

        // GET: Roles
        [HttpGet]
        [RequirePermission("role.list")]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAsync();

            return View(_mapper.Map<List<RoleDto>, List<Role>>(roles));
        }

        // GET: Roles/Details/5
        [HttpGet]
        [RequirePermission("role.details")]
        public async Task<IActionResult> Details(string name)
        {
            if (name == null)
            {
                return NotFound();
            }

            var roles = await _roleService.GetAsync();

            var roleDto = roles.First(r => r.Name.Equals(name));

            if (roleDto == null)
                return NotFound();

            var role = _mapper.Map<RoleDto, Role>(roleDto);

            role.SelectedClaims = role.Claims.Select(c => c.Value).ToArray();

            var claims = await _claimService.ListAsync();

            ViewBag.PossibleClaims = _mapper.Map<List<ClaimBase>, List<Claim>>(claims.ToList());

            return View(role);
        }

        // GET: Roles/Create
        [HttpGet]
        [RequirePermission("role.create")]
        public async Task<IActionResult> Create()
        {
            var claims = await _claimService.ListAsync();

            ViewBag.PossibleClaims = _mapper.Map<List<ClaimBase>, List<Claim>>(claims.ToList());

            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermission("role.create")]
        public async Task<IActionResult> Create([Bind("Name, SelectedClaims")] Role role)
        {
            if (ModelState.IsValid)
            {
                var claims = await _claimService.ListAsync();

                var roleDto = new RoleDto
                {
                    Name = role.Name,
                    Claims = role.SelectedClaims != null ? claims.Where(c => role.SelectedClaims.Contains(c.Value)).ToList() : null,
                };

                await _roleService.CreateAsync(roleDto);

                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Roles/Edit/5
        [HttpGet]
        [RequirePermission("role.edit")]
        public async Task<IActionResult> Edit(string name)
        {
            if (name == null)
            {
                return NotFound();
            }

            var roles = await _roleService.GetAsync();

            var roleDto = roles.First(r => r.Name.Equals(name));

            if (roleDto == null)
                return NotFound();

            var role = _mapper.Map<RoleDto, Role>(roleDto);

            role.SelectedClaims = role.Claims.Select(c => c.Value).ToArray();

            var claims = await _claimService.ListAsync();

            ViewBag.PossibleClaims = _mapper.Map<List<ClaimBase>, List<Claim>>(claims.ToList());

            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermission("role.edit")]
        public async Task<IActionResult> Edit(string name, [Bind("Name, SelectedClaims")] Role role)
        {
            if (name != role.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var claims = await _claimService.ListAsync();

                    var roleDto = new RoleDto
                    {
                        Name = role.Name,
                        Claims = role.SelectedClaims != null ? claims.Where(c => role.SelectedClaims.Contains(c.Value)).ToList() : null,
                    };

                    await _roleService.EditAsync(roleDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await RoleExists(role.Name))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(role);
        }

        private async Task<bool> RoleExists(string name)
        {
            return await _roleService.VerifyExistsAsync(name);
        }
    }
}
