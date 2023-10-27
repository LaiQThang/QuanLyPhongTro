using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongTro.Models.ViewModels
{
    public class RoomModel
    {
        private readonly RoomManagementContext _context;
        public RoomModel(RoomManagementContext context)
        {
            _context = context; 
        }


        public RoomInput input { get; set; }
        public class RoomInput
        {
            public List<PhongTro> phongTro { get; set;}
            public List<TinhThanh> tinhThanhs { get; set; }

            public PhongTro room { get; set; }

            [Required]
            [DisplayName("Id phong")]
            public int Id { get; set; }
            [Required]
            [DisplayName("Dia chi")]
            public string DiaChi { get; set; }

            [Required]
            [DisplayName("Gia")]
            public decimal Gia { get; set; }

            [Required]
            [DisplayName("Dien Tich")]
            public string DienTich { get; set; }

            [Required]
            [StringLength(11, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DisplayName("So Dien Thoai")]
            public string SDT { get; set; }

            [DisplayName("Yeu Thich")]
            public int YeuThich { get; set; }

            [Required]
            [DisplayName("Tinh Trang")]
            public byte TinhTrang { get; set; }

            [Required]
            [DisplayName("Tinh Thanh")]
            public int TinhThanh { get; set; }
            [Required]
            [DisplayName("Anh")]
            public string Anh { get; set; }
        }
        public List<PhongTro> GetAllRoom(string id)
        {
            return _context.phongTros.Where(res => res.NguoiDungID == id && res.flag == false).ToList();
        }

        public PhongTro GetFirstID(int? id)
        {
            var room = _context.phongTros.FirstOrDefault(rooms => rooms.Id == id);
            if (room != null)
            {
                return room;
            }
            return null;
        }

        public async Task<bool> EditRoom(RoomInput room, int id, string pathfile)
        {
            if (room == null || id == null)
            {
                return false;
            }
            var edit = GetFirstID(id);
            if(edit is PhongTro)
            {
                if(pathfile != "")
                {
                edit.Anh = pathfile;
                }
                edit.DiaChi = room.DiaChi;
                edit.Gia = room.Gia;
                edit.DienTich = room.DienTich;
                edit.SDT = room.SDT;
                edit.TinhTrang = room.TinhTrang;
                edit.TinhThanhId = room.TinhThanh;
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> CreateRoom(RoomInput input, string cookieId, string Anh)
        {
            if (input != null)
            {
                PhongTro room = new PhongTro()
                {
                    DiaChi = input.DiaChi,
                    Gia = input.Gia,
                    DienTich = input.DienTich,
                    SDT = input.SDT,
                    TinhTrang = input.TinhTrang,
                    TinhThanhId = input.TinhThanh,
                    Anh = Anh,
                    NguoiDungID = cookieId,
                };
                await _context.phongTros.AddAsync(room);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteRoom(int id)
        {
            if (id == 0)
            {
                return false;
            }
            var room = GetFirstID(id);
            room.flag = true;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
