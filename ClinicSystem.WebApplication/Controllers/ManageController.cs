using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ClinicSystem.Infrastructure.Model;
using ClinicSystem.WebApplication.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ClinicSystem.WebApplication.Models;

namespace ClinicSystem.WebApplication.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly IManageRepository _manageRepository;
        private readonly IManageValidationService _validationService;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController(IManageRepository manageRepository, IManageValidationService validationService)
        {
            _manageRepository = manageRepository;
            _validationService = validationService;
        }

        public ApplicationSignInManager SignInManager => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        public ApplicationUserManager UserManager => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Hasło zostało zmienione."
                : message == ManageMessageId.AddClinicSuccess ? "Przychodnia została dodana."
                : message == ManageMessageId.AddEmployeeSuccess ? "Pracownik dodany/edytowany poprawnie"
                : message == ManageMessageId.AddUnitTypeSuccess ? "Typ oddziału dodany poprawnie"
                : message == ManageMessageId.AddUnitSuccess ? "Oddział dodany poprawnie"
                : message == ManageMessageId.AddEmplacementSuccess ? "Stanowisko dodane poprawnie"
                : message == ManageMessageId.AddUnitTypeSuccess ? "Plan budżetu dodany poprawnie"
                : message == ManageMessageId.Error ? "Wystąpił nieoczekiwany błąd."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new ManageIndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId),
                RegisteredUsers = _manageRepository.GetAllRegisteredUsers()
            };

            return View(model);
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        public ActionResult RegisterEmployee(long personId, ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.AddEmployeeInvalidManager ? "Niepoprawny przełożony" : "";

            var person = _manageRepository.GetPersonById(personId);
            var employeeDataDto = _manageRepository.GetEmployeeDataDtoByPersonId(personId);

            var viewModel = new RegisterEmployeeViewModel
            {
                PersonId = personId,
                Name = person.NAME,
                LastName = person.LAST_NAME,
                HireDate = employeeDataDto.HireDate,
                Salary = employeeDataDto.Salary,
                UnitName = employeeDataDto.UnitName,
                UnitDtos = _manageRepository.GetUnitDtos(),
                ManagerDtos = _manageRepository.GetManagerDtos(),
                Emplacements = _manageRepository.GetAssignableEmplacements(),
                Roles = _manageRepository.GetAssignableRoles()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult RegisterEmployee(RegisterEmployeeViewModel registerEmployeeViewModel)
        {
            var person = _manageRepository.GetPersonById(registerEmployeeViewModel.PersonId);

            var employee = new EMPLOYEE
            {
                LAST_MOD_DATE = DateTime.Now,
                PERSON = person,
                PERSON_ID = person.ID,
                HIRE_DATE = registerEmployeeViewModel.HireDate,
                SALARY = registerEmployeeViewModel.Salary,
                UNIT_ID = registerEmployeeViewModel.UnitId,
                EMPLOYEE_ID = registerEmployeeViewModel.ManagerId,
                EMPLACEMENT_ID = registerEmployeeViewModel.EmplacementId
            };

            if (!_validationService.IsEditedEmployeesManagerValid(employee.UNIT_ID, registerEmployeeViewModel.ManagerId))
            {
                return RedirectToAction("RegisterEmployee",
                    new
                    {
                        personId = registerEmployeeViewModel.PersonId,
                        message = ManageMessageId.AddEmployeeInvalidManager
                    });
            }

            _manageRepository.CreateOrUpdateEmployee(employee, registerEmployeeViewModel.RoleId);

            return RedirectToAction("Index", new { Message = ManageMessageId.AddEmployeeSuccess });
        }

        public ActionResult AddClinic()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddClinic(AddClinicViewModel model)
        {
            var clinic = new CLINIC
            {
                LAST_MOD_DATE = DateTime.Now,
                ADDRESS = model.Address,
                NAME = model.Name
            };

            _manageRepository.CreateClinic(clinic);

            return RedirectToAction("Index", new { Message = ManageMessageId.AddClinicSuccess });
        }

        public ActionResult AddUnit()
        {
            var model = new AddUnitViewModel
            {
                Clinics = _manageRepository.GetAllClinics(),
                UnitTypes = _manageRepository.GetAllUnitTypes(),
                ParentUnitTypes = _manageRepository.GetAllUnitTypes()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddUnit(AddUnitViewModel model)
        {
            var unit = new UNIT
            {
                LAST_MOD_DATE = DateTime.Now,
                CLINIC_ID = model.ClinicId,
                UNIT_TYPE_ID = model.UnitTypeId,
                UNIT_ID = _manageRepository.GetUnitIdByClinicIdAndUnitTypeId(model.ClinicId, model.ParentUnitTypeId)
            };

            _manageRepository.CreateUnit(unit);

            return RedirectToAction("Index", new { Message = ManageMessageId.AddUnitSuccess }); ;
        }

        public ActionResult AddUnitType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUnitType(AddUnitTypeViewModel model)
        {
            var unitType = new UNIT_TYPE
            {
                LAST_MOD_DATE = DateTime.Now,
                UNIT_NAME = model.Name
            };

            _manageRepository.CreateUnitType(unitType);

            return RedirectToAction("Index", new { Message = ManageMessageId.AddUnitTypeSuccess }); ;
        }

        public ActionResult AddEmplacement()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEmplacement(AddEmplacementViewModel model)
        {
            var emplacement = new EMPLACEMENT
            {
                LAST_MOD_DATE = DateTime.Now,
                EMPLACEMENT_NAME = model.Name
            };

            _manageRepository.CreateEmplacement(emplacement);

            return RedirectToAction("Index", new { Message = ManageMessageId.AddEmplacementSuccess });
        }
        
        public ActionResult Diseases(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.AddDiseaseSuccess ? "Poprawnie dodano/edytowano chorobę" :
                message == ManageMessageId.RemoveDiseaseSuccess ? "Poprawnie usunięto chorobę" :
                message == ManageMessageId.RemoveDiseaseFail ? "Nie udało się usunąć choroby, dane są używane w innych obiektach aplikacji" :
                    "";

            var model = new DiseasesViewModel
            {
                Diseases = _manageRepository.GetAllDiseases()
            };

            return View(model);
        }

        [Authorize(Roles = "ADMINISTRATOR, MANAGER, DOCTOR")]
        public ActionResult AddDisease(long? diseaseId)
        {
            if (diseaseId.HasValue)
            {
                var disease = _manageRepository.GetDiseaseById(diseaseId.Value);
                var model = new AddDiseaseViewModel
                {
                    DiseaseId = diseaseId,
                    Code = disease.CODE,
                    Description = disease.CODE_DESCRIPTION
                };

                return View(model);
            }

            return View();
        }

        [Authorize(Roles = "ADMINISTRATOR, MANAGER, DOCTOR")]
        [HttpPost]
        public ActionResult AddDisease(AddDiseaseViewModel model)
        {
            if (model.DiseaseId.HasValue)
            {
                _manageRepository.UpdateDisease(model.DiseaseId.Value, model.Code, model.Description);
            }
            else
            {
                _manageRepository.SaveDisease(new DISEASE
                {
                    CODE = model.Code,
                    CODE_DESCRIPTION = model.Description
                });
            }

            return RedirectToAction("Diseases", new { Message = ManageMessageId.AddDiseaseSuccess });
        }

        [Authorize(Roles = "ADMINISTRATOR, MANAGER, DOCTOR")]
        public ActionResult RemoveDisease(long diseaseId)
        {
            var removed = _manageRepository.RemoveDisease(diseaseId);

            return RedirectToAction("Diseases",
                removed
                    ? new { Message = ManageMessageId.RemoveDiseaseSuccess }
                    : new { Message = ManageMessageId.RemoveDiseaseFail });
        }

        public ActionResult UnitPlans(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.AddUnitPlanSuccess ? "Poprawnie dodano/edytowano plan budżetowy" :
                "";

            var model = new UnitPlansViewModel
            {
                UnitPlanDtos = _manageRepository.GetUnitPlanDtos()
            };

            return View(model);
        }

        public ActionResult AddUnitPlan(long? unitPlanId)
        {
            var model = new AddUnitPlanViewModel();

            if (unitPlanId.HasValue)
            {
                var existingUnitPlan = _manageRepository.GetUnitPlanById(unitPlanId.Value);

                model.UnitPlanId = existingUnitPlan.UNIT_ID;
                model.BudgetType = existingUnitPlan.BUDGET_TYPE;
                model.DateFrom = existingUnitPlan.DATE_FROM;
                model.DateTo = existingUnitPlan.DATE_TO;
                model.Value = existingUnitPlan.VALUE;
                model.UnitId = existingUnitPlan.UNIT_ID;
            }

            model.UnitDtos = _manageRepository.GetUnitDtos();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddUnitPlan(AddUnitPlanViewModel model)
        {
            if (model.UnitPlanId.HasValue)
            {
                _manageRepository.UpdateUnitPlan(model.UnitPlanId.Value, model.BudgetType, model.DateFrom.Value,
                    model.DateTo.Value, model.UnitId, model.Value);
            }
            else
            {
                var unitPlan = new UNIT_PLAN
                {
                    LAST_MOD_DATE = DateTime.Now,
                    BUDGET_TYPE = model.BudgetType,
                    DATE_FROM = model.DateFrom.GetValueOrDefault(),
                    DATE_TO = model.DateTo.GetValueOrDefault(),
                    UNIT_ID = model.UnitId,
                    VALUE = model.Value
                };

                _manageRepository.CreateUnitPlan(unitPlan);
            }

            return RedirectToAction("UnitPlans", new { Message = ManageMessageId.AddUnitPlanSuccess });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            return user?.PasswordHash != null;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            AddEmployeeSuccess,
            AddEmployeeInvalidManager,
            AddClinicSuccess,
            AddUnitTypeSuccess,
            AddUnitSuccess,
            AddEmplacementSuccess,
            AddUnitPlanSuccess,
            Error,
            AddDiseaseSuccess,
            RemoveDiseaseSuccess,
            RemoveDiseaseFail
        }

        #endregion

    }
}