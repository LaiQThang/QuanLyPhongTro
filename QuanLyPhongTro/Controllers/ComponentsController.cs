using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.ViewModels;

namespace QuanLyPhongTro.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly RoomManagementContext _roomManagementContext;
        public ComponentsController(RoomManagementContext roomManagementContext)
        {
            _roomManagementContext = roomManagementContext;
        }
        public string GetValueCoookie(string cookieName )
        {

            if (!string.IsNullOrEmpty(cookieName) && Request != null && Request.Cookies.TryGetValue(cookieName, out string cookieValue))
            {
                System.Diagnostics.Debug.WriteLine(cookieValue, "LogThang");
                return cookieValue;
            }
            return null;
        }
    }
}
