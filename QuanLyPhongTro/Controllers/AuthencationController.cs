using Microsoft.AspNetCore.Mvc;

namespace QuanLyPhongTro.Controllers
{
    public class AuthencationController : Controller
    {
        public IActionResult Denied()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
