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

        public void CreateOrUpdateEmployee(EMPLOYEE employee)
        {
            var existingEmployee = GetEmployeeByPersonId(employee.PERSON_ID);

            if (existingEmployee == null)
            {
                var emplacement = _db.EMPLACEMENT.SingleOrDefault(e => e.EMPLACEMENT_NAME == "DOCTOR");

                if (emplacement == null)
                {
                    _db.EMPLACEMENT.Add(new EMPLACEMENT
                    {
                        EMPLACEMENT_NAME = "DOCTOR",
                        EMPLOYEE = new List<EMPLOYEE> { employee }
                    });
                }
                else
                {
                    emplacement.EMPLOYEE.Add(employee);
                }

                _db.EMPLOYEE.Add(employee);
            }
            else
            {
                existingEmployee.HIRE_DATE = employee.HIRE_DATE;
                existingEmployee.SALARY = employee.SALARY;
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

        public void AssignNewRole(string roleId, string aspNetUserId)
        {
            var newRole = _db.ASPNETROLES.SingleOrDefault(e => e.ID == roleId);
            var aspNetUser = _db.ASPNETUSERS.SingleOrDefault(e => e.ID == aspNetUserId);
            var previousRole = aspNetUser?.ASPNETROLES.SingleOrDefault();
            var employee = _db.EMPLOYEE.FirstOrDefault(e => e.PERSON.ASP_NET_USER_ID == aspNetUserId);
            var existingEmplacement = _db.EMPLACEMENT.FirstOrDefault(e => e.EMPLACEMENT_NAME == newRole.NAME);

            if (previousRole != null)
            {
                aspNetUser.ASPNETROLES.Remove(previousRole);
            }

            aspNetUser?.ASPNETROLES.Add(newRole);

            if (existingEmplacement == null)
            {
                var emplacement = new EMPLACEMENT
                {
                    EMPLACEMENT_NAME = newRole?.NAME,
                    EMPLOYEE = employee != null ? new List<EMPLOYEE> { employee } : null
                };

                _db.EMPLACEMENT.Add(emplacement);
            }
            else
            {
                existingEmplacement.EMPLOYEE.Add(employee);
            }

            _db.SaveChanges();
        }

        public string GetRoleIdFromPersonId(long personId)
        {
            var person = _db.PERSON.SingleOrDefault(e => e.ID == personId);

            return person?.ASPNETUSERS.ASPNETROLES.SingleOrDefault()?.ID;
        }

        public string GetRoleIdFromName(string roleName)
        {
            return _db.ASPNETROLES.SingleOrDefault(e => e.NAME == roleName)?.ID;
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

        public DoctorDataDto GetDoctorDataDtoByPersonId(long personId)
        {
            var doctor = _db.EMPLOYEE.FirstOrDefault(e =>
                e.PERSON_ID == personId && e.EMPLACEMENT.EMPLACEMENT_NAME == "DOCTOR");

            if (doctor != null)
            {
                return new DoctorDataDto
                {
                    HireDate = doctor.HIRE_DATE,
                    Salary = doctor.SALARY,
                    UnitName = doctor.UNIT.UNIT_TYPE.UNIT_NAME
                };
            }

            return new DoctorDataDto { HireDate = DateTime.Now, Salary = 0.0m };
        }

        public IEnumerable<ManagerDto> GetManagerDtos()
        {
            return _db.EMPLOYEE.Where(e => e.EMPLACEMENT.EMPLACEMENT_NAME == "MANAGER").Select(e => new ManagerDto
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
    }
}