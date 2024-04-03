using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop_Flowers.Models;
using Shop_Flowers.Responsitory;

namespace Shop_Flowers.Areas.Admin.Controllers
{
	[Area("Admin")]
    public class SanphamController : Controller
    {
		private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
		public SanphamController(DataContext context, IWebHostEnvironment webHostEnvironment)
		{
			_dataContext = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
           
            return View(await _dataContext.Sanpham.OrderByDescending(p => p.IdSanpham).ToListAsync());
        }
        public IActionResult Create()
        {
			ViewBag.Danhmuc = new SelectList(_dataContext.Danhmuc,"Id","Name");
            ViewBag.Loaihoa = new SelectList(_dataContext.Loaihoa, "IdLoaihoa", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SanphamModel sanpham)
        {
            
            if (!_dataContext.Danhmuc.Any(d => d.Id == sanpham.DanhmucId))
            {
                ModelState.AddModelError("DanhmucId", "Danh mục không tồn tại");
            }

            if (sanpham.DanhmucId == null )
            {
                ModelState.AddModelError("DanhmucId", "Chọn một danh mục");
            }
            else if (!_dataContext.Danhmuc.Any(d => d.Id == sanpham.DanhmucId))
            {
                ModelState.AddModelError("DanhmucId", "Danh mục không tồn tại");
            }

            sanpham.DanhmucId = 0;
            if (ModelState.IsValid)
            {
                sanpham.Slug = sanpham.Name.Replace(" ", "-");
                var slug = await _dataContext.Sanpham.FirstOrDefaultAsync(p => p.Slug == sanpham.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Sản phẩm đã có trong cơ sở dữ liệu");
                    return View(sanpham);
                }
                if (sanpham.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/sanpham");
                    string imageName = Guid.NewGuid().ToString() + "" + sanpham.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await sanpham.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                    sanpham.Image = imageName;
                }
                //sanpham.DanhmucId = 0;
                _dataContext.Add(sanpham);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Thêm sản phẩm thành công!";
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

            return View(sanpham);

        }
 
        public async Task<IActionResult> Delete(int id)
        {
            SanphamModel sanpham = await _dataContext.Sanpham.FindAsync(id);
            if (!string.Equals(sanpham.Image, "noimage.jpg"))
            {


                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/sanpham");
                string oldfileImage = Path.Combine(uploadsDir, sanpham.Image);
                if (System.IO.File.Exists(oldfileImage))
                {
                    System.IO.File.Delete(oldfileImage);
                }
            }
            _dataContext.Sanpham.Remove(sanpham);
            await _dataContext.SaveChangesAsync();
            TempData["error"] = "Sản phẩm đã xóa!";
            return RedirectToAction("Index");
        }
    }
}
