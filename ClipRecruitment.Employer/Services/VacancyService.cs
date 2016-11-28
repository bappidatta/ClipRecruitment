using ClipRecruitment.Domain;
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
    public class VacancyService
    {
        private DbContext _db;
        private Vacancy vacancy;

        public VacancyService(DbContext db)
        {
            this._db = db;
        }

        public async Task<VacancyViewModel> CreateVacancy(VacancyViewModel vacancyVM)
        {

            vacancy = new Vacancy
            {
                JobTitle = vacancyVM.JobTitle,
                Position = vacancyVM.Position,
                Experince = vacancyVM.Experince,
                EducationLabel = vacancyVM.EducationLabel,
                SalaryEstimate = vacancyVM.SalaryEstimate,
                JobDescription = vacancyVM.JobDescription,
                IsPermanent = vacancyVM.IsPermanent,
                IsTemporary = vacancyVM.IsTemporary,
                IsFullTime = vacancyVM.IsFullTime,
                IsPartTime = vacancyVM.IsPartTime,
                LocationList = vacancyVM.LocationList,
                SkillList = vacancyVM.SkillList
            };

            await _db.Vacancy.InsertOneAsync(vacancy);
            return GetVacancyByID(vacancy._id);
        }

        public VacancyViewModel GetVacancyByID(string id)
        {
            var vacancyId = new ObjectId(id);
            var filter = Builders<Vacancy>.Filter.Eq("_id", id);
            var vacancy = _db.Vacancy.Find(filter).SingleOrDefault();

            if (vacancy != null)
            {
                return new VacancyViewModel
                {
                    JobTitle = vacancy.JobTitle,
                    Position = vacancy.Position,
                    Experince = vacancy.Experince,
                    EducationLabel = vacancy.EducationLabel,
                    SalaryEstimate = vacancy.SalaryEstimate,
                    JobDescription = vacancy.JobDescription,
                    IsPermanent = vacancy.IsPermanent,
                    IsTemporary = vacancy.IsTemporary,
                    IsFullTime = vacancy.IsFullTime,
                    IsPartTime = vacancy.IsPartTime,
                    LocationList = vacancy.LocationList,
                    SkillList = vacancy.SkillList
                };
            }

            return null;
        }

    }
}
