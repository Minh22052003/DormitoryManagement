
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDistributedMemoryCache();  // Thêm bộ nhớ cache phân tán
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Đặt thời gian hết hạn cho session
    options.Cookie.HttpOnly = true;  // Đảm bảo cookie không thể truy cập từ JavaScript
    options.Cookie.Name = ".Manager.Session";
    options.Cookie.IsEssential = true;  // Đảm bảo session hoạt động ngay cả khi cookie không được chấp nhận
});





builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=SignIn}/{id?}");

app.Run();
