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
            var person = _db.PERSON.SingleOrDefault(e => e.ID == personId);

            var viewModel = new RoleEditViewModel
            {
                RoleId = person?.ASPNETUSERS.ASPNETROLES.SingleOrDefault()?.ID,
                AspNetUserId = person?.ASP_NET_USER_ID,
                Roles = _db.ASPNETROLES.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(RoleEditViewModel roleEditViewModel)
        {
            var newRole = _db.ASPNETROLES.SingleOrDefault(e => e.ID == roleEditViewModel.RoleId);
            var aspNetUser = _db.ASPNETUSERS.SingleOrDefault(e => e.ID == roleEditViewModel.AspNetUserId);
            var previousRole = aspNetUser?.ASPNETROLES.SingleOrDefault();

            if (previousRole != null)
            {
                aspNetUser.ASPNETROLES.Remove(previousRole);
            }

            aspNetUser?.ASPNETROLES.Add(newRole);

            _db.SaveChanges();

            return null;
        }
    }
}