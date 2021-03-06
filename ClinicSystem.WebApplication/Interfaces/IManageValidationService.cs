﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicSystem.Infrastructure.Model;

namespace ClinicSystem.WebApplication.Interfaces
{
    public interface IManageValidationService
    {
        bool IsEditedEmployeesManagerValid(long unitId, long? managerId);
        bool IsUnitPlanValid(long unitId, DateTime dateFrom, DateTime dateTo);
    }
}
