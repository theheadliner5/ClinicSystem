using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClinicSystem.Infrastructure.Interfaces;
using ClinicSystem.Infrastructure.Model;
using ClinicSystem.WebApplication.Interfaces;

namespace ClinicSystem.WebApplication.Repositories
{
    public class ExaminationRepository : IExaminationRepository
    {
        private readonly IClinicSystemDbContext _db;

        public ExaminationRepository(IClinicSystemDbContext db)
        {
            _db = db;
        }

        public IEnumerable<DISEASE> GetAllDiseases()
        {
            return _db.DISEASE.ToList();
        }

        public void SaveDisease(DISEASE disease)
        {
            _db.DISEASE.Add(disease);
            _db.SaveChanges();
        }
    }
}