using ClipRecruitment.Employer.Services;
using ClipRecruitment.Employer.ViewModels;
using MongoDB.Bson;
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
        public IHttpActionResult GetAllJob(int pageNo)
        {
            try
            {
                int skip = pageNo * 10;
                int take = 10;
                int count = 0;
                var result = jobService.GetAllJob(skip, take, out count);
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

        

        public IHttpActionResult SearchJob()
        {
            throw new NotImplementedException();
        }













        //[HttpGet]
        //[Route("api/Job/SeedJobs/")]
        //public async Task<IHttpActionResult> SeedJobs(int id)
        //{
        //    Random rnd = new Random();
            
        //    string title = "Job Title ";        
        //    string shortDesc = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";
        //    string longDesc = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. 
        //                        Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a 
        //                        galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also
        //                        the leap into electronic typesetting, remaining essentially unchanged.";
        //    string industry = "Lorem Ipsum is simply";

        //    for(int i = 1; i < 201; i++)
        //    {

        //        await CreateJobAsync(new JobViewModel
        //        {
        //            LongDescription = longDesc,
        //            ShortDescription = shortDesc,
        //            JobTitle = title + i.ToString(),
        //            IndustryType = industry,
        //            SalaryFrom = rnd.Next(15000, 20000),
        //            SalaryTo = rnd.Next(25000, 35000)                    
        //        });
        //    }

        //    return Ok(true);

        //}

    }
}
