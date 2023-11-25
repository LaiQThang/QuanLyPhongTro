using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Data;

namespace QuanLyPhongTro.Models.ViewModels
{
    public class ActiveUserModel
    {
        private readonly RoomManagementContext _roomManagementContext;
        public ActiveUserModel(RoomManagementContext roomManagementContext)
        {
            _roomManagementContext = roomManagementContext;
        }
        public int Id { get; set; }
        public DateTime TimeLogin { get; set; }
        public string? ApplicationUserId { get; set; }

        public async Task<int> getCountUser()
        {
            return await _roomManagementContext.activeUsers.CountAsync();
        }
    }
}
