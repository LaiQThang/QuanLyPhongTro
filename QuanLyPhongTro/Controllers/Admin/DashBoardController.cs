using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using QuanLyPhongTro.Models.ViewModels.Admin;
using QuanLyPhongTro.Data;
using Microsoft.AspNetCore.Identity;
using QuanLyPhongTro.Controllers.Components;
using Microsoft.AspNetCore.Mvc.Filters;
using QuanLyPhongTro.ActionFilter;
using QuanLyPhongTro.Models.Domain;

namespace QuanLyPhongTro.Controllers.Admin
{
    [Authorize]
    [ServiceFilter(typeof(FilterRole))]
    public class DashBoardController : ComponentsAdminController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoomManagementContext _context;

        public DashBoardController(RoomManagementContext room, UserManager<IdentityUser> userManager) : base(room)
        {
            _context = room;
            _userManager = userManager;
        }
        [Route("/admin")]
        public async Task<IActionResult> Index()
        {
            Authencation();
            return View("Index");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            var userName = GetValueFromCookie("AccountUser");
            var user = await _userManager.FindByNameAsync(userName);
            user.TwoFactorEnabled = false;
            await _context.SaveChangesAsync();

            var userId = GetValueFromCookie("AccountId");
            if (userId != null)
            {
                var del = await _context.activeUsers.Where(res => res.ApplicationUserId == userId).FirstAsync();
                if (del is ActiveUser)
                {
                    _context.Remove(del);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Login", "Authencation");
        }

    }
}
