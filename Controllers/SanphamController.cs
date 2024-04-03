using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Flowers.Models;
using Shop_Flowers.Responsitory;

namespace Shop_Flowers.Controllers
{
    public class SanphamController : Controller
    {
		private readonly DataContext _dataContext;
		public SanphamController(DataContext context)
		{
			_dataContext = context;
		}
		public IActionResult Index()
        {
            return View();
        }
		public IActionResult Chitietsanpham()
		{
			return View();
		}
		
	}
}
