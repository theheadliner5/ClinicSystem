﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicSystem.Infrastructure.Dtos;
using ClinicSystem.Infrastructure.Model;

namespace ClinicSystem.WebApplication.Interfaces
{
    public interface IManageRepository
    {
        IEnumerable<ASPNETROLES> GetAllRoles();
        IEnumerable<CLINIC> GetAllClinics();
        IEnumerable<UNIT_TYPE> GetAllUnitTypes();
        IEnumerable<UNIT> GetAllUnits();
        IEnumerable<UnitDto> GetUnitDtos();
        void CreateOrUpdateEmployee(EMPLOYEE employee, string roleId);
        EMPLOYEE GetEmployeeByPersonId(long personId);
        PERSON GetPersonById(long id);
        void CreateClinic(CLINIC clinic);
        void CreateUnitType(UNIT_TYPE unitType);
        void CreateUnit(UNIT unit);
        long? GetUnitIdByClinicIdAndUnitTypeId(long clinicId, long? parentUnitTypeId);
        EmployeeDataDto GetEmployeeDataDtoByPersonId(long personId);
        IEnumerable<ManagerDto> GetManagerDtos();
        void CreateEmplacement(EMPLACEMENT emplacement);
        IEnumerable<EMPLACEMENT> GetAssignableEmplacements();
        IEnumerable<ASPNETROLES> GetAssignableRoles();
        void CreateUnitPlan(UNIT_PLAN unitPlan);
        IEnumerable<RegisteredUserDto> GetAllRegisteredUsers();
        IEnumerable<DISEASE> GetAllDiseases();
        void SaveDisease(DISEASE disease);
        DISEASE GetDiseaseById(long diseaseId);
        void UpdateDisease(long diseaseId, string code, string description);
        bool RemoveDisease(long diseaseId);
        IEnumerable<UnitPlanDto> GetUnitPlanDtos();
        UNIT_PLAN GetUnitPlanById(long unitPlanId);
        void UpdateUnitPlan(long unitPlanId, string budgetType, DateTime dateFrom, DateTime dateTo, long unitId, decimal value);
        bool RemoveUnitPlan(long unitPlanId);
        IEnumerable<ClinicDto> GetAllClinicDtos();
        CLINIC GetClinicById(long clinicId);
        void UpdateClinic(long clinicId, string name, string address);
        bool RemoveClinic(long clinicId);
    }
}
