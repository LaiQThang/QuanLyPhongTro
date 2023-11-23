using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.ActionFilter;
using QuanLyPhongTro.Controllers.Components;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.Pagination;
using QuanLyPhongTro.Models.ViewModels;

namespace QuanLyPhongTro.Controllers
{
    [ServiceFilter(typeof(FilterRoleClient))]
    public class BookedRoomController : ComponentsController
    {
        private readonly RoomManagementContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BookedRoomController(RoomManagementContext roomManagementContext, UserManager<IdentityUser> userManager) : base(roomManagementContext)
        {
            _context = roomManagementContext;
            _userManager = userManager;

        }
        public async Task<IActionResult> BookedRoomIndex(int pg = 1)
        {
            Authencation();

            const int pageSize = 3;
            if (pg < 1)
            {
                pg = 1;
            }

            var model = new BookedModel(_context);
            var userID = getUserID();
            var resCount = await model.GetCountBooked(userID);
            ViewData["CountBooked"] = resCount;

            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            var list = model.GetAllRoomBooked(userID, recSkip, pageSize);

            if (list == null)
            {
                return View();
            }
            var viewModel = viewModelBooked(list, null);
            this.ViewBag.Pager = pager;
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

        
        
    }
}
