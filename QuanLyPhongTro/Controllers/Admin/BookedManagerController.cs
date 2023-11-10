using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Controllers.Components;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.ViewModels.Admin;

namespace QuanLyPhongTro.Controllers.Admin
{
    [Authorize]
    public class BookedManagerController : ComponentsAdminController
    {
        private readonly RoomManagementContext _context;

        private readonly UserManager<IdentityUser> _userManager;
        public BookedManagerController(RoomManagementContext room, UserManager<IdentityUser> userManager) :base(room)
        {
            _context = room;
            _userManager = userManager;
        }
        public async Task<IActionResult> BookedList()
        {
            Authencation();
            if (await CheckRole() == true)
            {
                return RedirectToAction("Denied", "Authencation");
            }
            var model = new BookedManagerModel(_context);
            var list = model.getAllBooked();
            var viewModel = viewModeBooked(list);
            return View("BookedList", viewModel);
        }

        public BookedManagerModel viewModeBooked(List<ChiTietDatPhong> chiTietDatPhongs)
        {

            var model = new BookedManagerModel(_context)
            {
                chiTietDatPhongs = chiTietDatPhongs
            };
            return model;
        }

        public async Task<bool> CheckRole()
        {
            var user = GetValueFromCookie("AccountUser");
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
        
    }
}
