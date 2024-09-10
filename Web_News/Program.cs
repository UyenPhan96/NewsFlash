using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.EntityFrameworkCore;
using Web_News.Models;
using Web_News.Services.Account;
using Web_News.Services.EmailService;
using Web_News.Areas.Admin.ServiceAd;
using Web_News.Areas.Admin.ServiceAd.CategorySV;
using Web_News.Areas.Admin.ServiceAd.NewsSV;
using Web_News.Services.ContactSV;
using Web_News.Areas.Admin.ServiceAd.AdvertisementSV;

var builder = WebApplication.CreateBuilder(args);

// Các Dịch vụ đăng ký Trang chủ NewsFlash
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IContactService, ContactService>();

// Các Dịch vụ đăng ký Trang Chủ Admin
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<INewsService, NewsService>();

builder.Services.AddScoped<IAdvertisementService, AdvertisementService>();



builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContextConnection")));
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Đăng ký các dịch vụ xác thực và quyền hạn
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Account/Login";               
    options.AccessDeniedPath = "/Account/AccessDenied"; 
})
.AddFacebook(options =>
{
    options.AppId = builder.Configuration["Authentication:Facebook:AppId"] ?? throw new InvalidOperationException("Facebook AppId is not configured.");
    options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"] ?? throw new InvalidOperationException("Facebook AppSecret is not configured.");
    options.ClaimActions.MapJsonKey("urn:facebook:id", "id");
})
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? throw new InvalidOperationException("Google ClientId is not configured.");
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? throw new InvalidOperationException("Google ClientSecret is not configured.");
    options.ClaimActions.MapJsonKey("urn:google:id", "sub");
});


// Add services to the container.
builder.Services.AddControllersWithViews();

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
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
