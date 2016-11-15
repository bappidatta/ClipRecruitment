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
        public IMongoDatabase database
        {
            get
            {
                var client = new MongoClient("mongodb://localhost:27017");
                return client.GetDatabase("ClipRecruitment");
            }
        }


        public void abc()
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
