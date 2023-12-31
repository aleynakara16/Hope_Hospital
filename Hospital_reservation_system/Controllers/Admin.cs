﻿using Hospital_reservation_system.Entities;
using Hospital_reservation_system.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital_reservation_system.Controllers
{
    [Authorize(Roles = "admin")]
    public class Admin : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public Admin(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DoktorEkle()
        {
            var policlinicList = _databaseContext.Policlinics.ToList();

            // View'e verileri gönder
            if (policlinicList != null)
            {

                ViewBag.PoliclinicList = new SelectList(policlinicList, "Policlinic_Id", "Policlinic_Name");
            }
            return View();
        }

        [HttpPost]
        public IActionResult DoktorEkle(DoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_databaseContext.Doctors.Any(x => x.Id == model.id))
                {
                    ModelState.AddModelError(nameof(model.id), "TC is already exists.");
                    var policlinicList = _databaseContext.Policlinics.ToList();

                    // View'e verileri gönder
                    if (policlinicList != null)
                    {

                        ViewBag.PoliclinicList = new SelectList(policlinicList, "Policlinic_Id", "Policlinic_Name");
                    }
                    return View(model); // Buraya 'return' ekledik
                }
                // Poliklinik nesnesini bul
                var policlinic = _databaseContext.Policlinics.Find(model.PoliclinicId);
                Doctor doktor = new()
                {
                    name = model.name,
                    Id = model.id,
                    Password = model.Password,
                    Policlinic = policlinic,
                };

                _databaseContext.Doctors.Add(doktor);
                int affectedRowCount = _databaseContext.SaveChanges();

                if (affectedRowCount == 0)
                {
                    ModelState.AddModelError("", "Doctor can not be added.");
                }
                else
                {
                    ModelState.AddModelError("", "Doctor be added.");
                    return RedirectToAction("DoktorEkle", "Admin");
                }
            }

            return View(model);
        }

        public IActionResult DoktorSil(string Id)
        {
            Doctor doctor = _databaseContext.Doctors.Find(Id);

            if (doctor != null)
            {
                _databaseContext.Doctors.Remove(doctor);
                _databaseContext.SaveChanges();
            }

            return RedirectToAction(nameof(DoktorListele));

        }
        public IActionResult AdminEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminEkle(AdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                //username kontrolü
                if (_databaseContext.Admins.Any(x => x.Admin_mail.ToLower() == model.Admin_mail.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Admin_mail), "Username is already exists.");
                    View(model);
                }
                //userId kontrolü
                if (_databaseContext.Admins.Any(x => x.Admin_Id.ToString() == model.Admin_Id))
                {
                    ModelState.AddModelError(nameof(model.Admin_Id), "TC is already exists.");
                    View(model);
                }
                Entities.Admin user = new()
                {
                    Admin_mail = model.Admin_mail,
                    Admin_Id = long.Parse(model.Admin_Id),
                    Admin_Password = model.Admin_Password,

                };

                _databaseContext.Admins.Add(user);
                int affectedRowCount = _databaseContext.SaveChanges();

                if (affectedRowCount == 0)
                {
                    ModelState.AddModelError("", "User can not be added.");
                }
                else
                {
                    return RedirectToAction("AdminEkle", "Admin");
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult DoktorListele()
        {
            List<Doctor> doctorsList = _databaseContext.Doctors.ToList();

            // Convert Appointments to AppointmentViewModel
            IEnumerable<DoctorViewModel> doctorViewModels = doctorsList.Select(doctor => new DoctorViewModel
            {
                id = doctor.Id,
                name = doctor.name,
                PoliclinicId = doctor.PoliclinicID.ToString()

            });

            return View(doctorViewModels);
        }

    }
}