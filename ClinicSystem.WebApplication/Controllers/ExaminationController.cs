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
    public class ExaminationController : Controller
    {
        private readonly IExaminationRepository _examinationRepository;

        public ExaminationController(IExaminationRepository examinationRepository)
        {
            _examinationRepository = examinationRepository;
        }

        public ActionResult Index()
        {
            return View();
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

        public ActionResult AddDisease()
        {
            return View();
        }

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

        public enum ExaminationMessageId
        {
            AddDiseaseSuccess
        }
    }
}