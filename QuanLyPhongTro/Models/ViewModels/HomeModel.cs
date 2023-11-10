using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.StoredProcedure;
using System.Linq;

namespace QuanLyPhongTro.Models.ViewModels
{
    public class HomeModel
    {
        private readonly RoomManagementContext _context;

        public HomeModel(RoomManagementContext room)
        {
            _context = room;
        }

        public HomeInput homeInput { get; set; }

        public class HomeInput
        {
            public List<BaiDang> baiDangs { get; set; }
        }

        public async Task<List<BaiDang>> getPosterPageHome()
        {
            var model = _context.baiDangs.Where(res => res.flag == false && res != null).ToList();
            var num = 0;
            num = model.Count();
            if (num > 6)
            {
                num = 6;
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
                .Where(res => res.BaiDang.flag == false)
                .OrderByDescending(res => res.BaiDang.NgayTao)
                .Take(num)
                .ToList();
            if (posters.Count == 0)
            {
                return null;
            }
            var result = posters.Select(poster => poster.BaiDang).ToList();
            return result;
        }

        public List<BaiDang> SearchHome(string searchHome)
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
                .Where(res => res.BaiDang.PhongTro.TinhThanh.TenTinh.Contains(searchHome) && res.BaiDang.flag == false)
                .ToList();
            if (posters.Count == 0)
            {
                return null;
            }
            var result = posters.Select(poster => poster.BaiDang).ToList();
            return result;
        }

        public async Task<List<ApiLiveSearch>> LiveSearch(string searchHome)
        {
            var posters = _context.apiLiveSearches.FromSqlRaw("LIVE_SEARCH {0}", searchHome);

            return await posters.ToListAsync();
        }






        //public async Task<List<BaiDang>> LiveSearch(string searchHome)
        //{
        //    var model = _context.baiDangs.Where(res => res.flag == false && res != null).ToList();
        //    var num = 0;
        //    num = model.Count();
        //    if (num > 6)
        //    {
        //        num = 6;
        //    }
        //    var posters = _context.baiDangs
        //        .Join(
        //            _context.phongTros,
        //            bd => bd.PhongTroId,
        //            pt => pt.Id,
        //            (bd, pt) => new { BaiDang = bd, PhongTro = pt })
        //        .Join(
        //            _context.tinhThanhs,
        //            tfk => tfk.PhongTro.TinhThanhId,
        //            tpk => tpk.Id,
        //            (tfk, tpk) => new { BaiDang = tfk.BaiDang, PhongTro = tfk.PhongTro, TinhThanh = tpk })
        //        .Where(res => res.BaiDang.PhongTro.TinhThanh.TenTinh.Contains(searchHome) && res.BaiDang.flag == false)
        //        .OrderByDescending(res => res.BaiDang.NgayTao)
        //        .Take(num);

        //    var result = await posters.Select(poster => poster.BaiDang).ToListAsync();
        //    return result;
        //}
    }
}
