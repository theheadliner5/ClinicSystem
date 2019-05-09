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
        IEnumerable<VisitDto> GetAllVisitDtos();
        IEnumerable<PATIENT_DIAGNOSE> GetPatientDiagnosesByUnitId(long unitId);
        IEnumerable<PATIENT_DIAGNOSE> GetPatientDiagnosesByPersonId(long personId);
        IEnumerable<PATIENT_DIAGNOSE> GetAllPatientDiagnoses();
        IEnumerable<DIAGNOSTICS> GetDiagnosticsByPatientVisitId(long visitId);
        IEnumerable<PATIENT_DIAGNOSE> GetPatientDiagnosesByPatientVisitId(long visitId);
        VisitDto GetVisitDtoByVisitId(long visitId);
        void CreateExamination(string examinationName, DateTime examinationDate, decimal cost, long visitId,
            EMPLOYEE employee);
        EMPLOYEE GetAdministratorAccountEmployee(string userName);
        IEnumerable<PATIENT_MEDICINES> GetPatientMedicinesByPatientVisitId(long visitId);
        IEnumerable<MedicineDto> GetAllMedicineDtos();
        void CreatePatientMedicine(long typeId, string dose, DateTime treatmentDate, long visitId);
        void CreateDiagnose(long visitId, long diseaseId, string diagnose);
    }
}
