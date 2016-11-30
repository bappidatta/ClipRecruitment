using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipRecruitment.Domain.Models
{
    public class Candidates
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string VideoUrl { get; set; }
        public string ImageUrl { get; set; }
        public string CandidateName { get; set; }
        public string Objectives { get; set; }
        public string Profile { get; set; }
        public string Position { get; set; }
        public string Location { get; set; }
        public string Experince { get; set; }
        public decimal CurrentSalary { get; set; }
        public decimal ExpectedSalaryFrom { get; set; }
        public decimal ExpectedSalaryTo { get; set; }
        public bool IsFullTime { get; set; }
        public bool IsPartTime { get; set; }
        public bool IsPermanent { get; set; }
        public bool IsTemporary { get; set; }
        public bool IsRemote { get; set; }
        public bool IsLocum { get; set; }
        public List<string> Skills { get; set; }

        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string AuthID { get; set; }
        public List<string> IndustryList { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

    }
}
