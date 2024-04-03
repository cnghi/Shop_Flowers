using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Flowers.Responsitory;

namespace Shop_Flowers.Reponsitory.Components
{
	public class LoaihoaViewComponent : ViewComponent
	{
		private readonly DataContext _dataContext;
		public LoaihoaViewComponent(DataContext context)
		{
			_dataContext = context;
		}
		public async Task<IViewComponentResult> InvokeAsync() => View(await _dataContext.Loaihoa.ToListAsync());
	}
}
