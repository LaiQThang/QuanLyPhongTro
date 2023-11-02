using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.ViewModels.Admin;

namespace QuanLyPhongTro.Controllers.Admin
{
    [Authorize]
    public class PermissionManagerController : Controller
    {
        private readonly RoleManager<IdentityRole> _context;

        public PermissionManagerController(RoleManager<IdentityRole> role)
        {
            _context = role;
        }
        public async Task<IActionResult> PermissionList()
        {
            var model = new PermissionManagerModel(_context);
            var list = await model.getAllPermission();
            var viewModel = viewModelRole(list);
            return View("PermissionList", viewModel);
        }

        public PermissionManagerModel viewModelRole(List<IdentityRole> role)
        {
            var model = new PermissionManagerModel(_context)
            {
                Roles = role
            };
            return model;
        }
    }
}
