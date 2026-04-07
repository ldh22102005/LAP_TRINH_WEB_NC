using Microsoft.AspNetCore.Mvc;

namespace QL_CUA_HANG_BAN_XE_DAP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}