using eCommerce.Dal;
using eCommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.UI.Areas.Categories.Controllers
{
    [Area("Categories")]
    public class HomeController : Controller
    {
        private readonly ICommonRepository<Category> _categoryRepository;
        private readonly ICommonRepository<Product> _productRepository;

        public HomeController(ICommonRepository<Category> categoryRepository,ICommonRepository<Product> productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return View(categories);
        }

        // URL - Categories/Home/Create
        // [Authorize] // When the above url is opened first it directly goes to authorization page
        public IActionResult Create()
        {
            return View();
        }

        // By default it will be get so we are changing it to host
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if(ModelState.IsValid)
            {
                // Returns the number of rows affected by InsertAsync
                var result = await _categoryRepository.InsertAsync(category);
                if (result > 0)
                   return RedirectToAction("Index");
                else
                    return View();
            }
            return View();
        }

        public async Task<IActionResult> category1(int id)
        {
            ViewBag.ID = id;
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        //public async Task<IActionResult> CategoryPage(int id)
        //{

        //}
    }
}
