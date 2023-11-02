using QuanLyPhongTro.Data;

namespace QuanLyPhongTro.Models.ViewModels
{
    public class FooterModel
    {
        private readonly RoomManagementContext _context;
        public FooterModel(RoomManagementContext roomManagementContext)
        {
            _context = roomManagementContext;
        }

        public int CountBooked()
        {
            var num = _context.chiTietDatPhongs.Count();
            return num;
        }

        public int CountCustomer()
        {
            var num = _context.applicationUsers
                              .Count();
            return num;
        }

        public int CountPartner()
        {
            var num = _context.phongTros
                              .Select(res => res.ApplicationUserId)
                              .Distinct()
                              .Count();
            return num;
        }
        public int CountAccess()
        {
            var num = _context.ipAddresses
                              .Count();
            return num;
        }
    }
}
