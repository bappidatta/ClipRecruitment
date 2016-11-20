﻿using ClipRecruitment.Domain.Models;
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
                if(this.db == null)
                {
                    var client = new MongoClient("mongodb://localhost:27017");
                    this.db = client.GetDatabase("ClipRecruitment");
                }
                return this.db.GetCollection<Job>("Jobs");
            }
        }

        public IMongoCollection<BsonDocument> Users
        {
            get
            {
                return GetCollection("Users");
            }
        }

        public IMongoCollection<BsonDocument> Employers
        {
            get
            {
                return GetCollection("Employers");
            }
        }

        public IMongoCollection<BsonDocument> Candidates
        {
            get
            {
                return GetCollection("Candidates");
            }
        }

        private IMongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            if (this.db == null)
            {
                var client = new MongoClient("mongodb://localhost:27017");
                this.db = client.GetDatabase("ClipRecruitment");
            }

            return db.GetCollection<BsonDocument>(collectionName);
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
