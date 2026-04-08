using Microsoft.AspNetCore.Mvc;
using QL_CUA_HANG_BAN_XE_DAP.Data;
using System.Linq;

namespace QL_CUA_HANG_BAN_XE_DAP.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products
                .OrderByDescending(p => p.Id)
                .ToList();

            return View(products);
        }
    }
}
