using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicSystem.Infrastructure.Model;
using ClinicSystem.WebApplication.Interfaces;
using ClinicSystem.WebApplication.Models;

namespace ClinicSystem.WebApplication.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAspNetRolesRepository _rolesRepository;

        public DoctorController(IPersonRepository personRepository, IEmployeeRepository employeeRepository, IAspNetRolesRepository rolesRepository)
        {
            _personRepository = personRepository;
            _employeeRepository = employeeRepository;
            _rolesRepository = rolesRepository;
        }
        
        public ActionResult Register(long personId)
        {
            var person = _personRepository.GetPersonById(personId);

            var viewModel = new RegisterDoctorViewModel
            {
                PersonId = personId,
                Name = person.NAME,
                LastName = person.LAST_NAME,
                HireDate = DateTime.Now,
                Salary = 0.0m
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(RegisterDoctorViewModel registerDoctorViewModel)
        {
            var person = _personRepository.GetPersonById(registerDoctorViewModel.PersonId);

            var employee = new EMPLOYEE
            {
                PERSON = person,
                PERSON_ID = person.ID,
                HIRE_DATE = registerDoctorViewModel.HireDate,
                SALARY = registerDoctorViewModel.Salary
            };

            _employeeRepository.CreateOrUpdateEmployee(employee);

            var roleId = _rolesRepository.GetRoleIdFromName("Doctor");
            _personRepository.AssignNewRole(roleId, person.ASP_NET_USER_ID);

            return RedirectToAction("Index", "Manage");
        }
    }
}