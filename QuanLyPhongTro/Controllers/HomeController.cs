using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.ViewModels;

namespace QuanLyPhongTro.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoomManagementContext _roomManagementContext;
        public HomeController(ILogger<HomeController> logger, RoomManagementContext roomManagementContext)
        {
            _logger = logger;
            _roomManagementContext = roomManagementContext;
        }

        public IActionResult Index()
        {
            string cookieName = "AccountUser";

            if (Request.Cookies.TryGetValue(cookieName, out string cookieValue))
            {
                ViewBag.CookieValue = cookieValue;
                //System.Diagnostics.Debug.WriteLine(result, "LogThang");
                return View();

            }
            return View();
        }
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("AccountUser");

            return RedirectToAction("Login", "Authencation");
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}