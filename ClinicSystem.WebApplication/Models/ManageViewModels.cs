using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ClinicSystem.Infrastructure.Dtos;
using ClinicSystem.Infrastructure.Model;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace ClinicSystem.WebApplication.Models
{
    public class ManageIndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
        public IEnumerable<RegisteredUserDto> RegisteredUsers { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Aktualne hasło'")]
        [DataType(DataType.Password)]
        [Display(Name = "Aktualne hasło")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Nowe hasło'")]
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

    public class RegisterEmployeeViewModel
    {
        public long PersonId { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Imię'")]
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Nazwisko'")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Data zatrudnienia'")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data zatrudnienia")]
        public DateTime HireDate { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Wynagrodzenie'")]
        [Display(Name = "Wynagrodzenie")]
        public decimal Salary { get; set; }
        [Display(Name = "Aktualny oddział")]
        public string UnitName { get; set; }
        public IEnumerable<UnitDto> UnitDtos { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Oddział'")]
        [Display(Name = "Oddział")]
        public long UnitId { get; set; }
        public IEnumerable<ManagerDto> ManagerDtos { get; set; }
        [Display(Name = "Przełożony")]
        public long? ManagerId { get; set; }
        public IEnumerable<EMPLACEMENT> Emplacements { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Stanowisko'")]
        [Display(Name = "Stanowisko")]
        public long EmplacementId { get; set; }
        public IEnumerable<ASPNETROLES> Roles { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Uprawnienia'")]
        [Display(Name = "Uprawnienia")]
        public string RoleId { get; set; }

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
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Przychodnia'")]
        [Display(Name = "Przychodnia")]
        public long ClinicId { get; set; }
        public IEnumerable<UNIT_TYPE> UnitTypes { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Typ oddziału'")]
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

    public class AddEmplacementViewModel
    {
        [Required(ErrorMessage = "Pole Nazwa stanowiska jest wymagane")]
        [Display(Name = "Nazwa stanowiska")]
        public string Name { get; set; }
    }

    public class AddUnitPlanViewModel
    {
        public IEnumerable<UnitDto> UnitDtos { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Oddział'")]
        [Display(Name = "Oddział")]
        public long UnitId { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Typ budżetu'")]
        [Display(Name = "Typ budżetu")]
        public string BudgetType { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Wartość'")]
        [Display(Name = "Wartość")]
        public decimal Value { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Data od'")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data od")]
        public DateTime? DateFrom { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Data do'")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data do")]
        public DateTime? DateTo { get; set; }
    }

    public class DiseasesViewModel
    {
        public IEnumerable<DISEASE> Diseases { get; set; }
    }

    public class AddDiseaseViewModel
    {
        public long? DiseaseId { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Kod'")]
        [Display(Name = "Kod")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Wprowadź wartość w polu 'Opis'")]
        [Display(Name = "Opis")]
        public string Description { get; set; }
    }
}