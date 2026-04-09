using Microsoft.AspNetCore.Mvc;
using QL_CUA_HANG_BAN_XE_DAP.Data;
using QL_CUA_HANG_BAN_XE_DAP.Models;
using System.Linq;

namespace QL_CUA_HANG_BAN_XE_DAP.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // ===== HIỂN THỊ + TÌM KIẾM =====
        public IActionResult Index(string keyword, int page = 1)
        {
            int pageSize = 9;
            var list = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
                list = list.Where(x => x.Name.Contains(keyword));

            int total = list.Count();
            var paged = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)total / pageSize);
            ViewBag.Keyword = keyword;
            return View(paged);
        }

        // ===== CHI TIẾT =====
        public IActionResult Detail(int id)
        {
            var p = _context.Products.Find(id);

            if (p == null)
            {
                return NotFound();
            }

            var category = _context.Categories.Find(p.CategoryId);
            ViewBag.CategoryName = category?.Name;

            return View(p);
        }

        // ===== THEO LOẠI =====
        public IActionResult ByCategory(int id, int page = 1)
        {
            int pageSize = 9;
            var category = _context.Categories.Find(id);
            var list = _context.Products.Where(x => x.CategoryId == id);

            int total = list.Count();
            var paged = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.CurrentCategory = id;
            ViewBag.CategoryName = category?.Name;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)total / pageSize);
            return View("Index", paged);
        }

        // ===== THÊM =====
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product p)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(p);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(p);
        }

        // ===== SỬA =====
        public IActionResult Edit(int id)
        {
            var p = _context.Products.Find(id);

            if (p == null)
            {
                return NotFound();
            }

            return View(p);
        }

        [HttpPost]
        public IActionResult Edit(Product p)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(p);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(p);
        }

        // ===== XÓA =====
        public IActionResult Delete(int id)
        {
            var p = _context.Products.Find(id);

            if (p != null)
            {
                _context.Products.Remove(p);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
