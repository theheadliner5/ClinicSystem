using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace ClinicSystem.WebApplication.Models
{
    public class RegisterDoctorViewModel
    {
        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data zatrudnienia")]
        public DateTime HireDate { get; set; }
        [Required]
        [Display(Name = "Wynagrodzenie")]
        public decimal Salary { get; set; }
    }
}