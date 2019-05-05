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
    public class ExaminationRepository : IExaminationRepository
    {
        private readonly IClinicSystemDbContext _db;

        public ExaminationRepository(IClinicSystemDbContext db)
        {
            _db = db;
        }

        public IEnumerable<DISEASE> GetAllDiseases()
        {
            return _db.DISEASE.ToList();
        }

        public void SaveDisease(DISEASE disease)
        {
            _db.DISEASE.Add(disease);
            _db.SaveChanges();
        }

        public PERSON GetLoggedPersonByUserName(string userName)
        {
            return _db.PERSON.SingleOrDefault(e => e.ASPNETUSERS.USERNAME == userName);
        }

        public EMPLOYEE GetEmployeeByPersonId(long personId)
        {
            return _db.EMPLOYEE.SingleOrDefault(e => e.PERSON_ID == personId);
        }

        public IEnumerable<VisitDto> GetUnitVisitDtosByUnitId(long unitId)
        {
            return _db.PATIENT_VISIT.Where(e => e.UNIT_ID == unitId).ToList().Select(e => new VisitDto
            {
                Id = e.ID,
                DateFrom = e.DATE_FROM,
                DateTo = e.DATE_TO.GetValueOrDefault(),
                PersonAddress = e.PERSON.ADDRESS,
                PersonName = e.PERSON.NAME + " " + e.PERSON.LAST_NAME,
                PersonPesel = e.PERSON.PESEL
            });
        }

        public IEnumerable<VisitDto> GetPatientVisitDtos(long personId)
        {
            return _db.PATIENT_VISIT.Where(e => e.PERSON_ID == personId).ToList().Select(e => new VisitDto
            {
                Id = e.ID,
                DateFrom = e.DATE_FROM,
                DateTo = e.DATE_TO.GetValueOrDefault(),
                PersonAddress = e.PERSON.ADDRESS,
                PersonPesel = e.PERSON.PESEL
            });
        }
    }
}