using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipRecruitment.Employer.ViewModels
{
    class JobViewModel
    {
        public ObjectId JobID { get; set; }
        public ObjectId EmployerID { get; set; }
        public string JobTitle { get; set; }
        public Decimal SalaryFrom { get; set; }
        public Decimal SalaryTo { get; set; }
        public string IndustryType { get; set; }
        public string Description { get; set; }
        
    }
}
