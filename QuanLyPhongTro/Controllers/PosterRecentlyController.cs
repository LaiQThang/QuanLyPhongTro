using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Controllers.Components;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.ViewModels;

namespace QuanLyPhongTro.Controllers
{
    public class PosterRecentlyController : ComponentsController
    {
        private readonly RoomManagementContext _roomManagementContext;
        public PosterRecentlyController(RoomManagementContext roomManagementContext) : base(roomManagementContext) 
        {
            _roomManagementContext = roomManagementContext;
        }
        public IActionResult Recently()
        {
            Authencation();
            var rel = GetValueFromCookie("PosterRecently");
            
            if (rel == null)
            {
                return View();
            }
            rel = rel.Replace("[", "").Replace("]", "").Replace("\"", "");
            string[] relArr = rel.Split(',');
            var model = new PosterModel(_roomManagementContext);
            var list = new List<Models.Domain.BaiDang>();
            var length = relArr.Length;
            var count = 0;
            for (var i = length - 2; i >= 0 ; i -= 2)
            {
                if(count == 3) { break; }
                count++;
                var value = relArr[i];
                var id = int.Parse(value);
                var result = model.getPosterRecently(id);
                list.Add(result);
            }
            ViewData["CountPoster"] = length / 2;
            this.ViewBag.ArrDateTime = relArr;
            return View(list);

        }
       
    }
}
