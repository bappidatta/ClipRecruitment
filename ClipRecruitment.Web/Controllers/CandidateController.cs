using ClipRecruitment.Candidate.Services;
using ClipRecruitment.Candidate.ViewModels;
using ClipRecruitment.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ClipRecruitment.Web.Controllers
{
    public class CandidateController : ApiController
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
    }
}