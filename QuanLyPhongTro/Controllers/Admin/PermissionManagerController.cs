using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.ViewModels.Admin;

namespace QuanLyPhongTro.Controllers.Admin
{
    [Authorize]
    public class PermissionManagerController : Controller
    {
        private readonly RoleManager<IdentityRole> _context;
        private readonly RoomManagementContext _room;
        private readonly UserManager<IdentityUser> _userManager;

        public PermissionManagerController(RoleManager<IdentityRole> role, UserManager<IdentityUser> userManager, RoomManagementContext room)
        {
            _context = role;
            _userManager = userManager;
            _room = room;
        }
        public async Task<IActionResult> PermissionList()
        {
            Authencation();
            if (await CheckRole() == true)
            {
                return RedirectToAction("Denied", "Authencation");
            }
            var model = new PermissionManagerModel(_context);
            var list = await model.getAllPermission();
            var viewModel = viewModelRole(list);
            return View("PermissionList", viewModel);
        }

        public PermissionManagerModel viewModelRole(List<IdentityRole> role)
        {
            var model = new PermissionManagerModel(_context)
            {
                Roles = role
            };
            return model;
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
        public string GetValueCoookie(string cookieName)
        {

            if (!string.IsNullOrEmpty(cookieName) && Request != null && Request.Cookies.TryGetValue(cookieName, out string cookieValue))
            {
                return cookieValue;
            }
            return null;
        }
        public bool Authencation()
        {
            var user = GetValueCoookie("AccountUser");
            if (user != null)
            {
                var model = new DashBoardModel(_room);
                var result = model.GetUserKey(user);
                ViewBag.CookieValue = result.HoTen;
                return true;
            }
            return false;
        }
    }
}
