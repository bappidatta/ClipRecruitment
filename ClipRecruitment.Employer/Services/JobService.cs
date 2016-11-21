using ClipRecruitment.Domain;
using ClipRecruitment.Domain.Models;
using ClipRecruitment.Employer.ViewModels;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
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
        private Job job;  
        
        public JobService(DbContext db)
        {
            _db = db;
        }

        public async Task<JobViewModel> CreateJobAsync(JobViewModel jobVM)
        {
            job = new Job
            {
                IndustryID = jobVM.IndustryID,
                InsolvencyID = jobVM.InsolvencyID,
                EmployerID = jobVM.EmployerID,
                SalaryFrom = jobVM.SalaryFrom,
                SalaryTo = jobVM.SalaryTo,
                ShortDescription = jobVM.ShortDescription,
                IsFullTime = jobVM.IsFullTime,
                IsPermanent = jobVM.IsPermanent,
                IsRemote = jobVM.IsRemote,
                Location = jobVM.Location,
                LongDescription = jobVM.LongDescription,
                Position = jobVM.Position,
                YearOfExperience = jobVM.YearOfExperience,
            };

            await _db.Jobs.InsertOneAsync(job);
            return GetJobByID(job._id);
        }

        public async Task<JobViewModel> UpdateJobAsync(JobViewModel jobVM)
        {
            var id = new ObjectId(jobVM._id);
            var filter = Builders<Job>.Filter.Eq("_id", id);
            var update = Builders<Job>.Update
                .Set("SalaryFrom", jobVM.SalaryFrom)
                .Set("SalaryTo", jobVM.SalaryTo)                
                .Set("LongDescription", jobVM.LongDescription)
                .Set("ShortDescription", jobVM.ShortDescription)
                .Set("EmployerID", jobVM.EmployerID);

            var result = await _db.Jobs.UpdateOneAsync(filter, update);            
            return GetJobByID(jobVM._id.ToString());

        }

        public List<JobViewModel> GetAllJob(int skip, int take, out int count)
        {
            count = _db.Jobs.Find(new BsonDocument()).ToList().Count();
            
            var result = _db.Jobs.AsQueryable<Job>();

            return (from j in result.Skip(skip).Take(take)
             select new JobViewModel
             {
                 IndustryID = j.IndustryID,
                 InsolvencyID = j.InsolvencyID,
                 EmployerID = j.EmployerID,
                 SalaryFrom = j.SalaryFrom,
                 SalaryTo = j.SalaryTo,
                 ShortDescription = j.ShortDescription,
                 IsFullTime = j.IsFullTime,
                 IsPermanent = j.IsPermanent,
                 IsRemote = j.IsRemote,
                 Location = j.Location,
                 LongDescription = j.LongDescription,
                 Position = j.Position,
                 YearOfExperience = j.YearOfExperience,
                 _id = j._id
             }).ToList();            
        }        

        public JobViewModel GetJobByID(string JobID)
        {
            var id = new ObjectId(JobID);
            var filter = Builders<Job>.Filter.Eq("_id", id);            
            var job = _db.Jobs.Find(filter).SingleOrDefault();
            if(job != null)
            {
                return new JobViewModel
                {
                    IndustryID = job.IndustryID,
                    InsolvencyID = job.InsolvencyID,
                    EmployerID = job.EmployerID,
                    SalaryFrom = job.SalaryFrom,
                    SalaryTo = job.SalaryTo,
                    ShortDescription = job.ShortDescription,
                    IsFullTime = job.IsFullTime,
                    IsPermanent = job.IsPermanent,
                    IsRemote = job.IsRemote,
                    Location = job.Location,
                    LongDescription = job.LongDescription,
                    Position = job.Position,
                    YearOfExperience = job.YearOfExperience,
                    _id = job._id
                };
            }
            return null;
        }   

        public List<string> GetLocations(string inputString)
        {
            var query = _db.Jobs.AsQueryable().AsQueryable();
            List<string>  locationList = (from l in query
                                          .Where(x => x.Location.ToLower().Contains(inputString.ToLower())) select l.Location)
                                          .Distinct()
                                          .ToList();
            
            return locationList;
        }

        public List<string> GetPositions(string inputString)
        {
            var query = _db.Jobs.AsQueryable().AsQueryable();
            List<string> positionList = (from l in query
                                         .Where(x => x.Position.ToLower().Contains(inputString.ToLower()))
                                         select l.Position)
                                          .Distinct()
                                          .ToList();

            return positionList;
        }

        public List<JobViewModel> SearchJobs(JobFilteringViewModel jobFilteringVM)
        {

            var query = _db.Jobs.AsQueryable().AsQueryable();
            
            // filter by industry type
            if (jobFilteringVM.IndustryID > 0)
            {
                query = (from j in query.Where(x => x.IndustryID == jobFilteringVM.IndustryID)
                         select j).AsQueryable();
            }
            // filter by insolvency
            //if (jobFilteringVM.InsolvencyID > 0)
            //{
            //    query = (from j in query.Where(x => job.InsolvencyID == jobFilteringVM.InsolvencyID)
            //             select j).AsQueryable();
            //}
            // is full time job
            if (jobFilteringVM.IsFullTime)
            {
                query = (from j in query.Where(x => x.IsFullTime == true) select j).AsQueryable();
            }

            if(jobFilteringVM.IsPartTime)
            {
                query = (from j in query.Where(x => x.IsPartTime == true) select j).AsQueryable();
            }
            // is permanent job
            if (jobFilteringVM.IsPermanent)
            {
                query = (from j in query.Where(x => x.IsPermanent == true) select j).AsQueryable();
            }
            
            if(jobFilteringVM.IsTemporary)
            {
                query = (from j in query.Where(x => x.IsTemporary == true) select j).AsQueryable();
            }
            // is remote job
            if (jobFilteringVM.IsRemote)
            {
                query = (from j in query.Where(x => x.IsRemote == true) select j).AsQueryable();
            }
            
            if(jobFilteringVM.IsLocal)
            {
                query = (from j in query.Where(x => x.IsLocal == true) select j).AsQueryable();
            }


            // filter by locations
            if (jobFilteringVM.LocationList != null && jobFilteringVM.LocationList.Count > 0)
            {
                query = (from j in query.Where(x => jobFilteringVM.LocationList.Contains(x.Location)) select j).AsQueryable();
            }

            //filter by positions
            //if (jobFilteringVM.PositionList != null && jobFilteringVM.PositionList.Count > 0)
            //{
            //    query = (from j in query.Where(x => jobFilteringVM.PositionList.Contains(x.Position)) select j).AsQueryable();
            //}

            //// year of experience
            //if (jobFilteringVM.ExperienceList != null && jobFilteringVM.ExperienceList.Count > 0)
            //{
            //    query = (from j in query.Where(x => jobFilteringVM.ExperienceList.Contains(x.YearOfExperience.ToString())) select j).AsQueryable();
            //}

            // experience and position 
            if(jobFilteringVM.PositionExperienceList.Count > 0)
            {
                foreach(var item in jobFilteringVM.PositionExperienceList)
                {
                    query = (from j in query
                             .Where(x => 
                                x.Position.ToLower().Contains(item.Position.ToLower())
                                &&  (
                                        x.YearOfExperience >= item.ExperienceRange.From && 
                                        item.ExperienceRange.From != 0 ? x.YearOfExperience <= item.ExperienceRange.From : x.YearOfExperience <= 100
                                    )
                             )                   
                             select j).AsQueryable();
                }
            }

            // salary Ranges
            if(jobFilteringVM.SalaryFrom != 0 && jobFilteringVM.SalaryTo != 0)
            {
                query = (from j in query.Where(x =>
                        (x.SalaryFrom >= jobFilteringVM.SalaryFrom && x.SalaryFrom < jobFilteringVM.SalaryTo)
                        && (x.SalaryTo <= jobFilteringVM.SalaryTo && x.SalaryTo > jobFilteringVM.SalaryFrom)
                     )
                         select j).AsQueryable();
            }

            var result = (from j in query
                          select new JobViewModel
                          {
                              IndustryID = j.IndustryID,
                              InsolvencyID = j.InsolvencyID,
                              EmployerID = j.EmployerID,
                              SalaryFrom = j.SalaryFrom,
                              SalaryTo = j.SalaryTo,
                              ShortDescription = j.ShortDescription,
                              IsFullTime = j.IsFullTime,
                              IsPermanent = j.IsPermanent,
                              IsRemote = j.IsRemote,
                              Location = j.Location,
                              LongDescription = j.LongDescription,
                              Position = j.Position,
                              YearOfExperience = j.YearOfExperience,
                              _id = j._id
                          }
                         ).ToList();

            return result;
        }
        
    }
}
