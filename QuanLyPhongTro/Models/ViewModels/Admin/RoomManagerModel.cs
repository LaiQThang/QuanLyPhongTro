using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;

namespace QuanLyPhongTro.Models.ViewModels.Admin
{
    public class RoomManagerModel
    {
        private readonly RoomManagementContext _context;

        public RoomManagerModel(RoomManagementContext room)
        {
            _context = room;
        }

        public List<PhongTro> phongTros { get; set; }

        public List<PhongTro> getAllRooms()
        {
            var model = _context.phongTros.ToList();
            return model;
        }
    }
}
