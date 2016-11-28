using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipRecruitment.Employer.ViewModels
{
    public class VacancyViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string JobTitle { get; set; }
        public string Position { get; set; }
        public string Experince { get; set; }
        public string EducationLabel { get; set; }
        public Decimal SalaryEstimate { get; set; }
        public string JobDescription { get; set; }
        public bool IsPermanent { get; set; }
        public bool IsTemporary { get; set; }
        public bool IsFullTime { get; set; }
        public bool IsPartTime { get; set; }
        public List<string> LocationList { get; set; }
        public List<string> SkillList { get; set; }

    }
}
