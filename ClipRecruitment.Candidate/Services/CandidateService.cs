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

namespace ClipRecruitment.Candidate.Services
{
   public class CandidateService
    {
        private DbContext _db;

        public CandidateService(DbContext _db)
        {
            this._db = _db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateVM"></param>
        /// <returns></returns>
        public async Task<CandidateViewModel> CreateCandidateAsync(CandidateViewModel candidateVM)
        {
            var document = candidateVM.ToBsonDocument();

            document.Remove("_id");

            await _db.Candidates.InsertOneAsync(document);

            return BsonSerializer.Deserialize<CandidateViewModel>(document);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CandidateViewModel> GetAllCandidateList()
        {
            List<CandidateViewModel> candidatelist = new List<CandidateViewModel>();

            var filter = FilterDefinition<BsonDocument>.Empty;
            var result = _db.Candidates.Find(FilterDefinition<BsonDocument>.Empty).ToList<BsonDocument>();

            foreach (var item in result)
            {
                candidatelist.Add(BsonSerializer.Deserialize<CandidateViewModel>(item));
            }

            return candidatelist;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidateVM"></param>
        /// <returns></returns>
        public CandidateViewModel UpdateCandidate(CandidateViewModel candidateVM)
        {
            var id = new ObjectId(candidateVM._id);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var update = Builders<BsonDocument>.Update
                .Set("FirstName", candidateVM.FirstName)
                .Set("LastName", candidateVM.LastName)
                .Set("MobileNo", candidateVM.MobileNo)
                .Set("Email", candidateVM.Email)
                .Set("Sex", candidateVM.Sex)                
                .Set("BirthDate", candidateVM.BirthDate);

            var result = _db.Candidates.UpdateOne(filter, update);

            return GetCandidateByID(candidateVM._id.ToString());
        }

                
        public CandidateViewModel GetCandidateByID(string candidateId)
        {
            var id = new ObjectId(candidateId);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);

            var result = _db.Candidates.Find<BsonDocument>(filter).SingleOrDefault();

            var candidate = BsonSerializer.Deserialize<CandidateViewModel>(result);
            return candidate;
        }
    }
}
