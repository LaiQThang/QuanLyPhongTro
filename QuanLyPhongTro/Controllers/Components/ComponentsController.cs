using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.StoredProcedure;
using QuanLyPhongTro.Models.ViewModels;

namespace QuanLyPhongTro.Controllers.Components
{
    public class ComponentsController : Controller
    {
        private readonly RoomManagementContext _roomManagementContext;

        public ComponentsController(RoomManagementContext roomManagementContext)
        {
            _roomManagementContext = roomManagementContext;
        }
        

        protected void Authencation() 
        {
            var user = GetValueFromCookie("AccountUser");
            var model = new FooterModel(_roomManagementContext);
            var countBooked = model.CountBooked();
            var countCustomer = model.CountCustomer();
            var CountPartner = model.CountPartner();
            var CountAccess = model.CountAccess();
            ViewBag.CountBooked = countBooked;
            ViewBag.CountCustomer = countCustomer;
            ViewBag.CountPartner = CountPartner;
            ViewBag.CountAccess = CountAccess;
            ViewBag.CookieValue = user;
        }

        protected string GetValueFromCookie(string cookieName)
        {
            System.Diagnostics.Debug.WriteLine(Request, "thang3");

            if (!string.IsNullOrEmpty(cookieName) && Request != null && Request.Cookies.TryGetValue(cookieName, out string cookieValue))
            {
                return cookieValue;
            }
            return null;
        }

        protected string getUserID()
        {
            var userID = GetValueFromCookie("AccountId");
            return userID;
        }
        

    }
}
