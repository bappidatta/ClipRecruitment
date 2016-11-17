using ClipRecruitment.Candidate.Services;
using ClipRecruitment.Candidate.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ClipRecruitment.Web.Controllers
{
    public class CandidateController :ApiController
    {
        private CandidateService candidateService;

        public CandidateController(CandidateService candidateService)
        {
            this.candidateService = candidateService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Candidate/GetAllCandidates/")]
        public IHttpActionResult GetAllEmployers()
        {
            try
            {
                var result = candidateService.GetAllCandidateList();
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
        /// <param name="candidateVM"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Candidate/UpdateCandidate")]
        public IHttpActionResult UpdateCandidate(CandidateViewModel candidateVM)
        {
            if (!ModelState.IsValid)
                return Ok(new { Error = "ModelState Invalid!" });
            try
            {
                var candidate = candidateService.UpdateCandidate(candidateVM);
                return Ok(new { Success = candidate });
            }
            catch (Exception ex)
            {
                return Ok(new { Error = ex.Message });
            }
        }
    }
}