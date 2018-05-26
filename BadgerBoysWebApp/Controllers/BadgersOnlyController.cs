using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BadgerBoysWebApp.Data;
using BadgerBoysWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BadgerBoysWebApp.Controllers
{
    public class BadgersOnlyController : Controller
    {
        private static List<Testimonial> _Testimonials = DataAccess.GetAllTestimonials();

        public IActionResult Index()
        {
            return View();
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
    }
}