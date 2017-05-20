using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Dialogs;
using Autofac;
using System.Collections.Generic;
using Bot_Application3.Services;
using Bot_Application3.Model;
using Facebook;
using System.Threading;
using System.Reflection;
using Humanizer;
using System.Xml.Serialization;
using System.Web.Script.Serialization;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Xml;
using MongoDB.Bson.Serialization;

namespace Bot_Application3
{


    [Serializable]
    public class DisplayDialog : IDialog<object>
    {
        // private ResumeAfter<bool> play;


        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("That's an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.");
            //  PromptDialog.Confirm(context, play, "That's an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.");
            //   context.Wait(MessageReceivedAsync);


        }

        //public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        //{

        //      await context.PostAsync("wait");
        //      PromptDialog.Confirm(context,perform_Action ,"This is the information we were able to pull from facebook could you confirm if you would like me to use this information ?");
        //    //  context.Wait(MessageReceivedAsync); 
        //}

        //private async Task perform_Action(IDialogContext context, IAwaitable<bool> result)
        //{

        //    var confirm = await result;
        //    if (confirm) // They said yes
        //    {
        //        // Start a new Game
        //        await context.PostAsync("Hi Welcome! - Guess a number between 1 and 10");
        //        context.Wait(MessageReceivedAsync);
        //    }
        //    else // They said no
        //    {
        //        await context.PostAsync("Goodbye!");
        //        context.Wait(MessageReceivedAsync);
        //    }
        //}

        //public async Task StartAsync(IDialogContext context)
        //{
        //    context.Wait(this.MessageReceivedAsync);
        //}

        //public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        //{
        //    var message = await result;

        //    //We need to keep this data so we know who to send the message to. Assume this would be stored somewhere, e.g. an Azure Table
        //    ConversationStarter.toId = message.From.Id;
        //    ConversationStarter.toName = message.From.Name;
        //    ConversationStarter.fromId = message.Recipient.Id;
        //    ConversationStarter.fromName = message.Recipient.Name;
        //    ConversationStarter.serviceUrl = message.ServiceUrl;
        //    ConversationStarter.channelId = message.ChannelId;
        //    ConversationStarter.conversationId = message.Conversation.Id;

        //    //We create a timer to simulate some background process or trigger
        //    t = new Timer(new TimerCallback(timerEvent));
        //    t.Change(5000, Timeout.Infinite);

        //    var url = HttpContext.Current.Request.Url;
        //    //We now tell the user that we will talk to them in a few seconds
        //    await context.PostAsync("Hello! In a few seconds I'll send you a message proactively to demonstrate how bots can initiate messages. You can also make me send a message by accessing: " +
        //            url.Scheme + "://" + url.Host + ":" + url.Port + "/api/CustomWebApi");
        //    context.Wait(MessageReceivedAsync);
        //}
        //public void timerEvent(object target)
        //{

