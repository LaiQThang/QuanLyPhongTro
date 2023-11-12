using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using System.Data.SqlTypes;

namespace QuanLyPhongTro.Models.ViewModels
{
    public class PosterModel
    {
        private readonly RoomManagementContext _context;

        public PosterModel(RoomManagementContext room)
        {
            _context = room;
            
        }

        public PosterInput posterInput { get; set; }

        public class PosterInput
        {
            public List<PhongTro> phongTros { get; set; }
            public List<BaiDang> baiDangs { get; set; }
            public BaiDang baiDang { get; set; }
            public List<TinhThanh> tinhs { get; set; }
            public int Id { get; set; }
            public string? TieuDe { get; set; }
            public DateTime? NgayTao { get; set; }
            public string? NoiDung { get; set; }
            public string? Anh { get; set; }
            public bool flag { get; set; }
            public byte? TrangThai { get; set; }
            public int NguoiDungID { get; set; }
            public int PhongTroId { get; set; }

        }

        public async Task<bool> CreatePoster(PosterInput posterInput, string userID)
        {
            if (posterInput == null)
            {
                return false;
            }
            DateTime dateTime = DateTime.Now;
            BaiDang baiDang = new BaiDang()
            {
                PhongTroId = posterInput.PhongTroId,
                TieuDe = posterInput.TieuDe,
                NgayTao = dateTime,
                NoiDung = posterInput.NoiDung,
                ApplicationUserId = userID,
                NguoiDungId = userID,
                flag = false,
            };
            await _context.baiDangs.AddAsync(baiDang);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> CountPosterUser(string userID)
        {
            return await _context.baiDangs
                .Where(res => res.ApplicationUserId == userID && res.flag == false)
                .CountAsync();
        }

        public List<BaiDang> getAllPosters(string userID, int recSkip, int pageSize)
        {
            var posters = _context.baiDangs
                .Join(
                    _context.phongTros,
                    bd => bd.PhongTroId,
                    pt => pt.Id,
                    (bd, pt) => new { BaiDang = bd, PhongTro = pt })
                .Join (
                    _context.tinhThanhs,
                    tfk => tfk.PhongTro.TinhThanhId,
                    tpk => tpk.Id,
                    (tfk, tpk) => new { BaiDang = tfk.BaiDang, PhongTro = tfk.PhongTro , TinhThanh = tpk})
                .Where(res => res.BaiDang.ApplicationUserId == userID && res.BaiDang.flag == false)
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

        public BaiDang getPoster(string userID, int id)
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
                .Where(res => res.BaiDang.ApplicationUserId == userID && res.BaiDang.Id == id)
                .ToList();
            if (posters.Count == 0)
            {
                return null;
            }
            var result = posters.Select(poster => poster.BaiDang).First();
            return result;
        }

        public async Task<bool> EditPoster(PosterInput posterInput, int posterId)
        {
            var modelPoster = _context.baiDangs.FirstOrDefault(res => res.Id == posterId);
            if (posterInput == null || modelPoster == null)
            {
                return false;
            }
            if (modelPoster != null)
            {
                modelPoster.TieuDe = posterInput.TieuDe;
                modelPoster.NoiDung = posterInput.NoiDung;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeletePoster(int id)
        {
            var data = _context.baiDangs.FirstOrDefault(res => res.Id == id);
            if (data == null)
            {
                return false;
            }
            data.flag = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
