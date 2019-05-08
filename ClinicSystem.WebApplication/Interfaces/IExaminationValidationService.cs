using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.WebApplication.Interfaces
{
    public interface IExaminationValidationService
    {
        bool IsUnitPlanValid(long visitId, DateTime dateTime);
    }
}
