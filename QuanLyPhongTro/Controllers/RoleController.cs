using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.ActionFilter;
using QuanLyPhongTro.Models.ViewModels;

namespace QuanLyPhongTro.Controllers
{
    [ServiceFilter(typeof(FilterRoleClient))]

    public class RoleController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RoleController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [Route("/role/list")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync("thanglai2k2@gmail.com");
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new UserRoleViewModel
            {
                UserName = user.UserName,
                Roles = userRoles.ToList()
            };

            return View(model);
        }
    }
}
