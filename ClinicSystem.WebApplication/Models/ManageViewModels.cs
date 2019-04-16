using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ClinicSystem.Infrastructure.Dtos;
using ClinicSystem.Infrastructure.Model;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace ClinicSystem.WebApplication.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
        public IList<PERSON> Users { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} musi posiadać przynajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Nowe hasło i potwierdzenie są różne.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Aktualne hasło")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi posiadać przynajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Nowe hasło i potwierdzenie są różne.")]
        public string ConfirmPassword { get; set; }
    }

    public class EditRoleViewModel
    {
        public IEnumerable<ASPNETROLES> Roles { get; set; }
        [Display(Name = "Rola")]
        public string RoleId { get; set; }
        public string AspNetUserId { get; set; }
    }

    public class RegisterDoctorViewModel
    {
        [Required]
        public long PersonId { get; set; }
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
        [Display(Name = "Aktualny oddział")]
        public string UnitName { get; set; }
        [Required]
        public IEnumerable<UnitDto> UnitDtos { get; set; }
        [Required]
        [Display(Name = "Oddział")]
        public long UnitId { get; set; }
    }

    public class AddClinicViewModel
    {
        [Required(ErrorMessage = "Pole Nazwa jest wymagane")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Pole Adres jest wymagane")]
        [Display(Name = "Adres")]
        public string Address { get; set; }
    }

    public class AddUnitViewModel
    {
        public IEnumerable<CLINIC> Clinics { get; set; }
        [Required]
        [Display(Name = "Przychodnia")]
        public long ClinicId { get; set; }
        public IEnumerable<UNIT_TYPE> UnitTypes { get; set; }
        [Required]
        [Display(Name = "Typ oddziału")]
        public long UnitTypeId { get; set; }
        public IEnumerable<UNIT_TYPE> ParentUnitTypes { get; set; }
        [Display(Name = "Oddział nadrzędny")]
        public long? ParentUnitTypeId { get; set; }
    }

    public class AddUnitTypeViewModel
    {
        [Required(ErrorMessage = "Pole Nazwa oddziału jest wymagane")]
        [Display(Name = "Nazwa oddziału")]
        public string Name { get; set; }
    }
}