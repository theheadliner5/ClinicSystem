using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.Routing.Constraints;
using ClinicSystem.Infrastructure.Dtos;
using ClinicSystem.Infrastructure.Model;

namespace ClinicSystem.WebApplication.Models
{
    public class VisitIndexViewModel
    {
        public string Message { get; set; }
    }

    public class BookVisitFirstStepViewModel
    {
        [Display(Name = "Imię i nazwisko")]
        public string FullName { get; set; }
        public IEnumerable<CLINIC> Clinics { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Przychodnia'")]
        [Display(Name = "Przychodnia")]
        public long ClinicId { get; set; }
    }

    public class BookVisitSecondStepViewModel
    {
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Data wizyty'")]
        [Display(Name = "Data wizyty")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? DateFrom { get; set; }
        [Display(Name = "Czas obowiązywania (w minutach)")]
        public int Minutes { get; set; }
        public IEnumerable<UnitDto> UnitDtos { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Oddział'")]
        [Display(Name = "Oddział")]
        public long UnitId { get; set; }
        public long ClinicId { get; set; }
    }
}