using Hospital_reservation_system.Entities;
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

        public Admin(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
        }
       
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult DoktorEkle()
        {
            var policlinicList =  _databaseContext.Policlinics.ToList();

            // View'e verileri gönder
            if (policlinicList != null)
            {
                ViewBag.PoliclinicList = new SelectList(policlinicList, "Policlinic_Id", "Policlinic_Name");
            }
            return View();
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult DoktorEkle(DoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                //username kontrolü
                if (_databaseContext.Doctors.Any(x => x.name.ToLower() == model.name.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.name), "Username is already exists.");
                    return View(model); // Buraya 'return' ekledik
                }

                //userId kontrolü
                if (_databaseContext.Doctors.Any(x => x.Id == model.id))
                {
                    ModelState.AddModelError(nameof(model.id), "TC is already exists.");
                    return View(model); // Buraya 'return' ekledik
                }

                Doctor doktor = new()
                {
                    name = model.name,
                    Id = model.id,
                    Password = model.Password,
                    Policlinic = model.Policlinic
                };

                _databaseContext.Doctors.Add(doktor);
                int affectedRowCount = _databaseContext.SaveChanges();

                if (affectedRowCount == 0)
                {
                    ModelState.AddModelError("", "Doctor can not be added.");
                }
                else
                {
                    return RedirectToAction("DoktorEkle", "Admin");
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult AdminEkle()
        {
            return View();
        }

        [AllowAnonymous]
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
                if (_databaseContext.Admins.Any(x => x.Admin_Id == model.Admin_Id))
                {
                    ModelState.AddModelError(nameof(model.Admin_Id), "TC is already exists.");
                    View(model);
                }
                Entities.Admin user = new()
                {
                    Admin_mail = model.Admin_mail,
                    Admin_Id = model.Admin_Id,
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

    }
}
