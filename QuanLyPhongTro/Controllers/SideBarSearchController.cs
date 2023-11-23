using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.ActionFilter;
using QuanLyPhongTro.Controllers.Components;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Pagination;
using QuanLyPhongTro.Models.ViewModels;

namespace QuanLyPhongTro.Controllers
{
    [ServiceFilter(typeof(FilterRoleClient))]

    public class SideBarSearchController : ComponentsController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoomManagementContext _roomManagementContext;
        public SideBarSearchController(ILogger<HomeController> logger, RoomManagementContext roomManagementContext) : base(roomManagementContext)
        {
            _logger = logger;
            _roomManagementContext = roomManagementContext;
        }

        public async Task <IActionResult> SearchAll(string name, DateTime ngayBD, DateTime ngayKT, int pg = 1) 
        {
            Authencation();

            const int pageSize = 3;
            if (pg < 1)
            {
                pg = 1;
            }

            var model = new SideBarSearchModel(_roomManagementContext);

            int recsCount = await model.getCountSearch(name, ngayBD);
            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var list = model.getPosterSearch(name, ngayBD, ngayKT, recSkip, pageSize);
            var viewModel = viewModelSide(list);
            this.ViewBag.Pager = pager;
            ViewBag.ValueSearch = name;
            ViewBag.ValueDateS = ngayBD;
            ViewBag.ValueDateE = ngayKT;
            return View(viewModel);
        }

        [Route("/api/sortPoster/{name?}/{ngayBD?}/{ngayKT?}/{pg?}")]
        public async Task<IActionResult> SortPort(string name, DateTime ngayBD, DateTime ngayKT, int pg = 1)
        {
            string sqlFormattedDate = ngayBD.ToString("yyyy-MM-dd");
            const int pageSize = 3;
            if (pg < 1)
            {
                pg = 1;
            }
            int recSkip = (pg - 1) * pageSize;
            var model = new SideBarSearchModel(_roomManagementContext);
            var list = await model.SortPoster(name, sqlFormattedDate, ngayKT, recSkip, pageSize);
            return Json(list);
        }

        public SideBarSearchModel viewModelSide(List<Models.Domain.BaiDang> baiDangs)
        {
            var model = new SideBarSearchModel(_roomManagementContext)
            {
                baiDangs = baiDangs,
            };
            return model;
            
        }
       

    }
}
