﻿using System;
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
            return _db.PATIENT_VISIT.Where(e => e.ID == visitId).ToList().Select(e => new VisitDto
            {
                Id = e.ID,
                DateFrom = e.DATE_FROM,
                DateTo = e.DATE_TO.GetValueOrDefault(),
                PersonAddress = e.PERSON.ADDRESS,
                PersonName = e.PERSON.NAME + " " + e.PERSON.LAST_NAME,
                PersonPesel = e.PERSON.PESEL
            }).SingleOrDefault();
        }

        public void CreateExamination(string examinationName, DateTime examinationDate, decimal cost,
            long visitId, long employeeId)
        {
            var examination = new EXAMINATION
            {
                LAST_MOD_DATE = DateTime.Now,
                EXAMINATION_NAME = examinationName,
                COST = cost
            };

            _db.EXAMINATION.Add(examination);

            var unitId =_db.PATIENT_VISIT.FirstOrDefault(e => e.ID == visitId)?.UNIT_ID;

            var unitPlan = _db.UNIT_PLAN.FirstOrDefault(e => e.UNIT_ID == unitId &&
                e.DATE_FROM <= examinationDate && examinationDate <= e.DATE_TO);

            var diagnostics = new DIAGNOSTICS
            {
                LAST_MOD_DATE = DateTime.Now,
                EXAMINATION = examination,
                EXAMINATION_DATE = examinationDate,
                PATIENT_VISIT_ID = visitId,
                EMPLOYEE_ID = employeeId,
                UNIT_PLAN = unitPlan
            };

            _db.DIAGNOSTICS.Add(diagnostics);

            _db.SaveChanges();
        }

        public EMPLOYEE GetAdministratorAccountEmployee(string userName)
        {
            var person = _db.PERSON.SingleOrDefault(e => e.ASPNETUSERS.USERNAME == userName
                                                         && e.NAME == "Admin"); // TEMP, BECAUSE OF USER_ID REPETITIONS

            return _db.EMPLOYEE.SingleOrDefault(e => e.PERSON_ID == person.ID);
        }

        public IEnumerable<PATIENT_MEDICINES> GetPatientMedicinesByPatientVisitId(long visitId)
        {
            return _db.PATIENT_MEDICINES.Where(e => e.PATIENT_VISIT_ID == visitId).ToList();
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
    }
}