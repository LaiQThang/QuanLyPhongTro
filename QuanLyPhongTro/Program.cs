using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<RoomManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionRoomManagement")));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Authencation/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        option.AccessDeniedPath = "/Authencation/Denied";
  //      option.Cookie.Name = "Authentication";
		//option.Cookie.HttpOnly = true;
	});




builder.Services.AddIdentityCore<IdentityUser>()
	//.AddDefaultTokenProviders()
	.AddRoles<IdentityRole>()
    .AddSignInManager<SignInManager<IdentityUser>>()
	.AddEntityFrameworkStores<RoomManagementContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireNonAlphanumeric = false; // Yêu cầu ít nhất một ký tự không thuộc bảng chữ cái hoặc chữ số
	options.Password.RequireUppercase = false; // Yêu cầu ít nhất một ký tự viết hoa
	options.Password.RequireLowercase = false; 
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Client", policy => policy.RequireRole("Client"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.UseMiddleware<LogMiddleware>();

app.Run();
