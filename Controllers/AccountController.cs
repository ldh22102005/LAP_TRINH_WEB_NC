using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        // Demo login cứng
        if (email == "admin@gmail.com" && password == "123")
        {
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Sai tài khoản hoặc mật khẩu!";
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }


}