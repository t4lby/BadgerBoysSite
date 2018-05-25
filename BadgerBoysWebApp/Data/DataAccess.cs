using BadgerBoysWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BadgerBoysWebApp.Data
{
    public static class DataAccess
    {
        public static List<Testimonial> GetAllTestimonials()
        {
            return XDocument.Load("Data/site-data.xml")
                .Root
                .Element("testimonials")
                .Elements("testimonial")
                .Select(n => new Testimonial
                {
                    Id = int.Parse(n.Attribute("id").Value),
                    Name = n.Element("name").Value,
                    Content = n.Element("content").Value
                })
                .ToList();
        }

        public static Testimonial GetTestimonial(int id)
        {
            return XDocument.Load("Data/site-data.xml")
                .Root
                .Element("testimonials")
                .Elements("testimonial")
                .Where(t => t.Attribute("id").Value == id.ToString())
                .Select(n => new Testimonial
                {
                    Id = id,
                    Name = n.Element("name").Value,
                    Content = n.Element("content").Value
                }).Single();
        }
    }
}
