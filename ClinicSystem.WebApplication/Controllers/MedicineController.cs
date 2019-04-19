using System;
using System.Web.Mvc;
using ClinicSystem.Infrastructure.Model;
using ClinicSystem.WebApplication.Interfaces;
using ClinicSystem.WebApplication.Models;

namespace ClinicSystem.WebApplication.Controllers
{
    public class MedicineController : Controller
    {
        private readonly IMedicineRepository _medicineRepository;

        public MedicineController(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        [Authorize(Roles = "ADMINISTRATOR, MANAGER, DOCTOR")]
        // GET: Medicine
        public ActionResult Index(MedicineMessageId? message)
        {
            ViewBag.StatusMessage = 
                message == MedicineMessageId.AddMedicineSuccess ? "Poprawnie dodano lek" :
                message == MedicineMessageId.AddMedicineTypeSuccess ? "Poprawnie dodano typ leku" : 
                "";

            var model = new MedicineIndexViewModel
            {
                MedicineDtos = _medicineRepository.GetAllMedicineDtos()
            };

            return View(model);
        }

        public ActionResult AddMedicineType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMedicineType(AddMedicineTypeViewModel model)
        {
            var medicineType = new MEDICINE_TYPE
            {
                LAST_MOD_DATE = DateTime.Now,
                MEDICINE_NAME = model.Name,
                ACTIVE_INGREDIENT = model.ActiveIngredient
            };

            _medicineRepository.CreateMedicineType(medicineType);

            return RedirectToAction("Index", new { Message = MedicineMessageId.AddMedicineTypeSuccess });
        }

        public ActionResult AddMedicine()
        {
            var model = new AddMedicineViewModel
            {
                MedicineTypes = _medicineRepository.GetAllMedicineTypes()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddMedicine(AddMedicineViewModel model)
        {
            var medicine = new MEDICINE_ORDER
            {
                MEDICINE_TYPE_ID = model.MedicineTypeId,
                MEDICINE_BATCH_SERIES = model.BatchSeries,
                COST = model.Cost ?? 0,
                EXPIRE_DATE = model.ExpirationDate ?? DateTime.MaxValue
            };

            _medicineRepository.CreateMedicine(medicine);

            return RedirectToAction("Index", new { Message = MedicineMessageId.AddMedicineSuccess });
        }

        public enum MedicineMessageId
        {
            AddMedicineSuccess,
            AddMedicineTypeSuccess
        }
    }
}