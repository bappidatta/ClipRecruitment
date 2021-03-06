﻿using ClipRecruitment.Domain;
using ClipRecruitment.Domain.Models;
using ClipRecruitment.Employer.ViewModels;
using MongoDB.Bson;
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
                Industry = jobVM.Industry,
                InsolvencyID = jobVM.InsolvencyID,
                EmployerID = jobVM.EmployerID,
                SalaryFrom = jobVM.SalaryFrom,
                SalaryTo = jobVM.SalaryTo,
                IsFullTime = jobVM.IsFullTime,
                IsPermanent = jobVM.IsPermanent,
                IsRemote = jobVM.IsRemote,
                IsLocal = jobVM.IsLocal,
                IsPartTime = jobVM.IsPartTime,
                IsTemporary = jobVM.IsRemote,
                LocationList = jobVM.LocationList,
                Description = jobVM.Description,
                Position = jobVM.Position,
                YearOfExperience = jobVM.YearOfExperience,
                SkillSet = jobVM.SkillSet, 
                EducationLevel = jobVM.EducationLevel
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
                .Set("LongDescription", jobVM.Description)
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
                 Industry = j.Industry,
                 InsolvencyID = j.InsolvencyID,
                 EmployerID = j.EmployerID,
                 SalaryFrom = j.SalaryFrom,
                 SalaryTo = j.SalaryTo,
                 IsFullTime = j.IsFullTime,
                 IsPermanent = j.IsPermanent,
                 IsRemote = j.IsRemote,
                 LocationList = j.LocationList,
                 Description = j.Description,
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
                    Industry = job.Industry,
                    InsolvencyID = job.InsolvencyID,
                    EmployerID = job.EmployerID,
                    SalaryFrom = job.SalaryFrom,
                    SalaryTo = job.SalaryTo,
                    IsFullTime = job.IsFullTime,
                    IsPermanent = job.IsPermanent,
                    IsRemote = job.IsRemote,
                    LocationList = job.LocationList,
                    Description = job.Description,
                    Position = job.Position,
                    YearOfExperience = job.YearOfExperience,
                    _id = job._id,
                    ApplicationList = job.ApplicationList,
                    EducationLevel = job.EducationLevel,
                    SkillSet = job.SkillSet,
                    IsArchived = job.IsArchived,
                    IsLocal = job.IsLocal,
                    IsPartTime = job.IsPartTime,
                    IsTemporary = job.IsTemporary
                };
            }
            return null;
        }   
        
        public List<JobViewModel> SearchJobs(JobFilteringViewModel jobFilteringVM, int skip, int take, out int count)
        {
            var query = _db.Jobs.AsQueryable<Job>().AsQueryable();

            // filter by industry type
            if (!string.IsNullOrEmpty(jobFilteringVM.Industry))
            {
                query = (from j in query.Where(x => x.Industry.ToLower().Contains(jobFilteringVM.Industry.ToLower()))
                         select j).AsQueryable();
            }

            //filter by insolvency
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


            //filter by locations
            if (jobFilteringVM.LocationList != null && jobFilteringVM.LocationList.Count > 0)
            {
                query = (from j in query.Where(x => jobFilteringVM.LocationList.Contains(x.LocationList[0])) select j).AsQueryable();
            }

            if (jobFilteringVM.SalaryFrom != 0 && jobFilteringVM.SalaryTo != 0)
            {
                query = (from j in query.Where(x =>
                        (x.SalaryFrom >= jobFilteringVM.SalaryFrom && x.SalaryFrom < jobFilteringVM.SalaryTo)
                        && (x.SalaryTo <= jobFilteringVM.SalaryTo && x.SalaryTo > jobFilteringVM.SalaryFrom)
                     )
                         select j).AsQueryable();
            }
            

            // experience and position 
            if (jobFilteringVM.PositionList.Count > 0)
            {
                //List<string> posList = (from j in query select j.Position.ToLower()).ToList();


                // List<string> positionlist = jobFilteringVM.PositionList.ConvertAll(x => x.ToLower());

                //query = (from j in query.Where(x => positionlist.Contains(x.Position.ToLower())) select j).AsQueryable();

                query = (from j in query.Where(x => jobFilteringVM.PositionList.Contains(x.Position)) select j).AsQueryable();
                //query = (from j in query.Where(x => jobFilteringVM.PositionList.Contains(x.Position.ToLower())) select j).AsQueryable();
                //query = (from j in query.Where(x => jobFilteringVM.PositionList.ConvertAll(m => m.ToLower()).Contains(x.Position.ToLower())) select j).AsQueryable();
            }

            count = query.ToList().Count();

            var result = (from j in query.Skip(skip).Take(take)
                          select new JobViewModel
                          {
                              Industry = j.Industry,
                              InsolvencyID = j.InsolvencyID,
                              EmployerID = j.EmployerID,
                              SalaryFrom = j.SalaryFrom,
                              SalaryTo = j.SalaryTo,
                              IsFullTime = j.IsFullTime,
                              IsPermanent = j.IsPermanent,
                              IsRemote = j.IsRemote,
                              LocationList = j.LocationList,
                              Description = j.Description,
                              Position = j.Position,
                              YearOfExperience = j.YearOfExperience,
                              _id = j._id                              
                          }
                         ).ToList();
            

            return result;
        }

        public List<Application> GetApplicationsByJobId(string jobId)
        {
            var query = _db.Jobs.AsQueryable();
            var result = (from j in query.Where(x => x._id == jobId) select j.ApplicationList).SingleOrDefault();
            return result;
        }
        
        public bool IsApplied(List<JobViewModel> jobList, string applicantId)
        {
            foreach(var item in jobList)
            {
                var applications = GetApplicationsByJobId(item._id);
                if (applications != null && applications.Count > 0)
                {
                    List<string> applicants = applications.Select(x => x.ApplicantId).ToList();
                    if (applicants != null && applicants.Contains(applicantId))
                        return true;
                }
            }
            return false;
        }

        public bool IsApplied(string jobId, string applicantId)
        {
            var applications = GetApplicationsByJobId(jobId);
            if (applications != null && applications.Count > 0)
            {
                List<string> applicants = applications.Select(x => x.ApplicantId).ToList();
                if (applicants != null && applicants.Contains(applicantId))
                    return true;
            }
            return false;
        }

        public void AddApplicant(List<JobViewModel> jobList, string applicantId)
        {
            foreach(var item in jobList)
            {                
                var filter = Builders<Job>.Filter.Eq("_id", item._id);
                //var update = Builders<Job>.Update.Push<Application>("ApplicationList", 
                //            new Application {
                //                ApplicantId = applicantId,
                //                ApplicationDate = DateTime.UtcNow });


                var update = Builders<Job>.Update.Push("ApplicationList", new Application
                {
                    ApplicantId = applicantId,
                    ApplicationDate = DateTime.UtcNow
                });
                _db.Jobs.UpdateOne(filter, update);
            }
        }
        

    }
}
