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

        public async Task<bool> CheckRole()
        {
            var user = GetValueFromCookie("AccountUser");
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


    }
}
