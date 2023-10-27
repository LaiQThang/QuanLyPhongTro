using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongTro.Models.Domain
{
    public class ChiTietDatPhong
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public DateTime? ThoiGianBD { get; set; }
        public DateTime? ThoiGianKT { get; set; }
        public int? SoNguoi { get; set; }
        public string GhiChu { get; set; } = null!;
        public bool flag { get; set; }
        public int PhongTroId { get; set; }
        public PhongTro PhongTro { get; set; } = null!;
        public int NguoiDungID { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;

    }
}
