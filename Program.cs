using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shop_Flowers.Responsitory;
using Microsoft.Extensions.DependencyInjection;
using Shop_Flowers.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Shop_FlowersContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Shop_FlowersContext") ?? throw new InvalidOperationException("Connection string 'Shop_FlowersContext' not found.")));
// Connection Db
builder.Services.AddDbContext<DataContext>(options =>
{
	options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectedDb"]);
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Sanpham}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seeding Data
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
app.Run();

