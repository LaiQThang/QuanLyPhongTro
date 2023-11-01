using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.ViewModels;

namespace QuanLyPhongTro.Controllers
{
    public class BookedRoomController : Controller
    {
        private readonly RoomManagementContext _context;

        public BookedRoomController(RoomManagementContext roomManagementContext)
        {
            _context = roomManagementContext;
        }
        public IActionResult BookedRoomIndex()
        {
            Authencation();
            var model = new BookedModel(_context);
            var userID = getUserID();
            var list = model.GetAllRoomBooked(userID);
            if (list == null)
            {
                return View();
            }
            var viewModel = viewModelBooked(list, null);
            System.Diagnostics.Debug.WriteLine(list.Any(), "ThangLog");
            return View(viewModel);
        }


        public IActionResult BookedDetail(int roomId)
        {
            Authencation();
            var model = new BookedModel(_context);
            var userID = getUserID();
            var list = model.GetRoomBooked(userID, roomId);
            if (list == null)
            {
                return View();
            }
            var viewModel = viewModelBooked(null, list);
            return View(viewModel);
        }

        public async Task<IActionResult> DeleteBooked(int ctidphongdat)
        {
            Authencation();
            if (ctidphongdat == 0) 
            {
                ViewData["Message"] = "Đã có lỗi xả ra vui lòng liên hệ số điện thoại chủ nhà!";
                return View(); 
            }
            var model = new BookedModel(_context);
            await model.DeleteBooked(ctidphongdat);
            return RedirectToAction("BookedRoomIndex");
        }

        public BookedModel viewModelBooked(List<Models.Domain.ChiTietDatPhong> chiTietDatPhongs, ChiTietDatPhong chiTietDatPhong)
        {
            var model = new BookedModel(_context)
            {
                chiTietDatPhongs = chiTietDatPhongs,
                chiTietDatPhong = chiTietDatPhong
            };
            return model;
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

            if (user != null)
            {
                ViewBag.CookieValue = user;
                return true;
            }
            return false;
        }
    }
}
