using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TheProject.ReportWebApplication.Models;
using TheProject.ReportWebApplication.Services;

namespace TheProject.ReportWebApplication.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        #region Properties
        private UserService userService;
        #endregion


        #region Constructor
        public AccountController()
        {
            this.userService = new UserService();
        }

        #endregion

        #region Methods
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

           User result = userService.Login(model.Email, model.Password);
            //var resulst = userService.GetBuildingByFacilityId();
            if (result != null)
            {
                return RedirectToLocal(returnUrl);
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Facility");
        }
        #endregion

    }
}