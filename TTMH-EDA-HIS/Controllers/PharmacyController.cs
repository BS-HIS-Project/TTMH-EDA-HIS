using Microsoft.AspNetCore.Mvc;
using TTMH_EDA_HIS.Data;

namespace TTMH_EDA_HIS.Controllers
{
    public class PharmacyController : Controller
    {
        private readonly HisdbContext _context;

        public PharmacyController(HisdbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}
