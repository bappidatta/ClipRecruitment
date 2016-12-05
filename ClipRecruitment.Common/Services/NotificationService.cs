using ClipRecruitment.Common.ViewModels;
using ClipRecruitment.Domain;
using ClipRecruitment.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ClipRecruitment.Common.Services
{
    public class NotificationService
    {
        private Notification notification;
        private DbContext _db;

        public NotificationService(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationVM"></param>
        /// <returns></returns>
        public async Task<NotificationViewModel> createNotification(NotificationViewModel notificationVM)
        {
            notification = new Notification
            {
                userId = notificationVM.userId,
                status = notificationVM.status,
                title = notificationVM.title,
                details = notificationVM.details,
                setDate = DateTime.Now
            };

            await _db.Notification.InsertOneAsync(notification);

            return GetNotificationById(notification._id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationID"></param>
        /// <returns></returns>
        public NotificationViewModel GetNotificationById(string notificationID)
        {
            var id = new ObjectId(notificationID);
            var filter = Builders<Notification>.Filter.Eq("_id", id);
            var notifications = _db.Notification.Find(filter).SingleOrDefault();

            if (notifications != null)
            {
                return new NotificationViewModel
                {
                    _id = notifications._id,
                    userId = notification.userId,
                    status = notification.status,
                    title = notification.title,
                    details = notification.details,
                    setDate = notification.setDate
                };
            }

            return null;
        }

        /// <summary>
        /// Get List of notification By user ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<NotificationViewModel> GetNotificationListByUserId(string userId)
        {
            var result = _db.Notification.AsQueryable<Notification>();
            return (from n in result
                    where n.userId == userId
                    select new NotificationViewModel
                    {
                        _id = n._id,
                        userId = n.userId,
                        status = n.status,
                        title = n.title,
                        details = n.details,
                        setDate = n.setDate
                    }).ToList();
        }
    }
}