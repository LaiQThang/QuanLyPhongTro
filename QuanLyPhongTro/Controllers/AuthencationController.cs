﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;
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
		private readonly IPasswordHasher<IdentityUser> _passwordHasher;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly RoomManagementContext _roomManagementContext;



		public AuthencationController(
			UserManager<IdentityUser> userManager,
			IUserStore<IdentityUser> userStore,
			SignInManager<IdentityUser> signInManager,
			ILogger<InputModel> logger,
			RoleManager<IdentityRole> roleManager,
			IPasswordHasher<IdentityUser> passwordHasher,
			RoomManagementContext roomManagementContext)
		{
			_userManager = userManager;
			_userStore = userStore;
			_signInManager = signInManager;
			_logger = logger;
			_roleManager = roleManager;
			_passwordHasher = passwordHasher;
			_roomManagementContext = roomManagementContext;
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
				var model = new InputModel(_userManager, _userStore, _signInManager, _logger, _roleManager, _passwordHasher);
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
			var access = new AccessAuthModel(_roomManagementContext, _passwordHasher, _signInManager, _userManager);
			var check = access.CheckAccount(modelLogin.UserName, modelLogin.PasswordHash, modelLogin.KeepLogin);

            if(await check == true)
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

				string cookieUser = "AccountUser";
				string cookieId = "AccountId";
                CookieOptions cookieOptions = new CookieOptions
				{
					Expires = DateTime.Now.AddMinutes(20),
				};
                var user = await _userManager.FindByNameAsync(modelLogin.UserName);
                Response.Cookies.Append(cookieUser, modelLogin.UserName, cookieOptions);
				Response.Cookies.Append(cookieId, user.Id, cookieOptions);
				System.Diagnostics.Debug.WriteLine(user.Id, "ThangLog");
                return RedirectToAction("Index", "Home");
			}
			ViewData["ValidateMessage"] = "Login Faild";
			return View();
		}
	}
}
