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
            var user = _db.ASPNETUSERS.SingleOrDefault(e => e.USERNAME == userName);

            if (user != null && user.ASPNETROLES.Any(e => e.NAME == "ADMINISTRATOR"))
            {
                return null;
            }

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

        public IEnumerable<VisitDto> GetAllVisitDtos()
        {
            return _db.PATIENT_VISIT.ToList().Select(e => new VisitDto
            {
                Id = e.ID,
                DateFrom = e.DATE_FROM,
                DateTo = e.DATE_TO.GetValueOrDefault(),
                PersonAddress = e.PERSON.ADDRESS,
                PersonName = e.PERSON.NAME + " " + e.PERSON.LAST_NAME,
                PersonPesel = e.PERSON.PESEL
            });
        }

        public IEnumerable<PATIENT_DIAGNOSE> GetPatientDiagnosesByUnitId(long unitId)
        {
            return _db.PATIENT_DIAGNOSE.Where(e => e.PATIENT_VISIT.UNIT_ID == unitId).ToList();
        }

        public IEnumerable<PATIENT_DIAGNOSE> GetPatientDiagnosesByPersonId(long personId)
        {
            return _db.PATIENT_DIAGNOSE.Where(e => e.PATIENT_VISIT.PERSON_ID == personId);
        }

        public IEnumerable<PATIENT_DIAGNOSE> GetAllPatientDiagnoses()
        {
            return _db.PATIENT_DIAGNOSE.ToList();
        }

        public IEnumerable<DIAGNOSTICS> GetDiagnosticsByPatientVisitId(long visitId)
        {
            return _db.DIAGNOSTICS.Where(e => e.PATIENT_VISIT_ID == visitId).ToList();
        }

        public IEnumerable<PATIENT_DIAGNOSE> GetPatientDiagnosesByPatientVisitId(long visitId)
        {
            return _db.PATIENT_DIAGNOSE.Where(e => e.PATIENT_VISIT_ID == visitId).ToList();
        }

        public VisitDto GetVisitDtoByVisitId(long visitId)
        {
            return _db.PATIENT_VISIT.Where(e => e.PATIENT_VISIT_ID == visitId).Select(e => new VisitDto
            {
                Id = e.ID,
                DateFrom = e.DATE_FROM,
                DateTo = e.DATE_TO.GetValueOrDefault(),
                PersonAddress = e.PERSON.ADDRESS,
                PersonName = e.PERSON.NAME + " " + e.PERSON.LAST_NAME,
                PersonPesel = e.PERSON.PESEL
            }).SingleOrDefault();
        }
    }
}