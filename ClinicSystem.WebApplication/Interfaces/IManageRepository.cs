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
        void CreateOrUpdateEmployee(EMPLOYEE employee);
        EMPLOYEE GetByPersonId(long personId);
        PERSON GetPersonById(long id);
        void AssignNewRole(string roleId, string aspNetUserId);
        IEnumerable<ASPNETROLES> GetAllRoles();
        string GetRoleIdFromPersonId(long personId);
        string GetRoleIdFromName(string roleName);
    }
}
