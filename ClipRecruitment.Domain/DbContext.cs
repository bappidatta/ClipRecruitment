using ClipRecruitment.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipRecruitment.Domain
{
    public class DbContext
    {
        private IMongoDatabase db;
            

        public IMongoCollection<Job> Jobs
        {
            get
            {   
                return getDb().GetCollection<Job>("Jobs");
            }
        }

        public IMongoCollection<JobIndustry> JobIndustries
        {
            get
            {
                return getDb().GetCollection<JobIndustry>("JobIndustries");
            }
        }
        
        public IMongoCollection<Employers> Employers
        {
            get
            {

                return getDb().GetCollection<Employers>("Employers");
            }
        }

        public IMongoCollection<Candidates> Candidates
        {
            get
            {
                return getDb().GetCollection<Candidates>("Candidates");
            }
        }

        public IMongoCollection<Positions> Positions
        {
            get
            {
                return getDb().GetCollection<Positions>("Positions");
            }
        }

        public IMongoCollection<Locations> Locations
        {
            get
            {
                return getDb().GetCollection<Locations>("Locations");
            }
        }

        public IMongoCollection<Skills> Skills
        {
            get
            {
                return getDb().GetCollection<Skills>("Skills");
            }
        }

        public IMongoCollection<Vacancy> Vacancy
        {
            get
            {
                return getDb().GetCollection<Vacancy>("Vacancy");
            }
        }

        private IMongoDatabase getDb()
        {
            if (this.db == null)
            {
                var client = new MongoClient("mongodb://localhost:27017");
                this.db = client.GetDatabase("ClipRecruitment");
            }

            return db;
        }

        private void abc()
        {

            var client = new MongoClient("mongodb://localhost:27017");

            var database = client.GetDatabase("foo");

            var collection = database.GetCollection<BsonDocument>("bar");

            var document = new BsonDocument
            {
                { "name", "MongoDB" },
                { "type", "Database" },
                { "count", 1 },
                { "info", new BsonDocument
                    {
                        { "x", 203 },
                        { "y", 102 }
                    }}
            };

            collection.InsertOne(document);
        }

    }
}
