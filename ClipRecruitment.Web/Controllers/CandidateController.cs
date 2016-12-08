using AspNet.Identity.MongoDB;
using ClipRecruitment.Candidate.Services;
using ClipRecruitment.Candidate.ViewModels;
using ClipRecruitment.Common.Services;
using ClipRecruitment.Employer.Services;
using ClipRecruitment.Employer.ViewModels;
using ClipRecruitment.Web.App_Start;
using ClipRecruitment.Web.Models;
using ClipRecruitment.Web.NotificationHubs;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ClipRecruitment.Web.Controllers
{
    [System.Web.Http.Authorize]
    public class CandidateController : ApiController
    {
        private CandidateService candidateService;
        private JobService jobService;
        private NotificationService notificationService;
        private IHubContext hubContext;
        public CandidateController(CandidateService candidateService, JobService jobService, 
            NotificationService notificationService)
        {
            this.candidateService = candidateService;
            this.jobService = jobService;
            this.notificationService = notificationService;
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(HttpContext.Current.GetOwinContext()
                                                        .Get<ApplicationIdentityContext>().Users));
            this.hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
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

        private string _userId
        {
            get
            {
                return Request.GetOwinContext().Authentication.User.Identity.Name;
            }
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> SignUp(CandidateViewModel candidateVM)
        {

            if (!ModelState.IsValid)
                return Ok(new { Error = "Invalid data submitted!" });


            var user = new ApplicationUser { UserName = candidateVM.Email, Email = candidateVM.Email, IsEmployer = false };
            try
            {
                var result = await UserManager.CreateAsync(user, candidateVM.Password);
                candidateVM.AuthID = user.Id;
                await candidateService.CreateCandidateAsync(candidateVM);                
            }
            catch (Exception)
            {
                return Ok(new { Error = "Someting went wrong while creating user profile!" });
            }
            return Ok(new { Success = "User account created successfully!" });
        }


        [HttpGet]
        [Route("api/Candidate/GetAllCandidates/")]
        public IHttpActionResult GetAllCandidates(int pageNo)
        {
            try
            {
                int count = 0;

                var result = candidateService.GetAllCandidateList(
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

        
        [HttpPost]
        [Route("api/Candidate/Createcandidate/")]
        public async Task<IHttpActionResult> CreateCandidateAsync(CandidateViewModel candidateVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var candidate = await candidateService.CreateCandidateAsync(candidateVM);
                    return Ok(new { Success = candidate });
                }
                catch (Exception ex)
                {
                    return Ok(new { Error = "" + ex.Message });
                }
            }
            return Ok(new { Error = "Could Not Saved Data!" });
        }

      
        [HttpPost]
        [Route("api/Candidate/UpdateCandidate")]
        public async Task<IHttpActionResult> UpdateCandidate(CandidateViewModel candidateVM)
        {
            if (!ModelState.IsValid)
                return Ok(new { Error = "ModelState Invalid!" });
            try
            {
                var candidate = await candidateService.UpdateCandidate(candidateVM);
                return Ok(new { Success = candidate });
            }
            catch (Exception ex)
            {
                return Ok(new { Error = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/Candidate/SearchCandidate/")]
        public IHttpActionResult SearchCandidate(CandidateFilteringViewModel candidateFilteringVM)
        {
            if (!ModelState.IsValid)
                return Ok(new { Error = "Invalid ModelState" });

            try
            {
                var candidates = candidateService.SearchCandidates(candidateFilteringVM);
                return Ok(new { Success = candidates });
            }
            catch (Exception ex)
            {
                return Ok(new { Error = ex.Message });
            }
        }


        [HttpPost]
        [Route("api/Candidate/ApplyToJobs/")]
        public IHttpActionResult ApplyToJobs(List<JobViewModel> jobList)
        {
            if (!ModelState.IsValid)
                return Ok(new { Error = "Invalid Data Submitted" });            
            if (!jobService.IsApplied(jobList, _userId))
            {
                try
                {
                    jobService.AddApplicant(jobList, _userId);
                    string empId = jobList[0].EmployerID;
                    var notification = notificationService.ForNewJobApplication(empId);
                    //hubContext.Clients.User(empId).onNewJobApplication(notification);
                    hubContext.Clients.All.onNewJobApplication(notification);
                    return Ok(new { Success = "Applied to jobs!" });
                }
                catch(Exception ex)
                {
                    return Ok(new { Error = "Cannot apply!" });
                }
            }
            return Ok(new { Error = "Already applied!" });
        }
        
    }
}