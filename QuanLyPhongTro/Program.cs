using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Data;

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
    });


builder.Services.AddIdentityCore<IdentityUser>()
	//.AddDefaultTokenProviders()
	.AddRoles<IdentityRole>()
    .AddSignInManager<SignInManager<IdentityUser>>()
	.AddEntityFrameworkStores<RoomManagementContext>();


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



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authencation}/{action=Login}/{id?}");

app.Run();
