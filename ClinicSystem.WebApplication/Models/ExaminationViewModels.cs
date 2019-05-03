using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ClinicSystem.Infrastructure.Model;

namespace ClinicSystem.WebApplication.Models
{
    public class ExaminationIndexViewModel
    {

    }

    public class DiseasesViewModel
    {
        public IEnumerable<DISEASE> Diseases { get; set; }
    }

    public class AddDiseaseViewModel
    {
        [Required]
        [Display(Name = "Kod")]
        public string Code { get; set; }
        [Required]
        [Display(Name = "Opis")]
        public string Description { get; set; }
    }
}