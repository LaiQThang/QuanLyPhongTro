using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongTro.Models.Domain
{
    public class IpAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? ipAddress { get; set; }
        public string? request { get; set; }
        public string? method { get; set; }
        public string? path { get; set; }
        public string? queryString { get; set; }
        public string? userAgent { get; set; }

    }
}
