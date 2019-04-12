using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClinicSystem.Infrastructure.Interfaces;
using ClinicSystem.Infrastructure.Model;
using ClinicSystem.WebApplication.Interfaces;

namespace ClinicSystem.WebApplication.Facade
{
    public class AspNetRolesRepository : IAspNetRolesRepository
    {
        private readonly IClinicSystemDbContext _db;

        public AspNetRolesRepository(IClinicSystemDbContext db)
        {
            _db = db;
        }

        public IEnumerable<ASPNETROLES> GetAllRoles()
        {
            return _db.ASPNETROLES.ToList();
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
    }
}