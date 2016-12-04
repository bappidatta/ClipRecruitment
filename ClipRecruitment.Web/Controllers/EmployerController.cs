using AspNet.Identity.MongoDB;
using ClipRecruitment.Employer.Services;
using ClipRecruitment.Employer.ViewModels;
using ClipRecruitment.Web.App_Start;
using ClipRecruitment.Web.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ClipRecruitment.Web.Controllers
{
    [Authorize]
    public class EmployerController : ApiController
    {
        private EmployerService employerService;

        public EmployerController(EmployerService employerService)
        {
            this.employerService = employerService;

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

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> SignUp(EmployerViewModel employerVM)
        {

            if (!ModelState.IsValid)
                return Ok(new { Error = "Invalid data submitted!" });


            var user = new ApplicationUser { UserName = employerVM.Email, Email = employerVM.Email, IsEmployer = true };
            try
            {
                var result = await UserManager.CreateAsync(user, employerVM.Password);
                employerVM.AuthID = user.Id;
                await employerService.CreateEmployerAsync(employerVM);
            }
            catch (Exception)
            {
                return Ok(new { Error = "Someting went wrong while creating user profile!" });
            }
            return Ok(new { Success = "User account created successfully!" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Employer/GetAllEmployers/")]
        public IHttpActionResult GetAllEmployers(int pageNo)
        {
            try
            {
                int count = 0;
                var result = employerService.GetAllEmployerList(
                    skip: (pageNo * Pagination.Size),
                    take: Pagination.Size,
                    count: out count);
                return Ok(new { Success = result, Count = count });
            }
            catch (Exception ex)
            {
                return Ok(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employerVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Employer/CreateEmployer/")]
        public async Task<IHttpActionResult> CreateEmployerAsync(EmployerViewModel employerVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employer = await employerService.CreateEmployerAsync(employerVM);
                    return Ok(new { Success = employer });
                }
                catch (Exception ex)
                {
                    return Ok(new { Error = "" + ex.Message });
                }
            }
            return Ok(new { Error = "Could Not Saved Data!" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employerVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Employer/UpdateEmployer")]
        public async Task<IHttpActionResult> UpdateEmployer(EmployerViewModel employerVM)
        {
            if (!ModelState.IsValid)
                return Ok(new { Error = "ModelState Invalid!" });
            try
            {
                var employer = await employerService.UpdateEmployer(employerVM);
                return Ok(new { Success = employer });
            }
            catch (Exception ex)
            {
                return Ok(new { Error = ex.Message });
            }
        }
    }
}