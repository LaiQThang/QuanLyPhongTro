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

    public class UserManagerController : ComponentsAdminController
    {
        private readonly RoomManagementContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public UserManagerController(RoomManagementContext room, UserManager<IdentityUser> userManager) : base(room)
        {
            _context = room;
            _userManager = userManager;
        }
        public async Task<IActionResult> UserList()
        {
            Authencation();
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
