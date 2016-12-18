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
                
        public async Task<CandidateViewModel> CreateCandidateAsync(CandidateViewModel candidateVM)
        {

            candidates = new Candidates
            {
                CandidateName = candidateVM.CandidateName,
                VideoUrl = candidateVM.VideoUrl,
                ImageUrl = candidateVM.ImageUrl,
                Objectives = candidateVM.Objectives,
                Profile = candidateVM.Profile,
                Position = candidateVM.Position,
                Location = candidateVM.Location,
                Experince = candidateVM.Experince,
                CurrentSalary = candidateVM.CurrentSalary,
                ExpectedSalaryFrom = candidateVM.ExpectedSalaryFrom,
                ExpectedSalaryTo = candidateVM.ExpectedSalaryTo,
                IsFullTime = candidateVM.IsFullTime,
                IsPermanent = candidateVM.IsPermanent,
                IsPartTime = candidateVM.IsPartTime,
                IsTemporary = candidateVM.IsTemporary,
                IsRemote = candidateVM.IsRemote,
                IsLocum = candidateVM.IsLocum,
                Skills = candidateVM.Skills,
                MobileNo = candidateVM.MobileNo,
                Email = candidateVM.Email,
                Sex = candidateVM.Sex,
                
                AuthID = candidateVM.AuthID,
                FirstName = candidateVM.FirstName,
                IndustryList = candidateVM.IndustryList,
                Surname = candidateVM.Surname,    
                DocumentList = candidateVM.DocumentList           
            };

            await _db.Candidates.InsertOneAsync(candidates);

            return GetCandidateByID(candidates._id);
        }

        public List<CandidateViewModel> GetAllCandidateList(int skip, int take, out int count)
        {
            count = _db.Candidates.Find(new BsonDocument()).ToList().Count();
            var result = _db.Candidates.AsQueryable<Candidates>();

            return (from c in result.Skip(skip).Take(take)
                    select new CandidateViewModel
                    {
                        _id = c._id,
                        CandidateName = c.CandidateName,
                        VideoUrl = c.DocumentList[1].Guid,
                        ImageUrl = c.ImageUrl,
                        Objectives = c.Objectives,
                        Profile = c.Profile,
                        Position = c.Position,
                        Location = c.Location,
                        Experince = c.Experince,
                        CurrentSalary = c.CurrentSalary,
                        ExpectedSalaryFrom = c.ExpectedSalaryFrom,
                        ExpectedSalaryTo = c.ExpectedSalaryTo,
                        IsFullTime = c.IsFullTime,
                        IsPermanent = c.IsPermanent,
                        IsPartTime = c.IsPartTime,
                        IsLocum = c.IsLocum,
                        IsRemote = c.IsRemote,
                        IsTemporary = c.IsTemporary,
                        Skills = c.Skills,
                        MobileNo = c.MobileNo,
                        Email = c.Email,
                        Sex = c.Sex, 
                        DocumentList = c.DocumentList
                    }).ToList();
        }

        public async Task<CandidateViewModel> UpdateCandidate(CandidateViewModel candidateVM)
        {
            var id = new ObjectId(candidateVM._id);
            var filter = Builders<Candidates>.Filter.Eq("_id", id);
            var update = Builders<Candidates>.Update
                .Set("CandidateName", candidateVM.CandidateName)
                .Set("VideoUrl", candidateVM.VideoUrl)
                .Set("ImageUrl", candidateVM.ImageUrl)
                .Set("Objectives", candidateVM.Objectives)
                .Set("Profile", candidateVM.Profile)
                .Set("Position", candidateVM.Position)
                .Set("Location", candidateVM.Location)
                .Set("Experince", candidateVM.Experince)
                .Set("CurrentSalary", candidateVM.CurrentSalary)
                .Set("ExpectedSalaryFrom", candidateVM.ExpectedSalaryFrom)
                .Set("ExpectedSalaryTo", candidateVM.ExpectedSalaryTo)
                .Set("IsFullTime", candidateVM.IsFullTime)
                .Set("IsPermanent", candidateVM.IsPermanent)
                .Set("IsPartTime", candidateVM.IsPartTime)
                .Set("IsRemote", candidateVM.IsRemote)
                .Set("IsTemporary", candidateVM.IsTemporary)
                .Set("IsLocum", candidateVM.IsLocum)
                .Set("Skills", candidateVM.Skills)
                .Set("MobileNo", candidateVM.MobileNo)
                .Set("Email", candidateVM.Email)
                .Set("Sex", candidateVM.Sex);

            var result = await _db.Candidates.UpdateOneAsync(filter, update);

            return GetCandidateByID(candidateVM._id.ToString());
        }
        
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
                    VideoUrl = candidates.VideoUrl,
                    ImageUrl = candidates.ImageUrl,
                    Objectives = candidates.Objectives,
                    Profile = candidates.Profile,
                    Position = candidates.Position,
                    Location = candidates.Location,
                    Experince = candidates.Experince,
                    CurrentSalary = candidates.CurrentSalary,
                    ExpectedSalaryFrom = candidates.ExpectedSalaryFrom,
                    ExpectedSalaryTo = candidates.ExpectedSalaryTo,
                    IsFullTime = candidates.IsFullTime,
                    IsPermanent = candidates.IsPermanent,
                    IsPartTime = candidates.IsPartTime,
                    IsTemporary = candidates.IsTemporary,
                    IsRemote = candidates.IsRemote,
                    IsLocum = candidates.IsLocum,
                    Skills = candidates.Skills,
                    MobileNo = candidates.MobileNo,
                    Email = candidates.Email,
                    Sex = candidates.Sex,
                    DocumentList = candidates.DocumentList
                };
            }
            //var candidate = BsonSerializer.Deserialize<CandidateViewModel>(result);
            return null;
        }

        public List<CandidateViewModel> SearchCandidates(CandidateFilteringViewModel candidateFilteringVM)
        {
            var query = _db.Candidates.AsQueryable().AsQueryable();
            //List<IQueryable> queryList = new List<IQueryable>();

            //if (!String.IsNullOrEmpty(candidateFilteringVM.Profile.Trim()))
            //{
            //    query = (from c in query.Where(x => x.Profile.ToLower().Contains(candidateFilteringVM.Profile.ToLower())) select c).AsQueryable();
            //}

            //if (candidateFilteringVM.PositionList != null && candidateFilteringVM.PositionList.Count() > 0)
            //{
            //    foreach (string position in candidateFilteringVM.PositionList)
            //    {
            //        query = (from c in query.Where(x => x.Position.ToLower().Contains(position.ToLower())) select c).AsQueryable();
            //    }

            //}

            //query = (from c in query.Where(x => x.IsFullTime == candidateFilteringVM.IsFullTime) select c).AsQueryable();

            //query = (from c in query.Where(x => x.IsPermanent == candidateFilteringVM.IsPermanent) select c).AsQueryable();

            //if (!candidateFilteringVM.isVideoProfileSearch)
            //{
            //    query = (from c in query.Where(x => x.IsPartTime == candidateFilteringVM.IsPartTime) select c).AsQueryable();

            //    query = (from c in query.Where(x => x.IsTemporary == candidateFilteringVM.IsTemporary) select c).AsQueryable();

            //    query = (from c in query.Where(x => x.IsRemote == candidateFilteringVM.IsRemote) select c).AsQueryable();

            //    query = (from c in query.Where(x => x.IsLocum == candidateFilteringVM.IsLocum) select c).AsQueryable();


            //    if (candidateFilteringVM.ExpectedSalaryFrom != 0 && candidateFilteringVM.ExpectedSalaryTo != 0)
            //    {
            //        query = (from c in query.Where(x =>
            //            (x.ExpectedSalaryFrom >= candidateFilteringVM.ExpectedSalaryFrom && x.ExpectedSalaryFrom < candidateFilteringVM.ExpectedSalaryTo)
            //            && (x.ExpectedSalaryTo <= candidateFilteringVM.ExpectedSalaryTo && x.ExpectedSalaryTo > candidateFilteringVM.ExpectedSalaryFrom)
            //            )
            //                 select c).AsQueryable();
            //    }
            //}
            // by location 
            if (candidateFilteringVM.LocationList != null && candidateFilteringVM.LocationList.Count() > 0)
            {
                //foreach (string location in candidateFilteringVM.LocationList)
                //{
                //    query = (from c in query.Where(x => x.Location.ToLower().Contains(location.ToLower())) select c).AsQueryable();
                //}
                query = (from c in query.Where(x=> candidateFilteringVM.LocationList.Contains(x.Location)) select c).AsQueryable();
            }

            //if (candidateFilteringVM.Skills != null && candidateFilteringVM.Skills.Count() > 0)
            //{
            //    foreach (string skill in candidateFilteringVM.Skills)
            //    {
            //        query = (from c in query.Where(x => x.Skills.Contains(skill)) select c).AsQueryable();
            //    }

            //}


            var result = (from c in query
                          select new CandidateViewModel
                          {
                              _id = c._id,
                              CandidateName = c.CandidateName,
                              VideoUrl = c.DocumentList[1].Guid,
                              ImageUrl = c.ImageUrl,
                              Objectives = c.Objectives,
                              Profile = c.Profile,
                              Position = c.Position,
                              Location = c.Location,
                              Experince = c.Experince,
                              CurrentSalary = c.CurrentSalary,
                              ExpectedSalaryFrom = c.ExpectedSalaryFrom,
                              ExpectedSalaryTo = c.ExpectedSalaryTo,
                              IsFullTime = c.IsFullTime,
                              IsPermanent = c.IsPermanent,
                              IsPartTime = c.IsPartTime,
                              IsTemporary = c.IsTemporary,
                              IsRemote = c.IsRemote,
                              IsLocum = c.IsLocum,
                              Skills = c.Skills,
                              MobileNo = c.MobileNo,
                              Email = c.Email,
                              Sex = c.Sex,
                              DocumentList = c.DocumentList,
                              
                          }).ToList();

            return result;
        }
              
    }
}
