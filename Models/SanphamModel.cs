using Shop_Flowers.Reponsitory.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop_Flowers.Models
{
	public class SanphamModel
	{

		[Key]
		public int IdSanpham { get; set; }
		[Required(ErrorMessage = "Yêu cầu nhập tên Sản phẩm")]
		public string Name { get; set; }
		public string Slug { get; set; }

	
		[Required(ErrorMessage = "Yêu cầu nhập Mô tả Sản phẩm")]
		public string Description { get; set; }
		[Required, Range(0.01, double.MaxValue, ErrorMessage = "Yêu cầu nhập Giá sản phẩm")]
		public decimal Price { get; set; }
		[Required, Range(1, int.MaxValue, ErrorMessage = "Chọn một loài hoa")]
		public int LoaihoaId { get; set; }
        public int DanhmucId { get; set; }
		public string Image { get; set; } = "noimage.jpg";
		[NotMapped]
		[FileExtension]
		public IFormFile ImageUpload { get; set; }

	}
}

		