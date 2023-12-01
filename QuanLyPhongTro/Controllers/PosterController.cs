using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.ActionFilter;
using QuanLyPhongTro.Controllers.Components;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.Pagination;
using QuanLyPhongTro.Models.ViewModels;
using static QuanLyPhongTro.Models.ViewModels.PosterModel;

namespace QuanLyPhongTro.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(FilterRoleClient))]

    public class PosterController : ComponentsController
    {
        private readonly RoomManagementContext _roomManagementContext;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<IdentityUser> _userManager;


        public PosterController(RoomManagementContext roomManagementContext, IWebHostEnvironment env, UserManager<IdentityUser> userManager) : base(roomManagementContext)
        {
            _roomManagementContext = roomManagementContext;
            _env = env;
            _userManager = userManager;
        }
        public async Task<IActionResult> PosterIndex(int pg = 1, string status = "still")
        {
            Authencation();

            const int pageSize = 3;
            if (pg < 1)
            {
                pg = 1;
            }
            bool convertStatus = false;
            if(status == "still")
            {
                convertStatus = false;
            }
            else if(status == "unstill")
            {
                convertStatus = true;
            }
            var model = new PosterModel(_roomManagementContext);
            var userID = getUserID();

            int recsCount = await model.CountPosterUser(userID, convertStatus);

            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            var listPoster = model.getAllPosters(userID, recSkip, pageSize, convertStatus);

            var viewModel = getViewModel(listPoster,null);
            this.ViewBag.Pager = pager;
            ViewData["CountPoster"] = recsCount;
            this.ViewBag.Status = status;
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

        
        public PosterModel.PosterInput getViewModel(List<Models.Domain.BaiDang> baiDangs, Models.Domain.BaiDang baiDang)
        {
            var userID = getUserID();
            var modelRoom = new RoomModel(_roomManagementContext);
            var listRoom = modelRoom.GetAllRoomPoster(userID);
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
