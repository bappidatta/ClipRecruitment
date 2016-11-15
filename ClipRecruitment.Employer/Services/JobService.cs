using ClipRecruitment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipRecruitment.Employer.Services
{
    public class JobService
    {
        private DbContext _db;
        public JobService(DbContext db)
        {
            _db = db;
        }

        
    }
}
