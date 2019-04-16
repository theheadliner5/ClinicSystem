using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Infrastructure.Dtos
{
    public class DoctorDataDto
    {
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public string UnitName { get; set; }
    }
}
