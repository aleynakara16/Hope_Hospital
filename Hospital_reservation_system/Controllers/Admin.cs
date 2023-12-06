using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_reservation_system.Controllers
{
    [Authorize(Roles = "admin")]
    public class Admin : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
