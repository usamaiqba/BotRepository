using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bot_Application3.Model
{
    public class MongoUser
    {

        private static BotContext ec = new BotContext();
        private static BotContext ac = null;

        public static bool add_basic_info(BsonDocument info)
        {        
            var Client = new MongoClient();
            var MongoDB = Client.GetDatabase("Botdatabase");
            var Collec = MongoDB.GetCollection<BsonDocument>("users");
           
            try
            {
                Collec.InsertOneAsync(info);
                 return true;
            }
            catch
            {
                 return false;
            }
        }

        public static void add_edu_infos(List<BsonDocument> multiple)
        {
            var Client = new MongoClient();
            var MongoDB = Client.GetDatabase("Botdatabase");
            var Collec = MongoDB.GetCollection<BsonDocument>("users");
            var filter = Builders<BsonDocument>.Filter.Eq("user_id", "882135325197415");
            var update = Builders<BsonDocument>.Update.Set("educational", new BsonArray(multiple));
            try
            {
                Collec.UpdateOneAsync(filter, update);
            }
            catch
            {

            }
        }

        public static void add_edu_info(BsonDocument edu)
        {
            var Client = new MongoClient();
            var MongoDB = Client.GetDatabase("Botdatabase");
            var Collec = MongoDB.GetCollection<BsonDocument>("users");
            try
            {
                Collec.InsertOneAsync(edu);             
            }
            catch
            {
               
            }
        }

        public static void add_prof_info(BsonDocument prof)
        {
            var Client = new MongoClient();
            var MongoDB = Client.GetDatabase("Botdatabase");
            var Collec = MongoDB.GetCollection<BsonDocument>("users");
            try
            {
                Collec.InsertOneAsync(prof);
            
            }
            catch
            {

            }
        }

        public static basic_infos exist_user(string id)
        {
             ac = new BotContext();

            var Client = new MongoClient();
            var MongoDB = Client.GetDatabase("Botdatabase");
            var Collec = MongoDB.GetCollection<BsonDocument>("users");


            var query = (from exs in ec.basic_infos
                         where exs.user_id == id
                         select exs).SingleOrDefault();
            return query;
        }

        public static void upd_pro_info()
        {
            try
            {
                ec.SaveChanges();
                // return true;
            }
            catch
            {
                //return false;
            }

        }

        public static professional_Infos find_user_occu(string id)
        {
            // ac = new BotContext();
            var query = (from fin in ec.professional_Infos
                         where fin.user_id == id
                         select fin).SingleOrDefault();
            return query;
        }

        public static int get_skill_id(string skill)
        {
            ac = new BotContext();
            var query = (from ski in ac.skills
                         where ski.skill_name == skill
                         select ski.skill_id).SingleOrDefault();
            return query;
        }



        public static void DoSomethingAsync(string user)
        {


            // var collection = db.GetCollection<BsonDocument>("locations");
            var Client = new MongoClient();
            var MongoDB = Client.GetDatabase("bot");
            var Collec = MongoDB.GetCollection<BsonDocument>("tests");

            //var locations = new List<BsonDocument>();
            //var json = JObject.Parse(user);
            //BsonDocument document = BsonDocument.Parse(user);
            //var bs = BsonSerializer.Deserialize<BsonDocument>(user); //Deserialize JSON String to BSon Document
            //Collec.InsertOneAsync(bs);
            //var mcollection = Program._database.GetCollection<BsonDocument>("test_collection_05");
            //await mcollection.InsertOneAsync(bsdocument); //Insert into mongoDB

            //foreach (var j in bs)
            //{
            //    locations.Add(j);

            //}


            //foreach (var d in json["locations"])
            //{

            //    //  var context = BsonDeserializationContext.CreateRoot(jsonReader);
            //    //  var document = Collec.DocumentSerializer.Deserialize<BsonDocument>(d);
            //  //  var document = BsonSerializer.Deserialize<BsonDocument>(user);

            //    //  locations.Add(document);

            //}
            //  Collec.InsertManyAsync(locations).wait();


            //var Client = new MongoClient();
            //var MongoDB = Client.GetDatabase("bot");
            //var Collec = MongoDB.GetCollection<BsonDocument>("tests");
            var documnt = new BsonDocument
{
    {"Brand","Dll"},
    {"Price","40"},
    {"Ram","89GB"},
    {"HardDisk","78TB"},
    {"Screen","16inch"}
};
            // Collec.InsertOneAsync(bs);
            // Console.ReadLine();

        }
    }
}