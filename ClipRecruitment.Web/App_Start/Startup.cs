using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

[assembly: OwinStartup(typeof(ClipRecruitment.Web.App_Start.Startup))]
namespace ClipRecruitment.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            
        }
    }

    public static class Pagination
    {
        public static int Size = 10;

    }
}