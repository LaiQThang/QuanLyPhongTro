using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.ViewModels.Admin;

namespace QuanLyPhongTro.Controllers.Admin
{
    [Authorize]
    public class PosterManagerController : Controller
    {
        private readonly RoomManagementContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PosterManagerController(RoomManagementContext room, UserManager<IdentityUser> userManager)
        {
            _context = room;
            _userManager = userManager;
        }
        public async Task<IActionResult> PosterList()
        {
            Authencation();
            if (await CheckRole() == true)
            {
                return RedirectToAction("Denied", "Authencation");
            }
            var model = new PosterManagerModel(_context);
            var list = model.getAllPosters();
            var viewModel = viewModelPoster(list);
            return View("PosterList", viewModel);
        }

        public PosterManagerModel viewModelPoster(List<Models.Domain.BaiDang> baiDangs)
        {
            var model = new PosterManagerModel(_context)
            {
                baiDangs = baiDangs
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
                var model = new DashBoardModel(_context);
                var result = model.GetUserKey(user);
                ViewBag.CookieValue = result.HoTen;
                return true;
            }
            return false;
        }
    }
}
