using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ClinicSystem.Infrastructure.Model;

namespace ClinicSystem.WebApplication.Models
{
    public class RoleEditViewModel
    {
        public IEnumerable<AspNetRoles> Roles { get; set; }
        [Display(Name = "Rola")]
        public string RoleName { get; set; }
        public long PersonId { get; set; }
    }
}