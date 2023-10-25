using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongTro.Data;
using QuanLyPhongTro.Models.Domain;
using QuanLyPhongTro.Models.ViewModels;
using System.Net;

namespace QuanLyPhongTro.Controllers
{
	[Authorize]
	public class ProfileController : Controller
	{
        // GET: ProfileController
        private readonly RoomManagementContext _roomManagementContext;
        private readonly IWebHostEnvironment _env;
        //private readonly ILogger _logger;	
        public ProfileController(RoomManagementContext roomManagementContext, IWebHostEnvironment env)
        {
            _roomManagementContext = roomManagementContext;
            _env = env;
        }
        public ActionResult Index()
		{
            string cookieName = "AccountUser";

            if (Request.Cookies.TryGetValue(cookieName, out string cookieValue))
            {
                ViewBag.CookieValue = cookieValue;
				var model = new ProfileModel(_roomManagementContext);
				var result = model.ProfileGet(cookieValue);
                //System.Diagnostics.Debug.WriteLine(result, "LogThang");
                string messagesucc = TempData["MessangeSuccess"] as string;
                string messageerr = TempData["MessangeError"] as string;
                    ViewData["MessangeSuccess"] = messagesucc;
                    ViewData["MessangeError"] = messageerr;
                return View(result);

            }
            return View();
		}

        [HttpPost]
        public async Task<IActionResult> Edit(IFormFile Anh, string HoTen, string Email, string PhoneNumber, string GioiTinh)
        {
            string cookieName = "AccountUser";

            byte sex = 1;
            string pathfile = "";

            if(GioiTinh == "Nam")
            {
                sex = 0;
            }
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
                //System.Diagnostics.Debug.WriteLine(pathfile, "LogThang");


            }


            var model = new ProfileModel(_roomManagementContext);

            if (Request.Cookies.TryGetValue(cookieName, out string cookieValue))
            {
                var rmImg = _roomManagementContext.applicationUsers.FirstOrDefault(p => p.UserName == cookieValue);
                if (rmImg.Anh != null)
                {
                    string imagePath = Path.Combine(_env.WebRootPath, "imgUP");

                    // Đường dẫn đầy đủ của file ảnh
                    string filePath = Path.Combine(imagePath, rmImg.Anh);

                    // Kiểm tra xem file có tồn tại hay không
                    if (System.IO.File.Exists(filePath))
                    {
                        // Xoá file
                        System.IO.File.Delete(filePath);

                    }
                }
                var result = model.EditUser(cookieValue, pathfile, HoTen, Email, PhoneNumber, sex);
                if (await result == true)
                {
                    TempData["MessangeSuccess"] = "Sửa thành công";
                    return RedirectToAction("Index");
                }

            }
            TempData["MessangeError"] = "Sửa thất bại";
            return RedirectToAction("Index");
        }
	}
}
