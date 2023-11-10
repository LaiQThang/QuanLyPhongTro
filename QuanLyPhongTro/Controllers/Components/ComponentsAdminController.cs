using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.ViewModels;
using QuanLyPhongTro.Models.ViewModels.Admin;

namespace QuanLyPhongTro.Controllers.Components
{
    public class ComponentsAdminController : Controller
    {
        private readonly RoomManagementContext _context;
        public ComponentsAdminController(RoomManagementContext roomManagementContext)
        {
            _context = roomManagementContext;
        }
        protected string GetValueFromCookie(string cookieName)
        {
            if (!string.IsNullOrEmpty(cookieName) && Request != null && Request.Cookies.TryGetValue(cookieName, out string cookieValue))
            {
                return cookieValue;
            }
            return null;
        }
        protected void Authencation()
        {
            var user = GetValueFromCookie("AccountUser");
            //System.Diagnostics.Debug.WriteLine(user, "thang");

            var model = new DashBoardModel(_context);
            var result = model.GetUserKey(user);
            ViewBag.CookieValue = result.HoTen;
        }
    }
}
