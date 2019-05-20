using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ClinicSystem.Infrastructure.Dtos;
using ClinicSystem.Infrastructure.Model;

namespace ClinicSystem.WebApplication.Models
{
    public class MedicineIndexViewModel
    {
        public IEnumerable<MedicineDto> MedicineDtos { get; set; }
    }

    public class AddMedicineTypeViewModel
    {
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Nazwa'")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Aktywny składnik'")]
        [Display(Name = "Aktywny składnik")]
        public string ActiveIngredient { get; set; }
    }

    public class AddMedicineViewModel
    {
        public IEnumerable<MEDICINE_TYPE> MedicineTypes { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Typ'")]
        [Display(Name = "Typ")]
        public long MedicineTypeId { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Seria'")]
        [Display(Name = "Seria")]
        public string BatchSeries { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Cena'")]
        [Display(Name = "Cena")]
        public decimal? Cost { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Data przydatności'")]
        [Display(Name = "Data przydatności")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpirationDate { get; set; }
    }
}