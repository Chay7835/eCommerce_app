using eCommerce.UI.ExtensionMethods;
using eCommerce.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eCommerce.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {    
            // ViewBag.x = HttpContext.Session.Id;
            HttpContext.Session.SetInt32("CustomerId", 120);
            HttpContext.Session.SetString("Role", "Admin");
            HttpContext.Session.Set<double>("Pi", 3.14);
            ViewBag.PI = HttpContext.Session.Get<double>("PI");
            ViewBag.SessionId = HttpContext.Session.Id;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
