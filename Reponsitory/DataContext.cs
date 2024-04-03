using Microsoft.EntityFrameworkCore;
using Shop_Flowers.Models;
using System.Data;

namespace Shop_Flowers.Responsitory
{
	public class DataContext : DbContext 
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{ 
		}
		public DbSet<LoaihoaModel> Loaihoa { get; set; }
		public DbSet<SanphamModel> Sanpham { get; set; }
		public DbSet<DanhmucModel> Danhmuc { get; set;}
	}
}
