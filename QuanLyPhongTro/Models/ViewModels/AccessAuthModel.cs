using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using System;

namespace QuanLyPhongTro.Models.ViewModels
{
	public class AccessAuthModel
	{
        private readonly RoomManagementContext _roomManagementContext;
        private readonly SignInManager<IdentityUser> _signInManager;
		private readonly IPasswordHasher<IdentityUser> _passwordHasher;
		private readonly UserManager<IdentityUser> _userManager;
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }


        [TempData]
        public string ErrorMessage { get; set; }

        public AccessAuthModel(
			RoomManagementContext roomManagementContext,
			IPasswordHasher<IdentityUser> password, 
			SignInManager<IdentityUser> signInManager,
			UserManager<IdentityUser> user)
        {
            _roomManagementContext = roomManagementContext;
            _passwordHasher = password;
			_signInManager = signInManager;
			_userManager = user;
        }

		public async Task<bool> AccessFailCount(string user)
		{
            var model = await _userManager.FindByEmailAsync(user);
			if(model.AccessFailedCount >= 3)
			{
				return true;
			}
			return false;

        }

        public async Task<bool> CheckAccount(string user,  string password, bool KeepLogin)
        {
			var res = _roomManagementContext.applicationUsers.FirstOrDefault(p => p.UserName == user);
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            //foreach (var provider in ExternalLogins)
            //{
            //    var providerName = provider.Name;
            //    var providerDisplayName = provider.DisplayName;

            //    System.Diagnostics.Debug.WriteLine(providerName, providerDisplayName, "Logcheck123");
            //}
            if (res != null)
			{
				//var result = _passwordHasher.VerifyHashedPassword(null, res.PasswordHash, password);
				////var checkpass = VerifyPassword(res.PasswordHash, password);
				//System.Diagnostics.Debug.WriteLine(result, "logcheck");

				//return result == PasswordVerificationResult.Success;
				//         }
				var user2 = await _userManager.FindByEmailAsync(user);
				if (user2 != null)
				{
				var result = await _signInManager.CheckPasswordSignInAsync(user2, password, lockoutOnFailure: false);
				//System.Diagnostics.Debug.WriteLine(result, "Logcheck");
					if (result.Succeeded)
					{
						return true;
					}
					else
					{
                        user2.AccessFailedCount = user2.AccessFailedCount + 1;
						await _roomManagementContext.SaveChangesAsync();
					}
				}
				System.Diagnostics.Debug.WriteLine(user, "Logcheck1");
				System.Diagnostics.Debug.WriteLine(password, "Logcheck2");
				System.Diagnostics.Debug.WriteLine(KeepLogin, "Logcheck3");

				
			}
			return false;
        }

		//public bool VerifyPassword(string password, string passwordHash)
		//{
		//	// Tạo một đối tượng User tạm thời
		//	var user = new IdentityUser();

		//	// So sánh mật khẩu nhập vào với password hash
		//	var result = _passwordHasher.VerifyHashedPassword(user, passwordHash, password);
		//	System.Diagnostics.Debug.WriteLine(result, "Logcheck2");


		//	// Kiểm tra kết quả của việc so sánh
		//	return result == PasswordVerificationResult.Success;
		//}
	}
}
