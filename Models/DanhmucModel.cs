﻿using System.ComponentModel.DataAnnotations;

namespace Shop_Flowers.Models
{
    public class DanhmucModel
    {
        [Key]   
        public int Id { get; set; }
        [Required, MinLength(4,ErrorMessage = "Yêu cầu nhập tên Danh mục")]
        public string Name { get; set; }
		[Required, MinLength(4, ErrorMessage = "Yêu cầu nhập Mô tả Danh mục")]
		public string Description { get; set; }
        public string Slug { get; set; }
        public long Status { get; set; }
    }
}
