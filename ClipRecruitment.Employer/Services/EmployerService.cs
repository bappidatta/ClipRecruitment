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
    public class EmployerService
    {
        private DbContext _db;
        private Employers employers;

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
            employers = new Employers
            {
                CompanyName = employerVM.CompanyName,
                Email = employerVM.Email,
                Location = employerVM.Location,
                MobileNo = employerVM.MobileNo
            };

            await _db.Employers.InsertOneAsync(employers);
            return GetEmployerByID(employers._id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EmployerViewModel> GetAllEmployerList(int skip, int take, out int count)
        {
            count = _db.Employers.Find(new BsonDocument()).ToList().Count();

            var result = _db.Employers.AsQueryable<Employers>();

            return (from e in result.Skip(skip).Take(take)
                    select new EmployerViewModel
                    {
                        CompanyName = e.CompanyName,
                        Email = e.Email,
                        Location = e.Location,
                        MobileNo = e.MobileNo

                    }).ToList();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employerVM"></param>
        /// <returns></returns>
        public async Task<EmployerViewModel> UpdateEmployer(EmployerViewModel employerVM)
        {
            var id = new ObjectId(employerVM._id);
            var filter = Builders<Employers>.Filter.Eq("_id", id);
            var update = Builders<Employers>.Update
                .Set("CompanyName", employerVM.CompanyName)
                .Set("Email", employerVM.Email)
                .Set("Location", employerVM.Location)
                .Set("MobileNo", employerVM.MobileNo);

            var result = _db.Employers.UpdateOneAsync(filter, update);

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
            var filter = Builders<Employers>.Filter.Eq("_id", id);

            var employers = _db.Employers.Find(filter).SingleOrDefault();

            if (employers != null)
            {
                return new EmployerViewModel
                {
                    _id = employers._id,
                    CompanyName = employers.CompanyName,
                    Email = employers.Email,
                    Location = employers.Location,
                    MobileNo = employers.MobileNo
                };
            }

            return null;
        }
    }
}
