using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using BadgerBoysWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using BadgerBoysWebApp.Data;

namespace BadgerBoysWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Gigs()
        {
            return View(
                DataAccess.GetAllGigs()
                .Where(g => g.Date > DateTime.Now)
                .OrderBy(g => g.Date));
        }

        public IActionResult Testimonials()
        {
            return View(DataAccess.GetAllTestimonials());
        }

        public IActionResult Media()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
