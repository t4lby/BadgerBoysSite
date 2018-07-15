using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using BadgerBoysWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using BadgerBoysWebApp.Data;
using System.Net.Mail;

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
            return View("Contact");
        }

        public IActionResult SubmitBooking(Booking booking)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.sendgrid.net";
            client.Port = 587;

            // setup Smtp authentication
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential("azure_cc18401b53e5436e7acb9457b0a3d640@azure.com", "Polo2468!");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("badgerboysband@gmail.com");
                msg.To.Add(new MailAddress(booking.Email));
                msg.CC.Add(new MailAddress("badgerboysband@gmail.com"));

                msg.Subject = "Badger Boys Booking: " + booking.Date.ToShortDateString();
                msg.IsBodyHtml = true;
                msg.Body = string.Format("<p>Thank you for contacting us! </p> <p>We have received your request for the following event...</p> <p>Date: {0}</p> <p>Description: {1}</p> <p> We will contact you shortly with our availability and a quote.</p> <p>Best Wishes,</p> <p>Badger Boys</p>", booking.Date, booking.Description);
                client.Send(msg);

                
                ViewBag.Message = "Thank you! We will be in touch shortly";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error occured while sending your message." + ex.Message;
            }

            
            return View("Contact");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
