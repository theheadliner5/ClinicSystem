using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicSystem.WebApplication.Controllers
{
    public class MedicineController : Controller
    {
        [Authorize(Roles = "Administrator, Manager, Doctor")]
        // GET: Medicine
        public ActionResult Index(MedicineMessageId? message)
        {
            ViewBag.StatusMessage = 
                message == MedicineMessageId.AddMedicineSuccess ? "Poprawnie dodano lekarstwo" :
                message == MedicineMessageId.AddMedicineTypeSuccess ? "Poprawnie dodano typ lekarstwa" : 
                "";
                
            return View();
        }

        public enum MedicineMessageId
        {
            AddMedicineSuccess,
            AddMedicineTypeSuccess
        }
    }
}