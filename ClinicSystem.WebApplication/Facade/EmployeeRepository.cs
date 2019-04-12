using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClinicSystem.Infrastructure.Interfaces;
using ClinicSystem.Infrastructure.Model;
using ClinicSystem.WebApplication.Interfaces;

namespace ClinicSystem.WebApplication.Facade
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IClinicSystemDbContext _db;

        public EmployeeRepository(IClinicSystemDbContext db)
        {
            _db = db;
        }

        public void CreateOrUpdateEmployee(EMPLOYEE employee)
        {
            var existingEmployee = GetByPersonId(employee.PERSON_ID);

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

        public EMPLOYEE GetByPersonId(long personId)
        {
            return _db.EMPLOYEE.SingleOrDefault(e => e.PERSON_ID == personId);
        }
    }
}