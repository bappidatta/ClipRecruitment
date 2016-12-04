using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipRecruitment.Domain.Models
{
    [BsonIgnoreExtraElements]
    public class Employers
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string AuthID { get; set; }
        public string CompanyName { get; set; }        
        public string CompanyContact { get; set; }
        public string MobileNo { get; set; }        
        public string Location { get; set; }
        public List<string> IndustryList { get; set; }
        public string RegistrationNo { get; set; }
        public List<string> AddressList { get; set; }

    }
}
