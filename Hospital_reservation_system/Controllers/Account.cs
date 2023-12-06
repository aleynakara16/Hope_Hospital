using Microsoft.AspNetCore.Mvc;
using Hospital_reservation_system.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
//using NETCore.Encrypt.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims;
using Hospital_reservation_system.Entities;

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

                User user = _databaseContext.Users.SingleOrDefault(x => x.Username.ToLower() == model.Username.ToLower() && x.Password == model.Password.ToString());

                if (user != null)
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
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect.");
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
                if (_databaseContext.Users.Any(x => x.Id.ToString() == model.UserID.ToString()))
                {
                    ModelState.AddModelError(nameof(model.UserID), "TC is already exists.");
                    View(model);
                }
                User user = new()
                {
                    Username = model.Username,
                    Password = model.Password
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
            return View();
        }

    }
}
