using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using ClinicSystem.Infrastructure.Interfaces;
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
                : message == ManageMessageId.EditRoleSuccess ? "Rola edytowana pomyślnie."
                : message == ManageMessageId.AddClinicSuccess ? "Przychodnia została dodana."
                : message == ManageMessageId.AddEmployeeSuccess ? "Pracownik dodany/edytowany poprawnie"
                : message == ManageMessageId.AddUnitTypeSuccess ? "Typ oddziału dodany poprawnie"
                : message == ManageMessageId.AddUnitSuccess ? "Oddział dodany poprawnie"
                : message == ManageMessageId.AddEmplacementSuccess ? "Stanowisko dodane poprawnie"
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
                Users = _manageRepository.GetAllUsers()
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

        public ActionResult EditRole(long personId)
        {
            var person = _manageRepository.GetPersonById(personId);

            var model = new EditRoleViewModel
            {
                RoleId = person?.ASPNETUSERS.ASPNETROLES.SingleOrDefault()?.ID,
                AspNetUserId = person?.ASP_NET_USER_ID,
                Roles = _manageRepository.GetAllRoles().Where(e => e.NAME != "DOCTOR")
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditRole(EditRoleViewModel model)
        {
            _manageRepository.AssignNewRole(model.RoleId, model.AspNetUserId);

            return RedirectToAction("Index", new { Message = ManageMessageId.EditRoleSuccess });
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
                EMPLACEMENT_NAME = model.Name
            };

            _manageRepository.CreateEmplacement(emplacement);

            return RedirectToAction("Index", new { Message = ManageMessageId.AddEmplacementSuccess });
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
            EditRoleSuccess,
            AddEmployeeSuccess,
            AddEmployeeInvalidManager,
            AddClinicSuccess,
            AddUnitTypeSuccess,
            AddUnitSuccess,
            AddEmplacementSuccess,
            Error
        }

        #endregion

    }
}