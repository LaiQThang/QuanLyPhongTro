using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.ActionFilter;
using QuanLyPhongTro.Controllers.Components;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.Pagination;
using QuanLyPhongTro.Models.ViewModels;

namespace QuanLyPhongTro.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(FilterRoleClient))]

    public class RoomController : ComponentsController
    {
        private readonly RoomManagementContext _roomManagementContext;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<IdentityUser> _userManager;

        //private readonly ILogger _logger;	


        public RoomController(RoomManagementContext roomManagementContext, IWebHostEnvironment env, UserManager<IdentityUser> userManager) : base(roomManagementContext)
        {
            _roomManagementContext = roomManagementContext;
            _env = env;
            _userManager = userManager;
        }
        

        
        public async Task<IActionResult> RoomIndex(int pg = 1)
        {
            Authencation();
            

            const int pageSize = 3;
            if (pg < 1)
            {
                pg = 1;
            }

            var user = getUserID();
            var room = new RoomModel(_roomManagementContext);
            var listProvince = GetAllProvince();
            int recsCount = await room.CountRoomUser(user);

            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;


            var listRoom = room.GetAllRoom(user, recSkip, pageSize);
            var viewModel = new RoomModel.RoomInput
            {
                tinhThanhs = listProvince,
                phongTro = listRoom,
            };
            this.ViewBag.Pager = pager;
            ViewData["CountRoom"] = recsCount;
            return View(viewModel);
        }

        public IActionResult CreateRoom()
        {
            Authencation();

            if (ViewModelRoom() != null)
            {
                var viewModel = ViewModelRoom();
                return View(viewModel);
            }
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> CreateRoom(RoomModel.RoomInput roomModel, IFormFile Anh)
        {
            Authencation();
            if (roomModel != null)
            {
                var model = new RoomModel(_roomManagementContext);
                var cookieId = GetValueFromCookie("AccountId");

                string pathfile = "";
                if (Anh != null)
                {
                    // Tạo đường dẫn lưu file dựa trên thư mục wwwroot/images và tên file gốc
                    string uploadsFolder = Path.Combine(_env.WebRootPath, "imgUp");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Anh.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Lưu file vào thư mục
                    await using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        Anh.CopyTo(fileStream);
                        pathfile = uniqueFileName;
                    }
                }
                bool result = await model.CreateRoom(roomModel, cookieId, pathfile);
                if (result)
                {
                return RedirectToAction("RoomIndex");
                }
            }
            var viewModel = ViewModelRoom();
            return View(viewModel);
        }

        public async Task<IActionResult> EditRoom(int? roomId)
        {
            Authencation();
            if (roomId != null)
            {
                
                int nonNullableInt = roomId ?? 0;
                var viewModel = ViewModelRoomEdit(nonNullableInt);
                
                    //ViewData["PhongTroId"] = new SelectList(_roomManagementContext.phongTros, "Id", "Id", baiDang.PhongTroId);
                   
                    return View(viewModel);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditRoom(int Id,RoomModel.RoomInput roomModel, IFormFile Anh)
        {
            Authencation();
            var viewModel = ViewModelRoomEdit(Id);

            if (roomModel == null || Id == null)
            {
                return NotFound();
            }
            if (roomModel != null)
            {
                var model = new RoomModel(_roomManagementContext);
                string pathfile = "";
                System.Diagnostics.Debug.WriteLine(Anh, "LogThang");
                if (Anh != null && Anh.Length > 0)
                {
                    // Tạo đường dẫn lưu file dựa trên thư mục wwwroot/images và tên file gốc
                    string uploadsFolder = Path.Combine(_env.WebRootPath, "imgUp");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Anh.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Lưu file vào thư mục
                    await using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        Anh.CopyTo(fileStream);
                        pathfile = uniqueFileName;
                    }
                    var rmImg = model.GetFirstID(Id);
                    if (rmImg.Anh != null)
                    {
                        string imagePath = Path.Combine(_env.WebRootPath, "imgUP");

                        // Đường dẫn đầy đủ của file ảnh
                        string filePathOld = Path.Combine(imagePath, rmImg.Anh);

                        // Kiểm tra xem file có tồn tại hay không
                        if (System.IO.File.Exists(filePathOld))
                        {
                            // Xoá file
                            System.IO.File.Delete(filePathOld);

                        }
                    }
                }
                
                bool result = await model.EditRoom(roomModel, Id, pathfile);
                if (result)
                {
                    ViewData["MessangeSuccess"] = "Sửa thành công";
                    return View(viewModel);
                }
            }
            ViewData["MessangeSuccess"] = "Có lỗi xảy ra";
            return View(viewModel);
        }

        public async Task<JsonResult> DeleteRoom(int roomId)
        {
            if(roomId == 0)
            {
                return new JsonResult(new { errors = "Có lỗi xảy ra" })
                {
                    StatusCode = 500
                };
            }
            var model = new RoomModel(_roomManagementContext);
            await model.DeleteRoom(roomId);
            
            return new JsonResult(Ok());
        }

        public RoomModel.RoomInput ViewModelRoom()
        {
            var user = getUserID();
            var room = new RoomModel(_roomManagementContext);
            var listProvince = GetAllProvince();
            var listRoom = room.GetAllRoomPoster(user);
            var viewModel = new RoomModel.RoomInput
            {
                tinhThanhs = listProvince,
                phongTro = listRoom,
            };
            return viewModel;
        }

        public List<TinhThanh> GetAllProvince()
        {
            var province = new ProvinceModel(_roomManagementContext);
            var listProvince = province.getAllProvince();
            return listProvince;
        }
        public RoomModel.RoomInput ViewModelRoomEdit(int Id)
        {
            var model = new RoomModel(_roomManagementContext);
            var listProvince = GetAllProvince();
            var result = model.GetFirstID(Id);

            var viewModel = new RoomModel.RoomInput
            {
                room = result,
                tinhThanhs = listProvince,
            };
            return viewModel;
        }
    }
}
