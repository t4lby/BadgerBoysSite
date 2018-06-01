using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BadgerBoysWebApp.Data;
using BadgerBoysWebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BadgerBoysWebApp.Controllers
{
    [Authorize]
    public class BadgersOnlyController : Controller
    {
        private static List<Testimonial> _Testimonials = DataAccess.GetAllTestimonials();
        private static List<Gig> _Gigs = DataAccess.GetAllGigs();

        public IActionResult Index()
        {
            return View("Index");
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            var claims = new List<Claim>
            {
                
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            return Index();
        }

        public IActionResult ListTestimonials()
        {
            _Testimonials = DataAccess.GetAllTestimonials();
            return View(_Testimonials);
        }

        public IActionResult EditTestimonial(int index)
        {
            return View(_Testimonials[index]);
        }

        [HttpPost]
        public IActionResult EditTestimonial(Testimonial testimonial)
        {
            _Testimonials[testimonial.Index] = testimonial;
            DataAccess.SaveAllTestimonials(_Testimonials);
            return RedirectToAction("ListTestimonials");
        }

        public IActionResult AddTestimonial()
        {
            _Testimonials = DataAccess.GetAllTestimonials();
            _Testimonials.Insert(0, new Testimonial());
            return RedirectToAction("EditTestimonial", new { index = 0 });
        }

        public IActionResult DeleteTestimonial(int index)
        {
            _Testimonials.RemoveAt(index);
            DataAccess.SaveAllTestimonials(_Testimonials);
            return RedirectToAction("ListTestimonials");
        }

        public IActionResult MoveTestimonialUp(int index)
        {
            if (index != 0)
            {
                var toMove = _Testimonials[index];
                _Testimonials.RemoveAt(index);
                _Testimonials.Insert(index - 1, toMove);
                DataAccess.SaveAllTestimonials(_Testimonials);
            }
            return RedirectToAction("ListTestimonials");
        }

        public IActionResult MoveTestimonialDown(int index)
        {
            if (index < _Testimonials.Count - 1)
            {
                var toMove = _Testimonials[index];
                _Testimonials.RemoveAt(index);
                _Testimonials.Insert(index + 1, toMove);
                DataAccess.SaveAllTestimonials(_Testimonials);
            }
            return RedirectToAction("ListTestimonials");
        }

        public IActionResult ListGigs()
        {
            _Gigs = DataAccess.GetAllGigs();
            return View(_Gigs);
        }

        public IActionResult AddGig()
        {
            _Gigs.Insert(0, new Gig());
            return RedirectToAction("EditGig", new { index = 0 });
        }

        public IActionResult EditGig(int index)
        {
            return View(_Gigs[index]);
        }

        [HttpPost]
        public IActionResult EditGig(Gig gig)
        {
            _Gigs[gig.Index] = gig;
            DataAccess.SaveAllGigs(_Gigs
                .OrderByDescending(g => g.Date)
                .ToList());
            return RedirectToAction("ListGigs");
        }

        public IActionResult DeleteGig(int index)
        {
            _Gigs.RemoveAt(index);
            DataAccess.SaveAllGigs(_Gigs);
            return RedirectToAction("ListGigs");
        }
    }
}