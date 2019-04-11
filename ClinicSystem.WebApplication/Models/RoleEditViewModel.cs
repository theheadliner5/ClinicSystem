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
        public IEnumerable<ASPNETROLES> Roles { get; set; }
        [Display(Name = "Rola")]
        public string RoleId { get; set; }
        public string AspNetUserId { get; set; }
    }
}