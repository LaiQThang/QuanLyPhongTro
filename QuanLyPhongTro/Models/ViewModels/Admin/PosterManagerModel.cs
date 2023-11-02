using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;

namespace QuanLyPhongTro.Models.ViewModels.Admin
{
    public class PosterManagerModel
    {
        private readonly RoomManagementContext _context;

        public PosterManagerModel(RoomManagementContext room)
        {
            _context = room;
        }

        public List<BaiDang> baiDangs { get; set; }

        public List<BaiDang> getAllPosters()
        {
            var model = _context.baiDangs.ToList();
            return model;
        }
    }
}
