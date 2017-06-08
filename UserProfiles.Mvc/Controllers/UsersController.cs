using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UserProfiles.Api.Services;
using UserProfiles.Common.Models.Entities;
using UserProfiles.Common.Models.Enums;
using UserProfiles.Common.Models.Requests;
using UserProfiles.Mvc.Models;
using UserProfiles.Security.Attributes;
using User = UserProfiles.Mvc.Models.User;

namespace UserProfiles.Mvc.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IClaimService _claimService;
        private readonly IResourceIdentityService _resourceIdentityService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, 
            IRoleService roleService, 
            IClaimService claimService, 
            IMapper mapper, IResourceIdentityService resourceIdentityService)
        {
            _userService = userService;
            _roleService = roleService;
            _claimService = claimService;
            _mapper = mapper;
            _resourceIdentityService = resourceIdentityService;
        }

        // GET: Users
        [HttpGet]
        [RequirePermission("user.list")]
        public async Task<IActionResult> Index()
        {
            var usersDto = await _userService.GetDetailsAsync();

            var users = usersDto.Select(u => new User { Id = u.Id, Name = u.UserName, Email = u.Email });

            return View(users);
        }

        // GET: Users/Details/5
        [HttpGet]
        [RequirePermission("user.details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var userDto = await _userService.GetDetailsByIdAsync(id.Value);

            if (userDto == null)
                return NotFound();

            var user = new User
            {
                Id = userDto.Id,
                Name = userDto.UserName,
                Email = userDto.Email,
                SelectedRoles = userDto.Roles?.Select(r => r.Name).ToArray(),
                SelectedClaims = userDto.Claims?.Select(c => c.Value).ToArray(),
                SelectedResources = userDto.ResourceAccesses?.Select(c => c.Id).ToArray()
            };

            var roles = await _roleService.GetAsync();
            var claims = await _claimService.ListAsync();
            var resources = await _resourceIdentityService.ListAsync();

            ViewBag.PossibleClaims = _mapper.Map<List<ClaimBase>, List<Claim>>(claims.ToList());
            ViewBag.PossibleRoles = _mapper.Map<List<RoleDto>, List<Role>>(roles.ToList());
            ViewBag.PossibleMerchantResources = _mapper.Map<List<ResourceIdentityDto>, List<ResourceIdentity>>(resources.Where(c => c.Type == IdentityType.Merchant).ToList());
            ViewBag.PossibleBusinessResources = _mapper.Map<List<ResourceIdentityDto>, List<ResourceIdentity>>(resources.Where(c => c.Type == IdentityType.Business).ToList());

            return View(user);
        }

        // GET: Users/Create
        [HttpGet]
        [RequirePermission("user.create")]
        public async Task<IActionResult> Create()
        {
            var roles = await _roleService.GetAsync();
            var claims = await _claimService.ListAsync();
            var resources = await _resourceIdentityService.ListAsync();

            ViewBag.PossibleClaims = _mapper.Map<List<ClaimBase>, List<Claim>>(claims.ToList());
            ViewBag.PossibleRoles = _mapper.Map<List<RoleDto>, List<Role>>(roles.ToList());
            ViewBag.PossibleMerchantResources = _mapper.Map<List<ResourceIdentityDto>, List<ResourceIdentity>>(resources.Where(c => c.Type == IdentityType.Merchant).ToList());
            ViewBag.PossibleBusinessResources = _mapper.Map<List<ResourceIdentityDto>, List<ResourceIdentity>>(resources.Where(c => c.Type == IdentityType.Business).ToList());

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermission("user.create")]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,SelectedRoles,SelectedClaims,SelectedResources")] User user)
        {
            if (ModelState.IsValid)
            {
                var claims = await _claimService.ListAsync();
                var roles = await _roleService.GetAsync();

                var userDto = new CreateAccountRequest
                {
                    Name = user.Name,
                    Email = user.Email,
                    Claims = user.SelectedClaims != null ? claims.Where(c => user.SelectedClaims.ToList().Contains(c.Value)).Select(c => c.Value).ToArray() : null,
                    Roles = user.SelectedRoles != null ? roles.Where(c => user.SelectedRoles.ToList().Contains(c.Name)).Select(c => c.Name).ToArray() : null,
                    Resources = user.SelectedResources
                };

                await _userService.CreateAsync(userDto);

                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        [HttpGet]
        [RequirePermission("user.edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var userDto = await _userService.GetDetailsByIdAsync(id.Value);

            if (userDto == null)
                return NotFound();

            var user = new User
            {
                Id = userDto.Id,
                Name = userDto.UserName,
                Email = userDto.Email,
                SelectedRoles = userDto.Roles?.Select(r => r.Name).ToArray(),
                SelectedClaims = userDto.Claims?.Select(c => c.Value).ToArray(),
                SelectedResources = userDto.ResourceAccesses?.Select(c => c.Id).ToArray()
            };

            var roles = await _roleService.GetAsync();
            var claims = await _claimService.ListAsync();
            var resources = await _resourceIdentityService.ListAsync();

            ViewBag.PossibleClaims = _mapper.Map<List<ClaimBase>, List<Claim>>(claims.ToList());
            ViewBag.PossibleRoles = _mapper.Map<List<RoleDto>, List<Role>>(roles.ToList());
            ViewBag.PossibleMerchantResources = _mapper.Map<List<ResourceIdentityDto>, List<ResourceIdentity>>(resources.Where(c => c.Type == IdentityType.Merchant).ToList());
            ViewBag.PossibleBusinessResources = _mapper.Map<List<ResourceIdentityDto>, List<ResourceIdentity>>(resources.Where(c => c.Type == IdentityType.Business).ToList());

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermission("user.edit")]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Email,SelectedRoles,SelectedClaims, SelectedResources")] User user)
        {
            if (id != user.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var claims = await _claimService.ListAsync();
                    var roles = await _roleService.GetAsync();

                    var userDto = new EditAccountRequest
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Claims = user.SelectedClaims != null ? claims.Where(c => user.SelectedClaims.Contains(c.Value)).Select(c => c.Value).ToList() : null,
                        Roles = user.SelectedRoles != null ? roles.Where(c => user.SelectedRoles.Contains(c.Name)).Select(c => c.Name).ToList() : null,
                        Resources = user.SelectedResources
                    };

                    await _userService.EditAsync(userDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(user);
        }

        private bool UserExists(int id)
        {
            var user = _userService.GetDetailsByIdAsync(id);

            return user != null;
        }
    }
}
