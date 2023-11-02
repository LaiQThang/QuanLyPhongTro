using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;

namespace QuanLyPhongTro.Models.ViewModels.Admin
{
    public class UserManagerModel
    {
        private readonly RoomManagementContext _context;

        public UserManagerModel(RoomManagementContext room)
        {
            _context = room;
        }

        public List<ApplicationUser> Users { get; set; }

        public List<ApplicationUser> getAllUser()
        {
            var model = _context.applicationUsers.ToList();
            return model;
        }
    }
}
