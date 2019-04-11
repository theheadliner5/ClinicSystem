using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicSystem.Infrastructure.Interfaces;
using ClinicSystem.Infrastructure.Model;
using ClinicSystem.WebApplication.Interfaces;
using ClinicSystem.WebApplication.Models;

namespace ClinicSystem.WebApplication.Controllers
{
    public class RoleEditController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly IAspNetRolesRepository _rolesRepository;

        public RoleEditController(IPersonRepository personRepository, IAspNetRolesRepository rolesRepository)
        {
            _personRepository = personRepository;
            _rolesRepository = rolesRepository;
        }

        public ActionResult Edit(long personId)
        {
            var person = _personRepository.GetPersonById(personId);

            var viewModel = new RoleEditViewModel
            {
                RoleId = person?.ASPNETUSERS.ASPNETROLES.SingleOrDefault()?.ID,
                AspNetUserId = person?.ASP_NET_USER_ID,
                Roles = _rolesRepository.GetAllRoles()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(RoleEditViewModel roleEditViewModel)
        {
            _personRepository.AssignNewRole(roleEditViewModel.RoleId, roleEditViewModel.AspNetUserId);

            return RedirectToAction("Index", "Manage");
        }
    }
}