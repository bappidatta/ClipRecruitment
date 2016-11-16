using ClipRecruitment.Employer.Services;
using ClipRecruitment.Employer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ClipRecruitment.Web.Controllers
{
    public class EmployerController : ApiController
    {
        private EmployerService employerService;

        public EmployerController(EmployerService employerService)
        {
            this.employerService = employerService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Employer/GetAllEmployers/")]
        public IHttpActionResult GetAllEmployers()
        {
            try
            {
                var result = employerService.GetAllEmployerList();
                return Ok(new { Success = result });
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

        [HttpPost]
        [Route("api/Employer/UpdateEmployer")]
        public IHttpActionResult UpdateEmployer(EmployerViewModel employerVM)
        {
            if (!ModelState.IsValid)
                return Ok(new { Error = "ModelState Invalid!" });
            try
            {
                var employer = employerService.UpdateEmployer(employerVM);
                return Ok(new { Success = employer });
            }
            catch (Exception ex)
            {
                return Ok(new { Error = ex.Message });
            }
        }
    }
}