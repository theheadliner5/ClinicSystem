using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicSystem.Infrastructure.Model;

namespace ClinicSystem.Infrastructure.Dtos
{
    public class RegisteredUserDto
    {
        public long PersonId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Pesel { get; set; }
        public string RoleName { get; set; }
        public string ClinicName { get; set; }
        public string SupervisorName { get; set; }
        public string EmplacementName { get; set; }
    }
}
