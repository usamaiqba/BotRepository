using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using SmbiBotApp.Services;
using Microsoft.Bot.Builder.Luis.Models;
using SmbiBotApp.Model;
using Microsoft.Bot.Builder.Dialogs.Internals;
using System.Text;
using Microsoft.Bot.Builder.Dialogs;
using System.Reflection;
using Autofac;
using System.Collections.Generic;
using System.Windows.Input;
using System.Web.UI.WebControls;
using Facebook;
using System.Threading;
using Humanizer;
using System.Xml.Serialization;
using System.Web.Script.Serialization;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Xml;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json.Linq;


namespace SmbiBotApp
{

    [Serializable]
    //public class DisplayDialog : IDialog<object>
    //{
    //    private ResumeAfter<bool> play;

    //    public  async Task StartAsync(IDialogContext context)
    //    {
    //       await context.PostAsync("Are you sure you want to save");
    //       // PromptDialog.Confirm(context,play,"create the profile");
    //      //  await context.Wait(SendAsync);
    //          context.Wait(MessageReceivedAsync);       
    //    }

    //    public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
    //    {
    //        await context.PostAsync("Not correct. Guess again.");
    //        PromptDialog.Confirm(context,play,"Yes I did it");
            
    //        // context.Wait(MessageReceivedAsync);          
    //    }
    //}

    [BotAuthentication]
    public class MessagesController : ApiController
    {

        HttpClient client = new HttpClient();//1
        public static string userid;//2
        public static readonly Uri FacebookOauthCallback = new Uri("http://localhost:3979/api/OAuthCallback");//3
        public List<string> col = new List<string>();
        public static List<string> data = new List<string>();
        public static List<string> avoid = new List<string>();
        static string[] profile = new string[7];
        static int count = 0;
        static int flag = 0;
        static int counter = 1;
        static int callback = 0;
        static int des_pro = 0;
        static string id = null;
        static string occupation = null;
        static string entity = null;
        static int pro_count = 1;
        static bool wait = true;
        static int ques_count = 0;
        static int score = 0;
        static int flagi = 0;
        public static int repeat = 1;
        List<string> ques = new List<string>();
        List<string> options = new List<string>();
        public static Testing testsretrieved = null;
        public static Record[] display = null;


