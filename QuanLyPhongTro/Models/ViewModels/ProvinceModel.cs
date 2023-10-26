using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;

namespace QuanLyPhongTro.Models.ViewModels
{
    public class ProvinceModel
    {
        private readonly RoomManagementContext _context;
        public ProvinceModel(RoomManagementContext room)
        {
              _context = room;
        }

        public List<TinhThanh> getAllProvince()
        {
            var provinces = _context.tinhThanhs.ToList();
            if (provinces.Count > 0)
            {
            return provinces;
            }
            return null;
        } 
    }
}
