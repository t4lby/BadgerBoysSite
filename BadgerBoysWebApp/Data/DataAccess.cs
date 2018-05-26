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
            var testis = XDocument.Load("Data/site-data.xml")
                .Root
                .Element("testimonials")
                .Elements("testimonial")
                .Select(n => new Testimonial
                {
                    Name = n.Element("name").Value,
                    Content = n.Element("content").Value
                })
                .ToList();
            for (int i = 0; i < testis.Count; i++)
            {
                testis[i].Index = i;
            }
            return testis;
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
                    Index = id,
                    Name = n.Element("name").Value,
                    Content = n.Element("content").Value
                }).Single();
        }

        public static void SaveTestimonial(Testimonial t)
        {
            var doc = XDocument.Load("Data/site-data.xml");
            var element = doc.Root
                .Element("testimonials")
                .Elements("testimonial")
                .Where(el => el.Attribute("id").Value == t.Index.ToString())
                .Single();
            element.Element("name").Value = t.Name;
            element.Element("content").Value = t.Content;
            doc.Save("Data/site-data.xml");
        }

        public static void SaveAllTestimonials(List<Testimonial> testimonials)
        {
            var doc = XDocument.Load("Data/site-data.xml");
            var tNode = doc.Root
                .Element("testimonials");
            tNode.RemoveAll();
            foreach (Testimonial t in testimonials)
            {
                tNode.Add(new XElement(
                    "testimonial",
                    new XElement("name", t.Name),
                    new XElement("content", t.Content)
                    ));
            }
            doc.Save("Data/site-data.xml");
        }
    }
}
