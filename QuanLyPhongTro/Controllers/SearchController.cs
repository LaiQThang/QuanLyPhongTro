using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.ViewModels;

namespace QuanLyPhongTro.Controllers
{
    public class SearchController : Controller
    {
        private readonly RoomManagementContext _context;
        public SearchController(RoomManagementContext roomManagementContext)
        {
            _context = roomManagementContext;
        }
        public IActionResult TopPoster()
        {
            Authencation();
            var model = new SearchModel(_context);
            var list = model.getPosterSeeMore();
            var viewModel = viewModelSearch(list, null);
            ViewData["Content"] = "Bài đăng mới nhất";
            return View(viewModel);
        }

        public IActionResult PosterDetail(int id) 
        {
            Authencation();
            var model = new SearchModel(_context);
            var list = model.GetPosterID(id);
            var viewModel = viewModelSearch(null, list);
            string messageerr = TempData["MessangeError"] as string;
            ViewData["MessangeError"] = messageerr;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> OrderRoom(int idRoom, int poster, DateTime ngayBD, DateTime ngayKT, string ghichu)
        {
            var userID = getUserID();
            if(userID == null)
            {
                return RedirectToAction("Login", "Authencation");
            }
            var model = new SearchModel(_context);
            var result = await model.OrderRoom(idRoom, poster, ngayBD, ngayKT, userID, ghichu);
            if(result == true)
            {
                return RedirectToAction("BookedRoomIndex", "BookedRoom");
            }
            TempData["MessageError"] = "Có lỗi xảy ra !";
            return RedirectToAction("PosterDetail");
        }

        public string GetValueCoookie(string cookieName)
        {

            if (!string.IsNullOrEmpty(cookieName) && Request != null && Request.Cookies.TryGetValue(cookieName, out string cookieValue))
            {
                return cookieValue;
            }
            return null;
        }
        public string getUserID()
        {
            var userID = GetValueCoookie("AccountId");
            return userID;
        }
        public bool Authencation()
        {
            var user = GetValueCoookie("AccountUser");
            var model = new FooterModel(_context);
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

        public SearchModel viewModelSearch(List<Models.Domain.BaiDang> baiDangList, Models.Domain.BaiDang baiDang) 
        {
            var model = new SearchModel(_context)
            {
                baiDangs = baiDangList,
                baiDang = baiDang
            };

            return model;
        }
    }
}
