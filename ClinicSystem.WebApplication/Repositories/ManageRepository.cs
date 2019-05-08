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
    public class ManageRepository : IManageRepository
    {
        private readonly IClinicSystemDbContext _db;

        public ManageRepository(IClinicSystemDbContext db)
        {
            _db = db;
        }

        public IList<PERSON> GetAllUsers()
        {
            return _db.PERSON.ToList();
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
    }
}