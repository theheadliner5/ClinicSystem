using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicSystem.WebApplication.Interfaces;
using ClinicSystem.WebApplication.Models;

namespace ClinicSystem.WebApplication.Controllers
{
    public class VisitController : Controller
    {
        private IVisitRepository _visitRepository;

        public VisitController(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }

        [Authorize]
        public ActionResult BookVisitFirstStep()
        {
            var person = _visitRepository.GetPersonByUserName(User.Identity.Name);

            var model = new BookVisitFirstStepViewModel
            {
                FullName = person.NAME + " " + person.LAST_NAME,
                Clinics = _visitRepository.GetAllClinics()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult BookVisitFirstStep(BookVisitFirstStepViewModel model)
        {
            return RedirectToAction("BookVisitSecondStep", new { clinicId = model.ClinicId });
        }

        public ActionResult BookVisitSecondStep(long clinicId)
        {
            var model = new BookVisitSecondStepViewModel
            {
                UnitDtos = _visitRepository.GetUnitDtosForClinic(clinicId)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult BookVisitSecondStep(BookVisitSecondStepViewModel model)
        {
            return View(model);
        }
    }
}