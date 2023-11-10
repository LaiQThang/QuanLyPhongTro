using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;

namespace QuanLyPhongTro.Controllers.Components
{
    public class ComponentsFilterController : Controller
    {
        private readonly RoomManagementContext _roomManagementContext;
        private readonly UserManager<IdentityUser> _userManager;

        public ComponentsFilterController(RoomManagementContext roomManagementContext, UserManager<IdentityUser> userManager)
        {
            _roomManagementContext = roomManagementContext;
            _userManager = userManager;
        }
        protected string GetValueFromCookie(string cookieName)
        {
            if (!string.IsNullOrEmpty(cookieName) && Request != null && Request.Cookies.TryGetValue(cookieName, out string cookieValue))
            {
                return cookieValue;
            }
            return null;
        }
        protected async Task<bool> CheckRole()
        {
            var user = GetValueFromCookie("AccountUser");
            if (user == null)
            {
                return false;
            }
            System.Diagnostics.Debug.WriteLine(user, "thang");
            var userCheck = await _userManager.FindByNameAsync(user);
            var userRoles = await _userManager.GetRolesAsync(userCheck);
            foreach (var role in userRoles)
            {
                if (role == "Admin")
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> CallFunctionFromBaseController()
        {
            var check = await CheckRole();
            return check;
        }
    }
}
