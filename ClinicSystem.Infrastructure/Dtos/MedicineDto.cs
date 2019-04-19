using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Infrastructure.Dtos
{
    public class MedicineDto
    {
        public string Name { get; set; }
        public string ActiveIngredient { get; set; }
        public decimal Cost { get; set; }
        public string BatchSeries { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Amount { get; set; }
    }
}
