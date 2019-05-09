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
        private readonly IExaminationValidationService _validationService;
        
        public ExaminationController(IExaminationRepository examinationRepository, IExaminationValidationService validationService)
        {
            _examinationRepository = examinationRepository;
            _validationService = validationService;
        }

        [Authorize]
        public ActionResult Index()
        {
            var person = _examinationRepository.GetLoggedPersonByUserName(User.Identity.Name);
            var model = new ExaminationIndexViewModel();

            if (person != null)
            {
                var employee = _examinationRepository.GetEmployeeByPersonId(person.ID);
                if (employee != null)
                {
                    model.UnitName = employee.UNIT.UNIT_TYPE.UNIT_NAME + ", " + employee.UNIT.CLINIC.NAME + " " +
                                     employee.UNIT.CLINIC.ADDRESS;
                    model.UnitVisitDtos = _examinationRepository.GetUnitVisitDtosByUnitId(employee.UNIT_ID);
                    model.PatientDiagnoses = _examinationRepository.GetPatientDiagnosesByUnitId(employee.UNIT_ID);
                }
                else
                {
                    model.PatientVisitDtos = _examinationRepository.GetPatientVisitDtos(person.ID);
                    model.PatientDiagnoses = _examinationRepository.GetPatientDiagnosesByPersonId(person.ID);
                }
            }
            else
            {
                model.UnitVisitDtos = _examinationRepository.GetAllVisitDtos();
                model.PatientDiagnoses = _examinationRepository.GetAllPatientDiagnoses();
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

        public ActionResult VisitDetails(long visitId, ExaminationMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ExaminationMessageId.AddExaminationSuccess ? "Poprawnie dodano badanie" :
                message == ExaminationMessageId.AddExaminationFail ? "Nie udało się dodać badania - wybrany oddział przychodni nie posiada planu budżetowego dla wskazanej daty badania" :
                message == ExaminationMessageId.AddDiagnoseSuccess ? "Poprawnie dodano diagnozę" :
                message == ExaminationMessageId.AddPatientMedicineSuccess ? "Poprawnie przepisano lekarstwa" :
                    "";

            var model = new VisitDetailsViewModel
            {
                VisitDto = _examinationRepository.GetVisitDtoByVisitId(visitId),
                Diagnostics = _examinationRepository.GetDiagnosticsByPatientVisitId(visitId),
                PatientDiagnoses = _examinationRepository.GetPatientDiagnosesByPatientVisitId(visitId),
                Medicines = _examinationRepository.GetPatientMedicinesByPatientVisitId(visitId)
            };

            return View(model);
        }

        public ActionResult AddDiagnose(long visitId)
        {
            var model = new AddDiagnoseViewModel
            {
                VisitId = visitId,
                Diseases = _examinationRepository.GetAllDiseases()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddDiagnose(AddDiagnoseViewModel model)
        {
            _examinationRepository.CreateDiagnose(model.VisitId, model.DiseaseId, model.Diagnose);

            return RedirectToAction("VisitDetails",
                routeValues: new { visitId = model.VisitId, message = ExaminationMessageId.AddDiagnoseSuccess });
        }

        public ActionResult AddExamination(long visitId)
        {
            var model = new AddExaminationViewModel
            {
                VisitId = visitId
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddExamination(AddExaminationViewModel model)
        {
            var person = _examinationRepository.GetLoggedPersonByUserName(User.Identity.Name);

            var employee = person != null
                ? _examinationRepository.GetEmployeeByPersonId(person.ID)
                : null;

            if (_validationService.IsUnitPlanValid(model.VisitId, model.ExaminationDate.GetValueOrDefault()))
            {
                _examinationRepository.CreateExamination(model.ExaminationName,
                    model.ExaminationDate.GetValueOrDefault(),
                    model.Cost, model.VisitId, employee);
            }
            else
            {
                return RedirectToAction("VisitDetails",
                    routeValues: new { visitId = model.VisitId, message = ExaminationMessageId.AddExaminationFail });
            }

            return RedirectToAction("VisitDetails",
                routeValues: new { visitId = model.VisitId, message = ExaminationMessageId.AddExaminationSuccess });
        }

        public ActionResult AddPatientMedicine(long visitId)
        {
            var model = new AddPatientMedicineViewModel
            {
                VisitId = visitId,
                MedicineDtos = _examinationRepository.GetAllMedicineDtos()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddPatientMedicine(AddPatientMedicineViewModel model)
        {
            _examinationRepository.CreatePatientMedicine(model.TypeId, model.Dose,
                model.TreatmentDate.GetValueOrDefault(), model.VisitId);

            return RedirectToAction("VisitDetails",
                routeValues: new { visitId = model.VisitId, message = ExaminationMessageId.AddPatientMedicineSuccess });
        }

        public enum ExaminationMessageId
        {
            AddDiseaseSuccess,
            AddExaminationSuccess,
            AddExaminationFail,
            AddDiagnoseSuccess,
            AddPatientMedicineSuccess
        }
    }
}