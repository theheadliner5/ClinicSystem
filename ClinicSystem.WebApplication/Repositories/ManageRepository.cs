using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using ClinicSystem.Infrastructure.Dtos;
using ClinicSystem.Infrastructure.Interfaces;
using ClinicSystem.Infrastructure.Model;
using ClinicSystem.WebApplication.Interfaces;

namespace ClinicSystem.WebApplication.Repositories
{
    public class ManageRepository : IManageRepository
    {
        private readonly IClinicSystemDbContext _db;

        public ManageRepository(IClinicSystemDbContext db)
        {
            _db = db;
        }

        public IEnumerable<ASPNETROLES> GetAllRoles()
        {
            return _db.ASPNETROLES.ToList();
        }

        public IEnumerable<CLINIC> GetAllClinics()
        {
            return _db.CLINIC.ToList();
        }

        public IEnumerable<UNIT_TYPE> GetAllUnitTypes()
        {
            return _db.UNIT_TYPE.ToList();
        }

        public IEnumerable<UNIT> GetAllUnits()
        {
            return _db.UNIT.ToList();
        }

        public IEnumerable<UnitDto> GetUnitDtos()
        {
            return _db.UNIT.ToList().Select(e => new UnitDto
            {
                UnitId = e.ID,
                UnitName = $"{e.UNIT_TYPE.UNIT_NAME}, {e.CLINIC.NAME}, {e.CLINIC.ADDRESS}"
            });
        }

        public void CreateOrUpdateEmployee(EMPLOYEE employee, string roleId)
        {
            var existingEmployee = GetEmployeeByPersonId(employee.PERSON_ID);
            var newRole = _db.ASPNETROLES.SingleOrDefault(e => e.ID == roleId);

            if (existingEmployee == null)
            {
                var currentRole = employee.PERSON.ASPNETUSERS.ASPNETROLES.SingleOrDefault();
                currentRole?.ASPNETUSERS.Remove(employee.PERSON.ASPNETUSERS);

                employee.PERSON.ASPNETUSERS.ASPNETROLES.Add(newRole);
                _db.EMPLOYEE.Add(employee);
            }
            else
            {
                existingEmployee.LAST_MOD_DATE = employee.LAST_MOD_DATE;
                existingEmployee.HIRE_DATE = employee.HIRE_DATE;
                existingEmployee.SALARY = employee.SALARY;
                existingEmployee.UNIT_ID = employee.UNIT_ID;
                existingEmployee.EMPLOYEE_ID = employee.EMPLOYEE_ID;
                existingEmployee.EMPLACEMENT_ID = employee.EMPLACEMENT_ID;

                var currentRole = existingEmployee.PERSON.ASPNETUSERS.ASPNETROLES.SingleOrDefault();

                existingEmployee.PERSON.ASPNETUSERS.ASPNETROLES.Remove(currentRole);
                existingEmployee.PERSON.ASPNETUSERS.ASPNETROLES.Add(newRole);
            }

            _db.SaveChanges();
        }

        public EMPLOYEE GetEmployeeByPersonId(long personId)
        {
            return _db.EMPLOYEE.SingleOrDefault(e => e.PERSON_ID == personId);
        }

        public PERSON GetPersonById(long id)
        {
            return _db.PERSON.SingleOrDefault(e => e.ID == id);
        }

        public void CreateClinic(CLINIC clinic)
        {
            _db.CLINIC.Add(clinic);
            _db.SaveChanges();
        }

        public void CreateUnitType(UNIT_TYPE unitType)
        {
            _db.UNIT_TYPE.Add(unitType);
            _db.SaveChanges();
        }

        public void CreateUnit(UNIT unit)
        {
            _db.UNIT.Add(unit);
            _db.SaveChanges();
        }

        public long? GetUnitIdByClinicIdAndUnitTypeId(long clinicId, long? parentUnitTypeId)
        {
            return _db.UNIT.FirstOrDefault(e => e.CLINIC_ID == clinicId && e.UNIT_TYPE_ID == parentUnitTypeId.Value)?.ID;
        }

        public EmployeeDataDto GetEmployeeDataDtoByPersonId(long personId)
        {
            var employee = _db.EMPLOYEE.FirstOrDefault(e => e.PERSON_ID == personId);

            if (employee != null)
            {
                return new EmployeeDataDto
                {
                    HireDate = employee.HIRE_DATE,
                    Salary = employee.SALARY,
                    UnitName = employee.UNIT.UNIT_TYPE.UNIT_NAME
                };
            }

            return new EmployeeDataDto { HireDate = DateTime.Now, Salary = 0.0m };
        }

