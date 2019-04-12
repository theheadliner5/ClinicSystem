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
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController(IManageRepository manageRepository)
        {
            _manageRepository = manageRepository;
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
                : message == ManageMessageId.AddUnitTypeSuccess ? "Typ oddziału dodany poprawnie"
                : message == ManageMessageId.AddUnitSuccess ? "Oddział dodany poprawnie"
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
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
                Roles = _manageRepository.GetAllRoles()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditRole(EditRoleViewModel model)
        {
            _manageRepository.AssignNewRole(model.RoleId, model.AspNetUserId);

            return RedirectToAction("Index", new { Message = ManageMessageId.EditRoleSuccess });
        }

        public ActionResult RegisterDoctor(long personId)
        {
            var person = _manageRepository.GetPersonById(personId);

            var viewModel = new RegisterDoctorViewModel
            {
                PersonId = personId,
                Name = person.NAME,
                LastName = person.LAST_NAME,
                HireDate = DateTime.Now,
                Salary = 0.0m
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult RegisterDoctor(RegisterDoctorViewModel registerDoctorViewModel)
        {
            var person = _manageRepository.GetPersonById(registerDoctorViewModel.PersonId);

            _manageRepository.CreateOrUpdateEmployee(new EMPLOYEE
            {
                PERSON = person,
                PERSON_ID = person.ID,
                HIRE_DATE = registerDoctorViewModel.HireDate,
                SALARY = registerDoctorViewModel.Salary
            });

            var roleId = _manageRepository.GetRoleIdFromName("Doctor");
            _manageRepository.AssignNewRole(roleId, person.ASP_NET_USER_ID);

            return RedirectToAction("Index", "Manage");
        }

        public ActionResult AddClinic()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddClinic(AddClinicViewModel model)
        {
            _manageRepository.CreateClinic(new CLINIC
            {
                ADDRESS = model.Address,
                NAME = model.Name
            });

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
            _manageRepository.CreateUnit(new UNIT
            {
                CLINIC_ID = model.ClinicId,
                UNIT_TYPE_ID = model.UnitTypeId,
                UNIT_ID = _manageRepository.GetUnitIdByClinicIdAndUnitTypeId(model.ClinicId, model.ParentUnitTypeId)
            }); 

            return RedirectToAction("Index", new { Message = ManageMessageId.AddUnitSuccess }); ;
        }

        public ActionResult AddUnitType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUnitType(AddUnitTypeViewModel model)
        {
            _manageRepository.CreateUnitType(new UNIT_TYPE
            {
                UNIT_NAME = model.Name
            });

            return RedirectToAction("Index", new { Message = ManageMessageId.AddUnitTypeSuccess }); ;
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
            AddClinicSuccess,
            AddUnitTypeSuccess,
            AddUnitSuccess,
            Error
        }

        #endregion

    }
}