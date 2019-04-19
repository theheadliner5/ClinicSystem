using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicSystem.Infrastructure.Dtos;
using ClinicSystem.Infrastructure.Model;

namespace ClinicSystem.WebApplication.Interfaces
{
    public interface IMedicineRepository
    {
        IEnumerable<MedicineDto> GetAllMedicineDtos();
        void CreateMedicineType(MEDICINE_TYPE medicineType);
        void CreateMedicine(MEDICINE_ORDER medicine);
        IEnumerable<MEDICINE_TYPE> GetAllMedicineTypes();
    }
}
