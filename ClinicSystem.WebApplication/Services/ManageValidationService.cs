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

        public bool IsInsertedUnitValid(UNIT unit)
        {
            return true;
        }
    }
}