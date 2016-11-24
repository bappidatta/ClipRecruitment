using ClipRecruitment.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClipRecruitment.Web.Controllers
{
    public class CommonController : ApiController
    {
        private CommonService commonService;

        public CommonController(CommonService commonService)
        {
            this.commonService = commonService;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Common/GetLocations")]
        public IHttpActionResult GetLocations(string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                return Ok(new { Success = commonService.GetLocations(inputString) });
            }
            return Ok(new { Error = "Not Available" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Common/GetPositions")]
        public IHttpActionResult GetPositions(string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                return Ok(new { Success = commonService.GetPositions(inputString) });
            }
            return Ok(new { Error = "Not Available" });
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="inputString"></param>
       /// <returns></returns>
        [HttpGet]
        [Route("api/Common/GetSkills")]
        public IHttpActionResult GetSkills(string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                return Ok(new { Success = commonService.GetSkills(inputString) });
            }
            return Ok(new { Error = "Not Available" });
        }
    }
}
