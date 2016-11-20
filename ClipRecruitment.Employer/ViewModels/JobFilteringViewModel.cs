using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipRecruitment.Employer.ViewModels
{
    public class JobFilteringViewModel
    {
        public int IndustryType { get; set; }
        public string KeyWord { get; set; }
        public List<string> Location { get; set; }
       
        public int Insolvency { get; set; }
        public List<string> Position { get; set; }
        public bool IsFullTime { get; set; }
        public bool IsPartTime { get; set; }
        public bool IsPermanent { get; set; }
        public bool IsTemporary { get; set; }
        public bool IsRemote { get; set; }
        public bool IsLocal { get; set; }
        public List<string> Experience { get; set; }
        public decimal SalaryTo { get; set; }
        public decimal SalaryFrom { get; set; }
    }

    public class SalaryRange
    {
        public Decimal SalaryFrom { get; set; }
        public Decimal SalaryTo { get; set; }
    }    
}
