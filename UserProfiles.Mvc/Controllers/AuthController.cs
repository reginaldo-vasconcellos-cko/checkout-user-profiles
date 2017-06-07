using Microsoft.AspNetCore.Mvc;

namespace UserProfiles.Mvc.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Unauthorized()
        {
            return View();
        }
    }
}