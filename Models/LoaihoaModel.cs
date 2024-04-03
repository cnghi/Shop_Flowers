using System.ComponentModel.DataAnnotations;

namespace Shop_Flowers.Models
{
    public class LoaihoaModel
    {
		[Key]
		public int IdLoaihoa { get; set; }
		[Required, MinLength(4, ErrorMessage = "Yêu cầu nhập tên Loai hoa")]
		public string Name { get; set; }
		[Required, MinLength(4, ErrorMessage = "Yêu cầu nhập Mô tả Loai hoa")]
		public string Description { get; set; }
		public string Slug { get; set; }
		public long Status { get; set; }
	}
}
