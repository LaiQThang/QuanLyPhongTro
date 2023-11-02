using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using QuanLyPhongTro.Models.ViewModels.Admin;
using QuanLyPhongTro.Data;

namespace QuanLyPhongTro.Controllers.Admin
{
    [Authorize]
    public class DashBoardController : Controller
    {
        private readonly RoomManagementContext _context;

        public DashBoardController(RoomManagementContext room)
        {
            _context = room;
        }
        [Route("/admin")]
        public IActionResult Index()
        {
            Authencation();
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
