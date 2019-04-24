using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Infrastructure.Dtos
{
    public class DoctorDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string EmplacementName { get; set; }
        public string ClinicName { get; set; }
        public string ClinicAddress { get; set; }
        public string UnitName { get; set; }
    }
}