        public IEnumerable<ManagerDto> GetManagerDtos()
        {
            return _db.EMPLOYEE.Where(e => e.EMPLACEMENT.EMPLACEMENT_NAME == "Manager").ToList().Select(e => new ManagerDto
            {
                ManagerId = e.ID,
                FullName = $"{e.PERSON.NAME} {e.PERSON.LAST_NAME}, {e.UNIT.CLINIC.NAME}, {e.UNIT.CLINIC.ADDRESS}"
            });
        }

        public void CreateEmplacement(EMPLACEMENT emplacement)
        {
            _db.EMPLACEMENT.Add(emplacement);
            _db.SaveChanges();
        }

        public IEnumerable<EMPLACEMENT> GetAssignableEmplacements()
        {
            return _db.EMPLACEMENT.ToList();
        }

        public IEnumerable<ASPNETROLES> GetAssignableRoles()
        {
            return _db.ASPNETROLES.Where(e => e.NAME != "ADMINISTRATOR").ToList();
        }

        public void CreateUnitPlan(UNIT_PLAN unitPlan)
        {
            _db.UNIT_PLAN.Add(unitPlan);
            _db.SaveChanges();
        }

        public IEnumerable<RegisteredUserDto> GetAllRegisteredUsers()
        {
            return _db.PERSON.Where(e => e.ASPNETUSERS.ASPNETROLES.All(q => q.NAME != "ADMINISTRATOR"))
                .ToList().Select(e => new RegisteredUserDto
            {
                PersonId = e.ID,
                Name = e.NAME,
                LastName = e.LAST_NAME,
                Pesel = e.PESEL,
                UserName = e.ASPNETUSERS.USERNAME,
                RoleName = e.ASPNETUSERS.ASPNETROLES.SingleOrDefault()?.NAME
            });
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

        public DISEASE GetDiseaseById(long diseaseId)
        {
            return _db.DISEASE.SingleOrDefault(e => e.ID == diseaseId);
        }

        public void UpdateDisease(long diseaseId, string code, string description)
        {
            var existingDisease = _db.DISEASE.FirstOrDefault(e => e.ID == diseaseId);

            if (existingDisease != null)
            {
                existingDisease.CODE = code;
                existingDisease.CODE_DESCRIPTION = description;
                existingDisease.LAST_MOD_DATE = DateTime.Now;
            }

            _db.SaveChanges();
        }

        public bool RemoveDisease(long diseaseId)
        {
            var disease = _db.DISEASE.FirstOrDefault(e => e.ID == diseaseId);

            if (disease != null && !disease.PATIENT_DIAGNOSE.Any())
            {
                _db.DISEASE.Remove(disease);
                _db.SaveChanges();
                return true;
            }

            return false;
        }

        public IEnumerable<UnitPlanDto> GetUnitPlanDtos()
        {
            return _db.UNIT_PLAN.ToList().Select(e => new UnitPlanDto
            {
                Id = e.ID,
                BudgetType = e.BUDGET_TYPE,
                DateFrom = e.DATE_FROM,
                DateTo = e.DATE_TO,
                UnitDetails = e.UNIT.UNIT_TYPE.UNIT_NAME + ", " + e.UNIT.CLINIC.NAME + ", " + e.UNIT.CLINIC.ADDRESS,
                Value = e.VALUE
            });
        }

        public UNIT_PLAN GetUnitPlanById(long unitPlanId)
        {
            return _db.UNIT_PLAN.FirstOrDefault(e => e.ID == unitPlanId);
        }

        public void UpdateUnitPlan(long unitPlanId, string budgetType, DateTime dateFrom, DateTime dateTo, long unitId, decimal value)
        {
            var existingUnitPlan = _db.UNIT_PLAN.FirstOrDefault(e => e.ID == unitPlanId);

            if (existingUnitPlan != null)
            {
                existingUnitPlan.LAST_MOD_DATE = DateTime.Now;
                existingUnitPlan.BUDGET_TYPE = budgetType;
                existingUnitPlan.DATE_FROM = dateFrom;
                existingUnitPlan.DATE_TO = dateTo;
                existingUnitPlan.UNIT_ID = unitId;
                existingUnitPlan.VALUE = value;
            }

            _db.SaveChanges();
        }
    }
}