using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
