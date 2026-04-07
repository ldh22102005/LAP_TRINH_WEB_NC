using QL_CUA_HANG_BAN_XE_DAP.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();
app.UseSession();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddSession();
app.UseSession();