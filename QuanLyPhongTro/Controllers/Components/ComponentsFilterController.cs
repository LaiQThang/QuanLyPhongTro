using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;

namespace QuanLyPhongTro.Controllers.Components
{
    public class ComponentsFilterController : Controller
    {
        private readonly RoomManagementContext _roomManagementContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ComponentsFilterController(RoomManagementContext roomManagementContext, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _roomManagementContext = roomManagementContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        protected async Task<string> GetValueFromCookie(string cookieName)
        {

            if (_httpContextAccessor.HttpContext?.Request != null)
            {
                if (_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(cookieName, out string cookieValue))
                {
                    System.Diagnostics.Debug.WriteLine(cookieValue, "thang3");
                    return cookieValue;
                }
            }

            return null;
        }

        //Nếu action dùng chung thì là protected còn dùng để gọi lại hàm thì public
        public async Task<bool> CallFunctionCheckRoleAdmin()
        {
            var user = await GetValueFromCookie("AccountUser");
            var roleName = "Admin";
            if (user == null)
            {
                return false;
            }
            return await IsRole(roleName, user);
        }
        public async Task<bool> CallFunctionCheckRoleClient()
        {
            var user = await GetValueFromCookie("AccountUser");
            var roleName = "Client";
            if (user == null)
            {
                return false;
            }
            return await IsRole(roleName, user);
        }

        public async Task<bool> IsRole(string roleName, string user)
        {
            var userCheck = await _userManager.FindByNameAsync(user);
            var userRoles = await _userManager.GetRolesAsync(userCheck);
            if (!userRoles.Contains(roleName))
            {
                return true;
            }
            return false;
        }


    }
}
