using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
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

        public List<BaiDang> getPosterPageHome()
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
    }
}
