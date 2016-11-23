using ClipRecruitment.Common.Services;
using ClipRecruitment.Employer.Services;
using ClipRecruitment.Employer.ViewModels;
using ClipRecruitment.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;


namespace ClipRecruitment.Web.Controllers
{
    public class JobController : ApiController
    {
        private JobService jobService;
        private CommonService commonService;
        
        public JobController(JobService jobService, CommonService commonService)
        {
            this.jobService = jobService;
            this.commonService = commonService;
        }


        [HttpGet]
        [Route("api/Job/GetAllJob/")]        
        public IHttpActionResult GetAllJob(int pageNo)
        {
            try
            {               
                int count = 0;
                var result = jobService.GetAllJob(
                    skip: (pageNo * Pagination.Size), 
                    take: Pagination.Size, 
                    count: out count);

                return Ok(new { Success = result, Count = count });
            }
            catch(Exception ex)
            {
                return Ok(new { Error = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/Job/CreateJob/")]
        public async Task<IHttpActionResult> CreateJobAsync(JobViewModel jobVM)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var job = await jobService.CreateJobAsync(jobVM);
                    return Ok(new { Success = job});
                }
                catch(Exception ex)
                {
                    return Ok(new { Error = "" + ex.Message });
                }
            }
            return Ok(new { Error = "ModelSate Invalid!" });
        }



        [HttpPost]
        [Route("api/Job/UpdateJob")]
        public async Task<IHttpActionResult> UpdateJobAsync(JobViewModel jobVM)
        {
            if (!ModelState.IsValid)
                return Ok(new { Error = "ModelState Invalid!" });
            try
            {
                var job =  await jobService.UpdateJobAsync(jobVM);
                return Ok(new { Success = job });
            }
            catch (Exception ex)
            {
                return Ok(new { Error = ex.Message });
            }
        }


        [HttpGet]
        [Route("api/Job/GetJobByID/")]
        public IHttpActionResult GetJobByID(string jobID)
        {
            try
            {
                
                var data = jobService.GetJobByID(jobID);
                return Ok(new { Success = data});
            }
            catch(Exception ex)
            {
                return Ok(new { Error = ex.Message });
            }
        }

        
        [HttpPost]
        [Route("api/Job/SearchJob/")]
        public IHttpActionResult SearchJob(JobFilteringViewModel jobFilteringVM)
        {
            if(!ModelState.IsValid)
                return Ok(new { Error = "Invalid ModelState" });

            try
            {
                var jobs = jobService.SearchJobs(jobFilteringVM);
                return Ok(new { Success = jobs });
            }
            catch(Exception ex)
            {
                return Ok(new { Error = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/Job/GetLocations")]
        public IHttpActionResult GetLocations(string inputString)
        {
            if(!string.IsNullOrEmpty(inputString))
            {
                return Ok(new { Success = commonService.GetLocations(inputString) });
            }
            return Ok(new { Error = "Not Available" });
        }

        [HttpGet]
        [Route("api/Job/GetPositions")]
        public IHttpActionResult GetPositions(string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                return Ok(new { Success = commonService.GetPositions(inputString) });
            }
            return Ok(new { Error = "Not Available" });
        }

        [HttpGet]
        [Route("api/Job/SeedJobs/")]
        public async Task<IHttpActionResult> SeedJobs(int id)
        {
            decimal from = 15000;
            decimal to = 25000;
            string shortDesc = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";
            string longDesc = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. 
                                Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a 
                                galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also
                                the leap into electronic typesetting, remaining essentially unchanged.";

            List<string> positins = new List<string>() {
                "Software Developer", "Lawer","Software Engineer","Project Manager","Clerk",
                "Nurse", "Doctor", "Math Teacher", "Scientist" };
            List<string> locations = new List<string>() { "Amble", "Beverley", "Bewdley", "Bilston", "Bolsover", "Chatteris", "Crediton",
                "Dartmouth", "Dorking", "Dorking", "Dorking", "Dorking"
            };

            int switcher = 0;

            for (int i = 1; i < 26; i++)
            {
                // 1. Full - Permanent - Local
                // 2. Full - Permanent - Remote
                // 3. Full - Temp - Remote
                // 4. Full - Temp - Local

                await CreateJobAsync(new JobViewModel
                {
                    IndustryID = switcher,
                    InsolvencyID = switcher,
                    LongDescription = longDesc,
                    ShortDescription = shortDesc,
                    Position = positins[switcher],
                    Location = locations[switcher],
                    YearOfExperience = switcher,                    
                    IsFullTime = true, // 
                    IsPartTime = false,
                    IsPermanent = false, // 
                    IsTemporary = true,
                    IsLocal = true, //
                    IsRemote = false,
                    SalaryFrom = from + 500,
                    SalaryTo = to + 500,
                });
                from += 500;
                to += 500;
                if (switcher > 6)
                    switcher = 0;
                else
                    switcher++;
            }

            return Ok(true);

        }

    }
}
