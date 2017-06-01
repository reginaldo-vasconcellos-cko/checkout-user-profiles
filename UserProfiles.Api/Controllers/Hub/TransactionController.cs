using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserProfiles.Api.Models.Enums;
using UserProfiles.Api.Security.Attributes;
using UserProfiles.Api.Security.Requirements;
using UserProfiles.Api.Services;

namespace UserProfiles.Api.Controllers.Hub
{
    [Produces("application/json")]
    public class TransactionController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;

        public TransactionController(IAuthorizationService authorizationService, 
                ITransactionService transactionService, 
                IUserService userService)
        {
            _authorizationService = authorizationService;
            _transactionService = transactionService;
            _userService = userService;
        }

        [HttpGet]
        [RequirePermission("transaction.get")]
        [Route("api/transactions")]
        public async Task<IActionResult> Get()
        {
            var user = await _userService.GetByNameAsync(User.Identity.Name);

            return Ok(_transactionService.Get(user.Id));
        }

        [HttpGet]
        [RequirePermission("transaction.getByMerchant")]
        [Route("api/merchant/{id}/transactions")]
        public async Task<IActionResult> GetMerchantTransactions(int id)
        {
            if (!await _authorizationService.AuthorizeAsync(User, null, new ResourceAccessRequirement(id, IdentityType.Merchant)))
                return new ChallengeResult();

            var user = await _userService.GetByNameAsync(User.Identity.Name);

            return Ok(_transactionService.GetByMerchantId(id, user.Id));
        }

        [HttpGet]
        [RequirePermission("transaction.getByBusiness")]
        [Route("api/business/{id}/transactions")]
        public async Task<IActionResult> GetBusinessTransactions(int id)
        {
            if (!await _authorizationService.AuthorizeAsync(User, null, new ResourceAccessRequirement(id, IdentityType.Business)))
                return new ChallengeResult();

            return Ok(_transactionService.GetByBusinessId(id));
        }
    }
}