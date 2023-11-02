using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.ViewModels.Admin;

namespace QuanLyPhongTro.Controllers.Admin
{
    [Authorize]
    public class UserManagerController : Controller
    {
        private readonly RoomManagementContext _context;

        public UserManagerController(RoomManagementContext room)
        {
            _context = room;
        }
        public IActionResult UserList()
        {
            var model = new UserManagerModel(_context);
            var list = model.getAllUser();
            var viewModel = viewModelUser(list);
            return View("UserList", viewModel);
        }

        public UserManagerModel viewModelUser(List<ApplicationUser> users)
        {
            var model = new UserManagerModel(_context)
            {
                Users = users
            };
            return model;
        }
    }
}
