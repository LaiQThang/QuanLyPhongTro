using Castle.MicroKernel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.ActionFilter;
using QuanLyPhongTro.Controllers.Components;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.ViewModels.Admin;

namespace QuanLyPhongTro.Controllers.Admin
{
    [Authorize]
    //[ServiceFilter(typeof(FilterRole))]
    //[FilterRole("Admin")]
    [ServiceFilter(typeof(FilterRole)) ]

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

    }
}
