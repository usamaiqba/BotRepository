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

namespace Bot_Application3
{

    [Serializable]
    public class DisplayDialog : IDialog<object> 
    {
        private ResumeAfter<bool> play;
        
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("That's an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.");
            //PromptDialog.Confirm(context, play, "That's an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.");
         //   context.Wait(MessageReceivedAsync);

            // throw new NotImplementedException();
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
        static int count = 0;
        static int flag = 0;
        static int counter = 1;
        static int callback = 0;
      

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
              degree = 9
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
            col.Add("What web development languages are you familiar with : (select all those that apply)");
            col.Add("Rank your experience with the following: (All selected)");//16
            col.Add("What industry leading graphic design tools are you comfortable using ? adobe photoshop, adobe illustrator, adobe indesign, sketch");
            col.Add("Rank your experience / level of using design softwares:Sketch:Photoshop:Illustrator:indesign:");
            col.Add("How many projects or products you have worked on ? ");//19
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
            col.Add("Welcome! I'm Maz created by the people at PeopleHome to help you find the job right for you. So let's get started ? ");//35



        }

        //public async Task<HttpRequestMessage> Get([FromBody]Activity activity)
        //{
        //    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
        //    activity.CreateReply("hello");
        //    await connector.Conversations.SendToConversationAsync((Activity)activity);
        //    //  var response = Request.GetRequestContext();
        //    return null;          
        //}

        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            asked();
            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            await HandleSystemMessage(activity, connector);
            StateClient stateClient = activity.GetStateClient();
            BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
            var conv = activity.Conversation;
            userData.SetProperty<bool>("SentGreeting", true);
            await stateClient.BotState.SetUserDataAsync(activity.ChannelId, activity.From.Id, userData);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
        
