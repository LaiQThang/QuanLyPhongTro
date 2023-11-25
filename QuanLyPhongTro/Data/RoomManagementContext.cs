using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.StoredProcedure;

namespace QuanLyPhongTro.Data
{
    public class RoomManagementContext : IdentityDbContext
    {
        public RoomManagementContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<BaiDang> baiDangs { get; set; }
        public DbSet<ChiTietDatPhong> chiTietDatPhongs { get; set; }
        public DbSet<PhongTro> phongTros { get; set; }
        public DbSet<TinhThanh> tinhThanhs { get; set; }
        public DbSet<VungMien> VungMiens { get; set; }
        public DbSet<IpAddress> ipAddresses { get; set; }
        public DbSet<ActiveUser> activeUsers { get; set; }

        public DbSet<ApiGetPosters> apiGetPosters { get; set;}
        public DbSet<ApiLiveSearch> apiLiveSearches { get; set; }
    }
}
