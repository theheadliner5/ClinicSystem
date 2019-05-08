using System;
using System.Collections;
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
        public IEnumerable<PATIENT_MEDICINES> Medicines { get; set; }
    }

    public class AddDiagnoseViewModel
    {
        public long VisitId { get; set; }
    }

    public class AddExaminationViewModel
    {
        public long VisitId { get; set; }
        [Required]
        [Display(Name = "Nazwa badania")]
        public string ExaminationName { get; set; }
        [Required]
        [Display(Name = "Koszt")]
        public decimal Cost { get; set; }
        [Required]
        [Display(Name = "Data badania")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExaminationDate { get; set; }
    }

    public class AddPatientMedicineViewModel
    {
        public long VisitId { get; set; }
        public IEnumerable<MedicineDto> MedicineDtos { get; set; }
        [Required]
        [Display(Name = "Lek")]
        public long TypeId { get; set; }
        [Required]
        [Display(Name = "Dawkowanie")]
        public string Dose { get; set; }
        [Required]
        [Display(Name = "Data leczenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TreatmentDate { get; set; }
    }
}