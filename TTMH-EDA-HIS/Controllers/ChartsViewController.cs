using Microsoft.AspNetCore.Mvc;

namespace TTMH_EDA_HIS.Controllers
{
    public class ChartsViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
