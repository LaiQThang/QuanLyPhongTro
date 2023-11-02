using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.ViewModels.Admin;

namespace QuanLyPhongTro.Controllers.Admin
{
    [Authorize]
    public class RoomManagerController : Controller
    {
        private readonly RoomManagementContext _context;

        public RoomManagerController(RoomManagementContext room)
        {
            _context = room;
        }
        public IActionResult RoomList()
        {
            var model = new RoomManagerModel(_context);
            var list = model.getAllRooms();
            var viewModel = viewModeRoom(list);
            return View("RoomList", viewModel);
        }

        public RoomManagerModel viewModeRoom(List<PhongTro> phongTros)
        {
            var model = new RoomManagerModel(_context)
            {
                phongTros = phongTros
            };
            return model;
        }
    }
}
