using ClipRecruitment.Domain.ViewModels;
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
        [HttpPost]
        public IHttpActionResult Authenticate(User userVM)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IHttpActionResult Register(User userVM)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IHttpActionResult LogIn(User userVM)
        {
            throw new NotImplementedException();
        }



    }
}
