using Microsoft.AspNetCore.Mvc;

namespace TTMH_EDA_HIS.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
