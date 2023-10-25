using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;

namespace QuanLyPhongTro.Models.ViewModels
{
    public class ProfileModel
    {

        private readonly RoomManagementContext _roomManagementContext;
        public ProfileModel(RoomManagementContext roomManagementContext)
        {
            _roomManagementContext = roomManagementContext;
        }

        public IEnumerable<ApplicationUser> ProfileGet(string cookieUser)
        {
            var res = _roomManagementContext.applicationUsers.Where(p => p.UserName == cookieUser);
            if(res.Any())
            {
                return res;
            }
            return null;
        }



        public async Task<bool> EditUser(string cookie, string Anh, string HoTen, string Email, string PhoneNumber, byte GioiTinh)
        {
            var user = _roomManagementContext.applicationUsers.FirstOrDefault(p => p.UserName == cookie);
            if (user is ApplicationUser) 
            {
                user.Anh = Anh;
                user.HoTen = HoTen;
                user.Email = Email;
                user.PhoneNumber = PhoneNumber;
                user.GioiTinh = GioiTinh;


                await _roomManagementContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
