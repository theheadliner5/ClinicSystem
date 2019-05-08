using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClinicSystem.Infrastructure.Interfaces;
using ClinicSystem.WebApplication.Interfaces;

namespace ClinicSystem.WebApplication.Services
{
    public class ExaminationValidationService : IExaminationValidationService
    {
        private readonly IClinicSystemDbContext _db;

        public ExaminationValidationService(IClinicSystemDbContext db)
        {
            _db = db;
        }

        public bool IsUnitPlanValid(long visitId, DateTime examinationDate)
        {
            var unitId = _db.PATIENT_VISIT.FirstOrDefault(e => e.ID == visitId)?.UNIT_ID;
            var unitPlan = _db.UNIT_PLAN.FirstOrDefault(e => e.UNIT_ID == unitId &&
                                                             e.DATE_FROM <= examinationDate &&
                                                             examinationDate <= e.DATE_TO);

            return unitPlan != null;
        }
    }
}