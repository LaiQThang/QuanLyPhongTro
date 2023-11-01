using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;

namespace QuanLyPhongTro.Models.ViewModels
{
    public class SearchModel
    {
        private readonly RoomManagementContext _context;
        public SearchModel(RoomManagementContext roomManagementContext)
        {
            _context = roomManagementContext;
        }

        public List<BaiDang> baiDangs { get; set; }
        public BaiDang baiDang { get; set; }

        public int Id { get; set; }
        public string? TieuDe { get; set; }
        public DateTime? NgayTao { get; set; }
        public string? NoiDung { get; set; }
        public string? Anh { get; set; }
        public bool flag { get; set; }
        public byte? TrangThai { get; set; }
        public string NguoiDungId { get; set; }
        public string ApplicationUserId { get; set; }
        public int PhongTroId { get; set; }
        public List<BaiDang> getPosterSeeMore()
        {
            var model = _context.baiDangs.Where(res => res.flag == false && res != null).ToList();
            
            var posters = _context.baiDangs
                .Join(
                    _context.phongTros,
                    bd => bd.PhongTroId,
                    pt => pt.Id,
                    (bd, pt) => new { BaiDang = bd, PhongTro = pt })
                .Join(
                    _context.tinhThanhs,
                    tfk => tfk.PhongTro.TinhThanhId,
                    tpk => tpk.Id,
                    (tfk, tpk) => new { BaiDang = tfk.BaiDang, PhongTro = tfk.PhongTro, TinhThanh = tpk })
                .Where(res => res.BaiDang.flag == false)
                .OrderByDescending(res => res.BaiDang.NgayTao)
                .ToList();
            if (posters.Count == 0)
            {
                return null;
            }
            var result = posters.Select(poster => poster.BaiDang).ToList();
            return result;
        }

        public BaiDang GetPosterID( int id)
        {

            var posters = _context.baiDangs
                .Join(
                    _context.phongTros,
                    bd => bd.PhongTroId,
                    pt => pt.Id,
                    (bd, pt) => new { BaiDang = bd, PhongTro = pt })
                .Join(
                    _context.tinhThanhs,
                    tfk => tfk.PhongTro.TinhThanhId,
                    tpk => tpk.Id,
                    (tfk, tpk) => new { BaiDang = tfk.BaiDang, PhongTro = tfk.PhongTro, TinhThanh = tpk })
                .Join(
                    _context.applicationUsers,
                    pt => pt.BaiDang.ApplicationUserId,
                    nd =>  nd.Id,
                    (pt, nd) => new { BaiDang = pt.BaiDang, PhongTro = pt.PhongTro, TinhThanh = pt.TinhThanh, ApplicationUser = nd})
                .Where(res =>  res.BaiDang.Id == id && res.BaiDang.flag == false)
                .ToList();
            if (posters.Count == 0)
            {
                return null;
            }
            var result = posters.Select(poster => poster.BaiDang).First();
            return result;
        }

        public async Task<bool> OrderRoom(int room, int poster, DateTime ngayBD, DateTime ngayKT, string userID, string ghichu)
        {
            if(room == 0)
            {
                return false;
            }
            ChiTietDatPhong model = new ChiTietDatPhong()
            {
                flag = false,
                ThoiGianBD = ngayBD,
                ThoiGianKT = ngayKT,
                ApplicationUserId = userID,
                NguoiDungID = userID,
                PhongTroId = room,
                GhiChu = ghichu
            };
            var roomFirst = _context.phongTros.FirstOrDefault(res => res.Id == room);
            if (roomFirst != null)
            {
                roomFirst.flag = true;
            }
            var posterFirst = _context.baiDangs.FirstOrDefault(res => res.Id == poster);
            if(posterFirst != null)
            {
                posterFirst.flag = true;
            }
            await _context.chiTietDatPhongs.AddAsync(model);
            await _context.SaveChangesAsync();

            return true; 
        }
    }
}
