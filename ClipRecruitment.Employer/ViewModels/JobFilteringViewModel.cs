using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipRecruitment.Employer.ViewModels
{
    public class JobFilteringViewModel
    {
        public int IndustryID { get; set; }
        public string KeyWord { get; set; }
        public List<string> LocationList { get; set; }
       
        public int InsolvencyID { get; set; }
        public List<string> PositionList { get; set; }
        public bool IsFullTime { get; set; }
        public bool IsPartTime { get; set; }
        public bool IsPermanent { get; set; }
        public bool IsTemporary { get; set; }
        public bool IsRemote { get; set; }
        public bool IsLocal { get; set; }
        public List<PositionExperience> PositionExperienceList { get; set; }
        public decimal SalaryTo { get; set; }
        public decimal SalaryFrom { get; set; }
    }  

    public class PositionExperience
    {
        public string Position { get; set; }        
        public Range ExperienceRange { get; set; }
    }

    public class Range
    {
        public int From { get; set; }
        public int To { get; set; }
    }
}
