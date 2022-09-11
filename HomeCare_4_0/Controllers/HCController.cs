using HomeCare_4_0.Common;
using HomeCare_4_0.DAL;
using HomeCare_4_0.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace HomeCare_4_0.Controllers
{
    public class HCController : Controller
    {
        // GET: HC
        private HCRepository HCRepository;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public HCController()
        {
            this.HCRepository = new HCRepository(new HomeCareDBEntities());
        }

        public HCController(HCRepository HCRepository, ApplicationSignInManager signInManager, ApplicationUserManager userManager)
        {
            this.HCRepository = HCRepository;
            UserManager = userManager;
            SignInManager = signInManager;

        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public void SignIn()
        {
            if (!Request.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = System.Configuration.ConfigurationManager.AppSettings["redirectPath"] },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
        }
        /// <summary>
        /// Send an OpenID Connect sign-out request.
        /// </summary>
        public ActionResult SignOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(
                    OpenIdConnectAuthenticationDefaults.AuthenticationType,
                    CookieAuthenticationDefaults.AuthenticationType);
           
            UserDetail.clearSession();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            UserDetail.clearAllCookie();
            return RedirectToAction("Login", "HC");
        }

        // GET: COI
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            var Name = userClaims?.FindFirst("name")?.Value;
            var Email = userClaims?.FindFirst("preferred_username")?.Value;     

            var AdAuthen = HttpContext.GetOwinContext().Authentication.AuthenticateAsync("Cookies").GetAwaiter().GetResult();
            var isauth = Request.IsAuthenticated;
            if (isauth)
            {
                cAuthorization objAuten = new cAuthorization();
                bool result = objAuten.checkLogin(string.Empty, string.Empty, Email, string.Empty);
                if (result)
                {
                    return RedirectToLocal(returnUrl);
                }
                else {              
                    return RedirectToAction("LogoutAD", "HC");
                }
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult LogoutAD()
        {
            return View();
        }
        private class ApiLogin
        {
            public ApiLoginResponse response { get; set; }
            public int ResponseCode { get; set; }
            public string ResponseMsg { get; set; }
            public string RequestTime { get; set; }
            public string ResponseTime { get; set; }
        }

        private class ApiLoginResponse
        {
            public int UserID { get; set; }
            public string UserCode { get; set; }
            public string TitleNameTH { get; set; }
            public string FirstNameTH { get; set; }
            public string LastNameTH { get; set; }
            public string TitleNameEN { get; set; }
            public string FirstNameEN { get; set; }
            public string LastNameEN { get; set; }
            public string Profile { get; set; }
            public bool AllFlagProject { get; set; }
            public string Email { get; set; }
            public string HCRole { get; set; }
            public int DeptID { get; set; }
            public string DeptCode { get; set; }
            public string TokenCode { get; set; }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            /* Old Login */
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                string message = (string.IsNullOrEmpty(model.UserName) && string.IsNullOrEmpty(model.Password)) ? "Username and Password."
                                : (string.IsNullOrEmpty(model.UserName)) ? "Username."
                                : (string.IsNullOrEmpty(model.Password)) ? "Password." : "";

                if (!string.IsNullOrEmpty(message))
                {
                    message = "Please enter " + message;
                }

                ModelState.AddModelError("", message);
                return View(model);
            }

            cAuthorization objAuten = new cAuthorization();
            bool result = objAuten.checkLogin(model.UserName, model.Password, string.Empty, string.Empty);
            if (result)
            {
                if (UserDetail.Role != "VENDOR" && model.userType == "2")
                {
                    UserDetail.clearSession();
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
                }
                else
                {
                    return RedirectToLocal(returnUrl);
                }
                
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult IcontactAuthenticate(string username, string password, int project_id, int unit_id, string icontact_username, string icontact_userid, string emailAgent)
        {
            LoginViewModel model = new LoginViewModel();
            model.UserName = username;
            model.Password = password;
            model.Email = emailAgent;

            cAuthorization objAuten = new cAuthorization();
            bool result = objAuten.checkLogin(model.UserName, model.Password, model.Email, icontact_userid);

            using (var db = new HomeCareDBEntities())
            {
                var reference_unit = db.TK_SP_GET_REFERENCE_UNIT_OF_A_UNIT(unit_id).FirstOrDefault();
                if (reference_unit.ref_project_id != null && reference_unit.ref_id != null)
                {
                    int.TryParse(reference_unit.ref_project_id.ToString(), out project_id);
                    int.TryParse(reference_unit.ref_id.ToString(), out unit_id);
                }
            }

            if (result)
            {
                return RedirectToLocal("~/Home/SearchInformation/" + project_id + "/" + unit_id);
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return RedirectToAction("Login", "HC");
            }
        }

        public ActionResult PMRAuthenticate(string Project_id, int Unit_id)
        {


            LoginViewModel model = new LoginViewModel();
            model.UserName = "siriPMR";
            model.Password = "password";



            cAuthorization objAuten = new cAuthorization();
            bool result = objAuten.checkLogin(model.UserName, model.Password, string.Empty, string.Empty);
            if (result)
            {
                cb_MasterData objMasterData = new cb_MasterData();
                HC_SP_Get_ProjectDetail_Result ProjectDetail = objMasterData.getProjectDetailByProjectCode(Project_id);

                return RedirectToLocal("~/Home/SearchInformation/" + ProjectDetail.ProjectID + "/" + Unit_id);
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return RedirectToAction("Login", "HC");
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Default()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            UserDetail.clearSession();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "HC");
        }
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
                //return RedirectToAction("ChangePassword", new { Message = ManageMessageId.ChangePasswordSuccess });
                ModelState.AddModelError("", "Change Password Success");
            }

            AddErrors(result);
            return View(model);
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
        public ActionResult Contact(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }



    }
}