        public enum decision
        {
            firstName = 1,
            lastName = 2,
            myAge = 3,
            Gender = 4,
            currentAddress = 5,
            emailAddress = 6,
            university = 7,
            graduate = 8,
            degree = 9,
            projects = 10,
            title = 11,
            description = 12,
            viewProfile = 13,

        };
        private void asked()
        {
            col.Add("Before I get matching you to the perfect job. I'll have to first setup your profile.");
            col.Add("This is the information we were able to pull from facebook could you confirm if you would like me to use this information ?");
            col.Add("What is your firstname ?");
            col.Add("What is your last name ?");
            col.Add("How old are you ?");
            col.Add("Are you a guy or a girl ?");
            col.Add("Where are you currently living (city)?");
            col.Add("what is your email address ?");
            col.Add("Now that we're acquainted I would like to learn a little about your education and skills.");
            col.Add("What university did you attend ?");//9
            col.Add("When did you graduate ?");//10
            col.Add("What did you study ?");//11
            col.Add("Is this the highest level of education you attainted ?");//12
            col.Add("What sort of company are you interested in for investment ?");//13
            col.Add("What sort of designer are you ?");//14
            col.Add("How many projects or products you have worked on ?");//15
            col.Add("what is the title of your");//16
            col.Add("Briefly describe your"); //17
            col.Add("Rank your experience / level of using design softwares:Sketch:Photoshop:Illustrator:indesign:");
            col.Add("What web development languages are you familiar with : (select all those that apply)");//19
            col.Add("Describe the project or role you were most proud of ? What sort did you do in your capcity and what were the outcomes / achievements ? ");
            col.Add("Is there any other project, product , or work experience. You would like to share with us ?");
            col.Add("what sort of marketer are you ?");//22
            col.Add("my first name is");//13//23
            col.Add("my last name is");//14//24
            col.Add("my age is");//15//25
            col.Add("my gender is");//16//26
            col.Add("I currently live in");//17//26//27
            col.Add("my email address is");//28
            col.Add("I attend");//29
            col.Add("I graduated in");//30
            col.Add("I study");//31
            col.Add("Are you done");//32
            col.Add("What sort of role are you looking for ?");//33
            col.Add("Your profile has been successfully created");//34
            col.Add(" I'm maaz a chabot developed by folks at PeopleHome to help you find the job right for you !");//35
            col.Add("Project that i have been done so far are");//36
            col.Add("The title of my project is");//37
            col.Add("The description of my project is");//38
            col.Add("That's good now your basic profile has been set.If you want to update your profile information let us know ?");//39
            col.Add("Now we are going to take some tests to evaluate you and to build your professional profile");//40
            col.Add("Which technology you are intertested ?");//41
            col.Add("Which type of test you would like to give ?"); //42
            col.Add("Now let start the test here is your first question");//43
            col.Add("Do you want to give any other test ?");//44
            col.Add("Ok thanks for visiting our bot and share your experience");//45
            col.Add("You have been already given that test");//46
            col.Add("Which information would you like to update ?");//47
            col.Add("What sort of developer are you ?");//48
            col.Add("Before we can start matching you with jobs related to fullstack developer.you will need to complete the following modules at your own pace");//49
            col.Add("You will be tested on the skills and tools that are utilized as a front,backend,fullstack developer");//50
            col.Add("Demonstrate your core knowledge needed to program,how to write and execute applications");//51
            col.Add("Show your understanding and ability to utilize the languages powering the frontend of the web(HTML5,JavaScript,CSS)");//52
            col.Add("You will be tested on your understanding of both databases and their architecture whatever the programming language,operating system or application type be"); // 53
            col.Add("Show your understanding of the most commom backend languages that power processing and databases(PHP,Python,SQL)");// 54
            col.Add("Demonstrate your understanding of the most important security concerns when developing websites and what can be done to keep servers,software and data safe from harm");//55
            col.Add("Show your understanding of the workflow that makes it easier to build websites,track share project files and leverage code libraries");//56
            col.Add("Demonstrate your knowledge of the frontend and backend framework libraries to unlock more oppertunities");//57
            col.Add("Demonstrate your ability to apply design principles to your site to make it behave in ways that users want and expect");//58 
            col.Add("Demonstrate your ability to integrate accessibility into the design process to make your projects more accessible to all people");//59
            col.Add("Demonstrate your understanding of the concepts behind responsive design, covering concepts of density, fluid grids, images, as well as design strategies that guide the project");//60
            col.Add("Show your understanding and ability to utilize the most common frontend frameworks and tools (Bootstrap 3, React.js, Sass)"); //61
            col.Add("Demonstrate your skills in basics of Object-Oriented programming from abstraction and inheritance to your ability to use cases and create a conceptual modesl for your application");//62
            col.Add("Explain your understanding of Java SE, in which, counting a few, fancy mobile apps, desktop, web applications are built");//63
            col.Add("Show how you cater with common Java challenges that occur during the development process.");//64
            col.Add("Demonstrate your knowledge of the 7 object oriented design patters (singleton, observer, decorator, factor) that emblish your development skills.");//65
            col.Add("Demonstrate your ability to use Java to its fullest through platform and framework  agonistic code");//66
            col.Add("Demonstrate how well can you read and manage data from relational databases such as MySQL and SQL Server using the Java Database Connectivity (JDBC) API in applications programmed with Java");//67
            col.Add("You will be tested on online marketing techniques specifically on how to build a successful online marketing campaign for all digital, channels: search, video, social, email, and display");//68
            col.Add("Prove your understanding of the foundational concepts of search engine optimization from keyword planning, content optimization, link building, for ecommerce, local search, and mobile audiences");//69
            col.Add("You will be tested on your knowledge of using google analytics to measure website performance, traffic, advert performance, and social media activity");//70
            col.Add("Demonstrate your ability in developing, implementing, and measuring a successful content marketing strategy");//71
            col.Add("Demonstrate your knowledge of ensuring marketing campaigns keep up with pace of mobile, having your website and emails optimized for mobile visitors, launching SMS campaigns, advertising on mobile, and much more");//72
            col.Add("You will be tested on your knowledge of starting a lead generation program and converting prospects into loyal customers");//73
            col.Add("Demonstrate your understanding of growth hacking techniques that can quickly expand your customer base using low cost methods");//74
            col.Add("You will be tested on your abilities to integrate all the moving parts – email, social, media, search into a successful message of your brand");//75
            col.Add("Demonstrate your understanding of basic sales skills used to develop professionally and build more successful relationships");//76
            col.Add("Have your listening skills and behavior evaluated");//77
            col.Add("Demonstrate your ability to interact with others by being assertive");//78
            col.Add("Demonstrate your ability to bounce back from difficult situations");//79
            col.Add("Demonstrate your understanding of the essential steps that underlie ever sales process and their implementation into a workflow");//80
            col.Add("Show your ability to ask sales questions that lead to high quality interactions with customers and clients");//81
            col.Add("Demonstrate your ability of managing the relationship between your brand, product/service, price");//82
            col.Add("Demonstrate your understanding of negotiating deals that stick");//83

            col.Add("Now start the questions");//62

            
            ques.Add("HTML is what type of language");
            ques.Add("HTML use");
            ques.Add("The year in which HTML was first proposed _______.");

            options.Add("Scripting Language,Markup Language,Programming Language,Network Protocol");
            options.Add("User defined tags, Pre - specified tags, Fixed tags defined by the language, Tags only for linking");
            options.Add("1990,1980,2001,990");



        }

        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {

            if (wait == true)
            {
                wait = false;
                asked();
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                await HandleSystemMessage(activity, connector);
                var response = Request.CreateResponse(HttpStatusCode.OK);
                wait = true;
                return response;

            }
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.OK);
                return response;
            }

        }

        private async Task<Activity> HandleSystemMessage(Activity activity, ConnectorClient connector)
        {
            try
            { 
            var replymesge = string.Empty;
            Activity reply = new Activity();
            reply = activity.CreateReply();
            if (activity.Type == ActivityTypes.Message)
            {
                var phrase = activity.Text;
                    // reply.Text = activity.From.Id;
                    // await connector.Conversations.ReplyToActivityAsync(reply);

                    //XmlDocument doc = new XmlDocument();
                    //doc.Load("C:\\git\\BotRepository\\Bot Application3\\files\\usama3.xml");
                    //string jsonText = JsonConvert.SerializeXmlNode(doc); //XML to Json
                    // //          // string json = JsonConvert.SerializeObject(jsonText);

                    // ////write string to file
                    // ////  System.IO.File.WriteAllText("C:\\test\\path.json", jsonText);
                    //var bson = BsonSerializer.Deserialize<BsonDocument>(jsonText); //Deserialize JSON String to BSon Document

                    //var Client = new MongoClient("mongodb://rizwan:rizwan@ds127883.mlab.com:27883/talenthome");
                    //var MongoDB = Client.GetDatabase("talenthome");
                    //var Collec = MongoDB.GetCollection<BsonDocument>("questions");
                    //await Collec.InsertOneAsync(bson);
                    // ////var pro = new BsonDocument
                    ////  {
                    ////      {"totalquest" , new BsonArray().Add(bson) }
                    // // };
                    //await Collec.InsertOneAsync(bson);

                    //   //  var mcollection = Program._database.GetCollection<BsonDocument>("test_collection_05");
                    //   // await mcollection.InsertOneAsync(bsdocument); //Insert into mongoDB


                    //   // Insert new user object to collection
                    //   //users.Insert(user);
                    ////   LuisService.DoSomethingAsync(jsonText);
                    //   //LuisService.DoSomethingAsync();



                    var luisresp = await LuisService.ParseUserInput(phrase);

                    while (luisresp == null)
                    {
                        luisresp = await LuisService.ParseUserInput(phrase);
                    }


                back:

                if (luisresp.intents.Count() > 0)
                {
                    var str = luisresp.topScoringIntent;
                    try
                    {
                        var symb = string.Empty;
                    next:
                        switch (str.intent)
                        {
                            case "None":

                                var validation_result = new Tuple<bool, string>(true, "");//4
                              //  validation_result = input_validation(activity.Text, count);//5
                                    if (validation_result.Item1)//6
                                    {

                                        callback = 1;
                                        switch (count)
                                        {
                                            case 1:
                                                luisresp = await LuisService.ParseUserInput(col.ElementAt(23) + " " + luisresp.query);
                                                replymesge = luisresp.query;
                                                break;

                                            case 2:
                                                luisresp = await LuisService.ParseUserInput(col.ElementAt(24) + " " + luisresp.query);
                                                replymesge = luisresp.query;
                                                break;

                                            case 3:
                                                luisresp = await LuisService.ParseUserInput(col.ElementAt(25) + " " + luisresp.query);
                                                replymesge = luisresp.query;
                                                break;

                                            case 4:
                                                luisresp = await LuisService.ParseUserInput(col.ElementAt(26) + " " + luisresp.query);
                                                replymesge = luisresp.query;
                                                break;

                                            case 5:
                                                luisresp = await LuisService.ParseUserInput(col.ElementAt(27) + " " + luisresp.query);
                                                replymesge = luisresp.query;
                                                break;

                                            case 6:
                                                luisresp = await LuisService.ParseUserInput(col.ElementAt(28) + " " + luisresp.query);
                                                replymesge = luisresp.query;
                                                break;

                                            case 7:
                                                luisresp = await LuisService.ParseUserInput(col.ElementAt(29) + " " + luisresp.query);
                                                replymesge = luisresp.query;
                                                break;

                                            case 8:
                                                luisresp = await LuisService.ParseUserInput(col.ElementAt(30) + " " + luisresp.query);
                                                replymesge = luisresp.query;
                                                break;

                                            case 9:
                                                luisresp = await LuisService.ParseUserInput(col.ElementAt(31) + " " + luisresp.query);
                                                replymesge = luisresp.query;
                                                break;

                                            case 10:
                                                luisresp = await LuisService.ParseUserInput(col.ElementAt(36) + " " + luisresp.query);
                                                replymesge = luisresp.query;
                                                break;

                                            case 11:
                                                luisresp = await LuisService.ParseUserInput(col.ElementAt(37) + " " + luisresp.query);
                                                replymesge = luisresp.query;
                                                break;

                                            case 12:
                                                luisresp = await LuisService.ParseUserInput(col.ElementAt(38) + " " + luisresp.query);
                                                replymesge = luisresp.query;
                                                break;
                                        }
                                    }
                                    else//7
                                    {
                                        switch (count)
                                        {
                                            case 1:
                                            case 2:
                                            case 4:
                                            case 5:
                                            case 7:
                                            case 9:
                                                replymesge = col.ElementAt(48) + " using alphabets only..." + "\n" + validation_result.Item2;
                                                break;
                                            case 3:
                                            case 8:
                                            case 10:
                                                replymesge = col.ElementAt(48) + " using numbers only..." + "\n" + validation_result.Item2;
                                                break;
                                            case 6:
                                                replymesge = col.ElementAt(48) + " e.g. example@smbi.ca " + "\n" + validation_result.Item2;
                                                break;

                                            default:
                                                replymesge = col.ElementAt(48) + "\n" + validation_result.Item2;
                                                break;
                                        }
                                    }
                                    break;

                            case "questions":
                                callback = 0;
                                replymesge = luisresp.query;
                                    if (luisresp.entities.Length == 0)
                                    {
                                        entity = luisresp.query;
                                    }
                                    else
                                    {
                                        entity = luisresp.entities[0].entity.ToLower();
                                    }   

                                   
                                if (!avoid.Contains(entity))
                                {
                                    repeat = 1;
                                    if (entity == "started")
                                    {
                                        avoid.Clear();
                                    }
                                    else
                                    {
                                        avoid.Add(entity);
                                    }
                                        if (entity.Substring(0, 4) == "mode")
                                        {
                                            entity = entity.Substring(0, 4);
                                        }
                                        else if (entity.Substring(0, 4) == "modu")
                                        {
                                            entity = entity.Substring(0, 6);
                                        }
                                        else if (entity.Substring(0, 4) == "deve")
                                        {
                                            entity = entity.Substring(0, 9);
                                        }
                                        else if (entity.Substring(0, 4) == "mark")
                                        {
                                            entity = entity.Substring(0, 8);
                                        }
                                        else if (avoid.Contains("thanks"))
                                        {
                                            avoid.Add("another");
                                        }
                                        else if (avoid.Contains("test"))
                                        {
                                            avoid.Remove("test");
                                        }
                                        else if (avoid.Contains("another"))
                                        {
                                            int i = 0;
                                            while (avoid.Contains("mode" + i))
                                            {
                                                avoid.Remove("mode" + i);
                                                i++;
                                            }

                                        }
                                                   
                                    switch (entity)
                                    {
                                        case "started":
                                                //var conversationReference = Microsoft.Bot.Builder.ConnectorEx.Extensions.ToConversationReference(activity);
                                                //var fbLoginUrl = FacebookHelpers.GetFacebookLoginURL(conversationReference, FacebookOauthCallback.ToString());
                                                //reply.Text = "Please login in using this card";
                                                //reply.Attachments = new List<Attachment>();
                                                //List<CardAction> cardbuttons = new List<CardAction>();
                                                //CardAction button = new CardAction()
                                                //{
                                                //    Title = "Login",
                                                //    Type = "openUrl",

                                                //    Value = fbLoginUrl,

                                                //};
                                                ////.Attachments.Add(SigninCard.Create("You need to authorize me",
                                                ////                                       "Login to Facebook!",
                                                ////                                      fbLoginUrl
                                                ////                                     ).ToAttachment());
                                                //cardbuttons.Add(button);
                                                //SigninCard signin = new SigninCard()
                                                //{
                                                //    Buttons = cardbuttons,
                                                //};
                                                //Attachment jobattachment = signin.ToAttachment();
                                                //reply.Attachments.Add(jobattachment);
                                                //reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                                                //await connector.Conversations.ReplyToActivityAsync(reply);
                                                //while (userid == null)
                                                //{ }
                                            des_pro = 0;
                                            data.Clear();
                                            avoid.Clear();
                                            count = 1;
                                            counter = 1;
                                            pro_count = 1;
                                            reply.Text = "Bot is retrieving information" + "\n\n";
                                            await connector.Conversations.ReplyToActivityAsync(reply);
                                            FacebookServices fbc = new FacebookServices();
                                            profile = await fbc.GetUser("EAABdYDHKoG8BAJ0FkrKPTfCoJmHSEIyVkmLn6iXTPIxU8KRXIZCx5sQEJMSD0APTBz3vQI3CXalw0ZCPKZAyjZBbcjKeZB75arA2ZC1F7Jczfx0bKqKzDBRpZA1eFGJZAvsJvsx1zLA51JBw2vNhuJhDkonMp68zG6oZD");

                                            if (profile != null)
                                            {
                                                //data.Clear();
                                                //avoid.Clear();
                                                //count = 1;
                                                //counter = 1;
                                                //pro_count = 1;    
                                                id = profile[0];
                                                data.Add(id);
                                                var result = MongoUser.exist_user(profile[0]);
                                                string location = profile[5];
                                                string[] loc = (location.Substring(location.IndexOf(location.Substring(32))).Split(','));
                                                profile[5] = loc[0];
                                                if (result != null)
                                                {
                                                    var res = JsonConvert.DeserializeObject<MongoData>(result.ToJson());
                                                    if (res.basic.status == 1)
                                                    {
                                                        des_pro = 1;
                                                        count = 6;
                                                        counter = 6;
                                                        str.intent = "emailAddress";
                                                        goto next;
                                                    }
                                                    else if (res.basic.status == 2)
                                                    {
                                                        des_pro = 1;
                                                        count = 10;
                                                        counter = 10;
                                                        str.intent = "projects";
                                                        goto next;
                                                    }
                                                    else if (res.basic.status == 3)
                                                    {
                                                        des_pro = 2;
                                                        count = 12;
                                                        counter = 12;
                                                        str.intent = "test";
                                                        goto next;
                                                    }
                                                    else if (res.basic.status == 4)
                                                    {
                                                        des_pro = 1;
                                                        count = 13;
                                                        counter = 13;
                                                        str.intent = "viewProfile";
                                                        goto next;

                                                    }
                                                }
                                                else
                                                {
                                                    reply.Text = "Welcome" + " " + profile[1] + " !" + col.ElementAt(35);
                                                    await connector.Conversations.ReplyToActivityAsync(reply);
                                                    Thread.Sleep(1000);
                                                    replymesge = col.ElementAt(33);
                                                    JobOptions(reply);
                                                }
                                            }

                                            break;
                                        case "position":
                                            JobOptions(reply);
                                            break;
                                        case "profile":
                                            string[] occup = luisresp.query.Split();
                                            occupation = occup[0];
                                            reply.Text = col.ElementAt(0);
                                            await connector.Conversations.ReplyToActivityAsync(reply);
                                            Thread.Sleep(1000);
                                            reply.Text = "First Name:" + profile[1] + "\n\n" + "Last Name:" + profile[2] +
                                                         "\n\n" + "Gender:" + profile[4] + "\n\n" + "Location:" + profile[5] + "\n\n" + "Email:" + profile[6];
                                            await connector.Conversations.ReplyToActivityAsync(reply);
                                            Thread.Sleep(1100);
                                            replymesge = col.ElementAt(1);
                                            infoConfirm(reply);
                                            des_pro = 0;
                                            break;
                                        case "basic":
                                            des_pro = 1;
                                            count = 7;
                                            counter = 7;
                                            data.Clear();
                                            for (int i = 0; i <= 6; i++)
                                            {
                                                data.Add(profile[i]);
                                            }
                                            check_status(1);

                                            reply.Text = col.ElementAt(8);
                                            await connector.Conversations.ReplyToActivityAsync(reply);
                                            Thread.Sleep(2000);
                                            replymesge = col.ElementAt(9);

                                            break;
                                        case "details":
                                            des_pro = 1;
                                            replymesge = col.ElementAt(2);
                                            count = 1;
                                            break;
                                        case "company":
                                            replymesge = col.ElementAt(13);
                                            selectCompany(reply);
                                            break;

                                        case "interested":
                                            replymesge = refferedtochoice(id, reply);
                                            data.Add(luisresp.query.Substring(25));
                                            break;

                                        case "designer":
                                            string ski = luisresp.query.Substring(23);
                                            ski = string.Concat((ski).Split('/')).Replace(" ", "");
                                            var qury = MongoUser.get_skill_id(ski).ToList();
                                            var value = JObject.Parse(qury[1].Value.ToString())[ski].Value<string>();
                                            data.Add(value);
                                            replymesge = col.ElementAt(15);
                                            break;

                                        case "developer":
                                            replymesge = col.ElementAt(49);
                                            development_types(reply,luisresp);
                                            break;

                                        case "marketer":
                                             replymesge = col.ElementAt(49);
                                             marketing_types(reply, luisresp);
                                             break;

                                        case "module":
                                            //entity = luisresp.entities[0].entity.ToLower();
                                             replymesge = module_types(reply, luisresp, replymesge);
                                            // replymesge = check_module(entity, replymesge, reply);
                                             break;


                                       case "attend":
                                            count = 7;
                                            counter = 7;
                                            replymesge = col.ElementAt(9);
                                            break;

                                        case "update":
                                            replymesge = col.ElementAt(37);
                                            update_profile(reply);
                                            break;

                                        case "test":
                                            des_pro = 0;
                                            var usertest = JsonConvert.DeserializeObject<MongoData>(MongoUser.exist_user(id).ToJson());
                                            if (usertest.test != null)
                                            {
                                                var check = usertest.test.Where(x => x.technology.ToLower() == activity.Text.Substring(22).ToLower()).ToArray();
                                                if (check.Count() != 0)
                                                {
                                                    replymesge = col.ElementAt(46);
                                                    flagi = 0;
                                                }
                                                else
                                                {
                                                    data.Add(activity.Text.Substring(22).ToLower());
                                                    flagi = 2;
                                                }
                                            }
                                            else
                                            {
                                                data.Add(activity.Text.Substring(22).ToLower());
                                                flagi = 1;
                                            }
                                            if (flagi != 0)
                                            {

                                              //  reply.Text = col.ElementAt(43);
                                              //  await connector.Conversations.ReplyToActivityAsync(reply);
                                              //  Thread.Sleep(2000);

                                                testsretrieved = JsonConvert.DeserializeObject<Testing>(MongoUser.ret_sel_test(activity.Text.Substring(22).ToLower()).ToJson());
                                                display = (testsretrieved.record.Where(x => x.type.ToLower() == activity.Text.Substring(22).ToLower())).ToArray();
                                                replymesge = display[0].Statements;
                                                test(reply, display[0].Options, 0);

                                            }


                                            break;

                                        case "another":
                                            replymesge = col.ElementAt(41);
                                            choose_tech(reply);
                                            break;

                                        case "thanks":
                                            replymesge = col.ElementAt(45);
                                            commands(reply);
                                            break;


                                        case "data":
                                            var display_data = JsonConvert.DeserializeObject<MongoData>(MongoUser.exist_user(id).ToJson());

                                            replymesge = "First Name :" + " " + display_data.basic.first_name + "\n\n" + "Last Name :" + " " + display_data.basic.last_name + "\n\n" + "Age :" + " " + display_data.basic.age + "\n\n" +
                                                         "Gender :" + " " + display_data.basic.gender + "\n\n" + "Location :" + " " + display_data.basic.location + "\n\n" + "Email :" + " " + display_data.basic.email;

                                            foreach (var ed in display_data.educational)
                                            {
                                                replymesge = replymesge + "\n\n" +
                                                             "University :" + " " + ed.uni_name + "\n\n" +
                                                             "Passing Year :" + " " + ed.pass_year + "\n\n" +
                                                             "Degree :" + " " + ed.degree_name;
                                            }
                                            replymesge = replymesge + "\n\n" +
                                                         "Occupation Type :" + " " + display_data.professional.occupation_type + "\n\n" +
                                                         "Company Type :" + " " + display_data.professional.company_type + "\n\n" +
                                                         "Position Type :" + " " + display_data.professional.position_type;

                                            replymesge = replymesge + "\n\n" + "Total Projects :" + " " + display_data.project.no_of_projects;
                                            foreach (var dt in display_data.project.details)
                                            {
                                                replymesge = replymesge + "\n\n" +
                                                             "Title :" + " " + dt.title + "\n\n" +
                                                             "Description :" + " " + dt.description;
                                            }

                                            break;

                                        case "scores":
                                            var display_scores = JsonConvert.DeserializeObject<MongoData>(MongoUser.exist_user(id).ToJson());
                                            foreach (var sc in display_scores.test)
                                            {
                                                replymesge = "Technology :" + " " + sc.technology + "\n\n" +
                                                             "Score :" + " " + sc.score;
                                            }
                                            break;


                                        case "mode":

                                            if (ques_count <= display.Count())
                                            {
                                                var ans = activity.Text.Substring(0, 1);
                                                if (display[ques_count].Answers == ans)
                                                {
                                                    score++;
                                                }
                                                ++ques_count;
                                                if (ques_count < display.Count())
                                                {
                                                    replymesge = display[ques_count].Statements;
                                                    test(reply, display[ques_count].Options, ques_count);
                                                    Thread.Sleep(1100);
                                                }
                                                else
                                                {
                                                    data.Add(score.ToString());
                                                    reply.Text = "You have completed the" + data[1] + "track.Your results are as follows";
                                                    await connector.Conversations.ReplyToActivityAsync(reply);
                                                    reply.Text = "Technology:" + " " + data[1] + "\n\n" + "Score:" + " " + score;
                                                    await connector.Conversations.ReplyToActivityAsync(reply);
                                                    Thread.Sleep(1000);
                                                    save_user_test(flagi);
                                                    replymesge = col.ElementAt(44);
                                                    test_again(reply);

                                                }
                                            }

                                            break;
                                    }
                                }
                                else
                                {
                                    repeat = 0;
                                    avoid.Remove(avoid.Last());
                                }
                                break;

                            default:
                                callback = 0;
                                switch (counter)
                                {
                                    case 1:
                                        check_entity(symb, luisresp);
                                        replymesge = correctSequence(str.intent, replymesge, 3);
                                        break;

                                    case 2:
                                        check_entity(symb, luisresp);
                                        replymesge = correctSequence(str.intent, replymesge, 4);
                                        break;

                                    case 3:
                                        check_entity(symb, luisresp);
                                        replymesge = correctSequence(str.intent, replymesge, 5);
                                        break;
                                    case 4:
                                        check_entity(symb, luisresp);
                                        replymesge = correctSequence(str.intent, replymesge, 6);
                                        break;

                                    case 5:
                                        check_entity(symb, luisresp);
                                        replymesge = correctSequence(str.intent, replymesge, 7);
                                        break;

                                    case 6:

                                        if (counter == 6 && data.Count == 6)
                                        {
                                            check_entity(symb, luisresp);
                                            if (data.Count == 7)
                                            {
                                                check_status(0);
                                            }
                                        }
                                        reply.Text = correctSequence(str.intent, replymesge, 8);
                                        await connector.Conversations.ReplyToActivityAsync(reply);
                                        Thread.Sleep(2000);
                                        replymesge = col.ElementAt(9);
                                        break;

                                    case 7:
                                        check_entity(symb, luisresp);
                                        replymesge = correctSequence(str.intent, replymesge, 10);
                                        break;

                                    case 8:
                                        check_entity(symb, luisresp);
                                        replymesge = correctSequence(str.intent, replymesge, 11);
                                        break;

                                    case 9:
                                        check_entity(symb, luisresp);
                                        replymesge = correctSequence(str.intent, replymesge, 12);
                                        yesorno(reply);
                                        break;

                                    case 10:
                                        if (counter == 10 && data.Count >= 6)
                                        {
                                            check_entity(symb, luisresp);
                                            edu_pro_record();

                                        }

                                        var number = JsonConvert.DeserializeObject<MongoData>(MongoUser.exist_user(id).ToJson());
                                            if (number.project == null)
                                            {
                                                replymesge = "sorry wrong input";
                                            }
                                            else
                                            {

                                                if (pro_count > 0 && pro_count <= number.project.no_of_projects)
                                                {
                                                    if (pro_count > 1)
                                                    {
                                                        data.Add(luisresp.query.Substring(41));
                                                    }

                                                    replymesge = correctSequence("projects", replymesge, 16) + " " + pro_count.ToOrdinalWords() + " project ?";

                                                }
                                                else
                                                {
                                                    data.Add(luisresp.query.Substring(41));
                                                    pro_details();
                                                    reply.Text = col.ElementAt(39);
                                                    await connector.Conversations.ReplyToActivityAsync(reply);
                                                    Thread.Sleep(2000);
                                                    reply.Text = col.ElementAt(40);
                                                    await connector.Conversations.ReplyToActivityAsync(reply);
                                                    Thread.Sleep(2000);
                                                    replymesge = col.ElementAt(41);
                                                    choose_tech(reply);
                                                    count = count + 2;
                                                    counter = counter + 2;
                                                }
                                            }
                                        break;

                                    case 11:
                                        var numb = JsonConvert.DeserializeObject<MongoData>(MongoUser.exist_user(id).ToJson());
                                            if (numb.project == null)
                                            {
                                                replymesge = "sorry wrong input";
                                            }
                                            else
                                            {
                                                if (pro_count > 0 && pro_count <= numb.project.no_of_projects)
                                                {

                                                    if (pro_count == 1)
                                                    {
                                                        data.Add(luisresp.query);
                                                    }
                                                    else
                                                    {
                                                        data.Add(luisresp.query.Substring(27));
                                                    }

                                                    replymesge = correctSequence("title", replymesge, 17) + " " + pro_count.ToOrdinalWords() + " project";
                                                    pro_count++;
                                                    count = count - 2;
                                                    counter = counter - 2;
                                                }
                                                else
                                                {

                                                }
                                            }
                                        break;

                                    case 12:

                                            if (des_pro == 2)
                                            {
                                                des_pro = 0;
                                                replymesge = col.ElementAt(41);
                                                choose_tech(reply);
                                            }
                                            else
                                            {
                                                replymesge = "sorry wrong input";
                                            }

                                        break;

                                    case 13:
                                        replymesge = col.ElementAt(45);
                                        commands(reply);
                                        break;

                                }
                                break;

                        }
                        if (callback == 1)
                        {
                            callback = 0;
                            goto back;
                        }
                        if (symb != string.Empty)
                        {
                            data.Add(symb);
                        }

                        phrase.ToLower();
                        if (flag == 1 && (phrase == "yes" || phrase == "y"))
                        {
                            if (data.Count == 4)
                            {
                            }
                        }
                    }
                    catch (FacebookApiException ex)
                    {
                        replymesge = ex.Message.ToString();
                    }
                    if (repeat == 1)
                    {
                        reply.Text = replymesge;
                        await connector.Conversations.ReplyToActivityAsync(reply);
                    }

                }

                else
                {
                    replymesge = $"sorry.I could not get as to what you say";
                }
            }

            else if (activity.Type == ActivityTypes.DeleteUserData)
            {
                avoid.Clear();
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (activity.Type == ActivityTypes.ConversationUpdate)
            {
                count = 1;
                counter = 1;
                data.Clear();
                avoid.Clear();
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (activity.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (activity.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (activity.Type == ActivityTypes.Ping)
            {
            }
        }
        catch
        {
        }
            return null;
        }

       private async Task<Tuple<LuisResponse, string>> CallLuisAgain(LuisResponse luisresp, string reply, int y)
        {
            luisresp = await LuisService.ParseUserInput(col.ElementAt(y) + " " + luisresp.query);
            reply = luisresp.query;
            var tuple = new Tuple<LuisResponse, string>(luisresp, reply);
            return tuple;
        }

        private void check_entity(string symb, LuisResponse luisresp)
        {
            try
            {
                if (luisresp.entities.Length == 0)
                {
                    symb = luisresp.query;
                }
                else
                {
                    symb = luisresp.entities[0].entity.ToLower();
                }
           
                if (symb != string.Empty)
                {
                    data.Add(symb);
                }
            }
            catch  // handle entity if not found
            {


            }
        }

        private void save_user_test(int flagi)
        {
            int j = 1;
            var record = new BsonDocument
            {
                { "technology" , data.ElementAt(j++) },
                { "score"      , data.ElementAt(j++) },
            };

            MongoUser.upd_test_info(record, flagi, id);
            data.Clear();
            data.Add(id);
        }



        private void pro_details()
        {

            int cou = data.Count;//3 // 5// 7
            cou = (cou - 1) / 2; // 3 -1 = 2 // 5-1 = 4 // 7-1 = 6

            int j = 1;
            List<BsonDocument> multiple = new List<BsonDocument>();
            for (int i = 0; i < cou; i++)
            {
                var record = new BsonDocument
                        {
                           { "title"    , data.ElementAt(j++) },
                           { "description"   , data.ElementAt(j++) },
                        };

                multiple.Add(record);
            }

            MongoUser.upd_project_info(multiple, 3, id);
            data.Clear();
            data.Add(id);
        }



        private void edu_pro_record()
        {

            int cou = data.Count;//10 // 7// 6
            cou = cou - 3; // 10 -3 = 7 // 7-3 = 4 // 6-3 = 3
            cou = (cou - 1) / 3;

            int j = 1;
            List<BsonDocument> multiple = new List<BsonDocument>();
            for (int i = 0; i < cou; i++)
            {
                var record = new BsonDocument
                        {
                           { "uni_name"    , data.ElementAt(j++) },
                           { "pass_year"   , data.ElementAt(j++) },
                           { "degree_name" , data.ElementAt(j++) }
                        };

                multiple.Add(record);
            }
            var com = data.ElementAt(j++);
            var pos = data.ElementAt(j++);
            var proj = new BsonDocument
            {
                {"no_of_projects",int.Parse(data.ElementAt(j++)) }

            };
            MongoUser.add_edu_infos(multiple, id);
            MongoUser.upd_pro_info(com, pos, proj, 2, id);
            data.Clear();
            data.Add(id);
        }

        private bool check_status(int call)
        {
            if (id != null || occupation != null)
            {
                var Client = new MongoClient();
                var MongoDB = Client.GetDatabase("Botdatabase");
                var Collec = MongoDB.GetCollection<BsonDocument>("users");

                var info = new BsonDocument
                {
                    {"_id",data.ElementAt(0)},
                    {"basic",new BsonDocument
                     {
                        { "first_name" , data.ElementAt(1) },
                        { "last_name"  , data.ElementAt(2) },
                        { "age"        , "25" },//Convert.ToInt32(data.ElementAt(3)),
                        { "gender"     , data.ElementAt(4) },
                        { "location"   , data.ElementAt(5) },
                        { "email"      , data.ElementAt(6) },
                        { "status"     , 1               }
                     }
                   },
                    {"professional" , new BsonDocument
                     {
                        { "occupation_type" , occupation}
                     }
                    }
                };
                var chek = MongoUser.add_basic_info(info);
                occupation = null;
                flag = 0;
                data.Clear();
                data.Add(id);

                               
            }
            return false;
        }

        private string correctSequence(string intent, string reply, int x)
        {
            if (des_pro == 1)
            {

                if (Enum.GetName(typeof(decision), counter) == intent)
                {
                    reply = col.ElementAt(x);
                    count++;
                    counter++;
                }
                else
                {
                    reply = "Sorry wrong input" + "\n\n" + col.ElementAt(x - 1);
                }
            }
            else
            {
                reply = "Sorry wrong input";
                data.Remove(data.Last());

            }
            return reply;
        }

        
        protected void infoConfirm(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "Than save the user's basic information ?",
                Type = "postBack",
                Title = "Correct"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "Let's get to fixing your details. What's your first name ?",
                Type = "postBack",
                Title = "Incorrect",

            };

            cardButtons.Add(Button1);
            cardButtons.Add(Button2);

            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void test_again(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "You want to give another language test",
                Type = "postBack",
                Title = "Yes"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "OK Thanks for visiting our bot",
                Type = "postBack",
                Title = "No",
            };

            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void yesorno(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "What sort of company are you interested in working for:",
                Type = "postBack",
                Title = "Yes"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "What university did you attend ?",
                Type = "postBack",
                Title = "No",
            };

            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void commands(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "your stored data has been displayed",
                Type = "postBack",
                Title = "View Profile"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "your stored scores has been displayed",
                Type = "postBack",
                Title = "View Test Scores",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "What university did you attend ?",
                Type = "postBack",
                Title = "Help",
            };

            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void update_profile(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "ok you want to update your basic details",
                Type = "postBack",
                Title = "Basic Info"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "ok you want to update your educational information",
                Type = "postBack",
                Title = "Educational Info",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "ok you want to update your professional information",
                Type = "postBack",
                Title = "Professional Info",
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void select_design(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "ok you are designer of UX / UI",
                Type = "postBack",
                Title = "UX / UI Designer"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "ok you are designer of Graphic / Web",
                Type = "postBack",
                Title = "Graphic Designer",
            };
            //CardAction Button3 = new CardAction()
            //{
            //    Value = "ok you are designer of Full Stack",
            //    Type = "postBack",
            //    Title = "Full Stack",
            //};
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
          //  cardButtons.Add(Button3);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void select_graphic(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "ok you are designer of UX / UI",
                Type = "postBack",
                Title = "Layout and Composition"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "ok you are designer of Graphic / Web",
                Type = "postBack",
                Title = "Typography",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "ok you are designer of Full Stack",
                Type = "postBack",
                Title = "Logo Design",
            };
            CardAction Button4 = new CardAction()
            {
                Value = "ok you are designer of Full Stack",
                Type = "postBack",
                Title = "Design Tools",
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            cardButtons.Add(Button4);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void select_development(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "ok you are developeroffullstack",
                Type = "postBack",
                Title = "Full Stack Web Developer"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "ok you are developeroffrontend",
                Type = "postBack",
                Title = "Front End Web Developer",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "ok you are developerofapplication",
                Type = "postBack",
                Title = "Application Developer",
            };
            CardAction Button4 = new CardAction()
            {
                Value = "ok you are developerofmobile",
                Type = "postBack",
                Title = "Mobile Developer",
            };
            CardAction Button5 = new CardAction()
            {
                Value = "ok you are developerofcloud",
                Type = "postBack",
                Title = "Cloud Developer",
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            cardButtons.Add(Button4);
            cardButtons.Add(Button5);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void select_marketing(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "ok you are marketerofdigital",
                Type = "postBack",
                Title = "Digital Marketer"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "ok you are marketerofsales",
                Type = "postBack",
                Title = "Sales Representative",
            };

            cardButtons.Add(Button1);
            cardButtons.Add(Button2);

            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void development_types(Activity reply, LuisResponse luis)
        {
            var enty = luis.entities[0].entity.ToLower();
            enty = enty.Substring(11);
            switch (enty)
            {
                case "fullstack":
                    fullstack_developer(reply,luis);
                    break;
                case "frontend":
                    frontend_developer(reply, luis);
                    break;
                case "application":
                    application_developer(reply, luis);
                    break;
                case "mobile":
                    mobile_developer(reply, luis);
                    break;
                case "cloud":
                    cloud_developer(reply, luis);
                    break;

            }

        }

        private void marketing_types(Activity reply, LuisResponse luis)
        {
            var enty = luis.entities[0].entity.ToLower();
            enty = enty.Substring(10);
            switch (enty)
            {
                case "digital":
                    digital_marketer(reply, luis);
                    break;
                case "sales":
                    sales_representator(reply, luis);
                    break;
            }

        }

        private string module_types(Activity reply, LuisResponse luis, string mesj)
        {

            var enty = luis.entities[0].entity.ToLower();
            enty = enty.Substring(9);
            switch (enty)
            {
                case "fullstack":
                    mesj = fullstack_module(reply, luis, mesj);
                    break;
                case "frontend":
                    mesj = frontend_module(reply, luis, mesj);
                    break;
                case "application":
                    mesj = application_module(reply, luis, mesj);
                    break;
                case "mobile":
                    mesj = mobile_module(reply, luis, mesj);
                    break;
                case "cloud":
                    mesj = cloud_module(reply, luis, mesj);
                    break;

            }

            return mesj;
        }

        private void fullstack_developer(Activity reply, LuisResponse luis)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "ok you want to complete module1offullstack",
                Type = "postBack",
                Title = "Web Development Foundations"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "ok you want to complete module2offullstack",
                Type = "postBack",
                Title = "Programing Fundamentals",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "ok you want to complete module3offullstack",
                Type = "postBack",
                Title = "Essential Frontend Languages",
            };
            CardAction Button4 = new CardAction()
            {
                Value = "ok you want to complete module4offullstack",
                Type = "postBack",
                Title = "Database Foundations",
            };
            CardAction Button5 = new CardAction()
            {
                Value = "ok you want to complete module5offullstack",
                Type = "postBack",
                Title = "Essential Backend Languages",
            };
            CardAction Button6 = new CardAction()
            {
                Value = "ok you want to complete module6offullstack",
                Type = "postBack",
                Title = "Security Foundations",
            };
            CardAction Button7 = new CardAction()
            {
                Value = "ok you want to complete module7offullstack",
                Type = "postBack",
                Title = "Web Projects Workflows",
            };
            CardAction Button8 = new CardAction()
            {
                Value = "ok you want to complete module8offullstack",
                Type = "postBack",
                Title = "Fullstack Frameworks",
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            cardButtons.Add(Button4);
            cardButtons.Add(Button5);
            cardButtons.Add(Button6);
            cardButtons.Add(Button7);
            cardButtons.Add(Button8);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void frontend_developer(Activity reply, LuisResponse luis)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "ok you want to complete module1",
                Type = "postBack",
                Title = "Web Development Foundations"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "ok you want to complete module2",
                Type = "postBack",
                Title = "Programing Fundamentals",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "ok you want to complete module3",
                Type = "postBack",
                Title = "User Experience for Web Designers",
            };
            CardAction Button4 = new CardAction()
            {
                Value = "ok you want to complete module4",
                Type = "postBack",
                Title = "UX Foundations Accessibility",
            };
            CardAction Button5 = new CardAction()
            {
                Value = "ok you want to complete module5",
                Type = "postBack",
                Title = "Responsive Design",
            };
            CardAction Button6 = new CardAction()
            {
                Value = "ok you want to complete module6",
                Type = "postBack",
                Title = "Framework & Tools",
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            cardButtons.Add(Button4);
            cardButtons.Add(Button5);
            cardButtons.Add(Button6);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void application_developer(Activity reply, LuisResponse luis)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "ok you want to complete module1ofjava",
                Type = "postBack",
                Title = "Java"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "ok you want to complete module2ofcsharp",
                Type = "postBack",
                Title = "C-Sharp",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "ok you want to complete module3ofpython",
                Type = "postBack",
                Title = "Python",
            };
    
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void mobile_developer(Activity reply, LuisResponse luis)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "ok you want to complete module1",
                Type = "postBack",
                Title = "Android"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "ok you want to complete module2",
                Type = "postBack",
                Title = "iOS",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "ok you want to complete module3",
                Type = "postBack",
                Title = "Cross-Platform",
            };
       
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            HeroCard jobCard = new HeroCard()
            {
               
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void cloud_developer(Activity reply, LuisResponse luis)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "ok you want to complete module1",
                Type = "postBack",
                Title = "Web Development Foundations"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "ok you want to complete module2",
                Type = "postBack",
                Title = "Programing Fundamentals",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "ok you want to complete module3",
                Type = "postBack",
                Title = "Essential Frontend Languages",
            };
            CardAction Button4 = new CardAction()
            {
                Value = "ok you want to complete module4",
                Type = "postBack",
                Title = "Database Foundations",
            };
            CardAction Button5 = new CardAction()
            {
                Value = "ok you want to complete module5",
                Type = "postBack",
                Title = "Essential Backend Languages",
            };
            CardAction Button6 = new CardAction()
            {
                Value = "ok you want to complete module6",
                Type = "postBack",
                Title = "Security Foundations",
            };
            CardAction Button7 = new CardAction()
            {
                Value = "ok you want to complete module7",
                Type = "postBack",
                Title = "Web Projects Workflows",
            };
            CardAction Button8 = new CardAction()
            {
                Value = "ok you want to complete module8",
                Type = "postBack",
                Title = "Fullstack Frameworks",
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            cardButtons.Add(Button4);
            cardButtons.Add(Button5);
            cardButtons.Add(Button6);
            cardButtons.Add(Button7);
            cardButtons.Add(Button8);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void digital_marketer(Activity reply, LuisResponse luis)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "you selected the test onlinemarketing",
                Type = "postBack",
                Title = "Online Marketing Foundations"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "you selected the test seofoundations",
                Type = "postBack",
                Title = "SEO Foundations",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "you selected the test googleanalytics",
                Type = "postBack",
                Title = "Google Analytics",
            };
            CardAction Button4 = new CardAction()
            {
                Value = "you selected the test contentmarketing",
                Type = "postBack",
                Title = "Content Marketing Fundamentals",
            };
            CardAction Button5 = new CardAction()
            {
                Value = "you selected the test mobilemarketing",
                Type = "postBack",
                Title = "Mobile Marketing Foundations",
            };
            CardAction Button6 = new CardAction()
            {
                Value = "you selected the test leadgeneration",
                Type = "postBack",
                Title = "Lead Generation Foundations",
            };
            CardAction Button7 = new CardAction()
            {
                Value = "you selected the test growthhacking",
                Type = "postBack",
                Title = "Growth Hacking",
            };
            CardAction Button8 = new CardAction()
            {
                Value = "you selected the test onlinemarketingplan",
                Type = "postBack",
                Title = "Online Marketing Plan",
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            cardButtons.Add(Button4);
            cardButtons.Add(Button5);
            cardButtons.Add(Button6);
            cardButtons.Add(Button7);
            cardButtons.Add(Button8);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void sales_representator(Activity reply, LuisResponse luis)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "you selected the test salescareer",
                Type = "postBack",
                Title = "Sales Career"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "you selected the test effectivelistening",
                Type = "postBack",
                Title = "Effective Listening",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "you selected the test assertiveness",
                Type = "postBack",
                Title = "Assertiveness",
            };
            CardAction Button4 = new CardAction()
            {
                Value = "you selected the test resilience",
                Type = "postBack",
                Title = "Resilience",
            };
            CardAction Button5 = new CardAction()
            {
                Value = "you selected the test salesfoundations",
                Type = "postBack",
                Title = "Sales Foundations",
            };
            CardAction Button6 = new CardAction()
            {
                Value = "you selected the test askingquestions",
                Type = "postBack",
                Title = "Asking Questions",
            };
            CardAction Button7 = new CardAction()
            {
                Value = "you selected the test creatingcustomervalue",
                Type = "postBack",
                Title = "Creating Customer Value",
            };
            CardAction Button8 = new CardAction()
            {
                Value = "you selected the test salesnegotiation",
                Type = "postBack",
                Title = "Sales Negotiation",
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            cardButtons.Add(Button4);
            cardButtons.Add(Button5);
            cardButtons.Add(Button6);
            cardButtons.Add(Button7);
            cardButtons.Add(Button8);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private string fullstack_module(Activity reply,LuisResponse luis,string mesj)
        {
           
            var ent = luis.entities[0].entity.ToLower();
            ent = ent.Substring(0,7);
            switch (ent)
            {
                case "module1":
                    mesj = col.ElementAt(50);
                    webdevelopment_test(reply);

                    break;

                case "module2":
                    mesj = col.ElementAt(51);

                    break;

                case "module3":
                    mesj = col.ElementAt(52);
                    frontend_test(reply);

                    break;

                case "module4":
                    mesj = col.ElementAt(53);
                    break;

                case "module5":
                    mesj = col.ElementAt(54);
                    backend_test(reply);
                    break;

                case "module6":
                    mesj = col.ElementAt(55);
                    break;

                case "module7":
                    mesj = col.ElementAt(56);
                    projects_test(reply);
                    break;

                case "module8":
                    mesj = col.ElementAt(57);
                    break;

            }
            return mesj;
        }

        private string frontend_module(Activity reply, LuisResponse luis, string mesj)
        {

            var ent = luis.entities[0].entity.ToLower();
            ent = ent.Substring(0, 7);
            switch (ent)
            {
                case "module1":
                    mesj = col.ElementAt(50);
                    webdevelopment_test(reply);

                    break;

                case "module2":
                    mesj = col.ElementAt(51);

                    break;

                case "module3":
                    mesj = col.ElementAt(58);

                    break;

                case "module4":
                    mesj = col.ElementAt(59);
                    break;

                case "module5":
                    mesj = col.ElementAt(60);
               
                    break;

                case "module6":
                    mesj = col.ElementAt(61);
                    frameworktools_test(reply);
                    break;
            }
            return mesj;
        }
            
        private string application_module(Activity reply, LuisResponse luis, string mesj)
        {

            var ent = luis.entities[0].entity.ToLower();
            ent = ent.Substring(0, 7);
            switch (ent)
            {
                case "module1":
                    mesj = col.ElementAt(50);
                    java_test(reply,luis);

                    break;

                case "module2":
                    mesj = col.ElementAt(51);
                    csharp_test(reply,luis);
                    break;

                case "module3":
                    mesj = col.ElementAt(52);
                    python_test(reply,luis);

                    break;

            }
            return mesj;
        }

        private string mobile_module(Activity reply, LuisResponse luis, string mesj)
        {

            var ent = luis.entities[0].entity.ToLower();
            ent = ent.Substring(0, 7);
            switch (ent)
            {
                case "module1":
                    mesj = col.ElementAt(50);
                    webdevelopment_test(reply);

                    break;

                case "module2":
                    mesj = col.ElementAt(51);

                    break;

                case "module3":
                    mesj = col.ElementAt(52);
                    frontend_test(reply);

                    break;

                case "module4":
                    mesj = col.ElementAt(53);
                    break;

                case "module5":
                    mesj = col.ElementAt(54);
                    backend_test(reply);
                    break;

                case "module6":
                    mesj = col.ElementAt(55);
                    break;

                case "module7":
                    mesj = col.ElementAt(56);
                    projects_test(reply);
                    break;

                case "module8":
                    mesj = col.ElementAt(57);
                    break;

            }
            return mesj;
        }

        private string cloud_module(Activity reply, LuisResponse luis, string mesj)
        {

            var ent = luis.entities[0].entity.ToLower();
            ent = ent.Substring(0, 7);
            switch (ent)
            {
                case "module1":
                    mesj = col.ElementAt(50);
                    webdevelopment_test(reply);

                    break;

                case "module2":
                    mesj = col.ElementAt(51);

                    break;

                case "module3":
                    mesj = col.ElementAt(52);
                    frontend_test(reply);

                    break;

                case "module4":
                    mesj = col.ElementAt(53);
                    break;

                case "module5":
                    mesj = col.ElementAt(54);
                    backend_test(reply);
                    break;

                case "module6":
                    mesj = col.ElementAt(55);
                    break;

                case "module7":
                    mesj = col.ElementAt(56);
                    projects_test(reply);
                    break;

                case "module8":
                    mesj = col.ElementAt(57);
                    break;

            }
            return mesj;
        }
        
        private void webdevelopment_test(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "you selected the test frontend",
                Type = "postBack",
                Title = "Frontend"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "you selected the test backend",
                Type = "postBack",
                Title = "Backend",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "you selected the test fullstack",
                Type = "postBack",
                Title = "Fullstack",
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void frontend_test(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "you selected the test html",
                Type = "postBack",
                Title = "HTML"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "you selected the test css",
                Type = "postBack",
                Title = "CSS",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "you selected the test javascript",
                Type = "postBack",
                Title = "Javascript",
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void backend_test(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "you selected the test php",
                Type = "postBack",
                Title = "PHP"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "you selected the test python",
                Type = "postBack",
                Title = "Python",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "you selected the test sql",
                Type = "postBack",
                Title = "SQL",
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void projects_test(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "you selected the test gulp.js",
                Type = "postBack",
                Title = "Gulp.js"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "you selected the test git",
                Type = "postBack",
                Title = "Git",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "you selected the test browserify",
                Type = "postBack",
                Title = "Browserify",
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void frameworktools_test(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "you selected the test bootstrap",
                Type = "postBack",
                Title = "Bootstrap 3"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "you selected the test react.js",
                Type = "postBack",
                Title = "React.js",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "you selected the test sass",
                Type = "postBack",
                Title = "Sass",
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void java_test(Activity reply, LuisResponse luis)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "you selected the test objectorienteddesign",
                Type = "postBack",
                Title = "Object-Oriented Design"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "you selected the test javaessentialtraining",
                Type = "postBack",
                Title = "Java & Essential Training",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "you selected the test codeclinicjava",
                Type = "postBack",
                Title = "Code Clinic:Java",
            };
            CardAction Button4 = new CardAction()
            {
                Value = "you selected the test designpatterns",
                Type = "postBack",
                Title = "Design Patterns",
            };
            CardAction Button5 = new CardAction()
            {
                Value = "you selected the test advancedjavaprogramming",
                Type = "postBack",
                Title = "Advanced Java Programming",
            };
            CardAction Button6 = new CardAction()
            {
                Value = "you selected the test javadatabaseintegration",
                Type = "postBack",
                Title = "Java:Database Integration JDBC",
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            cardButtons.Add(Button4);
            cardButtons.Add(Button5);
            cardButtons.Add(Button6);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void csharp_test(Activity reply, LuisResponse luis)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "ok you want to complete module1offullstack",
                Type = "postBack",
                Title = "Web Development Foundations"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "ok you want to complete module2offullstack",
                Type = "postBack",
                Title = "Programing Fundamentals",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "ok you want to complete module3offullstack",
                Type = "postBack",
                Title = "Essential Frontend Languages",
            };
            CardAction Button4 = new CardAction()
            {
                Value = "ok you want to complete module4offullstack",
                Type = "postBack",
                Title = "Database Foundations",
            };
            CardAction Button5 = new CardAction()
            {
                Value = "ok you want to complete module5offullstack",
                Type = "postBack",
                Title = "Essential Backend Languages",
            };
            CardAction Button6 = new CardAction()
            {
                Value = "ok you want to complete module6offullstack",
                Type = "postBack",
                Title = "Security Foundations",
            };
            CardAction Button7 = new CardAction()
            {
                Value = "ok you want to complete module7offullstack",
                Type = "postBack",
                Title = "Web Projects Workflows",
            };
            CardAction Button8 = new CardAction()
            {
                Value = "ok you want to complete module8offullstack",
                Type = "postBack",
                Title = "Fullstack Frameworks",
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            cardButtons.Add(Button4);
            cardButtons.Add(Button5);
            cardButtons.Add(Button6);
            cardButtons.Add(Button7);
            cardButtons.Add(Button8);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private void python_test(Activity reply, LuisResponse luis)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "ok you want to complete module1offullstack",
                Type = "postBack",
                Title = "Web Development Foundations"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "ok you want to complete module2offullstack",
                Type = "postBack",
                Title = "Programing Fundamentals",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "ok you want to complete module3offullstack",
                Type = "postBack",
                Title = "Essential Frontend Languages",
            };
            CardAction Button4 = new CardAction()
            {
                Value = "ok you want to complete module4offullstack",
                Type = "postBack",
                Title = "Database Foundations",
            };
            CardAction Button5 = new CardAction()
            {
                Value = "ok you want to complete module5offullstack",
                Type = "postBack",
                Title = "Essential Backend Languages",
            };
            CardAction Button6 = new CardAction()
            {
                Value = "ok you want to complete module6offullstack",
                Type = "postBack",
                Title = "Security Foundations",
            };
            CardAction Button7 = new CardAction()
            {
                Value = "ok you want to complete module7offullstack",
                Type = "postBack",
                Title = "Web Projects Workflows",
            };
            CardAction Button8 = new CardAction()
            {
                Value = "ok you want to complete module8offullstack",
                Type = "postBack",
                Title = "Fullstack Frameworks",
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            cardButtons.Add(Button4);
            cardButtons.Add(Button5);
            cardButtons.Add(Button6);
            cardButtons.Add(Button7);
            cardButtons.Add(Button8);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }


        private void test_type(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "you want to give basic level test",
                Type = "postBack",
                Title = "Basic"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "you want to give medium level test",
                Type = "postBack",
                Title = "Medium",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "you want to give expert level test",
                Type = "postBack",
                Title = "Expert",
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        private string refferedtochoice(string id, Activity reply)
        {
            var result = MongoUser.exist_user(id);
            var occupy = JsonConvert.DeserializeObject<MongoData>(result.ToJson());
            string rep = null;
            switch (occupy.professional.occupation_type)
            {
              
                case "Design":
                    rep = col.ElementAt(14);
                    select_design(reply);
                    break;

                case "Development":
                    rep = col.ElementAt(48);
                    select_development(reply);
                    break;
                case "MarketingSales":
                    rep = col.ElementAt(22);
                    select_marketing(reply);
                    break;
           
             

            }

            return rep;
        }

        private void selectCompany(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "ok you are interested in Design/Development Studio",
                Type = "postBack",
                Title = "Design/Development Studio"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "ok you are interested in Financial Technology",
                Type = "postBack",
                Title = "Financial Technology"
            };
            CardAction Button3 = new CardAction()
            {
                Value = "ok you are interested in Financial Technology",
                Type = "postBack",
                Title = "Game Studio"
            };
            CardAction Button4 = new CardAction()
            {
                Value = "ok you are interested in ECommerce",
                Type = "postBack",
                Title = "ECommerce"
            };
            CardAction Button5 = new CardAction()
            {
                Value = "ok you are interested in Sales",
                Type = "postBack",
                Title = "Sales"
            };
            CardAction Button6 = new CardAction()
            {
                Value = "ok you are interested in Female Focused Technology",
                Type = "postBack",
                Title = "Female Focused Technology"
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            cardButtons.Add(Button4);
            cardButtons.Add(Button5);
            cardButtons.Add(Button6);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;

        }

        private void choose_invest(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "you selected the test Investor",  //technology
                Type = "postBack",
                Title = "Start"
            };
            cardButtons.Add(Button1);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;

        }

        private void choose_tech(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "you selected the test Android",  //technology
                Type = "postBack",
                Title = "Android"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "you selected the test IOS",
                Type = "postBack",
                Title = "IOS"
            };
            CardAction Button3 = new CardAction()
            {
                Value = "you selected the test Cross Platform",
                Type = "postBack",
                Title = "Cross Platform"
            };
            CardAction Button4 = new CardAction()
            {
                Value = "you selected the test Python",
                Type = "postBack",
                Title = "Python"
            };
            CardAction Button5 = new CardAction()
            {
                Value = "you selected the test PHP",
                Type = "postBack",
                Title = "PHP"
            };
            CardAction Button6 = new CardAction()
            {
                Value = "you selected the test C#",
                Type = "postBack",
                Title = "C#"
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            cardButtons.Add(Button4);
            cardButtons.Add(Button5);
            cardButtons.Add(Button6);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;

        }

        private void JobOptions(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "Design is an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "Design"

            };

            CardAction Button2 = new CardAction()
            {
                Value = "Development is an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "Development"
            };

            CardAction Button3 = new CardAction()
            {
                Value = "MarketingSales is an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "Marketing"
            };

            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
          
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;

        }

        private void test(Activity reply, string opt, int mod)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            string[] option = opt.Split('@');
            CardAction Button1 = new CardAction()
            {
                Value = "A your are still in test mode" + mod,
                Type = "postBack",
                Title = option[0],
            };
            CardAction Button2 = new CardAction()
            {
                Value = "B your are still in test mode" + mod,
                Type = "postBack",
                Title = option[1],
            };
            CardAction Button3 = new CardAction()
            {
                Value = "C your are still in test mode" + mod,
                Type = "postBack",
                Title = option[2],
            };
            CardAction Button4 = new CardAction()
            {
                Value = "D your are still in test mode" + mod,
                Type = "postBack",
                Title = option[3],
            };
            cardButtons.Add(Button1);
            cardButtons.Add(Button2);
            cardButtons.Add(Button3);
            cardButtons.Add(Button4);
            HeroCard jobCard = new HeroCard()
            {
                Buttons = cardButtons,
            };

            Attachment jobAttachment = jobCard.ToAttachment();
            reply.Attachments.Add(jobAttachment);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        }

        
    }

    //internal class MemberDialog : IDialog<object>
    //{
    //    public async Task StartAsync(IDialogContext context)
    //    {
    //        string userChoice = "Hi how are you";
    //        await context.PostAsync(userChoice);
    //       // context.Wait(MessageReceived);
    //    }
    //}
}