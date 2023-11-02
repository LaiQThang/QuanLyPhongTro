using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.ViewModels.Admin;

namespace QuanLyPhongTro.Controllers.Admin
{
    [Authorize]
    public class BookedManagerController : Controller
    {
        private readonly RoomManagementContext _context;

        public BookedManagerController(RoomManagementContext room)
        {
            _context = room;
        }
        public IActionResult BookedList()
        {
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
