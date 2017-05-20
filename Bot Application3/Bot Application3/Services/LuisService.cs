using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Bot_Application3.Model;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using MongoDB.Bson.Serialization;

namespace Bot_Application3.Services
{
    public class LuisService
    {
        public static async Task<LuisResponse> ParseUserInput(string phrase)
        {
            string returnvalue = string.Empty;
            string escapedString = Uri.EscapeDataString(phrase);
            using (var client = new HttpClient())
            {
                //   string uri = $"https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/45ea4b09-2acf-46fc-a23e-62c59ce38901?subscription-key=9898c808f2b74e0d8267ae2c39d818d3&verbose=true&q={escapedString}";
                string uri = $"https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/45ea4b09-2acf-46fc-a23e-62c59ce38901?subscription-key=9898c808f2b74e0d8267ae2c39d818d3&timezoneOffset=0.0&verbose=true&q={escapedString}";
                var msg = await client.GetAsync(uri);
                if (msg.IsSuccessStatusCode)
                {
                    var jsonresponse = await msg.Content.ReadAsStringAsync();
                    var jsonresp = msg.Content.ToBsonDocument();

                    var data = JsonConvert.DeserializeObject<LuisResponse>(jsonresponse);
                    var daa = JsonConvert.SerializeObject(data);
                    BsonDocument bs = new BsonDocument();
                    
                  //  bs = (BsonDocument)daa;
                    //DoSomethingAsync(jsonresp);

                    string creditApplicationJson = JsonConvert.SerializeObject(
                    new
                    {
                        jsonCreditApplication = data
                    });

                    return data;
                }

            }
            return null;

        }

        public static void DoSomethingAsync(string user)
        {


            // var collection = db.GetCollection<BsonDocument>("locations");
            var Client = new MongoClient();
            var MongoDB = Client.GetDatabase("bot");
            var Collec = MongoDB.GetCollection<BsonDocument>("tests");

            var locations = new List<BsonDocument>();
            var json = JObject.Parse(user);
            BsonDocument document = BsonDocument.Parse(user);
            var bs = BsonSerializer.Deserialize<BsonDocument>(user); //Deserialize JSON String to BSon Document
            Collec.InsertOneAsync(bs);
//            var mcollection = Program._database.GetCollection<BsonDocument>("test_collection_05");
 //           await mcollection.InsertOneAsync(bsdocument); //Insert into mongoDB

            foreach (var j in bs)
            {
              //  locations.Add(j);     
               
            }
            
            
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
         //   Collec.InsertOneAsync(bs);
            //Console.ReadLine();
        }
    }
}