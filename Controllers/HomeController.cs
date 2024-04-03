using Microsoft.AspNetCore.Mvc;
using Shop_Flowers.Models;
using Shop_Flowers.Responsitory;
using System.Diagnostics;

namespace Shop_Flowers.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            
            _logger = logger;
            _dataContext = context;
        }

        public IActionResult Index()
        {
            var sanpham = _dataContext.Sanpham.ToList();
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
