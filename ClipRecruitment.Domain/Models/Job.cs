using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipRecruitment.Domain.Models
{
    [BsonIgnoreExtraElements]
    public class Job
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string EmployerID { get; set; }
        public string Position { get; set; }
        public string Industry { get; set; }
        public Decimal SalaryFrom { get; set; }
        public Decimal SalaryTo { get; set; }
        public string Description { get; set; }
        public List<string> LocationList { get; set; }
        public int InsolvencyID { get; set; }
        public bool IsFullTime { get; set; }
        public bool IsPartTime { get; set; }
        public bool IsPermanent { get; set; }
        public bool IsTemporary { get; set; }
        public bool IsRemote { get; set; }
        public bool IsLocal { get; set; }
        public int YearOfExperience { get; set; }
        public List<string> SkillSet { get; set; }
        public string EducationLevel { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsArchived { get; set; }
    }
}
