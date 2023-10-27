using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongTro.Models.Domain
{
    public class PhongTro
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string? DiaChi { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Gia { get; set; }
        public string? DienTich { get; set; }
        public string Anh { get; set; }
        public string? SDT { get; set; }
        public int YeuThich { get; set; }
        public byte TinhTrang { get; set; }
        public bool flag { get; set; }
        public int TinhThanhId { get; set; }
        public TinhThanh TinhThanh { get; set; } = null!;
        public string NguoiDungID { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public ICollection<ChiTietDatPhong> ChiTietDatPhongs { get; set; } = null!;
        public ICollection<BaiDang> BaiDangs { get; set; } = null!;

    }
}