        private async Task<Activity> HandleSystemMessage(Activity activity, ConnectorClient connector)
        {
            var replymesge = string.Empty;
            BotData botdata = new BotData();                 
            Activity reply = new Activity();
            reply = activity.CreateReply();
            if (activity.Type == ActivityTypes.Message)
            {        
                var phrase = activity.Text;
                var luisresp = await LuisService.ParseUserInput(phrase);
            back:
                if (luisresp.intents.Count() > 0)
                {
                    var str = luisresp.topScoringIntent;
                    try
                    {                                
                        var symb = string.Empty;                     
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
                                }
                                
                                break;
                        
                            case "questions":
                                callback = 0;
                                replymesge = luisresp.query;
                                var entity = luisresp.entities[0].entity.ToLower();
                                switch (entity)
                                {
                                    case "started":
                                        reply.Text = col.ElementAt(35);
                                        await connector.Conversations.ReplyToActivityAsync(reply);
                                        Thread.Sleep(1000);
                                        replymesge = col.ElementAt(33);
                                        JobOptions(reply);
                                        break;
                                    case "position":
                                        JobOptions(reply);
                                        break;
                                    case "profile":
                                        reply.Text = col.ElementAt(0);
                                        await connector.Conversations.ReplyToActivityAsync(reply);
                                        Thread.Sleep(1000);
                                        replymesge = col.ElementAt(1);
                                        infoConfirm(reply);
                                        break;
                                    case "details":
                                        replymesge = col.ElementAt(2);
                                        break;
                                    case "company":
                                        selectCompany(reply);
                                        break;
                                }
                                count = 1;
                                break;     
                          
                            //    else if (luisresp.entities[0].entity == "profile")
                            //    {
                            //        reply.Text = col.ElementAt(0);
                            //        await connector.Conversations.ReplyToActivityAsync(reply);
                            //        replymesge = col.ElementAt(1);
                            //        infoConfirm(reply);
                            //        //facebook fb = new facebook();
                            //        //fb.getInfo();
                            //        //fb.GetDataFromFB();
                            //        //fb.getdatafromfb(); 
                                    
                            //    //  FacebookServices fbc = new FacebookServices();
                            ////      await fbc.GetUser("EAABdYDHKoG8BAB6QYjTIGrmqkjWxtkopv7ZAueiEgSFLGB5msm9EPBr9UJGDEw5KEe0xVX0BWPLPngL9JTRfFe9xRIzC4BX8DAYJxrq6ShuzX67TmaglBqFNA6douMSH67R8QyZAxfSWDda34yj1nzDkfX50xoY7c4aMBs11Q5ZCc4MA3M52ux3M1Q7ppUZD");
                            //        //await fbc.GetUser("EAACEdEose0cBADKExOaZCH277UMyc9oVdSBzwBiKZCWDUFujJ1OtpN6j2hnXcZB9ZABhRmWcA2crbn79znpwzpE1djPGJhGQIQM9fxGGhlsd8Srq4epjDI0AF5P94lZAIY09byVZCTuLQ1RZBJrnqBldATOSXiAs7vwHA125JmtnzSc0ZAZAfD54eZBbMrSQPHWZAIZD");    
                            //     //   await fbc.GetFacebookProfileAsync("EAABdYDHKoG8BAB6QYjTIGrmqkjWxtkopv7ZAueiEgSFLGB5msm9EPBr9UJGDEw5KEe0xVX0BWPLPngL9JTRfFe9xRIzC4BX8DAYJxrq6ShuzX67TmaglBqFNA6douMSH67R8QyZAxfSWDda34yj1nzDkfX50xoY7c4aMBs11Q5ZCc4MA3M52ux3M1Q7ppUZD");
                            //    }
                         
                            default:
                                callback = 0;
                                switch (counter)
                                {
                                    case 1:
                                        replymesge = correctSequence(str.intent,replymesge,3);
                                        break;

                                    case 2:
                                        replymesge = correctSequence(str.intent, replymesge, 4);
                                        break;

                                    case 3:
                                        replymesge = correctSequence(str.intent, replymesge, 5);
                                        break;
                                    case 4:
                                        replymesge = correctSequence(str.intent, replymesge, 6);
                                        break;

                                    case 5:
                                        replymesge = correctSequence(str.intent, replymesge, 7);
                                        break;

                                    case 6:
                                        reply.Text = correctSequence(str.intent, replymesge, 8);
                                        await connector.Conversations.ReplyToActivityAsync(reply);
                                        Thread.Sleep(2000);
                                        replymesge = col.ElementAt(9);
                                        break;

                                    case 7:
                                        replymesge = correctSequence(str.intent, replymesge, 10);
                                        break;

                                    case 8:
                                        replymesge = correctSequence(str.intent, replymesge, 11);
                                        break;

                                    case 9:
                                        replymesge = correctSequence(str.intent, replymesge, 12);
                                        yesorno(reply);
                                        break;
                                }
                                break;

                            //case "emailAddress"://6
                            //    symb = luisresp.entities[0].entity;
                            //    reply.Text = col.ElementAt(8);
                            //    await connector.Conversations.ReplyToActivityAsync(reply);
                            //    replymesge = col.ElementAt(9);
                            //    flag = 1;
                            //    counter++;
                            //    count = 7;
                            //    break;

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
                    catch(FacebookApiException ex)
                    {
                        // replymesge = luisresp.query;
                      replymesge =   ex.Message.ToString();

                        //   JobOptions(reply);
                        // replymesge = $"sorry";
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

        public async Task<Tuple<LuisResponse,string>> CallLuisAgain(LuisResponse luisresp,string reply, int y)
        {      
            luisresp = await LuisService.ParseUserInput(col.ElementAt(y) + " " + luisresp.query);
            reply = luisresp.query;
            var tuple = new Tuple<LuisResponse,string>(luisresp,reply);
            return tuple;
        }


        private string correctSequence(string intent , string reply,int x)
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

        protected void infoConfirm(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "This is the information we were able to pull from facebook could you confirm if you would like me to use this information ?",
                Type = "postBack",
                Title = "Correct"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "Let's get to fixing your details. What's your first name ?",
                Type = "postBack",
                Title = "Incorrect"
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
                Value = "What sort of company are you interested in working for:",
                Type = "postBack",
                Title = "Yes"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "That's an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "No"
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

        private void selectCompany(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "That's an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "Design/Development Studio"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "That's an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "Financial Technology"
            };
            CardAction Button3 = new CardAction()
            {
                Value = "That's an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "Game Studio"
            };
            CardAction Button4 = new CardAction()
            {
                Value = "That's an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "ECommerce"
            };
            CardAction Button5 = new CardAction()
            {
                Value = "That's an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "Sales"
            };
            CardAction Button6 = new CardAction()
            {
                Value = "That's an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
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

        private void JobOptions(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction Button1 = new CardAction()
            {
                Value = "That's an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "Product"
            };
            CardAction Button2 = new CardAction()
            {
                Value = "That's an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "Design"
            };
            CardAction Button3 = new CardAction()
            {
                Value = "That's an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "Development"
            };
            CardAction Button4 = new CardAction()
            {
                Value = "That's an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "Marketing"
            };
            CardAction Button5 = new CardAction()
            {
                Value = "That's an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
                Type = "postBack",
                Title = "Sales"
            };
            CardAction Button6 = new CardAction()
            {
                Value = "That's an interesting career choice. Before we use our magic sauce to match you let's get your profile setup.",
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
    }
}