using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongTro.Models.Domain
{
    public class VungMien
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string? TenMien { get; set; }

        public ICollection<TinhThanh> TinhThanhs { get; set; } = null!;
    }
}
