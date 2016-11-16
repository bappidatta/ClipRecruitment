using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipRecruitment.Employer.ViewModels
{
    public class JobViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string EmployerID { get; set; }

        public string JobTitle { get; set; }
        public Decimal SalaryFrom { get; set; }
        public Decimal SalaryTo { get; set; }
        public string IndustryType { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }

    }
}
