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

        public static List<Gig> GetAllGigs()
        {
            var gigs = XDocument.Load("Data/site-data.xml")
                .Root
                .Element("gigs")
                .Elements("gig")
                .Select(n => new Gig
                {
                    Location = n.Element("location").Value,
                    Description = n.Element("description").Value,
                    Date = new DateTime(
                        year: int.Parse(n.Element("date").Element("year").Value),
                        month: int.Parse(n.Element("date").Element("month").Value),
                        day: int.Parse(n.Element("date").Element("day").Value),
                        hour: int.Parse(n.Element("date").Element("hour").Value),
                        minute: int.Parse(n.Element("date").Element("minute").Value),
                        second: 0
                        ),
                    Link = n.Element("link").Value
                })
                .ToList();
            for (int i = 0; i < gigs.Count; i++)
            {
                gigs[i].Index = i;
            }
            return gigs;
        }

        public static void SaveAllGigs(List<Gig> gigs)
        {
            var doc = XDocument.Load("Data/site-data.xml");
            var gNode = doc.Root.Element("gigs");
            gNode.RemoveAll();
            foreach (Gig gig in gigs)
            {
                gNode.Add(new XElement(
                    "gig",
                    new XElement("location", gig.Location),
                    new XElement("description", gig.Description),
                    new XElement("link", gig.Link),
                    new XElement("date",
                        new XElement("year", gig.Date.Year),
                        new XElement("month", gig.Date.Month),
                        new XElement("day", gig.Date.Day),
                        new XElement("hour", gig.Date.Hour),
                        new XElement("minute", gig.Date.Minute)
                        )));
            }
            doc.Save("Data/site-data.xml");
        }
    }
}
