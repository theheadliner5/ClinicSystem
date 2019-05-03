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
    public class VisitController : Controller
    {
        private readonly IVisitRepository _visitRepository;

        public VisitController(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }

        [Authorize]
        public ActionResult Index(string message)
        {
            var model = new VisitIndexViewModel
            {
                Message = message
            };

            return View(model);
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
                ClinicId = clinicId,
                UnitDtos = _visitRepository.GetUnitDtosForClinic(clinicId)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult BookVisitSecondStep(BookVisitSecondStepViewModel model)
        {
            var person = _visitRepository.GetPersonByUserName(User.Identity.Name);

            _visitRepository.SaveVisit(new PATIENT_VISIT
            {
                LAST_MOD_DATE = DateTime.Now,
                CLINIC_ID = model.ClinicId,
                DATE_FROM = model.DateFrom ?? DateTime.Now,
                DATE_TO = model.DateFrom?.AddMinutes(model.Minutes) ?? DateTime.Now.AddMinutes(model.Minutes),
                UNIT_ID = model.UnitId,
                PERSON_ID = person.ID
            });

            var resultMessage = "";
            if (model.DateFrom != null)
            {
                resultMessage = "Poprawnie umówiono wizytę dnia " + model.DateFrom.Value.ToShortDateString() + " " +
                                model.DateFrom.Value.ToShortTimeString() + " w przychodni: " +
                                _visitRepository.GetClinicNameAndAddressById(model.ClinicId);
            }

            return RedirectToAction("Index", new { message = resultMessage });
        }
    }
}