using ClipRecruitment.Employer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public IHttpActionResult GetAllJob()
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult CreateJob()
        {
            jobService.CreateJob();
            return Ok();
        }

        public IHttpActionResult SearchJob()
        {
            throw new NotImplementedException();
        }

    }
}
