using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongTro.Models.StoredProcedure
{
    public class ApiGetPosters
    {
        public int Id { get; set; }
        public string? TieuDe { get; set; }
        public DateTime? NgayTao { get; set; }
        public string? NoiDung { get; set; }
        public string? DiaChi { get; set; }
        public decimal? Gia { get; set; }
        public string? Anh { get; set; }
        public int LuotXem { get; set; }


    }
}
