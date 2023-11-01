using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;

namespace QuanLyPhongTro.Models.ViewModels
{
    public class SideBarSearchModel
    {
        private readonly RoomManagementContext _context;
        public SideBarSearchModel(RoomManagementContext roomManagementContext)
        {
            _context = roomManagementContext;
        }
        public List<BaiDang> baiDangs { get; set; }

        public List<BaiDang> getPosterSearch(string name, DateTime? ngayBD, DateTime ngayKT)
        {
            var model = _context.baiDangs.Where(res => res.flag == false && res != null).ToList();
            System.Diagnostics.Debug.WriteLine(ngayBD, "Thanglog");
            DateTime date = DateTime.Parse("01/01/0001 12:00:00 SA"); 
            if (ngayBD == date)
            {
                ngayBD = DateTime.Now;
            }

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
                .Where(res => res.BaiDang.PhongTro.TinhThanh.TenTinh.Contains(name) 
                              && res.BaiDang.NgayTao < ngayBD
                              && res.BaiDang.flag == false
                              )
                .ToList();
            if (posters.Count == 0)
            {
                return null;
            }
            var result = posters.Select(poster => poster.BaiDang).ToList();
            return result;
        }
    }
}
