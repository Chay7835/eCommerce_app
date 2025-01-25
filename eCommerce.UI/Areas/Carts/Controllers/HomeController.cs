using eCommerce.Dal;
using eCommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.UI.Areas.Carts.Controllers
{

    [Area("Carts")]
    public class HomeController : Controller
    {
        private readonly INewCartRepository _newCartRepository;
        private readonly ICommonRepository<CartDetail> _cartDetailRepository;
        public HomeController(INewCartRepository newCartRepository, ICommonRepository<CartDetail> cartDetailRepository)
        {
            this._newCartRepository = newCartRepository;
            this._cartDetailRepository = cartDetailRepository;
        }

        public async Task<IActionResult> MyCart()
        {
            try
            {
                var myCartItems = await _newCartRepository.GetCartItems((int)HttpContext.Session.GetInt32("CartId"));
                return View(myCartItems);
            }
            catch (Exception ex)
            {
                return View("EmptyCart");
            }
        }
        public async Task<IActionResult> AddToCart(int id)
        {
            //Please check do u have Customer Id is session. If not, redirect the user to the Login page. After succesful login,
            //store the CustomerId into session variable and comback to the AddToCart action method once again
            HttpContext.Session.SetInt32("CustomerId", 1);
            if (HttpContext.Session.GetInt32("CartId") == null)
            {

                int cartId = await _newCartRepository.GenerateNewCart((int)HttpContext.Session.GetInt32("CustomerId"));
                HttpContext.Session.SetInt32("CartId", cartId);
            }

            await _cartDetailRepository.InsertAsync(new()
            {
                CartId = (int)HttpContext.Session.GetInt32("CartId"),
                ProductId = id
            });
            return RedirectToAction("MyCart");
        }
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            await _cartDetailRepository.DeleteAsync(id);
            return RedirectToAction("MyCart");
        }
    }
}