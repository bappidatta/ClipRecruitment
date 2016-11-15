using ClipRecruitment.Interview.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClipRecruitment.Web.Controllers
{
    public class UserController : ApiController
    {
        private TestService _TestService;

        public UserController(TestService TestService)
        {
            _TestService = TestService;
        }

        public IHttpActionResult GetUser(int id)
        {
            _TestService.test();
            return Ok();
        }

    }
}
