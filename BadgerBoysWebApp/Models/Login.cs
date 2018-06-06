using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BadgerBoysWebApp.Models
{
    public class Login
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
