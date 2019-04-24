using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicSystem.Infrastructure.Dtos;

namespace ClinicSystem.WebApplication.Interfaces
{
    public interface IDoctorRepository
    {
        IEnumerable<DoctorDto> GetAllDoctorDtos();
    }
}
