using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;

namespace QuanLyPhongTro.Models.ViewModels.Admin
{
    public class BookedManagerModel
    {
        private readonly RoomManagementContext _roomManagementContext;

        public BookedManagerModel(RoomManagementContext room)
        {
            _roomManagementContext = room;
        }

        public List<ChiTietDatPhong> chiTietDatPhongs { get; set; }

        public List<ChiTietDatPhong> getAllBooked()
        {
            var result = _roomManagementContext.chiTietDatPhongs
                .Join(
                    _roomManagementContext.phongTros,
                    pt => pt.PhongTroId,
                    ct => ct.Id,
                    (pt, ct) => new { ChiTietDatPhong = pt, PhongTro = ct })
                .Join(
                    _roomManagementContext.applicationUsers,
                    dp => dp.PhongTro.NguoiDungID,
                    au => au.Id,
                    (dp, au) => new { ChiTietDatPhong = dp.ChiTietDatPhong, PhongTro = dp.PhongTro, ApplicationUser = au })
                .Join(
                    _roomManagementContext.tinhThanhs,
                    ctp => ctp.ChiTietDatPhong.PhongTro.TinhThanhId,
                    tt => tt.Id,
                    (ctp, tt) => new { ChiTietDatPhong = ctp.ChiTietDatPhong, PhongTro = ctp.PhongTro, ApplicationUser = ctp.ApplicationUser, TinhThanh = tt })
                .ToList();
            if (result.Count == 0)
            {
                return null;
            }
            var list = result.Select(result => result.ChiTietDatPhong).ToList();
            return list;
        }
    }
}
