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
            PopliclicDropdowns(); // Dropdown listelerini dolduran yardımcı metod

            return View();
        }

        [HttpPost]
        public IActionResult Create(AppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doctor = _databaseContext.Doctors.Find(model.selecktedDoctorID);
                var user = _databaseContext.Users.Find(model.currentUserID);

                Appointments new_appointment = new()
                {
                    User = user,
                    Doctor = doctor,
                    Date = model.Date,
                    Time = model.Time,
                    Policlinicname = model.policlinicID.ToString()
                };

                _databaseContext.Appointments.Add(new_appointment);
                int affectedRowCount = _databaseContext.SaveChanges();

                if (affectedRowCount == 0)
                {
                    ModelState.AddModelError("", "Appointment can not be added.");
                }
                else
                {
                    return RedirectToAction(nameof(Details));
                }
            }

            PopliclicDropdowns(); // Dropdown listelerini dolduran yardımcı metod

            return View(model);
        }

        private void PopliclicDropdowns()
        {
            var selectedDoctorNameList = _databaseContext.Doctors.ToList();
            var PoliclinicList = _databaseContext.Policlinics.ToList();

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ViewBag.UserId = userId;

            // View'e verileri gönder
            if (selectedDoctorNameList != null)
            {
                ViewBag.selectedDoctorNameList = new SelectList(selectedDoctorNameList, "Id", "name");
            }
            if (PoliclinicList != null)
            {
                ViewBag.PoliclinicList = new SelectList(PoliclinicList, "Policlinic_Id", "Policlinic_Name");
            }
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
        private List<Doctor> GetDoctorsByPoliclinicFromDatabase(long policlinicID)
        {
            
            var doctors = _databaseContext.Doctors
                    .Where(d => d.PoliclinicID == policlinicID)
                    .ToList();

            return doctors;
        }
        public IActionResult Delete()
        {
            return View();
        }
        public IActionResult Details()
        {
            ShowAppointment();
            return View();
        }
        private void ShowAppointment()
        {
        }


    }
}
