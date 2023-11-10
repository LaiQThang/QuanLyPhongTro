using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Controllers.Components;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.ViewModels;

namespace QuanLyPhongTro.Controllers
{
    public class SideBarSearchController : ComponentsController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoomManagementContext _roomManagementContext;
        public SideBarSearchController(ILogger<HomeController> logger, RoomManagementContext roomManagementContext) : base(roomManagementContext)
        {
            _logger = logger;
            _roomManagementContext = roomManagementContext;
        }

        public IActionResult SearchAll(string name, DateTime ngayBD, DateTime ngayKT) 
        {
            Authencation();
            var model = new SideBarSearchModel(_roomManagementContext);
            var list = model.getPosterSearch(name, ngayBD, ngayKT);
            var viewModel = viewModelSide(list);
            return View(viewModel);
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
