using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClinicSystem.Infrastructure.Dtos;

namespace ClinicSystem.WebApplication.Models
{
    public class DoctorIndexViewModel
    {
        public IEnumerable<DoctorDto> DoctorDtos { get; set; }
    }
}