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
        public IEnumerable<DISEASE> Diseases { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Choroba'")]
        [Display(Name = "Choroba")]
        public long DiseaseId { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Diagnoza'")]
        [Display(Name = "Diagnoza")]
        public string Diagnose { get; set; }
    }

    public class AddExaminationViewModel
    {
        public long VisitId { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Nazwa badania'")]
        [Display(Name = "Nazwa badania")]
        public string ExaminationName { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Koszt'")]
        [Display(Name = "Koszt")]
        public decimal Cost { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Data badania'")]
        [Display(Name = "Data badania")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExaminationDate { get; set; }
    }

    public class AddPatientMedicineViewModel
    {
        public long VisitId { get; set; }
        public IEnumerable<MedicineDto> MedicineDtos { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Lek'")]
        [Display(Name = "Lek")]
        public long TypeId { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Dawkowanie'")]
        [Display(Name = "Dawkowanie")]
        public string Dose { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Data leczenia'")]
        [Display(Name = "Data leczenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TreatmentDate { get; set; }
    }
}