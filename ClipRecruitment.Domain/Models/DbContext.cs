using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipRecruitment.Domain.Models
{
   public class DbConnecion
    {
        private MongoClient client;
        public MongoClient DbClient
        {
            get
            {
                if(client == null)
                {
                    client = new MongoClient();
                }
                return client;
            }
        }
    }
}
