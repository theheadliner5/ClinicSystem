using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicSystem.WebApplication.Interfaces;
using ClinicSystem.WebApplication.Models;

namespace ClinicSystem.WebApplication.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public DoctorController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        // GET: Doctor
        public ActionResult Register(long personId)
        {
            var person = _personRepository.GetPersonById(personId);

            var viewModel = new RegisterDoctorViewModel
            {
                Name = person.NAME,
                LastName = person.LAST_NAME,
                HireDate = DateTime.Now,
                Salary = 0.0m
            };

            ModelState.Clear();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(RegisterDoctorViewModel registerDoctorViewModel)
        {
            return RedirectToAction("Index", "Manage");
        }
    }
}