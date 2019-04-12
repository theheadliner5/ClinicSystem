using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClinicSystem.WebApplication.Models
{
    public class AddClinicViewModel
    {
        [Required(ErrorMessage = "Pole Nazwa jest wymagane")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Pole Adres jest wymagane")]
        [Display(Name = "Adres")]
        public string Address { get; set; }
    }
}