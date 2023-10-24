using Azure.Core;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.WebUtilities;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuanLyPhongTro.Models.Domain;
using System;
using System.ComponentModel.DataAnnotations;
//using System.Text.Encodings.Web;
//using System.Text;
//using System.Web.WebPages.Html;

namespace QuanLyPhongTro.Models.ViewModels
{
	public class InputModel
	{
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
		private readonly ILogger<InputModel> _logger;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IPasswordHasher<IdentityUser> _passwordHasher;

		public InputModel(
			UserManager<IdentityUser> userManager,
			IUserStore<IdentityUser> userStore,
			SignInManager<IdentityUser> signInManager,
			ILogger<InputModel> logger,
			RoleManager<IdentityRole> roleManager,
			IPasswordHasher<IdentityUser> currentUserManager)
		{
			_userManager = userManager;
			_userStore = userStore;
			_emailStore = GetEmailStore();
			_signInManager = signInManager;
			_logger = logger;
			_roleManager = roleManager;
			_passwordHasher = currentUserManager;
		}

		public RegisterInput Input { get; set; }

		public class RegisterInput
		{
			[Required]
			[Display(Name = "Ho Ten")]
			public string HoTen { get; set; }

			[Required]
			[Display(Name = "Email")]
			public string Email { get; set; }

			[Required]
			[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
			[DataType(DataType.Password)]
			[Display(Name = "Password")]
			public string? Password { get; set; }

			[DataType(DataType.Password)]
			[Display(Name = "Confirm password")]
			[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
			public string ConfirmPassword { get; set; }

			[Required]
			[StringLength(11, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
			[Display(Name = "Phone Number")]
			public string PhoneNumber { get; set; }
		}

		

		public async Task<bool> OnPost(RegisterInput input)
		{

			if (input != null)
			{
				var user = CreateUser();


				await _userStore.SetUserNameAsync(user, input.Email, CancellationToken.None);
				await _emailStore.SetEmailAsync(user, input.Email, CancellationToken.None);

				user.HoTen = input.HoTen;
				user.PhoneNumber = input.PhoneNumber;

				//var psh = HashPassword(input.Password);
				//System.Diagnostics.Debug.WriteLine(psh, "LogThangPS");

				var result = await _userManager.CreateAsync(user, input.Password);

					System.Diagnostics.Debug.WriteLine(result, "LogThangInput");
				if (result.Succeeded)
				{
					_logger.LogInformation("User created a new account with password.");

					await _userManager.AddToRoleAsync(user, "Client");

					return true;
				}
				
			}

			return false;
		}

		private IUserEmailStore<IdentityUser> GetEmailStore()
		{
			if (!_userManager.SupportsUserEmail)
			{
				throw new NotSupportedException("The default UI requires a user store with email support.");
			}
			return (IUserEmailStore<IdentityUser>)_userStore;
		}

		private ApplicationUser CreateUser()
		{
			try
			{
				return Activator.CreateInstance<ApplicationUser>();
			}
			catch
			{
				throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
					$"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
					$"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
			}
		}
		public string HashPassword(string password)
		{
			// Tạo một đối tượng User để băm mật khẩu
			var user = new IdentityUser();

			// Đặt mật khẩu cho đối tượng User
			string PasswordHash = _passwordHasher.HashPassword(null, password);

			// Trả về mật khẩu đã được băm
			return PasswordHash;
		}
	}
}
