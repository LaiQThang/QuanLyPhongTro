using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using QuanLyPhongTro.Models.ViewModels.Admin;
using QuanLyPhongTro.Data;
using Microsoft.AspNetCore.Identity;

namespace QuanLyPhongTro.Controllers.Admin
{
    [Authorize]
    public class DashBoardController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoomManagementContext _context;

        public DashBoardController(RoomManagementContext room, UserManager<IdentityUser> userManager)
        {
            _context = room;
            _userManager = userManager;
        }
        [Route("/admin")]
        public async Task<IActionResult> Index()
        {
            Authencation();
            if (await CheckRole() == true)
            {
                return RedirectToAction("Denied", "Authencation");
            }
            return View("Index");
        }

        public string GetValueCoookie(string cookieName)
        {

            if (!string.IsNullOrEmpty(cookieName) && Request != null && Request.Cookies.TryGetValue(cookieName, out string cookieValue))
            {
                return cookieValue;
            }
            return null;
        }

        public async Task<bool> CheckRole()
        {
            var user = GetValueCoookie("AccountUser");
            var userName = "";
            if (user != null)
            {
                userName = user;
            }
            var userCheck = await _userManager.FindByNameAsync(userName);
            if (userCheck != null)
            {
                var userRoles = await _userManager.GetRolesAsync(userCheck);
                foreach (var role in userRoles)
                {
                    if (role == "Client")
                    {
                        System.Diagnostics.Debug.WriteLine(userRoles.ToString(), "ThangLog");
                        return true;
                    }
                }
            }
            return false;
        }

        public bool Authencation()
        {
            var user = GetValueCoookie("AccountUser");
            if (user != null)
            {
                var model = new DashBoardModel(_context);
                var result = model.GetUserKey(user);
                ViewBag.CookieValue = result.HoTen;
                return true;
            }
            return false;
        }

    }
}