        //    t.Dispose();
        //    ConversationStarter.Resume(ConversationStarter.conversationId, ConversationStarter.channelId); //We don't need to wait for this, just want to start the interruption here
        //}
    }

    [BotAuthentication]
    public class MessagesController : ApiController
    {

        public List<string> col = new List<string>();
        public static List<string> data = new List<string>();
        static string[] profile = new string[7];
        static int count = 0;
        static int flag = 0;
        static int counter = 1;
        static int callback = 0;
        static string id = null;
        static string occupation = null;
        static int pro_count = 1;
        static bool wait = true;
        static int test_count = 0;

        List<string> ques = new List<string>();
        List<string> options = new List<string>();


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
        };
        private void asked()
        {
            col.Add("That's an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.");
            col.Add("This is the information we were able to pull from facebook could you confirm if you would like me to use this information ?");
            col.Add("Let's get to fixing your details. What's your first name ?");
            col.Add("And your last name");
            col.Add("May I ask how old you are ?");
            col.Add("Are you a guy or a girl ? It's an odd question right");
            col.Add("Where do you currently live ?");
            col.Add("what is your email address ?");
            col.Add("Nice to meet you. Now i'm gonna ask you some questions to develop your professional profile.");
            col.Add("What university did you attend ?");//9
            col.Add("When did you graduate ?");//10
            col.Add("What did you study ?");//11
            col.Add("Is this the highest level of education you attainted ?");//12
            col.Add("What sort of company are you interested in working for:");//13
            col.Add("What sort of designer are you ?");//14
            col.Add("How many projects or products you have worked on ?");//15
            col.Add("what is the title of your");//16
            col.Add("Briefly describe your"); //17
            col.Add("Rank your experience / level of using design softwares:Sketch:Photoshop:Illustrator:indesign:");
            col.Add("What web development languages are you familiar with : (select all those that apply)");//19
            col.Add("Describe the project or role you were most proud of ? What sort did you do in your capcity and what were the outcomes / achievements ? ");
            col.Add("Is there any other project, product , or work experience. You would like to share with us ?");
            col.Add("what sort of developer are you ?");//22
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
            col.Add("What sort of position are you looking for ?");//33
            col.Add("Your profile has been successfully created");//34
            col.Add("I'm Maz created by the people at PeopleHome to help you find the job right for you. So let's get started ? ");//35
            col.Add("Project that i have been done so far are");//36
            col.Add("The title of my project is");//37
            col.Add("The description of my project is");//38
            col.Add("That's good now your basic profile has been set.If you want to update your profile information let us know ?");//39
            col.Add("Now we are going to take some tests to evaluate you and to build your professional profile");//40
            col.Add("Which technology you are intertested ?");//41
            col.Add("Which type of test you would like to give ?"); //42
            col.Add("Now let start the test here is your first question");//43
            col.Add("Do you want to give any other test ?");
            col.Add("Which information would you like to update ?");//39


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
                StateClient stateClient = activity.GetStateClient();
                BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
                await stateClient.BotState.SetUserDataAsync(activity.ChannelId, activity.From.Id, userData);
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
            var replymesge = string.Empty;
            Activity reply = new Activity();
            reply = activity.CreateReply();
            if (activity.Type == ActivityTypes.Message)
            {
                var phrase = activity.Text;
                var usr = new BsonDocument();
                usr["firstname"] = "usama";
                usr["lastname"] = "iqbal";
                usr["age"] = "25";

                //   XmlDocument doc = new XmlDocument();
                //   doc.Load("C:\\git\\BotRepository\\Bot Application3\\files\\quoestions.xml");
                //  string jsonText = JsonConvert.SerializeXmlNode(doc); //XML to Json
                //  // string json = JsonConvert.SerializeObject(jsonText);

                //   //write string to file
                //   System.IO.File.WriteAllText("C:\\test\\path.json", jsonText);
                //  // var bs = BsonSerializer.Deserialize<BsonDocument>(jsonText); //Deserialize JSON String to BSon Document


                //   //  var mcollection = Program._database.GetCollection<BsonDocument>("test_collection_05");
                //   // await mcollection.InsertOneAsync(bsdocument); //Insert into mongoDB


                //   // Insert new user object to collection
                //   //users.Insert(user);
                ////   LuisService.DoSomethingAsync(jsonText);
                //   //LuisService.DoSomethingAsync();



                var luisresp = await LuisService.ParseUserInput(phrase);

            //  activity.Text.Replace(activity.Text,"text replaced successfully");
            //  activity.RemoveMentionText(activity.Id);

            back:

                //  var xha = activity.Text.Replace(activity.Text, "text replaced successfully");
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

                                break;

                            case "questions":
                                callback = 0;
                                replymesge = luisresp.query;
                                var entity = luisresp.entities[0].entity.ToLower();
                                switch (entity)
                                {
                                    case "started":

                                        //facebook fb = new facebook();
                                        //fb.getdatafromfb();
                                        FacebookServices fbc = new FacebookServices();
                                        profile = await fbc.GetUser("EAACEdEose0cBAELSqQ75zMJ2lBdhueqtuZCGgRiZCTXhgJpOJL6PTDKJLXvtkZA1mgZBJIacM1T8PCcnbQTVJynU8xdRaLw23MFHLQ6k1AIo2ZBerGZBZCiQuZAIKyrZCwzbErhZAT0ZAeTZAiSM1YlK9yuVtqlCSb8BAFhWgrzYxJdCZAH8E3k2YD0KiIKT043T4kccZD");
                                        if (profile != null)
                                        {
                                            id = profile[0];
                                            data.Add(id);
                                            var result = user.exist_user(profile[0]);

                                            var res = user.find_user_occu(profile[0]);
                                            //string creditApplicationJson = JsonConvert.SerializeObject(
                                            //    new
                                            //    {
                                            //        jsonCreditApplication = res

                                            //    });

                                            //     var json = new JavaScriptSerializer().Serialize(result);
                                            //var daa = JsonConvert.SerializeObject(res);
                                            string location = profile[5];
                                            string[] loc = (location.Substring(location.IndexOf(location.Substring(32))).Split(','));
                                            profile[5] = loc[0];
                                            if (result != null)
                                            {

                                                if (result.status == 1)
                                                {
                                                    count = 6;
                                                    counter = 6;
                                                    str.intent = "emailAddress";
                                                    goto next;
                                                }
                                                else if (result.status == 2)
                                                {
                                                    count = 10;
                                                    counter = 10;
                                                    str.intent = "projects";
                                                    goto next;
                                                }
                                                else if (result.status == 3)
                                                {

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
                                        break;
                                    case "basic":
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
                                        string skill = luisresp.query.Substring(23);
                                        // data.Add(luisresp.query.Substring(23));
                                        data.Add((user.get_skill_id(skill)).ToString());
                                        replymesge = col.ElementAt(15);
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

                                    case "technology":
                                        replymesge = col.ElementAt(42);
                                        test_type(reply);
                                        break;

                                    case "test":
                                        reply.Text = col.ElementAt(43);
                                        await connector.Conversations.ReplyToActivityAsync(reply);
                                        Thread.Sleep(2000);
                                        replymesge = ques.ElementAt(0);
                                        test(reply, 0);
                                        break;

                                    case "mode":

                                        if (test_count <= 2)
                                        {
                                            replymesge = ques.ElementAt(1);
                                            //if (reply.Attachments != null)
                                            //{
                                            //    reply.Attachments.Clear();
                                            //    var ids = reply.Id;
                                            //    var rem = reply.RemoveMentionText(ids);
                                            //}
                                            test(reply, test_count++);
                                            await connector.Conversations.ReplyToActivityAsync(reply);
                                            Thread.Sleep(1100);
                                        }
                                        else
                                        {

                                        }
                                        break;

                                }
                                // count = 1;
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

                                    case 6://count&counter is 6

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

                                    case 10: //status 2 
                                        if (counter == 10 && data.Count >= 6)
                                        {
                                            //  check_entity(symb, luisresp);
                                            //  edu_pro_record();
                                        }
                                        var number = user.find_user_occu(id);
                                        if (pro_count > 0 && pro_count <= number.no_of_projects)
                                        {
                                            data.Add(luisresp.query);
                                            replymesge = correctSequence("projects", replymesge, 16) + " " + pro_count.ToOrdinalWords() + " project ?";

                                        }
                                        else
                                        {
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
                                        break;

                                    case 11://title
                                        var num = user.find_user_occu(id);
                                        var rem = reply.RemoveMentionText(reply.Id);


                                        if (pro_count > 0 && pro_count <= num.no_of_projects)
                                        {
                                            data.Add(luisresp.query);
                                            //   check_entity(symb, luisresp);
                                            replymesge = correctSequence("title", replymesge, 17) + " " + pro_count.ToOrdinalWords() + " project";
                                            var rema = reply.RemoveMentionText(reply.ReplyToId);
                                            rem = activity.RemoveMentionText(activity.Id);
                                            rem = activity.RemoveMentionText(activity.Recipient.Id);
                                            rem = activity.RemoveMentionText(activity.From.Id);
                                            var id = activity.From.Id;
                                            rema = activity.RemoveMentionText(activity.ReplyToId);

                                            pro_count++;

                                            count = count - 2;
                                            counter = counter - 2;
                                        }
                                        else
                                        {

                                        }
                                        break;

                                    case 12:
                                        // var num = user.find_user_occu(id);
                                        // if (pro_count > 0 && pro_count <= num.no_of_projects)
                                        //{
                                        data.Add(luisresp.query);
                                        replymesge = correctSequence(str.intent, replymesge, 17) + " " + pro_count.ToOrdinalWords() + " project";
                                        pro_count++;
                                        count--;
                                        counter--;
                                        //}
                                        //else
                                        //{

                                        //}
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
                }
                else
                {
                    replymesge = $"sorry.I could not get as to what you say";
                }

                reply.Text = replymesge;
                await connector.Conversations.ReplyToActivityAsync(reply);
            }

            else if (activity.Type == ActivityTypes.DeleteUserData)
            {

                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (activity.Type == ActivityTypes.ConversationUpdate)
            {
                counter = 1;
                data.Clear();
                //IConversationUpdateActivity update = activity;
                //using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, activity))
                //{
                //    //data.Clear();
                //    var client = scope.Resolve<IConnectorClient>();
                //    if (update.MembersAdded.Any())
                //    {
                //        var repli = activity.CreateReply();
                //        //repli.Attachments = new List<Attachment>();
                //        var newMembers = update.MembersAdded?.Where(t => t.Id != activity.Recipient.Id);

                //        //List<CardAction> cardButtons = new List<CardAction>();
                //        //CardAction plButton = new CardAction()
                //        //{
                //        //    Value = "what sort of position are you looking for ?",
                //        //    Type = "postBack",
                //        //    Title = "Get Started"
                //        //};
                //        //cardButtons.Add(plButton);
                //        ////  JobOptions(repli);
                //        //HeroCard plCard = new HeroCard()
                //        //{
                //        //    Buttons = cardButtons
                //        //};
                //        //Attachment plAttachment = plCard.ToAttachment();
                //        //repli.Attachments.Add(plAttachment);

                //foreach (var newMember in newMembers)
                //{
                ////    repli.Text = "Welcome";
                //    //if (!string.IsNullOrEmpty(newMember.Name))
                //    //{
                //    //    repli.Text += $" {newMember.Name}";
                //    //}
                // //   repli.Text += "! I'm Maz created by the people at PeopleHome to help you find the job right for you. So let's get-started ?";
                // //   repli.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                // //   await connector.Conversations.ReplyToActivityAsync(repli);
                //}
                //    }
                //}

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

            return null;
        }

        public async Task<Tuple<LuisResponse, string>> CallLuisAgain(LuisResponse luisresp, string reply, int y)
        {
            luisresp = await LuisService.ParseUserInput(col.ElementAt(y) + " " + luisresp.query);
            reply = luisresp.query;
            var tuple = new Tuple<LuisResponse, string>(luisresp, reply);
            return tuple;
        }

        private void check_entity(string symb, LuisResponse luisresp)
        {
            symb = luisresp.entities[0].entity;
            if (symb != string.Empty)
            {
                data.Add(symb);
            }
        }
        private void edu_pro_record()
        {

            int cou = data.Count;//10 // 7
            cou = cou - 3; // 10 -3 = 7 // 7-3 = 4 
            cou = (cou - 1) / 3;

            int j = 1;

            //if (cou == 1)
            //{

            List<BsonDocument> multiple = new List<BsonDocument>();
            for (int i = 0; i < cou; i++)
            {
                var record = new BsonDocument
                        {
                           { "uni_name"    , col.ElementAt(j++) },
                           { "pass_year"   , col.ElementAt(j++) },
                           { "degree_name" , col.ElementAt(j++) }
                        };

                multiple.Add(record);
            }
            MongoUser.add_edu_infos(multiple);
          //  }
            //else
            //{
            //    BsonArray[] edu = new BsonArray[cou];
            //    for (int i = 0; i < cou; i++)
            //    {
            //        edu[i].Add(data.ElementAt(0));
            //        edu[i].Add(data.ElementAt(j++));
            //        edu[i].Add(data.ElementAt(j++));
            //        edu[i].Add(data.ElementAt(j++));
            //    }
            //    MongoUser.add_edu_infos(edu);
            //}

            //if (cou == 1)
            //{
            //    educational_infos ed = new educational_infos();
            //    ed.user_id = data.ElementAt(0);
            //    ed.uni_name = data.ElementAt(j++);
            //    ed.pass_year = data.ElementAt(j++);
            //    ed.degree_name = data.ElementAt(j++);
            //    user.add_edu_info(ed);
            //}
            //else
            //{
            //    educational_infos[] edu = new educational_infos[cou];
            //    for (int i = 0; i < cou; i++)
            //    {
            //        edu[i].user_id = data.ElementAt(0);
            //        edu[i].uni_name = data.ElementAt(j++);
            //        edu[i].pass_year = data.ElementAt(j++);
            //        edu[i].degree_name = data.ElementAt(j++);
            //    }
            //    user.add_edu_infos(edu);
            //}
            //professional_Infos pi = user.find_user_occu(id);
            //{
            //    pi.company_type = data.ElementAt(j++);
            //    pi.position_type = data.ElementAt(j++);
            //    pi.no_of_projects = int.Parse(data.ElementAt(j++));
            //};
            //user.upd_pro_info();

            //basic_infos bi = user.exist_user(data.ElementAt(0));
            //{
            //    bi.status = 2;
            //}
            //user.upd_pro_info();
            //data.Clear();
        }

        public bool check_status(int call)
        {
            if (id != null || occupation != null)
            {
                var Client = new MongoClient();
                var MongoDB = Client.GetDatabase("Botdatabase");
                var Collec = MongoDB.GetCollection<BsonDocument>("user");

                var info = new BsonDocument
                {
                    {"_id",data.ElementAt(0)},
                    {"basic",new BsonDocument
                     {
                      //  { "user_id"    , data.ElementAt(0) },
                        { "first_name" , data.ElementAt(1) },
                        { "last_name"  , data.ElementAt(2) },
                        { "age"        , "25" },//Convert.ToInt32(data.ElementAt(3)),
                        { "gender"     , data.ElementAt(4) },
                        { "location"   , data.ElementAt(5) },
                        { "email"      , data.ElementAt(6) },
                        { "status"     , "1"               }
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

                Collec.InsertOneAsync(info);
                //var prof = new BsonDocument
                //{
                //    { "user_id"         , data.ElementAt(0) },
                //    { "occupation_type" , occupation        }
                //};
          

                //basic_infos ui = new basic_infos()
                //{
                //    user_id = data.ElementAt(0),
                //    first_name = data.ElementAt(1),
                //    last_name = data.ElementAt(2),
                //    age = 25,//Convert.ToInt32(data.ElementAt(3)),
                //    gender = data.ElementAt(4),
                //    location = data.ElementAt(5),
                //    email = data.ElementAt(6),
                //    status = 1
                //};

                //professional_Infos pi = new professional_Infos()
                //{
                //    user_id = data.ElementAt(0),
                //    occupation_type = occupation

                //};
                ////   var chek = user.add_basic_info(ui);
                ////  user.add_prof_info(pi);
                //occupation = null;
                //flag = 0;
                //data.Clear();
                //   if (chek)
                // {
                //    return true;                  
                // }

            }
            return false;
        }

        private string correctSequence(string intent, string reply, int x)
        {
            if (Enum.GetName(typeof(decision), counter) == intent)
            {
                reply = col.ElementAt(x);
                count++;
                counter++;
            }
            else
            {
                reply = "Sorry wrong input";
            }
            return reply;
        }

        //private async void niceTomeet(string intent, Activity reply, int x)
        //{
        //    string rep = null;
        //    reply.Text = correctSequence(intent, rep, 8);
        //    await connector.Conversations.ReplyToActivityAsync(reply);
        //    Thread.Sleep(2000);
        //    replymesge = col.ElementAt(9);
        //}

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

        private void yesorno(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "Yes,What sort of company are you interested in working for:",
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


        private void selectdesign(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "ok you are designer of UX / UI",
                Type = "postBack",
                Title = "UX / UI"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "ok you are designer of Graphic / Web",
                Type = "postBack",
                Title = "Graphic / Web",
            };
            CardAction Button3 = new CardAction()
            {
                Value = "ok you are designer of Full Stack",
                Type = "postBack",
                Title = "Full Stack",
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
            var occupy = user.find_user_occu(id);
            string rep = null;
            switch (occupy.occupation_type)
            {
                case "Product":
                    // col.ElementAt();
                    break;

                case "Design":
                    rep = col.ElementAt(14);
                    selectdesign(reply);
                    break;

                case "Development":
                    break;
                case "Marketing":
                    break;
                case "Sales":
                    break;
                case "Administration":
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

        private void choose_tech(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "you selected Android technology",
                Type = "postBack",
                Title = "Android"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "you selected IOS technology",
                Type = "postBack",
                Title = "IOS"
            };
            CardAction Button3 = new CardAction()
            {
                Value = "you selected Cross Platform technology",
                Type = "postBack",
                Title = "Cross Platform"
            };
            CardAction Button4 = new CardAction()
            {
                Value = "you selected Python technology",
                Type = "postBack",
                Title = "Python"
            };
            CardAction Button5 = new CardAction()
            {
                Value = "you selected PHP technology",
                Type = "postBack",
                Title = "PHP"
            };
            CardAction Button6 = new CardAction()
            {
                Value = "you selected C# technology",
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
                Value = "Product is an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "Product"

            };

            CardAction Button2 = new CardAction()
            {
                Value = "Design is an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "Design"

            };

            CardAction Button3 = new CardAction()
            {
                Value = "Development is an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "Development"
            };

            CardAction Button4 = new CardAction()
            {
                Value = "Marketing is an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "Marketing"
            };

            CardAction Button5 = new CardAction()
            {
                Value = "Sales is an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "Sales"
            };
            CardAction Button6 = new CardAction()
            {
                Value = "Administration is an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "Administration"
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


        private void test(Activity reply, int ind)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            string[] option = options.ElementAt(ind).Split(',');
            CardAction Button1 = new CardAction()
            {
                Value = "your are still in test mode",
                Type = "postBack",
                Title = option[0]
            };
            CardAction Button2 = new CardAction()
            {
                Value = "your are still in test mode",
                Type = "postBack",
                Title = option[1],
            };
            CardAction Button3 = new CardAction()
            {
                Value = "your are still in test mode",
                Type = "postBack",
                Title = option[2],
            };
            CardAction Button4 = new CardAction()
            {
                Value = "your are still in test mode",
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
}