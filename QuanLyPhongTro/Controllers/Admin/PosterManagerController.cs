using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.ViewModels.Admin;

namespace QuanLyPhongTro.Controllers.Admin
{
    [Authorize]
    public class PosterManagerController : Controller
    {
        private readonly RoomManagementContext _context;

        public PosterManagerController(RoomManagementContext room)
        {
            _context = room;
        }
        public IActionResult PosterList()
        {
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
