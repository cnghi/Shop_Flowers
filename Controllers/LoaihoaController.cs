using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Flowers.Models;
using Shop_Flowers.Responsitory;

namespace Shop_Flowers.Controllers
{
	public class LoaihoaController : Controller 
	{
		private readonly DataContext _dataContext;
		public LoaihoaController(DataContext context)
		{
			_dataContext = context;
		}
		
	}
}
