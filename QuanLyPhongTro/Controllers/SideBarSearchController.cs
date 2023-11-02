using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.ViewModels;

namespace QuanLyPhongTro.Controllers
{
    public class SideBarSearchController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoomManagementContext _roomManagementContext;
        public SideBarSearchController(ILogger<HomeController> logger, RoomManagementContext roomManagementContext)
        {
            _logger = logger;
            _roomManagementContext = roomManagementContext;
        }

        public IActionResult SearchAll(string name, DateTime ngayBD, DateTime ngayKT) 
        {
            Authencation();
            var model = new SideBarSearchModel(_roomManagementContext);
            var list = model.getPosterSearch(name, ngayBD, ngayKT);
            var viewModel = viewModelSide(list);
            return View(viewModel);
        }

        public SideBarSearchModel viewModelSide(List<Models.Domain.BaiDang> baiDangs)
        {
            var model = new SideBarSearchModel(_roomManagementContext)
            {
                baiDangs = baiDangs,
            };
            return model;
            
        }
        public bool Authencation()
        {
            var user = GetValueCoookie("AccountUser");
            var model = new FooterModel(_roomManagementContext);
            var countBooked = model.CountBooked();
            var countCustomer = model.CountCustomer();
            var CountPartner = model.CountPartner();
            var CountAccess = model.CountAccess();
            ViewBag.CountBooked = countBooked;
            ViewBag.CountCustomer = countCustomer;
            ViewBag.CountPartner = CountPartner;
            ViewBag.CountAccess = CountAccess;
            if (user != null)
            {
                ViewBag.CookieValue = user;
                return true;
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

    }
}
