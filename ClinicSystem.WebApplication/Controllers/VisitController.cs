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
        private readonly IVisitValidationService _validationService;

        public VisitController(IVisitRepository visitRepository, IVisitValidationService validationService)
        {
            _visitRepository = visitRepository;
            _validationService = validationService;
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

        public ActionResult BookVisitSecondStep(long clinicId, string validationMessage)
        {
            var model = new BookVisitSecondStepViewModel
            {
                ValidationMessage = validationMessage,
                Minutes = 30,
                ClinicId = clinicId,
                UnitDtos = _visitRepository.GetUnitDtosForClinic(clinicId)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult BookVisitSecondStep(BookVisitSecondStepViewModel model)
        {
            var person = _visitRepository.GetPersonByUserName(User.Identity.Name);

            var patientVisit = new PATIENT_VISIT
            {
                LAST_MOD_DATE = DateTime.Now,
                CLINIC_ID = model.ClinicId,
                DATE_FROM = model.DateFrom ?? DateTime.Now,
                DATE_TO = model.DateFrom?.AddMinutes(model.Minutes) ?? DateTime.Now.AddMinutes(model.Minutes),
                UNIT_ID = model.UnitId,
                PERSON_ID = person.ID
            };

            var validationResult = _validationService.IsPatientVisitValid(patientVisit);

            if (string.IsNullOrEmpty(validationResult))
            {
                _visitRepository.SaveVisit(patientVisit);
            }
            else
            {
                return RedirectToAction("BookVisitSecondStep",
                    new { clinicId = model.ClinicId, validationMessage = validationResult });
            }

            var resultMessage = "";
            if (model.DateFrom != null)
            {
                resultMessage = "Poprawnie umówiono wizytę dnia " + model.DateFrom.Value.ToString("dd.MM.yyyy HH:mm") +
                                " w przychodni: " +
                                _visitRepository.GetClinicNameAndAddressById(model.ClinicId);
            }

            return RedirectToAction("Index", new { message = resultMessage });
        }
    }
}