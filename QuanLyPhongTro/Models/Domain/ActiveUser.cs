using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongTro.Models.Domain
{
    public class ActiveUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime TimeLogin { get; set; }
        public string? ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
