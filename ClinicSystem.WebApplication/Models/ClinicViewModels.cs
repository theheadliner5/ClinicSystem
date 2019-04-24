using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClinicSystem.Infrastructure.Dtos;
using ClinicSystem.Infrastructure.Model;

namespace ClinicSystem.WebApplication.Models
{
    public class ClinicIndexViewModel
    {
        public IEnumerable<ClinicDto> ClinicDtos { get; set; }
    }
}