using QuanLyPhongTro.Controllers;
using QuanLyPhongTro.Data;

namespace QuanLyPhongTro.Models.ViewModels
{
    public class PosterRecentlyModel
    {

        public List<BaiDang> baiDangs {  get; set; }

        private readonly RoomManagementContext _context;

        public PosterRecentlyModel(RoomManagementContext room)
        {
            _context = room;

        }
    }
}
