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
    public class Account : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            else { }

            return View(model);
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            else { }

            return View(model);
        }


        public IActionResult Profil()
        {
            return View();
        }

    }
}
