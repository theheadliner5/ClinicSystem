using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClinicSystem.Infrastructure.Interfaces;
using ClinicSystem.Infrastructure.Model;
using ClinicSystem.WebApplication.Interfaces;

namespace ClinicSystem.WebApplication.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IClinicSystemDbContext _db;

        public AccountRepository(IClinicSystemDbContext db)
        {
            _db = db;
        }

        public void RegisterPerson(PERSON person)
        {
            _db.PERSON.Add(person);

            var patientRole = _db.ASPNETROLES.SingleOrDefault(e => e.NAME == "PATIENT");
            var aspNetUser = _db.ASPNETUSERS.SingleOrDefault(e => e.ID == person.USER_ID);

            aspNetUser?.ASPNETROLES.Add(patientRole);
            _db.SaveChanges();
        }
    }
}