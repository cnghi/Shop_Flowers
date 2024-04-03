using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Flowers.Models;
using Shop_Flowers.Responsitory;

namespace Shop_Flowers.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoaihoaController : Controller 
    {
        private readonly DataContext _dataContext;
        public LoaihoaController(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Loaihoa.OrderByDescending(p => p.IdLoaihoa).ToListAsync());
        }
        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoaihoaModel loaihoa)
        {
            if (ModelState.IsValid)
            {
                loaihoa.Slug = loaihoa.Name.Replace(" ", "-");
                var slug = await _dataContext.Loaihoa.FirstOrDefaultAsync(p => p.Slug == loaihoa.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Loài hoa đã có trong cơ sở dữ liệu");
                    return View(loaihoa);
                }
                _dataContext.Add(loaihoa);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Thêm loài hoa thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Model đang có một vài thứ đang bị lỗi";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);

                    }
                }
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }
            return View(loaihoa);

        }
        public async Task<IActionResult> Delete(int id)
        {
            LoaihoaModel loaihoa = await _dataContext.Loaihoa.FindAsync(id);


            _dataContext.Loaihoa.Remove(loaihoa);
            await _dataContext.SaveChangesAsync();
            TempData["error"] = "Loài hoa đã xóa!";
            return RedirectToAction("Index");
        }
    }
}
