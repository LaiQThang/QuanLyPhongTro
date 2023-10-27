﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.ViewModels;
using static QuanLyPhongTro.Models.ViewModels.PosterModel;

namespace QuanLyPhongTro.Controllers
{
    [Authorize]
    public class PosterController : Controller
    {
        private readonly RoomManagementContext _roomManagementContext;
        private readonly IWebHostEnvironment _env;

        public PosterController(RoomManagementContext roomManagementContext, IWebHostEnvironment env)
        {
            _roomManagementContext = roomManagementContext;
            _env = env;
        }
        public IActionResult PosterIndex()
        {
            Authencation();
            var model = new PosterModel(_roomManagementContext);
            var userID = getUserID();

            var listPoster = model.getAllPosters(userID);
            var viewModel = getViewModel(listPoster,null);
            return View(viewModel);
        }

        public async Task<IActionResult> CreatePoster()
        {
            Authencation();
            var viewModel = getViewModel(null, null);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePoster(PosterModel.PosterInput posterInput) 
        {
            Authencation();
            var viewModel = getViewModel(null, null);

            var modelPoster = new PosterModel(_roomManagementContext);
            var userID = getUserID();
            var check = await modelPoster.CreatePoster(posterInput, userID);
            if( check == true)
            {
                return RedirectToAction("PosterIndex");
            }
            ViewData["Message"] = "Có lỗi xảy ra";
            return View(viewModel);
        }

        public IActionResult EditPoster(int posterId)
        {
            Authencation();
            var model = new PosterModel(_roomManagementContext);
            var userID = getUserID();
            var result = model.getPoster(userID, posterId);
            var viewModel = getViewModel(null, result);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditPoster(PosterModel.PosterInput posterInput, int posterId) 
        {
            Authencation();
            var model = new PosterModel(_roomManagementContext);
            var result = await model.EditPoster(posterInput, posterId);
            var viewModel = viewModelEdit(posterId);

            if ( result == true )
            {
                ViewData["Message"] = "Thay đổi thành công";
                ViewData["Action"] = "success";
                return View(viewModel);
            }
            ViewData["Action"] = "danger";
            ViewData["Message"] = "Thay đổi thất bại";
            return View(viewModel);
        }

        [HttpPost]
        public async Task<JsonResult> DeletePoster(int posterId)
        {
            if (posterId == 0)
            {
                return new JsonResult(new { StatusCode = 500 });
            }
            var model = new PosterModel(_roomManagementContext);
            var check = await model.DeletePoster(posterId);
            if ( check == true )
            {
                return new JsonResult(Ok());
            }
            return new JsonResult(new { StatusCode = 500 });

        }

        public PosterModel.PosterInput viewModelEdit(int id)
        {
            var model = new PosterModel(_roomManagementContext);
            var userID = getUserID();
            var resultModel = model.getPoster(userID, id);
            var viewModel = getViewModel(null, resultModel);
            return viewModel;
        }

        public string GetValueCoookie(string cookieName)
        {

            if (!string.IsNullOrEmpty(cookieName) && Request != null && Request.Cookies.TryGetValue(cookieName, out string cookieValue))
            {
                return cookieValue;
            }
            return null;
        }

        public bool Authencation()
        {
            var user = GetValueCoookie("AccountUser");

            if (user != null)
            {
                ViewBag.CookieValue = user;
                return true;
            }
            return false;
        }
        public string getUserID()
        {
            var userID = GetValueCoookie("AccountId");
            return userID;
        }

        public PosterModel.PosterInput getViewModel(List<BaiDang> baiDangs, BaiDang baiDang)
        {
            var userID = getUserID();
            var modelRoom = new RoomModel(_roomManagementContext);
            var listRoom = modelRoom.GetAllRoom(userID);
            var viewModel = new PosterModel.PosterInput
            {
                phongTros = listRoom,
                baiDangs = baiDangs,
                baiDang = baiDang
            };
            return viewModel;
        }
    }
}