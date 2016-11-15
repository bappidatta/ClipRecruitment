using ClipRecruitment.Domain;
using MongoDB.Bson;
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

        public void CreateJob()
        {
            _db.GetCollection("Job").InsertOne(new BsonDocument {
                { "name", "MongoDB" },
                { "type", "Database" },
                { "count", 1 },
                { "info", new BsonDocument
                    {
                        { "x", 203 },
                        { "y", 102 }
                    }}
            });
        }
    }
}
