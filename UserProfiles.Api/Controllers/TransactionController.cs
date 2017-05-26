using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using UserProfiles.Api.Models.Enums;
using UserProfiles.Api.Repository;
using UserProfiles.Api.Security.Attributes;
using UserProfiles.Api.Security.Requirements;

namespace UserProfiles.Api.Controllers
{
    [Produces("application/json")]
    public class TransactionController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;

        public int UserId => _userRepository.GetByRefId(_userManager.FindByNameAsync(User.Identity.Name).Result.Id).Id;

        public TransactionController(UserManager<IdentityUser> userManager,
                IAuthorizationService authorizationService, 
                ITransactionRepository transactionRepository, 
                IUserRepository userRepository)
        {
            _userManager = userManager;
            _authorizationService = authorizationService;
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        [RequirePermission("transaction.get")]
        [Route("api/transactions")]
        public async Task<IActionResult> Get()
        {
            return Ok(_transactionRepository.Get(UserId));
        }

        [HttpGet]
        [RequirePermission("transaction.getByMerchant")]
        [Route("api/merchant/{id}/transactions")]
        public async Task<IActionResult> GetMerchantTransactions(int id)
        {
            if (!await _authorizationService.AuthorizeAsync(User, null, new ResourceAccessRequirement(id, IdentityType.Merchant)))
                return new ChallengeResult();

            var result = _transactionRepository.GetByMerchantId(id, UserId);

            return Ok(result);
        }

        [HttpGet]
        [RequirePermission("transaction.getByBusiness")]
        [Route("api/business/{id}/transactions")]
        public async Task<IActionResult> GetBusinessTransactions(int id)
        {
            if (!await _authorizationService.AuthorizeAsync(User, null, new ResourceAccessRequirement(id, IdentityType.Business)))
                return new ChallengeResult();

            return Ok(_transactionRepository.GetByBusinessId(id));
        }
    }
}