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
    public class EmployerService
    {
        private DbContext _db;

        public EmployerService(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employerVM"></param>
        /// <returns></returns>
        public async Task<EmployerViewModel> CreateEmployerAsync(EmployerViewModel employerVM)
        {
            var document = employerVM.ToBsonDocument();

            document.Remove("_id");

            await _db.Employers.InsertOneAsync(document);

            return BsonSerializer.Deserialize<EmployerViewModel>(document);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EmployerViewModel> GetAllEmployerList()
        {
            List<EmployerViewModel> employerList = new List<EmployerViewModel>();

            var filter = FilterDefinition<BsonDocument>.Empty;
            var result = _db.Employers.Find(FilterDefinition<BsonDocument>.Empty).ToList<BsonDocument>();

            foreach (var item in result)
            {
                employerList.Add(BsonSerializer.Deserialize<EmployerViewModel>(item));
            }

            return employerList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employerVM"></param>
        /// <returns></returns>
        public EmployerViewModel UpdateEmployer(EmployerViewModel employerVM)
        {
            var id = new ObjectId(employerVM._id);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var update = Builders<BsonDocument>.Update
                .Set("FirstName", employerVM.FirstName)
                .Set("LastName", employerVM.LastName)
                .Set("MobileNo", employerVM.MobileNo)
                .Set("Email", employerVM.Email)
                .Set("Sex", employerVM.Sex)
                .Set("Salary", employerVM.Salary)
                .Set("BirthDate", employerVM.BirthDate);

            var result = _db.Employers.UpdateOne(filter, update);

            return GetEmployerByID(employerVM._id.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employerId"></param>
        /// <returns></returns>
        public EmployerViewModel GetEmployerByID(string employerId)
        {
            var id = new ObjectId(employerId);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);

            var result = _db.Employers.Find<BsonDocument>(filter).SingleOrDefault();

            var employer = BsonSerializer.Deserialize<EmployerViewModel>(result);
            return employer;
        }
    }
}
