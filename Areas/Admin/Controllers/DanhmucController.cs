using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop_Flowers.Models;
using Shop_Flowers.Responsitory;

namespace Shop_Flowers.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class DanhmucController : Controller 
    {
        private readonly DataContext _dataContext;
        public DanhmucController(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Danhmuc.OrderByDescending(p => p.Id).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DanhmucModel danhmuc)
        {
            if (ModelState.IsValid)
            {
                danhmuc.Slug = danhmuc.Name.Replace(" ", "-");
                var slug = await _dataContext.Danhmuc.FirstOrDefaultAsync(p => p.Slug == danhmuc.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Danh mục đã có trong cơ sở dữ liệu");
                    return View(danhmuc);
                }
                _dataContext.Add(danhmuc);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Thêm danh mục thành công!";
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
            return View(danhmuc);

        }
        public async Task<IActionResult> Delete(int id)
        {
            DanhmucModel danhmuc = await _dataContext.Danhmuc.FindAsync(id);
            

            _dataContext.Danhmuc.Remove(danhmuc);
            await _dataContext.SaveChangesAsync();
            TempData["error"] = "Danh mục đã xóa!";
            return RedirectToAction("Index");
        }
    }
}
