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
    [ServiceFilter(typeof(FilterRole))]

    public class RoomManagerController : ComponentsAdminController
    {
        private readonly RoomManagementContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RoomManagerController(RoomManagementContext room, UserManager<IdentityUser> userManager) : base(room)
        {
            _context = room;
            _userManager = userManager;
        }
        public async Task<IActionResult> RoomList()
        {
            Authencation();
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
