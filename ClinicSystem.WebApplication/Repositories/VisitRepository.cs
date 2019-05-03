using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClinicSystem.Infrastructure.Dtos;
using ClinicSystem.Infrastructure.Interfaces;
using ClinicSystem.Infrastructure.Model;
using ClinicSystem.WebApplication.Interfaces;

namespace ClinicSystem.WebApplication.Repositories
{
    public class VisitRepository : IVisitRepository
    {
        private readonly IClinicSystemDbContext _db;

        public VisitRepository(IClinicSystemDbContext db)
        {
            _db = db;
        }

        public PERSON GetPersonByUserName(string userName)
        {
            return _db.PERSON.SingleOrDefault(e => e.ASPNETUSERS.USERNAME == userName);
        }

        public IEnumerable<CLINIC> GetAllClinics()
        {
            return _db.CLINIC.ToList();
        }

        public IEnumerable<UnitDto> GetUnitDtosForClinic(long clinicId)
        {
            return _db.UNIT.Where(e => e.CLINIC_ID == clinicId).ToList().Select(e => new UnitDto
            {
                UnitId = e.ID,
                UnitName = e.UNIT_TYPE.UNIT_NAME
            });
        }

        public void SaveVisit(PATIENT_VISIT visit)
        {
            _db.PATIENT_VISIT.Add(visit);
            _db.SaveChanges();
        }

        public string GetClinicNameAndAddressById(long clinicId)
        {
            var clinic = _db.CLINIC.SingleOrDefault(e => e.ID == clinicId);

            return clinic?.NAME + " " + clinic?.ADDRESS;
        }
    }
}