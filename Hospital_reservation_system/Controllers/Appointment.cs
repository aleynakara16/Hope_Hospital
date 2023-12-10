using Hospital_reservation_system.Entities;
using Hospital_reservation_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital_reservation_system.Controllers
{
    public class Appointment : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public Appointment(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            // Şehir isimlerini alın
            // var cities = _databaseContext.Hospitals.Select(h => h.City).Distinct().ToList();
            // ViewBag.Cities = new SelectList(cities);

            // Boş bir AppointmentViewModel oluşturun
            //var model = new AppointmentViewModel();

            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }


    }
}
