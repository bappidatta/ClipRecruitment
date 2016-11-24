using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipRecruitment.Candidate.ViewModels
{
    public class CandidateViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Url { get; set; }
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
        public bool IsPermanent { get; set; }
        public List<string> Skills { get; set; }

        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
    }
}
