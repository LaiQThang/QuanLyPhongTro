using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.ViewModels;
using System.Security.Claims;

namespace QuanLyPhongTro.Controllers
{
    public class AuthencationController : Controller
    {
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IUserStore<IdentityUser> _userStore;
		private readonly ILogger<InputModel> _logger;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AuthencationController(
			UserManager<IdentityUser> userManager,
			IUserStore<IdentityUser> userStore,
			SignInManager<IdentityUser> signInManager,
			ILogger<InputModel> logger,
			RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_userStore = userStore;
			_signInManager = signInManager;
			_logger = logger;
			_roleManager = roleManager;
		}
		public IActionResult Denied()
        {
            return View();
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if(claimUser.Identity.IsAuthenticated )
            {
				return RedirectToAction("Index", "Home");
			}
            return View();
        }

		
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
        public async Task<IActionResult> Register(InputModel.RegisterInput inputa)
        {
            if(true)
            {
				System.Diagnostics.Debug.WriteLine(inputa.HoTen, "LogThang");
				var model = new InputModel(_userManager, _userStore, _signInManager, _logger, _roleManager);
				var result = model.OnPost(inputa);
				if (await result == true)
				{
					ViewData["ValidateMessageSucess"] = "Register Success, Login now";
					return RedirectToAction("Login");
				}
				if (await result == false)
				{
					ViewData["ValidateMessage"] = "Error";
					return View();
				}

			}
			ViewData["ValidateMessage"] = "Error2";
			return View();
        }

        [HttpPost]
		public async Task<IActionResult> Login(ApplicationUser modelLogin)
		{
            if(modelLogin.UserName == "admin@gmai.com" && modelLogin.PasswordHash == "123123")
            {
                List<Claim> claims  = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.UserName),
                    new Claim("OtherProperties", "Emxample Bol")
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

				AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLogin
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), properties);

               return RedirectToAction("Index", "Home");
			}
			ViewData["ValidateMessage"] = "Login Faild";
			return View();
		}
	}
}
