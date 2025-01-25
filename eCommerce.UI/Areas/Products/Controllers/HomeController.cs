using Microsoft.AspNetCore.Mvc;
using eCommerce.Dal;
using System.ComponentModel.DataAnnotations.Schema;
using eCommerce.Models;
using Microsoft.Extensions.Caching.Memory;

namespace eCommerce.UI.Areas.Products.Controllers
{
    [Area("Products")]
    public class HomeController : Controller
    {
        /* private readonly ILogger _logger;

        // Constructor DI(Dependency Injection)
        // Its a construct container, acts like a container
        // You will pass the object according to the instance of the Class(According to the 
        // environment[Development,testing,etc]). 
        public HomeController(ILogger logger)
        {
            _logger = logger;
        }*/

        private readonly ICommonRepository<Product> _productRepo;
        // Implementation of Caching
        // Caching stores the frequent pages to reduce the heavy traffic when accessed by millions of people 
        private readonly IMemoryCache _productsCache;

        public HomeController(ICommonRepository<Product> productsRepo, IMemoryCache productsCache)
        {
            _productRepo = productsRepo;
            _productsCache = productsCache;
        }

        // should surround it with Task as IActionResult is synchronous
        public async Task<IActionResult> Index()
        {
            ViewBag.PageTitle = "eCommerce Products List!";
            ViewBag.PageTitle = "Find all the products under single roof";
            if(_productsCache.TryGetValue("eCommerceProductsCache",out List<Product> products))
            {
                return View(products);
            }
            var allProducts = await _productRepo.GetAllAsync();

            // Absolute for exact timing like after 24hrs removing the cache (Permanent)
            // Sliding is used like if the cache is not accessed for 1 hr, then that cache will be removed
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(new TimeSpan(0, 10, 0)).SetSlidingExpiration(new TimeSpan(0, 1, 0));

            _productsCache.Set("eCommerceProductsCache", allProducts,cacheEntryOptions);
            return View(allProducts);
        }
        public async Task<IActionResult> details(int id)
        {       
            ViewBag.Title = "Details of - ";
            var product = await _productRepo.GetDetailAsync(id);
            ViewBag.DiscountedAmount = product.UnitPrice - ((product.UnitPrice * product.Discount) / 100);
            return View(product);
        }
    }

}
