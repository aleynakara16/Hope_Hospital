﻿using Hospital_reservation_system.Entities;
using Hospital_reservation_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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


        public async Task<IActionResult> Index()
        {
            List<Appointments> appointmentsList = _databaseContext.Appointments.ToList();

            // Convert Appointments to AppointmentViewModel
            IEnumerable<AppointmentViewModel> appointmentViewModels = appointmentsList.Select(appointment => new AppointmentViewModel
            {
                currentUserID = appointment.UserID,
                selecktedDoctorID = appointment.DoctorID,
                policlinicID=appointment.Policlinicname,
                Date = appointment.Date,
                Time = appointment.Time
            });

            return View(appointmentViewModels);
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
                if (IsRandevuAvailable(model.selecktedDoctorID, model.Date + model.Time.TimeOfDay))
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
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Seçtiğiniz tarihte doktorun randevusu var");

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
        public IActionResult GetDoctorsByPoliclinic(string policlinicID)
        {
            // Burada veritabanınızdan seçilen poliklinik adına göre doktorları getirin.
            // Örnek veritabanı sorgusu:
            var doctors = GetDoctorsByPoliclinicFromDatabase(policlinicID);

            return Json(doctors);
        }

        // Bu metot, veritabanından seçilen poliklinik adına göre doktorları getirir.
        private List<Doctor> GetDoctorsByPoliclinicFromDatabase(string policlinicID)
        {

            var doctors = _databaseContext.Doctors
                    .Where(d => d.PoliclinicID == policlinicID)
                    .ToList();

            return doctors;
        }

        public bool IsRandevuAvailable(string doktorId, DateTime randevuTarihi)
        {
            // Belirtilen doktorun aynı gün aynı saatte başka bir randevusu var mı kontrol et
            bool isAvailable = !_databaseContext.Appointments
                .Any(r => r.DoctorID == doktorId && r.Date == randevuTarihi.Date && r.Time.TimeOfDay == randevuTarihi.TimeOfDay);

            return isAvailable;
        }

    }
}