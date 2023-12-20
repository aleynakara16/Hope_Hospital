using Microsoft.AspNetCore.Mvc;
using Hospital_reservation_system.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims;
using Hospital_reservation_system.Entities;
using System.Data;

namespace Hospital_reservation_system.Controllers
{
    [Authorize]
    public class Account : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public Account(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
        }
       
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                User user = _databaseContext.Users.SingleOrDefault(x => x.Username.ToLower() == model.Username.ToLower() && x.Password == model.Password.ToString() && x.Role=="user");
                Doctor doctor = _databaseContext.Doctors.SingleOrDefault(x => x.name.ToLower() == model.Username.ToLower() && x.Password == model.Password.ToString() && x.Role == "doctor");
                Entities.Admin admin = _databaseContext.Admins.SingleOrDefault(x => x.Admin_mail.ToLower() == model.Username.ToLower() && x.Admin_Password == model.Password.ToString() && x.Role == "admin");
                                
                if (user != null && doctor ==null && admin == null)
                {//login işleri burada yapılacak
                    if (user.Locked)
                    {
                        ModelState.AddModelError(nameof(model.Username), "User is locked.");
                        return View(model);
                    }
                    //cookşes de tutacaklarımızı belirleyelim
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));//claims.Add("Id", user.Id.ToString())); şeklinde de yapılablirdi
                    claims.Add(new Claim(ClaimTypes.Role, user.Role));
                    claims.Add(new Claim("Username", user.Username));

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                else if (doctor != null && user == null && admin == null)
                {//login işleri burada yapılacak
                    if (doctor.Locked)
                    {
                        ModelState.AddModelError(nameof(doctor.name), "doctor is locked.");
                        return View(model);
                    }
                    //cookşes de tutacaklarımızı belirleyelim
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, doctor.Id.ToString()));//claims.Add("Id", user.Id.ToString())); şeklinde de yapılablirdi
                    claims.Add(new Claim(ClaimTypes.Role, doctor.Role));
                    claims.Add(new Claim("Username", doctor.name));

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                else if (admin != null && user == null && doctor == null)
                {
                    //login işleri burada yapılacak
                    //cookşes de tutacaklarımızı belirleyelim
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, admin.Admin_Id.ToString()));//claims.Add("Id", user.Id.ToString())); şeklinde de yapılablirdi
                    claims.Add(new Claim(ClaimTypes.Role, admin.Role));
                    claims.Add(new Claim("Username", admin.Admin_mail));

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect. o hata ");
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
       
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //username kontrolü
                if (_databaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "Username is already exists.");
                    View(model);
                }
                //userId kontrolü
                if (_databaseContext.Users.Any(x => x.Id == model.UserID))
                {
                    ModelState.AddModelError(nameof(model.UserID), "TC is already exists.");
                    View(model);
                }
                User user = new()
                {
                    Username = model.Username,
                    Password = model.Password,
                    Id=model.UserID
                };

                _databaseContext.Users.Add(user);
                int affectedRowCount = _databaseContext.SaveChanges();

                if (affectedRowCount == 0)
                {
                    ModelState.AddModelError("", "User can not be added.");
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Profil()
        {
            ProfileInfoLoader();

            return View();
        }

        private void ProfileInfoLoader()
        {
            String userid = new String(User.FindFirstValue(ClaimTypes.NameIdentifier));

            User user = _databaseContext.Users.SingleOrDefault(x => x.Id == userid);
            Doctor doctor = _databaseContext.Doctors.SingleOrDefault(x => x.Id.ToString() == userid);
                       
            if (User.IsInRole("user"))
            {
                ViewData["Username"] = user.Username;

            }
            else if (User.IsInRole("doctor"))
            {
                    ViewData["Username"] = doctor.name;

            }

        }

        [HttpPost]
        public IActionResult ProfileChangeFullName([Required][StringLength(50)] string? Username)
        {
            if (ModelState.IsValid)
            {
                String userid = new String(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _databaseContext.Users.SingleOrDefault(x => x.Id.ToString() == userid);
                Doctor doctor = _databaseContext.Doctors.SingleOrDefault(x => x.Id.ToString() == userid);

                if (User.IsInRole("user"))
                {
                    user.Username = Username;
                    _databaseContext.SaveChanges();

                }
                else if (User.IsInRole("doctor"))
                {
                    doctor.name = Username;
                    _databaseContext.SaveChanges();
                }
                else { }
                return RedirectToAction(nameof(Profil));

            }

            ProfileInfoLoader();
            return View("Profil");
        }

        [HttpPost]
        public IActionResult ProfileChangePassword([Required][MinLength(6)][MaxLength(16)] string? password)
        {
            if (ModelState.IsValid)
            {
                String userid = new String(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _databaseContext.Users.SingleOrDefault(x => x.Id.ToString() == userid);
                Doctor doctor = _databaseContext.Doctors.SingleOrDefault(x => x.Id.ToString() == userid);
                if (User.IsInRole("user"))
                {
                    user.Password = password;
                    _databaseContext.SaveChanges();

                }
                else if (User.IsInRole("doctor"))
                {
                    doctor.Password = password;
                    _databaseContext.SaveChanges();

                }
                else { }
                ViewData["result"] = "PasswordChanged";
            }

            ProfileInfoLoader();
            return View("Profil");
        }

    }
}
