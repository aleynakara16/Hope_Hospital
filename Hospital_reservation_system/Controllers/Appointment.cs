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
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;// Giriş yapmış kullanıcının ID bilgisini al
            ViewBag.UserId = userId; // Kullanıcının ID bilgisini view'e gönder

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
            if (ModelState.IsValid)
            {
                var doctor = _databaseContext.Doctors.Find(model.selecktedDoctorID);//sORUN BURADA
                var user = _databaseContext.Users.Find(model.currentUserID);

                Appointments new_appointment = new()
                {
                   User=user,
                   Doctor=doctor,
                   Date=model.Date,
                   Time=model.Time,

                };

                _databaseContext.Appointments.Add(new_appointment);
                int affectedRowCount = _databaseContext.SaveChanges();

                if (affectedRowCount == 0)
                {
                    ModelState.AddModelError("", "User can not be added.");
                }
                else
                {
                    return RedirectToAction(nameof(Details));
                }
            }
            var selectedDoctorNameList = _databaseContext.Doctors.ToList();
            var PoliclinicList = _databaseContext.Policlinics.ToList();
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;// Giriş yapmış kullanıcının ID bilgisini al
            ViewBag.UserId = userId; // Kullanıcının ID bilgisini view'e gönder

            // View'e verileri gönder
            if (selectedDoctorNameList != null)
            {
                ViewBag.selectedDoctorNameList = new SelectList(selectedDoctorNameList, "Id", "name", "Policlinic");
            }
            if (PoliclinicList != null)
            {
                ViewBag.PoliclinicList = new SelectList(PoliclinicList, "Policlinic_Id", "Policlinic_Name");
            }

            return View(model);
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
            ShowAppointment();
            return View();
        }
        private void ShowAppointment()
        {
            String userid = new String(User.FindFirstValue(ClaimTypes.NameIdentifier));

            User user = _databaseContext.Users.SingleOrDefault(x => x.Id.ToString() == userid);
            Doctor doctor = _databaseContext.Doctors.SingleOrDefault(x => x.Id.ToString() == userid);

            if (User.IsInRole("user"))
            {
                ViewData["Username"] = user.Username;
                ViewData["Doctorname"] = doctor.name;

            }
            else if (User.IsInRole("doctor"))
            {
                ViewData["Username"] = doctor.name;

            }
        }


    }
}
