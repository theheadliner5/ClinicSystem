using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Infrastructure.Dtos
{
    public class UnitPlanDto
    {
        public long Id { get; set; }
        public string BudgetType { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public decimal Value { get; set; }
        public string UnitDetails { get; set; }
    }
}
