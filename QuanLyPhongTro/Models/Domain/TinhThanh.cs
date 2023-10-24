using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongTro.Models.Domain
{
    public class TinhThanh
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string? TenTinh { get; set; }
        public int VungMienId { get; set; }
        public VungMien VungMien { get; set; } = null!;

        public ICollection<PhongTro> PhongTros { get; set; } = null!;
    }
}
