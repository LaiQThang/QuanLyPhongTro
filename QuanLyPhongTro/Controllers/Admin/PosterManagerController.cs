using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.ActionFilter;
using QuanLyPhongTro.Controllers.Components;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.ViewModels.Admin;

namespace QuanLyPhongTro.Controllers.Admin
{
    [Authorize]
    [ServiceFilter(typeof(FilterRole))]

    public class PosterManagerController : ComponentsAdminController
    {
        private readonly RoomManagementContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PosterManagerController(RoomManagementContext room, UserManager<IdentityUser> userManager) : base(room) 
        {
            _context = room;
            _userManager = userManager;
        }
        public async Task<IActionResult> PosterList()
        {
            Authencation();
            var model = new PosterManagerModel(_context);
            var list = model.getAllPosters();
            var viewModel = viewModelPoster(list);
            return View("PosterList", viewModel);
        }

        public PosterManagerModel viewModelPoster(List<Models.Domain.BaiDang> baiDangs)
        {
            var model = new PosterManagerModel(_context)
            {
                baiDangs = baiDangs
            };
            return model;
        }

        
    }
}
