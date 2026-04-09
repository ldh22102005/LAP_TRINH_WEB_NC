using Microsoft.AspNetCore.Mvc;
using QL_CUA_HANG_BAN_XE_DAP.Data;
using QL_CUA_HANG_BAN_XE_DAP.Extensions;
using QL_CUA_HANG_BAN_XE_DAP.Models;

namespace QL_CUA_HANG_BAN_XE_DAP.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "Cart";
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(GetCart());
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity = 1, string? returnUrl = null)
        {
            if (quantity <= 0)
            {
                quantity = 1;
            }

            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return NotFound();
            }

            var cart = GetCart();
            cart.AddItem(product, quantity);
            SaveCart(cart);

            TempData["Success"] = $"Đã thêm \"{product.Name}\" vào giỏ hàng.";

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCart();
            cart.UpdateQuantity(productId, quantity);
            SaveCart(cart);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            var cart = GetCart();
            cart.RemoveItem(productId);
            SaveCart(cart);

            TempData["Success"] = "Đã xóa sản phẩm khỏi giỏ hàng.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Clear()
        {
            HttpContext.Session.Remove(CartSessionKey);
            TempData["Success"] = "Đã xóa toàn bộ giỏ hàng.";
            return RedirectToAction(nameof(Index));
        }

        private Cart GetCart()
        {
            return HttpContext.Session.GetObjectFromJson<Cart>(CartSessionKey) ?? new Cart();
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);
        }
    }
}
