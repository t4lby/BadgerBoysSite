using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadgerBoysWebApp.Models
{
    public class Gig
    {
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int Index { get; set; }
    }
}
