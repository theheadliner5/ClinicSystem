using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ClinicSystem.Infrastructure.Model;
using ClinicSystem.WebApplication.Interfaces;
using ClinicSystem.WebApplication.Models;

namespace ClinicSystem.WebApplication.Controllers
{
    public class ExaminationController : Controller
    {
        private readonly IExaminationRepository _examinationRepository;
        
        public ExaminationController(IExaminationRepository examinationRepository)
        {
            _examinationRepository = examinationRepository;
        }

        [Authorize]
        public ActionResult Index()
        {
            var person = _examinationRepository.GetLoggedPersonByUserName(User.Identity.Name);
            var employee = _examinationRepository.GetEmployeeByPersonId(person.ID);

            var model = new ExaminationIndexViewModel();

            if (employee != null)
            {
                model.UnitName = employee.UNIT.UNIT_TYPE.UNIT_NAME + ", " + employee.UNIT.CLINIC.NAME + " " + employee.UNIT.CLINIC.ADDRESS;
                model.UnitVisitDtos = _examinationRepository.GetUnitVisitDtosByUnitId(employee.UNIT_ID);
            }
            else
            {
                model.PatientVisitDtos = _examinationRepository.GetPatientVisitDtos(person.ID);
            }

            return View(model);
        }

        public ActionResult Diseases(ExaminationMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ExaminationMessageId.AddDiseaseSuccess ? "Poprawnie dodano chorobę do słownika" :
                    "";

            var model = new DiseasesViewModel
            {
                Diseases = _examinationRepository.GetAllDiseases()
            };

            return View(model);
        }

        [Authorize(Roles = "ADMINISTRATOR, MANAGER, DOCTOR")]
        public ActionResult AddDisease()
        {
            return View();
        }

        [Authorize(Roles = "ADMINISTRATOR, MANAGER, DOCTOR")]
        [HttpPost]
        public ActionResult AddDisease(AddDiseaseViewModel model)
        {
            _examinationRepository.SaveDisease(new DISEASE
            {
                CODE = model.Code,
                CODE_DESCRIPTION = model.Description
            });

            return RedirectToAction("Diseases", new { Message = ExaminationMessageId.AddDiseaseSuccess });
        }

        public ActionResult VisitDetails(long visitId)
        {
            return View();
        }

        public enum ExaminationMessageId
        {
            AddDiseaseSuccess
        }
    }
}