using Hospital_reservation_system.Entities;
using Hospital_reservation_system.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
                    View(model);
                }
                //userId kontrolü
                if (_databaseContext.Doctors.Any(x => x.Id.ToString() == model.id.ToString()))
                {
                    ModelState.AddModelError(nameof(model.id), "TC is already exists.");
                    View(model);
                }
               
                Doctor doktor = new() { 
                    name = model.name ,
                    Password = model.Password
                }; 
                
         
                _databaseContext.Doctors.Add(doktor);
                int affectedRowCount = _databaseContext.SaveChanges();

                if (affectedRowCount == 0)
                {
                    ModelState.AddModelError("", "Doctor can not be added.");
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }

            return View(model);
        }
    }
}
