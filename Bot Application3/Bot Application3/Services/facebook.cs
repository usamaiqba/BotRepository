using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook;
using System.Web.Configuration;
using System.Configuration;
using System.Net.Http;
using System.Dynamic;
using System.Threading.Tasks;

namespace Bot_Application3.Services
{
    public class facebook:System.Web.UI.Page
    {
        string app_id = "102667733606511";
        string app_secret = "1d81ecb0410c847acc39583d3d542643";
       // string scope = "publish_stream,manage_pages";

        public void getdatafromfb()
        {
            var settings = ConfigurationManager.GetSection("facebookSettings");
            if (settings != null)
            {
                FacebookClient fc = new FacebookClient();
                //fc.Get();
              
                //.getLoginStatus(function(response) {
                //    if (response.status === 'connected')
                //    {
                //        var accessToken = response.authResponse.accessToken;
                //    }
                //} );

                //  var current = settings as MySettings;
                //  apiKey = current.ApiKey;
            }

            // ViewData["ApiKey"] = ConfigurationManager.AppSettings["FBAppSecret"];

            string code = Request.QueryString["code"];
            string appId = WebConfigurationManager.AppSettings["FBAppID"];
            string appSecret = WebConfigurationManager.AppSettings["FBAppSecret"];

            if (code == "" || code == null)
            {
                //request for authentication
                Response.Redirect("https://graph.facebook.com/oauth/authorize?client_id=" + appId + "&redirect_uri=http://localhost:3979/");
            }
            else
            {
                FacebookClient fb = new FacebookClient();
              
              //  code = Request.QueryString["code"].ToString();
                dynamic result = fb.Get("oauth/access_token", new
                {
                    client_id = fb.AppId,
                    client_secret = fb.AppSecret,
                    grant_type = "fb_exchange_token",
                //    fb_exchange_token = accessToken
                });

                //fb = new MyFB();
                //fb.ApplicationSecret = appSecret;
                //fb.ApplicationID = appId;
                //  string accessToken = fb.GetAccessToken(code);
                //fb.AccessToken = accessToken;

                //ViewData["MyName"] = fb.GetMyName();
            }


        }

        //private WebRequest GetWebRequest(Uri url)
        //{

        //    WebRequest webRequest = WebRequest.Create(url);
        //    webRequest.Proxy = HttpWebRequest.DefaultWebProxy;
        //    webRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
        //    return webRequest;
        //}

        //internal string BaseUrl
        //{ get
        //    {
        //        return ConfigurationManager.AppSettings["GraphBaseUrl"].ToString();
        //    }
        //}

        //string appId = ConfigurationManager.AppSettings["AppId"];
        //string appSecret = ConfigurationManager.AppSettings["AppSecret"];
        //string redirectUrl = ConfigurationManager.AppSettings["RedirectUrl"];
        //string scope = ConfigurationManager.AppSettings["scope"];

        //public void getInfo()
        //{
        //    var facebookCookie = HttpContext.Current.Request.Cookies["fbsr_" + appId];
        //    if (Request.QueryString["code"] != null)
        //    {
        //        Response.Redirect(string.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}", appId, redirectUrl, appSecret, Request.QueryString["code"]));
        //    }
        //    else
        //    {
        //        Response.Redirect(string.Format("https://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}&scope={2}", appId, redirectUrl, scope));
        //    }

        //}

        //public override async Task ProcessRequestAsync(HttpContext context)
        //{
        //    WebClient wc = new WebClient();
        //    var result = await wc.DownloadStringTaskAsync("http://www.microsoft.com");
        //    // Do something with the result
        //}



        //public  async Task<string>GetFacebookProfileName(string accessToken)
        //{
        //    var uri = GetUri("https://graph.facebook.com/v2.8/me",
        //        Tuple.Create("fields", "id,name"),
        //        Tuple.Create("access_token", accessToken));

        //    var res = await FacebookRequest<FacebookProfile>(uri);
        //    return res.Name;
        //}

        private static Uri GetUri(string endPoint, params Tuple<string, string>[] queryParams)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            foreach (var queryparam in queryParams)
            {
                queryString[queryparam.Item1] = queryparam.Item2;
            }

            var builder = new UriBuilder(endPoint);
            builder.Query = queryString.ToString();
            return builder.Uri;
        }

        public void GetDataFromFB()
        {

             string access_token = "EAABdYDHKoG8BAB6QYjTIGrmqkjWxtkopv7ZAueiEgSFLGB5msm9EPBr9UJGDEw5KEe0xVX0BWPLPngL9JTRfFe9xRIzC4BX8DAYJxrq6ShuzX67TmaglBqFNA6douMSH67R8QyZAxfSWDda34yj1nzDkfX50xoY7c4aMBs11Q5ZCc4MA3M52ux3M1Q7ppUZD";
          //  string access_token = "EAACEdEose0cBANsfMZCvZAEwzZAvwpU6g0qJZBU0EjH3YBcAYrZBZAYa4cXRs1e5tDnDg2tZAitGd1GxkBTXXulrSM5LUtKaa92TQVKiGpI5cPS4d0AeGs4AeRmsgx13ZAZBFzyLO82fQ7QBZBiWjob4QLMq6RkFqoU79wiLxb2mnp61uMCa0y5avzvKR1dhxgHBcZD";
            var fb = new FacebookClient();
            fb.AppId = app_id;
            fb.AppSecret = app_secret;
            fb.AccessToken = access_token;

               WebClient wc = new WebClient();
               var res =  wc.DownloadStringTaskAsync("https://graph.facebook.com/v2.8/me/?fields=name,&client_id={client_Id},&app_id={app_Id},&app_secret={app_Secret},&access_token={accessToken}");

            dynamic parameters = new ExpandoObject();
         
            parameters.method = "users.getInfo";
            parameters.uids = "1323033824440894";      
            parameters.fields = new[] { "name", "first_name", "last_name" };
            dynamic result = fb.Post(1323033824440894);

            //var oauthClient = new FacebookOAuthClient
            //{
            //    AppId = "app_id",
            //    AppSecret = "app_secret"
            //};
            //var fb = new FacebookClient();
            //var resu = (IDictionary<string, object>)fb.Get("4");


            //dynamic result = oauthClient.GetApplicationAccessToken();
            //string appAccessToken = result.access_token;
            //var fb = new FacebookClient();
            //dynamic result = fb.Get("4");
            //var name = result.name;
            //Response.Redirect(string.Format("https://graph.facebook.com/oauth/authorize?client_id={0},&redirect_uri={1}", app_id, "https://facebook.botframework.com/api/v1/bots/SmbiBotApp20170320112332"));

            //if (Request["code"] == null)
            //{
            //    Response.Redirect(string.Format("https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}", app_id, Request.Url.Query, scope));
            //}
            //else
            //{
            //    Dictionary<string, string> tokens = new Dictionary<string, string>();

            //    string uri = string.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&scope={2}&code={3},&client_secret={4}", app_id, Request.Url.AbsoluteUri, scope, Request["code"].ToString(), app_secret);
            //    HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            //    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            //    {
            //        StreamReader reader = new StreamReader(response.GetResponseStream());
            //        string val = reader.ReadToEnd();
            //        foreach (var token in val.Split('&'))
            //        {
            //            tokens.Add(token.Substring(0, token.IndexOf("=")), token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
            //        }
            //    }
            //    //string access_token = tokens["access_token"];
            //    //var client = new FacebookClient(access_token);


            //}
        }

    }
}

