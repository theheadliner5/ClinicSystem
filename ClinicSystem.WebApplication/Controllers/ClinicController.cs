using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicSystem.WebApplication.Interfaces;
using ClinicSystem.WebApplication.Models;

namespace ClinicSystem.WebApplication.Controllers
{
    public class ClinicController : Controller
    {
        private readonly IClinicRepository _clinicRepository;

        public ClinicController(IClinicRepository clinicRepository)
        {
            _clinicRepository = clinicRepository;
        }

        // GET: Clinic
        public ActionResult Index()
        {
            var model = new ClinicIndexViewModel
            {
                ClinicDtos = _clinicRepository.GetAllClinicDtos()
            };

            return View(model);
        }
    }
}