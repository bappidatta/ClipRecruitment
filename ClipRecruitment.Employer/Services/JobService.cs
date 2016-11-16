using ClipRecruitment.Domain;
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
        
        public JobService(DbContext db)
        {
            _db = db;
        }

        public async Task<JobViewModel> CreateJobAsync(JobViewModel jobVM)
        {
            var document = jobVM.ToBsonDocument();

            document.Remove("_id");
            
            await _db.Jobs.InsertOneAsync(document);

            return BsonSerializer.Deserialize<JobViewModel>(document);
        }

        public JobViewModel UpdateJob(JobViewModel jobVM)
        {
            var id = new ObjectId(jobVM._id);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var update = Builders<BsonDocument>.Update
                .Set("JobTitle", jobVM.JobTitle)
                .Set("SalaryFrom", jobVM.SalaryFrom)
                .Set("SalaryTo", jobVM.SalaryTo)
                .Set("IndustryType", jobVM.IndustryType)
                .Set("Description", jobVM.Description)
                .Set("EmployerID", jobVM.EmployerID);

            var result  = _db.Jobs.UpdateOne(filter, update);

            return GetJobByID(jobVM._id.ToString());
        }

        public List<JobViewModel> GetAllJob()
        {
            List<JobViewModel> jobs = new List<JobViewModel>();

            var filter = FilterDefinition<BsonDocument>.Empty;
            var result = _db.Jobs.Find(FilterDefinition<BsonDocument>.Empty).ToList<BsonDocument>();
            
            foreach(var item in result)
            {
                jobs.Add(BsonSerializer.Deserialize<JobViewModel>(item));
            }

            return jobs;            
        }
        

        public JobViewModel GetJobByID(string JobID)
        {
            var id = new ObjectId(JobID);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);

            var result = _db.Jobs.Find<BsonDocument>(filter).SingleOrDefault();

            var job = BsonSerializer.Deserialize<JobViewModel>(result);
            return job;
        }

        public List<JobViewModel> SearchJobs(JobFilteringViewModel jobFilteringViewModel)
        {
            throw new NotImplementedException();
        }


        
    }
}
