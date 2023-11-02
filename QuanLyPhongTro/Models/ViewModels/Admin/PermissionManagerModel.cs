using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;

namespace QuanLyPhongTro.Models.ViewModels.Admin
{
    public class PermissionManagerModel
    {
        private readonly RoleManager<IdentityRole> _context;

        public PermissionManagerModel(RoleManager<IdentityRole> role)
        {
            _context = role;
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public List<IdentityRole> Roles { get; set; }

        public async Task<List<IdentityRole>> getAllPermission()
        {
            var roles = await _context.Roles.ToListAsync();
            return roles;
        }
    }
}
