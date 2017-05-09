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

        public static void DoSomethingAsync(BsonDocument user)
        {
            var Client = new MongoClient();
            var MongoDB = Client.GetDatabase("sho");
            var Collec = MongoDB.GetCollection<BsonDocument>("computers");
            var documnt = new BsonDocument
{
    {"Brand","Dll"},
    {"Price","40"},
    {"Ram","89GB"},
    {"HardDisk","78TB"},
    {"Screen","16inch"}
};
            Collec.InsertOneAsync(user);
            Console.ReadLine();
        }
    }
}