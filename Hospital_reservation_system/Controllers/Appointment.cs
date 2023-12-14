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
        [HttpPost]
        public IActionResult GetDoctorsByPoliclinic(long policlinicID)
        {
            // Burada veritabanınızdan seçilen poliklinik adına göre doktorları getirin.
            // Örnek veritabanı sorgusu:
            var doctors = GetDoctorsByPoliclinicFromDatabase(policlinicID);

            return Json(doctors);
        }

        // Bu metot, veritabanından seçilen poliklinik adına göre doktorları getirir.
        private List<string> GetDoctorsByPoliclinicFromDatabase(long policlinicID)
        {
            // Burada gerçek veritabanı sorgularınızı yapın ve doktorları getirin.
            // Örnek olarak sadece string listesi döndürüyorum:
            // Aşağıdaki kod gerçek bir veritabanı sorgusu olmamakla birlikte, bu adıma uygun bir sorgu yapılmalıdır.
            var doctors = _databaseContext.Doctors
                    .Where(d => d.PoliclinicID == policlinicID)
                    .Select(d => d.name)
                    .ToList();

            return doctors;
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
