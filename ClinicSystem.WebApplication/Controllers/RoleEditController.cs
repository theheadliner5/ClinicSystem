using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicSystem.Infrastructure.Interfaces;
using ClinicSystem.Infrastructure.Model;
using ClinicSystem.WebApplication.Models;

namespace ClinicSystem.WebApplication.Controllers
{
    public class RoleEditController : Controller
    {
        private readonly IClinicSystemDbContext _db;
        public RoleEditController(IClinicSystemDbContext db)
        {
            _db = db;
        }

        public ActionResult Edit(long personId)
        {
            var person = _db.person.SingleOrDefault(e => e.id == personId);

            var viewModel = new RoleEditViewModel
            {
                RoleName = person.AspNetUsers.AspNetRoles.SingleOrDefault()?.Name,
                PersonId = person.id,
                Roles = _db.AspNetRoles.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(RoleEditViewModel roleEditViewModel)
        {
            //TODO
            if (!ModelState.IsValid)
            {
                
            }



            return null;
        }
    }
}