using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.ViewModels;
using System.Text.Json.Serialization;
using System.Text.Json;
using QuanLyPhongTro.Controllers.Components;
using QuanLyPhongTro.ActionFilter;
using QuanLyPhongTro.Models.Pagination;
using System.Drawing.Printing;

namespace QuanLyPhongTro.Controllers
{
    [ServiceFilter(typeof(FilterRoleClient))]

    public class SearchController : ComponentsController
    {
        private readonly RoomManagementContext _context;
        public SearchController(RoomManagementContext roomManagementContext) : base(roomManagementContext)
        {
            _context = roomManagementContext;
        }
        public async Task<IActionResult> TopPoster(int pg = 1, string price = "price")
        {
            Authencation();
            const int pageSize = 3;
            if(pg < 1)
            {
                pg = 1;
            }

            var start = 0;
            var end = int.MaxValue;

            var model = new SearchModel(_context);

            switch (price) {
                case "low":  
                    start = 0;
                    end = 5000000;
                    break;
                case "medium":
                    start = 5000000;
                    end = 10000000;
                    break;
                case "hight":
                    start = 10000000;
                    end = int.MaxValue;
                    break;
            }

            int recsCount = await model.getCountPoster(start, end);
            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;
            //
            var list = model.getPosterSeeMore(recSkip, pageSize, start, end);

            var viewModel = viewModelSearch(list, null);

            this.ViewBag.Pager = pager;
            ViewData["Content"] = "Bài đăng";
            this.ViewBag.Price = price;
            return View(viewModel);
        }

        [Route("api/posters/{pg?}")]
        public async Task<JsonResult> GetPoster(int pg = 1)
        {
            const int pageSize = 3;
            var model = new SearchModel(_context);
            int recSkip = (pg - 1) * pageSize;


            var list = model.getPoster(pageSize, recSkip);

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
            string idConvert = id.ToString();
            //Xử lý phần cookie bài viết vừa xem
            var existCookie = GetValueFromCookie("PosterRecently");
            List<string> cookieRecently;

            if (string.IsNullOrEmpty(existCookie))
            {
                cookieRecently = new List<string>();
            }
            else
            {
                cookieRecently = JsonSerializer.Deserialize<List<string>>(existCookie);
            }
            if(!cookieRecently.Contains(idConvert))
            {
                cookieRecently.Add(idConvert);
                cookieRecently.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                var updatedCookieValue = JsonSerializer.Serialize(cookieRecently);
                Response.Cookies.Append("PosterRecently", updatedCookieValue,  new CookieOptions 
                {
                    Expires = DateTime.Now.AddMinutes(20),
                });
            }
            

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
