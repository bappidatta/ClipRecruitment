using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClipRecruitment.Common.ViewModels
{
    public class NotificationViewModel
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string userId { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public string details { get; set; }
        public DateTime setDate { get; set; }
    }
}