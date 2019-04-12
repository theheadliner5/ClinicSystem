﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;

namespace ClinicSystem.WebApplication.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Adres e-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "Numer PESEL musi posiadać dokładnie 11 znaków", MinimumLength = 11)]
        [Display(Name = "PESEL")]
        public string Pesel { get; set; }

        [Required]
        [Display(Name = "Data urodzenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi posiadać przynajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło i jego potwierdzenie są różne.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi posiadać przynajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło i jego potwierdzenie są różne.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
