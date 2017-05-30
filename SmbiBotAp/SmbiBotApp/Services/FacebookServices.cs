using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SmbiBotApp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook;
using System.Web.Configuration;
using System.Configuration;
using System.Collections;

namespace SmbiBotApp.Services
{
    public class FacebookServices
    {
        public async Task<dynamic> GetUser(string token)
        {
            var client = new FacebookClient();
            dynamic user = await client.GetTaskAsync("/me",
                new
                {
                    fields = "id,first_name,last_name,birthday,gender,location,email",
                    access_token = token
                });

            object val = user.GetType().GetProperty("Values").GetValue(user, null);
            string[] arr = ((IEnumerable)val).Cast<object>().Select(x => x.ToString()).ToArray();
            return arr;
        }

        public async Task<dynamic> GetToken()
        {
            var client = new FacebookClient();
            //    client.Get();
            client.AppId = "102667733606511";
            client.AppSecret = "1d81ecb0410c847acc39583d3d542643";


            dynamic user = await client.GetTaskAsync("/197349787419043",
                new
                {
                    fields = "access_token"
                    //  access_token = token
                });

            object val = user.GetType().GetProperty("Values").GetValue(user, null);
            string[] arr = ((IEnumerable)val).Cast<object>().Select(x => x.ToString()).ToArray();
            return arr;
        }


        public async Task<FacebookProfile> GetFacebookProfileAsync()
        {
            //  string escapedString = Uri.EscapeDataString(accessToken);
            string app_id = "102667733606511";
            string app_secret = "1d81ecb0410c847acc39583d3d542643";
            string client_Id = "1323033824440894";
            //  string uri =$"https://graph.facebook.com/endpoint?key=value&amp;access_token=102667733606511|1d81ecb0410c847acc39583d3d542643";
            //  string uri = string.Format("https://graph.facebook.com/{0}/{1}/{2}/{3}/name,gender",client_Id,app_id,app_secret,accessToken);

            //   string uri = string.Format("https://graph.facebook.com/v2.8/me/?fields=name,gender,&client_id={0}&client_secret={1}",app_id , app_secret);

            //  string uri =$"https://graph.facebook.com/oauth/access_token?&client_id={app_id}&client_secret={app_secret}&fb_exchange_token={accessToken}";
            //    string uri = $"https://graph.facebook.com/v2.8/197349787419043/?fields=access_token";
            //            string uri = "https://graph.facebook.com/endpoint?key=value&amp;access_token={0}" + accessToken;

            using (var client = new HttpClient())
            {

                string uri = "https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id=102667733606511&client_secret=1d81ecb0410c847acc39583d3d542643&fb_exchange_token=EAABdYDHKoG8BALEIlVUzXsZBF2fgdlE8twrYKnVQllYVds8j2VZCTlJm9hCOvKP5YHI03h31l3CF9BN1mmUk4UtQUUtEwTg8ZBGA1mykLGHrnb0bEThhIZC9FGhA5bLHZBfVNZBYpuWA5vKR6W0I7EVhGJc3h2sF78geyzl4J0ZBgZDZD";

                var msg = await client.GetAsync(uri);
                if (msg.IsSuccessStatusCode)
                {
                    var jsonResponse = await msg.Content.ReadAsStringAsync();
                    var profileData = JsonConvert.DeserializeObject<FacebookProfile>(jsonResponse);

                    return profileData;
                }
                return null;
            }

        }
    }
}
