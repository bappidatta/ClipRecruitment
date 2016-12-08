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


        public async Task<NotificationViewModel> CreateNotification(NotificationViewModel notificationVM)
        {
            notification = new Notification
            {
                UserId = notificationVM.UserId,
                IsRead = notificationVM.IsRead,
                Title = notificationVM.Title,
                Details = notificationVM.Details,
                Date = DateTime.Now,
                Link = notificationVM.Link
            };

            await _db.Notification.InsertOneAsync(notification);

            return GetNotificationById(notification._id);
        }
        
        public NotificationViewModel ForNewJobApplication(string userId)
        {
            notification = new Notification
            {
                UserId = userId,
                Date = DateTime.UtcNow,
                Details = "A new application have been submitted for this job!",
                IsRead = false,
                Link = "Click here to view the application",
                Title = "New Application"
            };

            _db.Notification.InsertOne(notification);
            return new NotificationViewModel
            {
                UserId = notification.UserId,
                Date = notification.Date,
                Details = notification.Details,
                IsRead = notification.IsRead,
                Link = notification.Link,
                Title = notification.Title,
                _id = notification._id
            };
        }
               
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
                    UserId = notification.UserId,
                    IsRead = notification.IsRead,
                    Title = notification.Title,
                    Details = notification.Details,
                    Date = notification.Date,
                    Link = notifications.Link
                };
            }
            return null;
        }
             
        public List<NotificationViewModel> GetNotificationListByUserId(string userId)
        {
            var result = _db.Notification.AsQueryable<Notification>();
            return (from n in result
                    where n.UserId == userId && n.IsRead == false
                    select new NotificationViewModel
                    {
                        _id = n._id,
                        UserId = n.UserId,
                        IsRead = n.IsRead,
                        Title = n.Title,
                        Details = n.Details,
                        Date = n.Date
                    }).ToList();
        }
        
    }
}