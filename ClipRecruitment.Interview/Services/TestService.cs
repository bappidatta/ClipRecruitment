using ClipRecruitment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipRecruitment.Interview.Services
{
    public class TestService
    {
        private DbContext _db;

        public TestService(DbContext db)
        {
            _db = db;
        }
        public void test()
        {
            _db.abc();
        }
    }
}
