using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;

namespace QuanLyPhongTro.Models.ViewModels
{
    public class IpAddressModel
    {
        private readonly RoomManagementContext _context;

        public IpAddressModel(RoomManagementContext room)
        {
            _context = room;
        }

        public async Task<bool> getIpAddress(string ip)
        {
            var ipcheck = _context.ipAddresses.FirstOrDefault(res => res.ipAddress == ip);
            if(ipcheck != null)
            {
                return true;
            }
            return false;
        }


        public async Task<bool> AddIpAddress(string ipAddress, string request, string method, string path, string queryString, string userAgent)
        {
            IpAddress model = new IpAddress();
            model.ipAddress = ipAddress;
            model.method = method;
            model.path = path;
            model.queryString = queryString;
            model.userAgent = userAgent;

            await _context.ipAddresses.AddAsync(model);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddIpAddress2(string ipAddress)
        {
            IpAddress model = new IpAddress();
            model.ipAddress = ipAddress;

            await _context.ipAddresses.AddAsync(model);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
