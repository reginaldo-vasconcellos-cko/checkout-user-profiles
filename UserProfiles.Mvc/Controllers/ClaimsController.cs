using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserProfiles.Api.Services;
using UserProfiles.Common.Models.Entities;
using UserProfiles.Mvc.Models;
using UserProfiles.Security.Attributes;

namespace UserProfiles.Mvc.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly IClaimService _claimService;
        private readonly IMapper _mapper;

        public ClaimsController(IClaimService claimService, 
                IMapper mapper)
        {
            _claimService = claimService;
            _mapper = mapper;
        }

        // GET: Claims
        [RequirePermission("claim.list")]
        public async Task<IActionResult> Index()
        {
            var claims = await _claimService.ListAsync();

            return View(_mapper.Map<List<ClaimBase>, List<Claim>>(claims.ToList()));
        }

        // GET: Claims/Details/5
        [RequirePermission("claim.details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _claimService.GetByIdAsync(id.Value);

            if (claim == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ClaimBase, Claim>(claim));
        }

        // GET: Claims/Create
        [RequirePermission("claim.create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Claims/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermission("claim.create")]
        public async Task<IActionResult> Create([Bind("Id,Type,Value")] Claim claim)
        {
            if (ModelState.IsValid)
            {
                await _claimService.CreateAsync(_mapper.Map<Claim, ClaimBase>(claim));

                return RedirectToAction("Index");
            }
            return View(claim);
        }

        // GET: Claims/Edit/5
        [RequirePermission("claim.edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _claimService.GetByIdAsync(id.Value);

            if (claim == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ClaimBase, Claim>(claim));
        }

        // POST: Claims/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermission("claim.edit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Value")] Claim claim)
        {
            if (id != claim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _claimService.EditAsync(_mapper.Map<Claim, ClaimBase>(claim));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ClaimExists(claim.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(claim);
        }

        private async Task<bool> ClaimExists(int id)
        {
            var claim = await _claimService.GetByIdAsync(id);

            return (claim != null);
        }
    }
}
