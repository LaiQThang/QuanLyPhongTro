using Microsoft.AspNetCore.Identity;

namespace QuanLyPhongTro.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string? HoTen { get; set; } 
        public byte GioiTinh { get; set; }
        public byte TrangThai { get; set; }
        public string Anh { get; set; }
        public bool KeepLogin { get; set; }
        public bool EnabledLogin { get; set; }
        public ICollection<ChiTietDatPhong> ChiTietDatPhongs { get; set; } = null!;
        public ICollection<BaiDang> BaiDangs { get; set; } = null!;
        public ICollection<PhongTro> PhongTros { get; set; } = null!;
        public ICollection<ActiveUser> ActiveUsers { get; set; } = null!;
    }
}
