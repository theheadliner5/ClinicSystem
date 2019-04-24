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
    public class ClinicRepository : IClinicRepository
    {
        private readonly IClinicSystemDbContext _db;

        public ClinicRepository(IClinicSystemDbContext db)
        {
            _db = db;
        }

        public IEnumerable<ClinicDto> GetAllClinicDtos()
        {
            return _db.CLINIC.ToList().Select(e => new ClinicDto
            {
                Name = e.NAME,
                Address = e.ADDRESS,
                Units = string.Join(", ", GetAllUnitsNamesForClinic(e.ID))
            });
        }

        private string[] GetAllUnitsNamesForClinic(long clinicId)
        {
            return _db.UNIT.Where(e => e.CLINIC_ID == clinicId).ToList().Select(e => e.UNIT_TYPE.UNIT_NAME).ToArray();
        }
    }
}