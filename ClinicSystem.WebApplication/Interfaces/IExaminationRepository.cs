using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicSystem.Infrastructure.Dtos;
using ClinicSystem.Infrastructure.Model;

namespace ClinicSystem.WebApplication.Interfaces
{
    public interface IExaminationRepository
    {
        IEnumerable<DISEASE> GetAllDiseases();
        void SaveDisease(DISEASE disease);
        PERSON GetLoggedPersonByUserName(string userName);
        EMPLOYEE GetEmployeeByPersonId(long personId);
        IEnumerable<VisitDto> GetUnitVisitDtosByUnitId(long unitId);
        IEnumerable<VisitDto> GetPatientVisitDtos(long personId);
    }
}
