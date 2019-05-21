using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClinicSystem.Infrastructure.Interfaces;
using ClinicSystem.Infrastructure.Model;
using ClinicSystem.WebApplication.Interfaces;

namespace ClinicSystem.WebApplication.Services
{
    public class ManageValidationService : IManageValidationService
    {
        private readonly IClinicSystemDbContext _db;

        public ManageValidationService(IClinicSystemDbContext db)
        {
            _db = db;
        }

        public bool IsEditedEmployeesManagerValid(long unitId, long? managerId)
        {
            if (managerId == null)
            {
                return true;
            }

            var manager = _db.EMPLOYEE.SingleOrDefault(e => e.ID == managerId);

            return manager?.UNIT.ID == unitId;
        }

        public bool IsUnitPlanValid(long unitId, DateTime dateFrom, DateTime dateTo)
        {
            var conflictingUnitPlans =
                _db.UNIT_PLAN.Where(e =>
                    (e.DATE_FROM >= dateFrom && e.DATE_FROM <= dateTo
                    || e.DATE_TO <= dateTo && e.DATE_TO >= dateFrom) &&
                    e.UNIT_ID == unitId);

            return !conflictingUnitPlans.Any() && dateFrom < dateTo;
        }
    }
}