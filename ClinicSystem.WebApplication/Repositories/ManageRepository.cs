﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public void CreateOrUpdateEmployee(EMPLOYEE employee)
        {
            var existingEmployee = GetEmployeeByPersonId(employee.PERSON_ID);

            if (existingEmployee == null)
            {
                var emplacement = _db.EMPLACEMENT.SingleOrDefault(e => e.EMPLACEMENT_NAME == "Doctor");

                if (emplacement == null)
                {
                    _db.EMPLACEMENT.Add(new EMPLACEMENT
                    {
                        EMPLACEMENT_NAME = "Doctor",
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

            if (previousRole != null)
            {
                aspNetUser.ASPNETROLES.Remove(previousRole);
            }

            aspNetUser?.ASPNETROLES.Add(newRole);

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
    }
}