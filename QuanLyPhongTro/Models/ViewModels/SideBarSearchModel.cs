using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.StoredProcedure;

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

        public async Task<int> getCountSearch(string name, DateTime? ngayBD)
        {
            DateTime date = DateTime.Parse("01/01/0001 12:00:00 SA");
            if (ngayBD == date)
            {
                ngayBD = DateTime.Now;
            }
            return await _context.baiDangs
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
                .CountAsync();
        }
        public List<BaiDang> getPosterSearch(string name, DateTime? ngayBD, DateTime ngayKT, int recSkip, int pageSize)
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
                .Skip(recSkip)
                .Take(pageSize)
                .ToList();
            if (posters.Count == 0)
            {
                return null;
            }
            var result = posters.Select(poster => poster.BaiDang).ToList();
            return result;
        }

        public async Task<List<ApiGetPosters>> SortPoster(string searchName, string sqlFormattedDate, DateTime ngayKT, int recSkip, int pageSize)
        {
            

            var posters = _context.apiGetPosters
                .FromSqlRaw("SORT_POSTERS @pageSize={0}, @recSkip={1}, @nameProvince={2}, @DateCheckin={3}", pageSize, recSkip, searchName, sqlFormattedDate);

            return await posters.ToListAsync();
        }
    }
}
