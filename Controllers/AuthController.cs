using Microsoft.AspNetCore.Mvc;
using QL_CUA_HANG_BAN_XE_DAP.Data;
using QL_CUA_HANG_BAN_XE_DAP.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace QL_CUA_HANG_BAN_XE_DAP.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // ================= HASH PASSWORD =================
        private string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        // ================= LOGIN =================
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var hashedPassword = HashPassword(password);

            var user = _context.Users
                .FirstOrDefault(x => x.Username == username && x.Password == hashedPassword);

            if (user != null)
            {
                HttpContext.Session.SetString("User", user.Username);
                return RedirectToAction("Index", "Product");
            }

            ViewBag.Error = "Sai tài khoản hoặc mật khẩu";
            return View();
        }

        // ================= REGISTER =================
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(
            string username,
            string password,
            string confirmPassword,
            string email,
            string phone)
        {
            // ===== CHECK RỖNG =====
            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phone))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin!";
                return View();
            }

            // ===== CHECK PASSWORD MATCH =====
            if (password != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu nhập lại không khớp!";
                return View();
            }

            // ===== CHECK USERNAME =====
            var checkUser = _context.Users
                .FirstOrDefault(x => x.Username == username);

            if (checkUser != null)
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại!";
                return View();
            }

            // ===== CHECK EMAIL =====
            var checkEmail = _context.Users
                .FirstOrDefault(x => x.Email == email);

            if (checkEmail != null)
            {
                ViewBag.Error = "Email đã tồn tại!";
                return View();
            }

            // ===== CHECK PHONE =====
            if (phone.Length < 9 || phone.Length > 11)
            {
                ViewBag.Error = "Số điện thoại không hợp lệ!";
                return View();
            }

            // ===== CREATE USER =====
            var user = new User
            {
                Username = username,
                Password = HashPassword(password), // 🔐 mã hóa
                Email = email,
                Phone = phone
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Success"] = "Đăng ký thành công!";
            return RedirectToAction("Login");
        }

        // ================= LOGOUT =================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}