using ClipRecruitment.Common.Services;
using ClipRecruitment.Common.ViewModels;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ClipRecruitment.Web.NotificationHubs
{
    [Authorize]
    [HubName("notification")]
    public class NotificationHub : Hub
    {
        public void Send()
        {
            //Clients.All.onNewJobApplication("HELLO");
        }

        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;

            Groups.Add(Context.ConnectionId, name);

            return base.OnConnected();
        }

    }
}