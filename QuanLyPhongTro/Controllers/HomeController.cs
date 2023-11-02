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

namespace QuanLyPhongTro.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoomManagementContext _roomManagementContext;
        public HomeController(ILogger<HomeController> logger, RoomManagementContext roomManagementContext)
        {
            _logger = logger;
            _roomManagementContext = roomManagementContext;
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

        public IActionResult SearchHome(string searchName)
        {
            Authencation();
            var model = new HomeModel(_roomManagementContext);
            var list = model.SearchHome(searchName);
            var viewModel = viewModelHome(list);
            return View(viewModel);
        }

        public HomeModel.HomeInput viewModelHome(List<Models.Domain.BaiDang> baiDangs)
        {
            var model = new HomeModel.HomeInput
            {
                baiDangs = baiDangs,
            };
            return model;
        }

        public bool Authencation()
        {
            var user = GetValueCoookie("AccountUser");
            var model = new FooterModel(_roomManagementContext);
            var countBooked = model.CountBooked();
            var countCustomer = model.CountCustomer();
            var CountPartner = model.CountPartner(); 
            var CountAccess = model.CountAccess(); 
            ViewBag.CountBooked = countBooked;
            ViewBag.CountCustomer = countCustomer;
            ViewBag.CountPartner = CountPartner;
            ViewBag.CountAccess = CountAccess;
            if (user != null)
            {
                ViewBag.CookieValue = user;
                return true;
            }
            return false;
        }
        public string GetValueCoookie(string cookieName)
        {

            if (!string.IsNullOrEmpty(cookieName) && Request != null && Request.Cookies.TryGetValue(cookieName, out string cookieValue))
            {
                return cookieValue;
            }
            return null;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}