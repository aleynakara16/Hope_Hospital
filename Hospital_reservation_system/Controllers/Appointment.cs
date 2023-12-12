using Hospital_reservation_system.Entities;
using Hospital_reservation_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

            var selectedDoctorNameList = _databaseContext.Doctors.ToList();
            var PoliclinicList = _databaseContext.Policlinics.ToList();
            // Giriş yapmış kullanıcının ID bilgisini al
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Kullanıcının ID bilgisini view'e gönder
            ViewBag.UserId = userId;

            // View'e verileri gönder
            if (selectedDoctorNameList != null)
            {
                ViewBag.selectedDoctorNameList = new SelectList(selectedDoctorNameList, "Id", "name", "Policlinic");
            }
            if (PoliclinicList != null)
            {
                ViewBag.PoliclinicList = new SelectList(PoliclinicList, "Policlinic_Id", "Policlinic_Name");
            }
        
            return View();
        }
       
        [HttpPost]
        public IActionResult Create(AppointmentViewModel model)
        {
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
