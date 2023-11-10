using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.ViewModels;
using System.Text.Json.Serialization;
using System.Text.Json;
using QuanLyPhongTro.Controllers.Components;

namespace QuanLyPhongTro.Controllers
{
    public class SearchController : ComponentsController
    {
        private readonly RoomManagementContext _context;
        public SearchController(RoomManagementContext roomManagementContext) : base(roomManagementContext)
        {
            _context = roomManagementContext;
        }
        public IActionResult TopPoster()
        {
            Authencation();
            var model = new SearchModel(_context);
            var list = model.getPosterSeeMore();
            var viewModel = viewModelSearch(list, null);
            ViewData["Content"] = "Bài đăng";
            return View(viewModel);
        }

        [Route("api/getposters")]
        public async Task<JsonResult> GetPoster()
        {
            var model = new SearchModel(_context);
            var list = model.getPoster();
            if (list == null)
            {
                return Json(null);
            }

            //var options = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.Preserve
            //};

            return Json(await list);
        }

        public IActionResult PosterDetail(int id) 
        {
            Authencation();
            var model = new SearchModel(_context);
            var list = model.GetPosterID(id);
            var viewModel = viewModelSearch(null, list);
            string messageerr = TempData["MessangeError"] as string;
            ViewData["MessangeError"] = messageerr;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> OrderRoom(int idRoom, int poster, DateTime ngayBD, DateTime ngayKT, string ghichu)
        {
            var userID = getUserID();
            if(userID == null)
            {
                return RedirectToAction("Login", "Authencation");
            }
            var model = new SearchModel(_context);
            var result = await model.OrderRoom(idRoom, poster, ngayBD, ngayKT, userID, ghichu);
            if(result == true)
            {
                return RedirectToAction("BookedRoomIndex", "BookedRoom");
            }
            TempData["MessageError"] = "Có lỗi xảy ra !";
            return RedirectToAction("PosterDetail");
        }


        public SearchModel viewModelSearch(List<Models.Domain.BaiDang> baiDangList, Models.Domain.BaiDang baiDang) 
        {
            var model = new SearchModel(_context)
            {
                baiDangs = baiDangList,
                baiDang = baiDang
            };

            return model;
        }
    }
}
