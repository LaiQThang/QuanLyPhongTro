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
            if (await CheckRole() == true)
            {
                return RedirectToAction("Denied", "Authencation");
            }
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
