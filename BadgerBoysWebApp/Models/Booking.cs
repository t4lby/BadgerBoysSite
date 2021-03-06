﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BadgerBoysWebApp.Models
{
    public class Booking
    {
        public string Name { get; set; }
        public string Email { get; set; }

        [Display(Name = "Event Date")]
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
