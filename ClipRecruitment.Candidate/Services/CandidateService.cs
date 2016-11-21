using ClipRecruitment.Domain;
using ClipRecruitment.Candidate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClipRecruitment.Candidate.ViewModels;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using ClipRecruitment.Domain.Models;

namespace ClipRecruitment.Candidate.Services
{
    public class CandidateService
    {
        private DbContext _db;
        private Candidates candidates;

        public CandidateService(DbContext db)
        {
            this._db = db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateVM"></param>
        /// <returns></returns>
        public async Task<CandidateViewModel> CreateCandidateAsync(CandidateViewModel candidateVM)
        {

            candidates = new Candidates
            {
                CandidateName = candidateVM.CandidateName,
                Profile = candidateVM.Profile,
                Position = candidateVM.Position,
                Location = candidateVM.Location,
                Experince = candidateVM.Experince,
                CurrentSalary = candidateVM.CurrentSalary,
                ExpectedSalaryFrom = candidateVM.ExpectedSalaryFrom,
                ExpectedSalaryTo = candidateVM.ExpectedSalaryTo,
                IsFullTime = candidateVM.IsFullTime,
                IsPermanent = candidateVM.IsPermanent,
                Skills = candidateVM.Skills,
                MobileNo = candidateVM.MobileNo,
                Email = candidateVM.Email,
                Sex = candidateVM.Sex
            };

            await _db.Candidates.InsertOneAsync(candidates);

            return GetCandidateByID(candidates._id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CandidateViewModel> GetAllCandidateList(int skip, int take, out int count)
        {
            count = _db.Candidates.Find(new BsonDocument()).ToList().Count();
            var result = _db.Candidates.AsQueryable<Candidates>();

            return (from c in result.Skip(skip).Take(take)
                    select new CandidateViewModel
                    {
                        _id = c._id,
                        CandidateName = c.CandidateName,
                        Profile = c.Profile,
                        Position = c.Position,
                        Location = c.Location,
                        Experince = c.Experince,
                        CurrentSalary = c.CurrentSalary,
                        ExpectedSalaryFrom = c.ExpectedSalaryFrom,
                        ExpectedSalaryTo = c.ExpectedSalaryTo,
                        IsFullTime = c.IsFullTime,
                        IsPermanent = c.IsPermanent,
                        Skills = c.Skills,
                        MobileNo = c.MobileNo,
                        Email = c.Email,
                        Sex = c.Sex
                    }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateVM"></param>
        /// <returns></returns>
        public async Task<CandidateViewModel> UpdateCandidate(CandidateViewModel candidateVM)
        {
            var id = new ObjectId(candidateVM._id);
            var filter = Builders<Candidates>.Filter.Eq("_id", id);
            var update = Builders<Candidates>.Update
                .Set("CandidateName", candidateVM.CandidateName)
                .Set("Profile", candidateVM.Profile)
                .Set("Position", candidateVM.Position)
                .Set("Location", candidateVM.Location)
                .Set("Experince", candidateVM.Experince)
                .Set("CurrentSalary", candidateVM.CurrentSalary)
                .Set("ExpectedSalaryFrom", candidateVM.ExpectedSalaryFrom)
                .Set("ExpectedSalaryTo", candidateVM.ExpectedSalaryTo)
                .Set("IsFullTime", candidateVM.IsFullTime)
                .Set("IsPermanent", candidateVM.IsPermanent)
                .Set("Skills", candidateVM.Skills)
                .Set("MobileNo", candidateVM.MobileNo)
                .Set("Email", candidateVM.Email)
                .Set("Sex", candidateVM.Sex);

            var result = _db.Candidates.UpdateOneAsync(filter, update);

            return GetCandidateByID(candidateVM._id.ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        public CandidateViewModel GetCandidateByID(string candidateId)
        {
            var id = new ObjectId(candidateId);
            var filter = Builders<Candidates>.Filter.Eq("_id", id);

            var candidates = _db.Candidates.Find(filter).SingleOrDefault();

            if (candidates != null)
            {
                return new CandidateViewModel
                {
                    _id = candidates._id,
                    CandidateName = candidates.CandidateName,
                    Profile = candidates.Profile,
                    Position = candidates.Position,
                    Location = candidates.Location,
                    Experince = candidates.Experince,
                    CurrentSalary = candidates.CurrentSalary,
                    ExpectedSalaryFrom = candidates.ExpectedSalaryFrom,
                    ExpectedSalaryTo = candidates.ExpectedSalaryTo,
                    IsFullTime = candidates.IsFullTime,
                    IsPermanent = candidates.IsPermanent,
                    Skills = candidates.Skills,
                    MobileNo = candidates.MobileNo,
                    Email = candidates.Email,
                    Sex = candidates.Sex
                };
            }
            //var candidate = BsonSerializer.Deserialize<CandidateViewModel>(result);
            return null;
        }

        public List<CandidateViewModel> SearchCandidates(CandidateFilteringViewModel candidateFilteringVM)
        {
            var query = _db.Candidates.AsQueryable().AsQueryable();
            List<IQueryable> queryList = new List<IQueryable>();

            if (!String.IsNullOrEmpty(candidateFilteringVM.Profile.Trim()))
            {
                query = (from c in query.Where(x => x.Profile.Contains(candidateFilteringVM.Profile)) select c).AsQueryable();
            }

            if (candidateFilteringVM.Position != null && candidateFilteringVM.Position.Count() > 0)
            {
                query = (from c in query.Where(x => candidateFilteringVM.Position.Contains(x.Position)) select c).AsQueryable();
            }

            query = (from c in query.Where(x => x.IsFullTime == candidateFilteringVM.IsFullTime) select c).AsQueryable();

            query = (from c in query.Where(x => x.IsPermanent == candidateFilteringVM.IsPermanent) select c).AsQueryable();

            if (candidateFilteringVM.Location != null && candidateFilteringVM.Location.Count() > 0)
            {
                query = (from c in query.Where(x => candidateFilteringVM.Location.Contains(x.Location)) select c).AsQueryable();
            }

            if (candidateFilteringVM.Skills != null && candidateFilteringVM.Skills.Count() > 0)
            {
                foreach(string skill in candidateFilteringVM.Skills)
                {
                    query = (from c in query.Where(x => x.Skills.Contains(skill)) select c).AsQueryable();
                }

            }

            var result = (from c in query
                          select new CandidateViewModel
                          {
                              _id = c._id,
                              CandidateName = c.CandidateName,
                              Profile = c.Profile,
                              Position = c.Position,
                              Location = c.Location,
                              Experince = c.Experince,
                              CurrentSalary = c.CurrentSalary,
                              ExpectedSalaryFrom = c.ExpectedSalaryFrom,
                              ExpectedSalaryTo = c.ExpectedSalaryTo,
                              IsFullTime = c.IsFullTime,
                              IsPermanent = c.IsPermanent,
                              Skills = c.Skills,
                              MobileNo = c.MobileNo,
                              Email = c.Email,
                              Sex = c.Sex
                          }).ToList();

            return result;
        }
    }
}
