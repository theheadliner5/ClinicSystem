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
        DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        DbSet<AspNetRoles> AspNetRoles { get; set; }
        DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        DbSet<AspNetUsers> AspNetUsers { get; set; }
        DbSet<clinic> clinic { get; set; }
        DbSet<diagnostics> diagnostics { get; set; }
        DbSet<disease> disease { get; set; }
        DbSet<emplacement> emplacement { get; set; }
        DbSet<employee> employee { get; set; }
        DbSet<employee_cost> employee_cost { get; set; }
        DbSet<examination> examination { get; set; }
        DbSet<medicine_order> medicine_order { get; set; }
        DbSet<medicine_type> medicine_type { get; set; }
        DbSet<patient_diagnose> patient_diagnose { get; set; }
        DbSet<patient_medicines> patient_medicines { get; set; }
        DbSet<patient_visit> patient_visit { get; set; }
        DbSet<person> person { get; set; }
        DbSet<unit> unit { get; set; }
        DbSet<unit_plan> unit_plan { get; set; }
        DbSet<unit_type> unit_type { get; set; }
        int SaveChanges();
    }
}
