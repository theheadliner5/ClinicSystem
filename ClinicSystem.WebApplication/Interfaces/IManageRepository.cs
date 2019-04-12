using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicSystem.Infrastructure.Model;

namespace ClinicSystem.WebApplication.Interfaces
{
    public interface IManageRepository
    {
        IList<PERSON> GetAllUsers();
        IEnumerable<ASPNETROLES> GetAllRoles();
        IEnumerable<CLINIC> GetAllClinics();
        IEnumerable<UNIT_TYPE> GetAllUnitTypes();
        IEnumerable<UNIT> GetAllUnits();
        void CreateOrUpdateEmployee(EMPLOYEE employee);
        EMPLOYEE GetEmployeeByPersonId(long personId);
        PERSON GetPersonById(long id);
        void AssignNewRole(string roleId, string aspNetUserId);
        string GetRoleIdFromPersonId(long personId);
        string GetRoleIdFromName(string roleName);
        void CreateClinic(CLINIC clinic);
        void CreateUnitType(UNIT_TYPE unitType);
        void CreateUnit(UNIT unit);
        long? GetUnitIdByClinicIdAndUnitTypeId(long clinicId, long? parentUnitTypeId);
    }
}
