using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;

namespace QuanLyPhongTro.Models.ViewModels
{
    public class BookedModel
    {
        private readonly RoomManagementContext _roomManagementContext;

        public BookedModel(RoomManagementContext room)
        {
            _roomManagementContext = room;
        }

        public List<ChiTietDatPhong> chiTietDatPhongs { get; set; }
        public ChiTietDatPhong chiTietDatPhong { get; set; }

        public async Task<int> GetCountBooked(string userID)
        {
            return await _roomManagementContext.chiTietDatPhongs.Where(res => res.ApplicationUserId == userID && res.flag == false).CountAsync();
        }
        public List<ChiTietDatPhong> GetAllRoomBooked(string userID, int skip, int size)
        {
            var result = _roomManagementContext.chiTietDatPhongs
                .Join(
                    _roomManagementContext.phongTros,
                    pt => pt.PhongTroId,
                    ct => ct.Id,
                    (pt, ct) => new {ChiTietDatPhong = pt, PhongTro = ct})
                .Join (
                    _roomManagementContext.applicationUsers,
                    dp => dp.PhongTro.NguoiDungID,
                    au => au.Id,
                    (dp, au) => new {ChiTietDatPhong = dp.ChiTietDatPhong, PhongTro = dp.PhongTro, ApplicationUser = au})
                .Join(
                    _roomManagementContext.tinhThanhs,
                    ctp => ctp.ChiTietDatPhong.PhongTro.TinhThanhId,
                    tt =>  tt.Id,
                    (ctp, tt) => new {ChiTietDatPhong = ctp.ChiTietDatPhong, PhongTro = ctp.PhongTro, ApplicationUser = ctp.ApplicationUser, TinhThanh = tt})
                .Where(res => res.ChiTietDatPhong.ApplicationUserId == userID && res.ChiTietDatPhong.flag == false)
                .Skip(skip)
                .Take(size)
                .ToList();
            if(result.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine(userID, "logTHnag");
                return null;
            }
            var list = result.Select(result => result.ChiTietDatPhong).ToList();
            return list;
        }

        public ChiTietDatPhong GetRoomBooked(string userID, int id)
        {
            var result = _roomManagementContext.chiTietDatPhongs
                .Join(
                    _roomManagementContext.phongTros,
                    pt => pt.PhongTroId,
                    ct => ct.Id,
                    (pt, ct) => new { ChiTietDatPhong = pt, PhongTro = ct })
                .Join(
                    _roomManagementContext.applicationUsers,
                    dp => dp.PhongTro.ApplicationUserId,
                    au => au.Id,
                    (dp, au) => new { ChiTietDatPhong = dp.ChiTietDatPhong, PhongTro = dp.PhongTro, ApplicationUser = au })
                .Join(
                    _roomManagementContext.tinhThanhs,
                    ctp => ctp.ChiTietDatPhong.PhongTro.TinhThanhId,
                    tt => tt.Id,
                    (ctp, tt) => new { ChiTietDatPhong = ctp.ChiTietDatPhong, PhongTro = ctp.PhongTro, ApplicationUser = ctp.ApplicationUser, TinhThanh = tt })
                .Where(res => res.ChiTietDatPhong.ApplicationUserId == userID && res.ChiTietDatPhong.Id == id && res.ChiTietDatPhong.flag == false)
                .ToList();
            if (result.Count == 0)
            {
                return null;
            }
            var list = result.Select(result => result.ChiTietDatPhong).First();
            return list;
        }

        public async Task<bool> DeleteBooked(int id)
        {
            var model = _roomManagementContext.chiTietDatPhongs.FirstOrDefault(res => res.Id == id);
            if (model != null)
            {
                var modelRoom = _roomManagementContext.phongTros.FirstOrDefault(res => res.Id == model.PhongTroId);
                if(modelRoom != null )
                {
                var modelPoster = _roomManagementContext.baiDangs.Where(res => res.PhongTroId == modelRoom.Id).ToList();
                    if(modelPoster != null)
                    {
                        foreach (var item in modelPoster) 
                        {
                            item.flag = false;
                        }
                    }
                    modelRoom.flag = false;
                    modelRoom.TinhTrang = 1;
                }
                model.flag = true;
                await _roomManagementContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
