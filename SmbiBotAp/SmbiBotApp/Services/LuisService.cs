using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using SmbiBotApp.Model;
using System.Threading.Tasks;


namespace SmbiBotApp.Services
{
    public class LuisService
    {
     
        public static async Task<LuisResponse> ParseUserInput(string phrase)
        {         
            string returnvalue = string.Empty;
            string escapedString = Uri.EscapeDataString(phrase);
            using (var client = new HttpClient())
            {
                  string uri = $"https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/45ea4b09-2acf-46fc-a23e-62c59ce38901?subscription-key=9898c808f2b74e0d8267ae2c39d818d3&verbose=true&q={escapedString}";
                  var msg = await client.GetAsync(uri);
                if (msg.IsSuccessStatusCode)
                {
                    var jsonresponse = await msg.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<LuisResponse>(jsonresponse);
                    return data;
                }         
            }
            return null;
        
        }

        //public static async Task<LuisData> ParseUserInp(string phrase)
        //{

        //    string returnvalue = string.Empty;
        //    string escapedString = Uri.EscapeDataString(phrase);
        //    using (var client = new HttpClient())
        //    {
        //        string uri = $"https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/219f7332-5d5c-4077-a307-81a8089fe1a2?subscription-key=885312f9045e4f0d8043cb4368279f65&verbose=true&q={escapedString}"; 
        //        var msg = await client.GetAsync(uri);
        //        if (msg.IsSuccessStatusCode)
        //        {
        //            var jsonresponse = await msg.Content.ReadAsStringAsync();
        //            var data = JsonConvert.DeserializeObject<LuisData>(jsonresponse);
        //            return data;
        //        }
        //    }
        //    return null;

        //}


    }
}