using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicSystem.Infrastructure.Model;

namespace ClinicSystem.Infrastructure.Interfaces
{
    public interface IClinicSystemDbContext
    {
        DbSet<ASPNETROLES> ASPNETROLES { get; set; }
        DbSet<ASPNETUSERCLAIMS> ASPNETUSERCLAIMS { get; set; }
        DbSet<ASPNETUSERLOGINS> ASPNETUSERLOGINS { get; set; }
        DbSet<ASPNETUSERS> ASPNETUSERS { get; set; }
        DbSet<CLINIC> CLINIC { get; set; }
        DbSet<DIAGNOSTICS> DIAGNOSTICS { get; set; }
        DbSet<DISEASE> DISEASE { get; set; }
        DbSet<EMPLACEMENT> EMPLACEMENT { get; set; }
        DbSet<EMPLOYEE> EMPLOYEE { get; set; }
        DbSet<EMPLOYEE_COST> EMPLOYEE_COST { get; set; }
        DbSet<EXAMINATION> EXAMINATION { get; set; }
        DbSet<MEDICINE_ORDER> MEDICINE_ORDER { get; set; }
        DbSet<MEDICINE_TYPE> MEDICINE_TYPE { get; set; }
        DbSet<PATIENT_DIAGNOSE> PATIENT_DIAGNOSE { get; set; }
        DbSet<PATIENT_MEDICINES> PATIENT_MEDICINES { get; set; }
        DbSet<PATIENT_VISIT> PATIENT_VISIT { get; set; }
        DbSet<PERSON> PERSON { get; set; }
        DbSet<UNIT> UNIT { get; set; }
        DbSet<UNIT_PLAN> UNIT_PLAN { get; set; }
        DbSet<UNIT_TYPE> UNIT_TYPE { get; set; }
        int SaveChanges();
    }
}
