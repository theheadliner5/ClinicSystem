using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClinicSystem.Infrastructure.Interfaces;
using ClinicSystem.Infrastructure.Model;
using ClinicSystem.WebApplication.Interfaces;

namespace ClinicSystem.WebApplication.Facade
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IClinicSystemDbContext _db;

        public PersonRepository(IClinicSystemDbContext db)
        {
            _db = db;
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
    }
}