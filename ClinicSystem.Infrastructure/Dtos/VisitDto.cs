using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Infrastructure.Dtos
{
    public class VisitDto
    {
        public long Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string PersonName { get; set; }
        public string PersonPesel { get; set; }
        public string PersonAddress { get; set; }
    }
}
