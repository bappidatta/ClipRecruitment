using ClipRecruitment.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClipRecruitment.Common.Services
{
    public class CommonService
    {
        private DbContext _db;
        public CommonService(DbContext db)
        {
            _db = db;
        }

        public List<string> GetLocations(string inputString)
        {
            var query = _db.Locations.AsQueryable().AsQueryable();
            List<string> locationList = (from l in query.Where(x => x.Location.ToLower().Contains(inputString.ToLower()))
                                         select l.Location)
                                          .Distinct()
                                          .ToList();

            return locationList;
        }

        public List<string> GetPositions(string inputString)
        {
            var query = _db.Positions.AsQueryable().AsQueryable();
            List<string> positionList = (from l in query
                                         .Where(x => x.Position.ToLower().Contains(inputString.ToLower()))
                                         select l.Position)
                                          .Distinct()
                                          .ToList();

            return positionList;
        }

        public List<string> GetSkills(string inputString)
        {
            var query = _db.Skills.AsQueryable().AsQueryable();
            List<string> skillList = (from l in query
                                         .Where(x => x.Skill.ToLower().Contains(inputString.ToLower()))
                                         select l.Skill)
                                          .Distinct()
                                          .ToList();

            return skillList;
        }
    }
}