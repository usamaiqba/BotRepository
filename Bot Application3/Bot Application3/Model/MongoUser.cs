using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
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
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "882135325197415");
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

        public static BsonDocument exist_user(string id)
        {                                                                                                                 
            var Client = new MongoClient();
            var MongoDB = Client.GetDatabase("Botdatabase");
            var Collec = MongoDB.GetCollection<BsonDocument>("users");
         
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
                var result = Collec.Find(filter).SingleOrDefault();   
                return result;   
        }

        //public static BsonDocument exist_test()
        //{
        //    var Client = new MongoClient();
        //    var MongoDB = Client.GetDatabase("test");
        //    var Collec = MongoDB.GetCollection<BsonDocument>("questions");
        //  //  return Collec.Find("t");
        //    //var filter1 = Builders<BsonDocument>.Filter.Eq("totalquest.type", "HTML");
        //    //var filter2 = Builders<BsonDocument>.Filter.Eq("type", "HTML");
        //    //var filter = Builders<User>.Filter.Eq(x => x.name, "system")
        //    //var result1 = Collec.Find(filter1).ToList();
        //    //var result2 = Collec.Find(ilter2).ToList();

        //   // return result1;

      
        //}


        public static void upd_basic_info(int sta)
        {
            var Client = new MongoClient();
            var MongoDB = Client.GetDatabase("Botdatabase");
            var Collec = MongoDB.GetCollection<BsonDocument>("users");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "882135325197415");
            var update = Builders<BsonDocument>.Update.Set("basic.status", sta);

            try
            {
                Collec.UpdateOneAsync(filter, update);
            }
            catch
            {

            }

        }

        public static void upd_pro_info(string com,string pos,BsonDocument proj,int sta)
        {
            var Client = new MongoClient();
            var MongoDB = Client.GetDatabase("Botdatabase");
            var Collec = MongoDB.GetCollection<BsonDocument>("users");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "882135325197415");
            var update1 = Builders<BsonDocument>.Update.
                Set("professional.company_type", com).
                Set("professional.position_type",pos);
            var update2 = Builders<BsonDocument>.Update.Set("project", proj);
            var update3 = Builders<BsonDocument>.Update.Set("basic.status", sta);
            try
            {
                Collec.UpdateOneAsync(filter, update1);
                Collec.UpdateOneAsync(filter, update2);
                Collec.UpdateOneAsync(filter, update3);
            }
            catch
            {

            }

        }

        public static void upd_project_info(List<BsonDocument> multiple , int sta)
        {
            var Client = new MongoClient();
            var MongoDB = Client.GetDatabase("Botdatabase");
            var Collec = MongoDB.GetCollection<BsonDocument>("users");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", "882135325197415");     
            var update1 = Builders<BsonDocument>.Update.Set("project.details", multiple);
            var update2 = Builders<BsonDocument>.Update.Set("basic.status", sta);
            try
            {
                Collec.UpdateOneAsync(filter, update1);
                Collec.UpdateOneAsync(filter, update2);
            }
            catch
            {

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

        public static BsonDocument get_skill_id(string ski)
        {            
            var Client = new MongoClient();
            var MongoDB = Client.GetDatabase("Botdatabase");
            var Collec = MongoDB.GetCollection<BsonDocument>("skills");

            var filter = Builders<BsonDocument>.Filter.Eq("skill.UXUI","1");
            var result = Collec.Find(filter).SingleOrDefault();
            if (result == null)
            {
                var skil = new BsonDocument
                {
                    {"skill",new BsonDocument
                     {
                        { "UXUI"       , "1" },
                        { "GraphicWeb" , "2" },
                        { "FullStack"  , "3" }
                     }
                   }
                };
                Collec.InsertOneAsync(skil);
                var filt = Builders<BsonDocument>.Filter.Eq("skill.UXUI", "1");
                return Collec.Find(filt).SingleOrDefault();
               
            }
            else
            {
                return result;
            }
        }


        public static BsonValue ret_sel_test()
        {
            var Client = new MongoClient();
            var MongoDB = Client.GetDatabase("test");
            var Collec = MongoDB.GetCollection<BsonDocument>("questions");
            var filter = Builders<BsonDocument>.Filter.Eq("dataset.record.type", "HTML");
            var result = Collec.Find(filter).SingleOrDefault().ElementAt(1).Value;

            return result;

            //var pos = JsonConvert.DeserializeObject<Testing>(result.ToJson());
            //var sto = (pos.record.Where(x => x.type == "HTML")).ToArray();
            ////  var rec = pos.record.ToArray();
            //var sav = sto[0].Answers;
            //sav = sto[0].Statements;
            //var rec = pos.record.ToArray();

            //sav = sto[0].Options;
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