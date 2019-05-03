using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicSystem.Infrastructure.Dtos;
using ClinicSystem.Infrastructure.Model;

namespace ClinicSystem.WebApplication.Interfaces
{
    public interface IVisitRepository
    {
        PERSON GetPersonByUserName(string userName);
        IEnumerable<CLINIC> GetAllClinics();
        IEnumerable<UnitDto> GetUnitDtosForClinic(long clinicId);
        void SaveVisit(PATIENT_VISIT visit);
        string GetClinicNameAndAddressById(long clinicId);
    }
}
