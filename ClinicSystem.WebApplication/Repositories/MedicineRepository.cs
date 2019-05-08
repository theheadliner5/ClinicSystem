using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using ClinicSystem.Infrastructure.Dtos;
using ClinicSystem.Infrastructure.Interfaces;
using ClinicSystem.Infrastructure.Model;
using ClinicSystem.WebApplication.Interfaces;

namespace ClinicSystem.WebApplication.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly IClinicSystemDbContext _db;

        public MedicineRepository(IClinicSystemDbContext db)
        {
            _db = db;
        }

        public IEnumerable<MedicineDto> GetAllMedicineDtos()
        {
            var medicineDtos = (from mo in _db.MEDICINE_ORDER
                join mt in _db.MEDICINE_TYPE on mo.MEDICINE_TYPE_ID equals mt.ID
                where !_db.PATIENT_MEDICINES.Any(pm => pm.MEDICINE_ORDER_ID == mo.ID)
                group new { mo, mt } by new
                    { mt.ID, mt.MEDICINE_NAME, mo.COST, mo.EXPIRE_DATE, mo.MEDICINE_BATCH_SERIES, mt.ACTIVE_INGREDIENT }
                into g
                select new MedicineDto
                {
                    TypeId = g.Key.ID,
                    Name = g.Key.MEDICINE_NAME,
                    Amount = g.Count(),
                    ActiveIngredient = g.Key.ACTIVE_INGREDIENT,
                    BatchSeries = g.Key.MEDICINE_BATCH_SERIES,
                    Cost = g.Key.COST,
                    ExpirationDate = g.Key.EXPIRE_DATE
                }).ToList();

            return medicineDtos;
        }

        public void CreateMedicineType(MEDICINE_TYPE medicineType)
        {
            _db.MEDICINE_TYPE.Add(medicineType);
            _db.SaveChanges();
        }

        public void CreateMedicine(MEDICINE_ORDER medicine)
        {
            _db.MEDICINE_ORDER.Add(medicine);
            _db.SaveChanges();
        }

        public IEnumerable<MEDICINE_TYPE> GetAllMedicineTypes()
        {
            return _db.MEDICINE_TYPE.ToList();
        }
    }
}