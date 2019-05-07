using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ClinicSystem.Infrastructure.Dtos;
using ClinicSystem.Infrastructure.Model;

namespace ClinicSystem.WebApplication.Models
{
    public class ExaminationIndexViewModel
    {
        public string UnitName { get; set; }
        public IEnumerable<VisitDto> UnitVisitDtos { get; set; }
        public IEnumerable<VisitDto> PatientVisitDtos { get; set; }
        public IEnumerable<PATIENT_DIAGNOSE> PatientDiagnoses { get; set; }
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

    public class VisitDetailsViewModel
    {
        public VisitDto VisitDto { get; set; }
        public IEnumerable<PATIENT_DIAGNOSE> PatientDiagnoses { get; set; }
        public IEnumerable<DIAGNOSTICS> Diagnostics { get; set; }
    }

    public class AddDiagnoseViewModel
    {
        public long VisitId { get; set; }
    }

    public class AddExaminationViewModel
    {
        public long VisitId { get; set; }
    }
}