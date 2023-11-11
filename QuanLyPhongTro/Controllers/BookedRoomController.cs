using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.ActionFilter;
using QuanLyPhongTro.Controllers.Components;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
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
        public async Task<IActionResult> BookedRoomIndex()
        {
            Authencation();
            if (await CheckRole() == true)
            {
                return RedirectToAction("Index", "DashBoard");
            }
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

        public async Task<bool> CheckRole()
        {
            var user = GetValueFromCookie("AccountUser");
            var userCheck = await _userManager.FindByNameAsync(user);
            var userRoles = await _userManager.GetRolesAsync(userCheck);
            foreach (var role in userRoles)
            {
                if (role == "Admin")
                {
                    System.Diagnostics.Debug.WriteLine(userRoles.ToString(), "ThangLog");
                    return true;
                }
            }
            return false;
        }

        
        
    }
}
