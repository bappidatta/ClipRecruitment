using ClipRecruitment.Domain.Models;
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
        public string EmployerID { get; set; }
        public string Position { get; set; }
        public string EducationLevel { get; set; }
        public string Industry { get; set; }
        public int InsolvencyID { get; set; }
        public Decimal SalaryFrom { get; set; }
        public Decimal SalaryTo { get; set; } 
        public string Description { get; set; }        
        public List<string> LocationList { get; set; }
        public bool IsFullTime { get; set; }
        public bool IsPartTime { get; set; }
        public bool IsPermanent { get; set; }
        public bool IsTemporary { get; set; }
        public bool IsRemote { get; set; }
        public bool IsLocal { get; set; }
        public bool IsArchived { get; set; }
        public int YearOfExperience { get; set; }
        public List<string> SkillSet { get; set; }
        public List<Application> ApplicationList { get; set; }
        public bool IsApplied { get; set; }

    }
}
