using ClipRecruitment.Web.App_Start;
using ClipRecruitment.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Owin.Host.SystemWeb;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using AspNet.Identity.MongoDB;
using System.Web.Http.Cors;
using ClipRecruitment.Candidate.ViewModels;
using ClipRecruitment.Candidate.Services;

namespace ClipRecruitment.Web.Controllers
{
    
    public class AccountController : ApiController
    {
        private CandidateService _candidateService;
        public AccountController(CandidateService candidateService)
        {
            _candidateService = candidateService;
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(HttpContext.Current.GetOwinContext()
                                                        .Get<ApplicationIdentityContext>().Users));
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

   
        private SignInHelper _helper;
        private SignInHelper SignInHelper
        {
            get
            {
                if (_helper == null)
                {
                    _helper = new SignInHelper(UserManager, AuthenticationManager);
                }
                return _helper;
            }
        }

        //
        // POST: /Account/Login

        [System.Web.Http.OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("ExternalLogin", Name = "ExternalLogin")]
        //[ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return Ok(false);
            }

            // This doen't count login failures towards lockout only two factor authentication
            // To enable password failures to trigger lockout, change to shouldLockout: true
            var result = await SignInHelper.PasswordSignIn(model.Email, model.Password, model.RememberMe, shouldLockout: false);
           
            if (result == App_Start.SignInStatus.Success)
                return Ok(new { userName = model.Email});
            return Ok(false);
        }
        
        //POST: /Account/Register
       [System.Web.Mvc.HttpPost]
       [System.Web.Mvc.Route("api/Account/Register/")]
       [System.Web.Mvc.AllowAnonymous]
       //[ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> Register(CandidateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    model.AuthID = user.Id;
                    try
                    {
                        await _candidateService.CreateCandidateAsync(model);
                    }
                    catch(Exception ex)
                    {

                    }
                    await SignInHelper.SignInAsync(user, false, false);                    
                    return Ok(true);
                }
            }

            // If we got this far, something failed, redisplay form
            return Ok(true);
        }

        [HttpGet]
        [Route("api/Account/SignOut/")]
        public IHttpActionResult SignOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalBearer);
            return Ok(true);
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

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        //private ActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    return RedirectToAction("Index", "Home");
        //}
        
        #endregion
    }
}

