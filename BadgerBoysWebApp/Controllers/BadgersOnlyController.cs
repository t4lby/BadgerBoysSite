using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BadgerBoysWebApp.Data;
using BadgerBoysWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BadgerBoysWebApp.Controllers
{
    public class BadgersOnlyController : Controller
    {
        private static bool _IsBadger = false;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Login login)
        {
            if (login.Password == "whitestripes")
            {
                _IsBadger = true;
            }
            return View();
        }

        public IActionResult ListTestimonials()
        {
            if (_IsBadger)
            {
                return View(DataAccess.GetAllTestimonials());
            }
            return RedirectToAction("Denied");
        }

        public IActionResult Denied()
        {
            return View();
        }
    }
}