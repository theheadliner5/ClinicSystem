using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClinicSystem.Infrastructure.Dtos;
using ClinicSystem.Infrastructure.Interfaces;
using ClinicSystem.WebApplication.Interfaces;

namespace ClinicSystem.WebApplication.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IClinicSystemDbContext _db;

        public DoctorRepository(IClinicSystemDbContext db)
        {
            _db = db;
        }

        public IEnumerable<DoctorDto> GetAllDoctorDtos()
        {
            return _db.EMPLOYEE.Where(e => e.PERSON.ASPNETUSERS.ASPNETROLES.Any(r => r.NAME == "DOCTOR")).ToList()
                .Select(e => new DoctorDto
                {
                    Name = e.PERSON.NAME,
                    LastName = e.PERSON.LAST_NAME,
                    EmplacementName = e.EMPLACEMENT.EMPLACEMENT_NAME,
                    ClinicName = e.UNIT.CLINIC.NAME,
                    ClinicAddress = e.UNIT.CLINIC.ADDRESS,
                    UnitName = e.UNIT.UNIT_TYPE.UNIT_NAME
                }).OrderBy(e => e.ClinicAddress).ThenBy(e => e.Name).ThenBy(e => e.LastName);
        }
    }
}