﻿using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.StoredProcedure;
using System.Reflection.Metadata.Ecma335;

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

        public async Task<int> getCountPoster(int start, int end)
        {
            decimal decimalStart = (decimal)start;
            decimal decimalEnd = (decimal)end;
            var poster = await _context.baiDangs
                .Include(b => b.PhongTro)
                .Where(res => res.flag == false && res.PhongTro.Gia > decimalStart && res.PhongTro.Gia < decimalEnd).CountAsync();
            return poster;
        }

        public List<BaiDang> getPosterSeeMore(int recSkip, int pageSize, int start, int end, string quantity)
        {
            decimal decimalStart = (decimal)start;
            decimal decimalEnd = (decimal)end;
            var model = _context.baiDangs.Where(res => res.flag == false && res != null).ToList();
            if (quantity == "low")
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
                    .Where(res => res.BaiDang.flag == false && res.PhongTro.Gia > decimalStart && res.PhongTro.Gia < decimalEnd)
                    .OrderByDescending(res => res.BaiDang.LuotXem)
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
            else if (quantity == "hight")
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
                    .Where(res => res.BaiDang.flag == false && res.PhongTro.Gia > decimalStart && res.PhongTro.Gia < decimalEnd)
                    .OrderBy(res => res.BaiDang.LuotXem)
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
            else
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
                   .Where(res => res.BaiDang.flag == false && res.PhongTro.Gia > decimalStart && res.PhongTro.Gia < decimalEnd)
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
        }

        public async Task<List<ApiGetPosters>> getPoster(int pageSize, int recsSkip)
        {

            var posters = _context.apiGetPosters
                .FromSqlRaw("GET_POSTERS @pageSize={0}, @recSkip={1}", pageSize, recsSkip);

            return await posters.ToListAsync();
        }

        public BaiDang GetPosterID(int id)
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
                    nd => nd.Id,
                    (pt, nd) => new { BaiDang = pt.BaiDang, PhongTro = pt.PhongTro, TinhThanh = pt.TinhThanh, ApplicationUser = nd })
                .Where(res => res.BaiDang.Id == id && res.BaiDang.flag == false)
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
            if (room == 0)
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
            var roomFirst = await _context.phongTros.FirstAsync(res => res.Id == room);
            if (roomFirst != null)
            {
                roomFirst.TinhTrang = 2;
            }
            var posterFirst = await _context.baiDangs.Where(res => res.PhongTroId == room).ToListAsync();
            if (posterFirst != null)
            {
                foreach (var val in posterFirst)
                {
                    val.flag = true;
                }
            }
            await _context.chiTietDatPhongs.AddAsync(model);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateQuantityView(int idPoster)
        {
            var rel = await _context.baiDangs.FirstOrDefaultAsync(res => res.Id == idPoster);
            if(rel == null) 
            {
                return false;
            }
            if(rel.LuotXem == null)
            {
                rel.LuotXem = 1;
            }
            else
            {
                rel.LuotXem = rel.LuotXem + 1;
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
