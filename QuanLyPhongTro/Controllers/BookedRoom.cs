using Microsoft.AspNetCore.Mvc;

namespace QuanLyPhongTro.Controllers
{
    public class BookedRoom : Controller
    {
        public IActionResult BookedRoomIndex()
        {
            return View();
        }


        public IActionResult BookedRoomInformation()
        {
            return View();
        }
    }
}
