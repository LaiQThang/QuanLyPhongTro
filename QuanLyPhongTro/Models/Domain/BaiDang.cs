using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongTro.Models.Domain
{
    public class BaiDang
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string? TieuDe { get; set; }
        public DateTime? NgayTao { get; set; }
        public string? NoiDung { get; set; }
        public string? Anh { get; set; }
        public bool flag { get; set; }
        public byte? TrangThai { get; set; }
        public int NguoiDungID { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public int PhongTroId { get; set; }
        public PhongTro PhongTro { get; set; } = null!;
    }
}
