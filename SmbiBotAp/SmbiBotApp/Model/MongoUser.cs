using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
namespace SmbiBotApp.Model
{
    public class MongoUser
    {
    
   
        
        public static bool add_basic_info(BsonDocument info)
        {
            //  var Client = new MongoClient("mongodb://nabeel:nabeel@ds161742.mlab.com:61742/peoplehome");
            var Client = new MongoClient("mongodb://rizwan:rizwan@ds127883.mlab.com:27883/talenthome");
            var MongoDB = Client.GetDatabase("talenthome");
            var Collec = MongoDB.GetCollection<BsonDocument>("botusers");

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


        public static void add_edu_infos(List<BsonDocument> multiple, string id)
        {
            // var Client = new MongoClient("mongodb://nabeel:nabeel@ds161742.mlab.com:61742/peoplehome");
            var Client = new MongoClient("mongodb://rizwan:rizwan@ds127883.mlab.com:27883/peoplehome");
            var MongoDB = Client.GetDatabase("talenthome");
            var Collec = MongoDB.GetCollection<BsonDocument>("botusers");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
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
            //  var Client = new MongoClient("mongodb://nabeel:nabeel@ds161742.mlab.com:61742/peoplehome");
            var Client = new MongoClient("mongodb://rizwan:rizwan@ds127883.mlab.com:27883/talenthome");

            var MongoDB = Client.GetDatabase("talenthome");
            var Collec = MongoDB.GetCollection<BsonDocument>("botusers");
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
            // var Client = new MongoClient("mongodb://nabeel:nabeel@ds161742.mlab.com:61742/peoplehome");
            var Client = new MongoClient("mongodb://rizwan:rizwan@ds127883.mlab.com:27883/talenthome");

            var MongoDB = Client.GetDatabase("talenthome");
            var Collec = MongoDB.GetCollection<BsonDocument>("botusers");
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
            //  var Client = new MongoClient("mongodb://nabeel:nabeel@ds161742.mlab.com:61742/peoplehome");
            var Client = new MongoClient("mongodb://rizwan:rizwan@ds127883.mlab.com:27883/talenthome");

            var MongoDB = Client.GetDatabase("talenthome");
            var Collec = MongoDB.GetCollection<BsonDocument>("botusers");

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


        public static void upd_basic_info(int sta, string id)
        {
            // var Client = new MongoClient("mongodb://nabeel:nabeel@ds161742.mlab.com:61742/peoplehome");
            var Client = new MongoClient("mongodb://rizwan:rizwan@ds127883.mlab.com:27883/talenthome");

            var MongoDB = Client.GetDatabase("talenthome");
            var Collec = MongoDB.GetCollection<BsonDocument>("botusers");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var update = Builders<BsonDocument>.Update.Set("basic.status", sta);

            try
            {
                Collec.UpdateOneAsync(filter, update);
            }
            catch
            {

            }

        }

        public static void upd_test_info(BsonDocument record, int sta, string id)
        {
           // var Client = new MongoClient("mongodb://nabeel:nabeel@ds161742.mlab.com:61742/peoplehome");
            var Client = new MongoClient("mongodb://rizwan:rizwan@ds127883.mlab.com:27883/talenthome");

            var MongoDB = Client.GetDatabase("talenthome");
            var Collec = MongoDB.GetCollection<BsonDocument>("botusers");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            if (sta == 1)
            {

                var update = Builders<BsonDocument>.Update.Set("test", new BsonArray().Add(record));
                Collec.UpdateOneAsync(filter, update);

            }
            else
            {
                var update = Builders<BsonDocument>.Update.Push("test", record);
                Collec.UpdateOneAsync(filter, update);

            }

        }


        public static void upd_pro_info(string com, string pos, BsonDocument proj, int sta, string id)
        {
           // var Client = new MongoClient("mongodb://nabeel:nabeel@ds161742.mlab.com:61742/peoplehome");
            var Client = new MongoClient("mongodb://rizwan:rizwan@ds127883.mlab.com:27883/talenthome");
        

            var MongoDB = Client.GetDatabase("talenthome");
            var Collec = MongoDB.GetCollection<BsonDocument>("botusers");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var update1 = Builders<BsonDocument>.Update.
                Set("professional.company_type", com).
                Set("professional.position_type", pos);
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

        public static void upd_project_info(List<BsonDocument> multiple, int sta, string id)
        {
         //   var Client = new MongoClient("mongodb://nabeel:nabeel@ds161742.mlab.com:61742/peoplehome");
            var Client = new MongoClient("mongodb://rizwan:rizwan@ds127883.mlab.com:27883/talenthome");

            var MongoDB = Client.GetDatabase("talenthome");
            var Collec = MongoDB.GetCollection<BsonDocument>("botusers");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
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


        public static BsonDocument get_skill_id(string ski)
        {
            // var Client = new MongoClient("mongodb://nabeel:nabeel@ds161742.mlab.com:61742/peoplehome");
            var Client = new MongoClient("mongodb://rizwan:rizwan@ds127883.mlab.com:27883/talenthome");

            var MongoDB = Client.GetDatabase("talenthome");
            var Collec = MongoDB.GetCollection<BsonDocument>("skills");

            var filter = Builders<BsonDocument>.Filter.Eq("skill.UXUI", "1");
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


        public static BsonValue ret_sel_test(string tech)
        {
            var Client = new MongoClient("mongodb://rizwan:rizwan@ds127883.mlab.com:27883/talenthome");
            var MongoDB = Client.GetDatabase("talenthome");
            var Collec = MongoDB.GetCollection<BsonDocument>("questions");
            var filter = Builders<BsonDocument>.Filter.Eq("dataset.record.type", tech);
            var result = Collec.Find(filter).SingleOrDefault().ElementAt(1).Value;

            return result;

        }

    }

}