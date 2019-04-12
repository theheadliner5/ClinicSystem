﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicSystem.Infrastructure.Model;

namespace ClinicSystem.WebApplication.Interfaces
{
    public interface IAspNetRolesRepository
    {
        IEnumerable<ASPNETROLES> GetAllRoles();
        string GetRoleIdFromPersonId(long personId);
        string GetRoleIdFromName(string roleName);
    }
}
