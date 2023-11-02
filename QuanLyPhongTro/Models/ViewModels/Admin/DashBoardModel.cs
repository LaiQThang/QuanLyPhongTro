using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;

namespace QuanLyPhongTro.Models.ViewModels.Admin
{
    public class DashBoardModel
    {
        private readonly RoomManagementContext _context;

        public DashBoardModel(RoomManagementContext room)
        {
            _context = room;
        }

        public ApplicationUser GetUserKey(string username)
        {
            var model = _context.applicationUsers.Where(res => res.Email == username).FirstOrDefault();
            if (model == null)
            {
                return null;
            }
            return model;
        }
    }
}
