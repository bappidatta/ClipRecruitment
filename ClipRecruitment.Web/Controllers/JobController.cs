using ClipRecruitment.Employer.Services;
using ClipRecruitment.Employer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ClipRecruitment.Web.Controllers
{
    public class JobController : ApiController
    {
        private JobService jobService;
        
        public JobController(JobService jobService)
        {
            this.jobService = jobService;
        }


        [HttpGet]
        [Route("api/Job/GetAllJob/")]
        public IHttpActionResult GetAllJob()
        {
            try
            {
                var result = jobService.GetAllJob();
                return Ok(new { Success = result });
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
        public IHttpActionResult UpdateJob(JobViewModel jobVM)
        {
            if (!ModelState.IsValid)
                return Ok(new { Error = "ModelState Invalid!" });
            try
            {
                var job = jobService.UpdateJob(jobVM);
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

        

        public IHttpActionResult SearchJob()
        {
            throw new NotImplementedException();
        }

    }
}
