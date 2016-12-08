using ClipRecruitment.Common.Services;
using ClipRecruitment.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ClipRecruitment.Web.Controllers
{
    public class NotificationController : ApiController
    {
        private NotificationService notificationService;

        public NotificationController(NotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Notification/CreateNotification/")]
        public async Task<IHttpActionResult> CreateNotification(NotificationViewModel notificationVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var notification = await notificationService.CreateNotification(notificationVM);
                    return Ok(new { Success = notification });
                }catch(Exception ex)
                {
                    return Ok(new { Error = ""+ex.Message});
                }
            }

            return Ok(new { Error = "Failed To Create Notification" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Notification/GetNotificationListByUserId/")]
        public IHttpActionResult GetNotificationListByUserId(String userId)
        {
            if (!String.IsNullOrEmpty(userId))
            {
                try {
                    var notificationList = notificationService.GetNotificationListByUserId(userId);
                    return Ok(new { Success = notificationList });
                }catch(Exception ex)
                {
                    return Ok(new { Error = ""+ ex.Message});
                }
            }

            return null;
        }
    }
}
