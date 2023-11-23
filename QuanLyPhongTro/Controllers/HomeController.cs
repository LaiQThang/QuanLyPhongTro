using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.ViewModels;
using QuanLyPhongTro.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using NuGet.Protocol.Core.Types;
using System.IO;
using Microsoft.AspNetCore.Identity;
using QuanLyPhongTro.Controllers.Components;
using QuanLyPhongTro.ActionFilter;
using QuanLyPhongTro.Models.Pagination;

namespace QuanLyPhongTro.Controllers
{
    //[Authorize]
    [ServiceFilter(typeof(FilterRoleClient))]

    public class HomeController : ComponentsController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoomManagementContext _roomManagementContext;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, RoomManagementContext roomManagementContext, UserManager<IdentityUser> userManager) : base(roomManagementContext)
        {
            _logger = logger;
            _roomManagementContext = roomManagementContext;
            _userManager = userManager;

            
        }

        public async Task<IActionResult> Index()
        {
            Authencation();
            
            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            var model = new IpAddressModel(_roomManagementContext);
            var check = await model.getIpAddress(Convert.ToString(ipAddress));
            if (check == false)
            {
                 await model.AddIpAddress2(ipAddress);
            }
            var modelHome = new HomeModel(_roomManagementContext);
            var list = await modelHome.getPosterPageHome();
            var viewModel = viewModelHome(list);
            DateTime date = DateTime.Now;
            string sqlFormattedDate = date.ToString("yyyy-MM-dd");
            ViewData["DateNow"] = sqlFormattedDate;
            return View(viewModel);
        }
        public async Task<IActionResult> Logout()
        {
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            return RedirectToAction("Login", "Authencation");
		}

        public async Task<IActionResult> SearchHome(string name, DateTime ngayBD, DateTime ngayKT, int pg = 1)
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
            var viewModel = viewModelHome(list);
            this.ViewBag.Pager = pager;
            ViewBag.ValueSearch = name;
            ViewBag.ValueDateS = ngayBD;
            ViewBag.ValueDateE = ngayKT;
            return View(viewModel);
        }

        [Route("api/livesearch/{searchName?}")]
        public async Task<IActionResult> LiveSearch(string searchName)
        {
            try
            {
                var model = new HomeModel(_roomManagementContext);
                var list = await model.LiveSearch(searchName);

                return Json(list);
            }
            catch (Exception ex)
            {
                // Ghi log hoặc trả về lỗi cụ thể
                System.Diagnostics.Debug.WriteLine("Lỗi: " + ex.Message);
                return StatusCode(500, "Đã xảy ra lỗi trong quá trình tìm kiếm.");
            }
        }

        public HomeModel.HomeInput viewModelHome(List<Models.Domain.BaiDang> baiDangs)
        {
            var model = new HomeModel.HomeInput
            {
                baiDangs = baiDangs,
            };
            return model;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}