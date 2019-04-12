using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
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

            var viewModel = new EditRoleViewModel
            {
                RoleId = person?.ASPNETUSERS.ASPNETROLES.SingleOrDefault()?.ID,
                AspNetUserId = person?.ASP_NET_USER_ID,
                Roles = _manageRepository.GetAllRoles()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditRole(EditRoleViewModel editRoleViewModel)
        {
            _manageRepository.AssignNewRole(editRoleViewModel.RoleId, editRoleViewModel.AspNetUserId);

            return RedirectToAction("Index", "Manage");
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

            var employee = new EMPLOYEE
            {
                PERSON = person,
                PERSON_ID = person.ID,
                HIRE_DATE = registerDoctorViewModel.HireDate,
                SALARY = registerDoctorViewModel.Salary
            };

            _manageRepository.CreateOrUpdateEmployee(employee);

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
            return View(model);
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
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion

    }
}