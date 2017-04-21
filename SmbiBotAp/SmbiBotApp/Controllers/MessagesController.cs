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

namespace SmbiBotApp
{

    [Serializable]
    public class DisplayDialog : IDialog<object>
    {
        private ResumeAfter<bool> play;

        public  async Task StartAsync(IDialogContext context)
        {
           await context.PostAsync("Are you sure you want to save");
           // PromptDialog.Confirm(context,play,"create the profile");
          //  await context.Wait(SendAsync);
              context.Wait(MessageReceivedAsync);       
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            await context.PostAsync("Not correct. Guess again.");
            PromptDialog.Confirm(context,play,"Yes I did it");
            
            // context.Wait(MessageReceivedAsync);          
        }
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
            col.Add("That's an interesting career choice. Before we use our magic sauce to match you let's get your profile setup."); //0
            col.Add("This is the information we were able to pull from facebook could you confirm if you would like me to use this information ?");//1
            col.Add("Let's get to fixing your details. What's your first name ?");//2
            col.Add("And your last name");//3
            col.Add("May I ask how old you are ?");//4
            col.Add("Are you a guy or a girl ? It's an odd question right");//5
            col.Add("Where do you currently live ?");//6
            col.Add("what is your email address ?");//7
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
            col.Add("my age is");//15/25
            col.Add("my gender is");//16//26
            col.Add("I currently live in");//17//27
            col.Add("my email address is");//28
            col.Add("I attend");//29
            col.Add("I graduated in");//30
            col.Add("I study");//31
            col.Add("Are you done");//18//32
            col.Add("What sort of position are you looking for ?");//33
            col.Add("Your profile has been successfully created");//34
            col.Add("Welcome! I'm Maz created by the people at PeopleHome to help you find the job right for you. So let's get started ? ");//35

        }

        public async void RetRespAgain(LuisResponse luisresp, string mesaj)
        {
            if (count == 1)
            {
                luisresp = await LuisService.ParseUserInput(col.ElementAt(4) + " " + luisresp.query);
                mesaj = luisresp.query;
            }
            else if (count == 2)
            {
                luisresp = await LuisService.ParseUserInput(col.ElementAt(5) + " " + luisresp.query);
                mesaj = luisresp.query;
            }
            else if (count == 3)
            {
                luisresp = await LuisService.ParseUserInput(col.ElementAt(6) + " " + luisresp.query);
                mesaj = luisresp.query;
            }
            else if (count == 4)
            {
                luisresp = await LuisService.ParseUserInput(col.ElementAt(7) + " " + luisresp.query);
                mesaj = luisresp.query;
            }
        }
    
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
             asked();               
             ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
             await HandleSystemMessage(activity ,connector);
             var response = Request.CreateResponse(HttpStatusCode.OK);
             return response;
        }
        
        private async Task<Activity> HandleSystemMessage(Activity activity , ConnectorClient connector)
        {        
            var replymesge = string.Empty;
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
                                        FacebookServices fbc = new FacebookServices();
                                        string[] res = await fbc.GetUser("EAACEdEose0cBADKExOaZCH277UMyc9oVdSBzwBiKZCWDUFujJ1OtpN6j2hnXcZB9ZABhRmWcA2crbn79znpwzpE1djPGJhGQIQM9fxGGhlsd8Srq4epjDI0AF5P94lZAIY09byVZCTuLQ1RZBJrnqBldATOSXiAs7vwHA125JmtnzSc0ZAZAfD54eZBbMrSQPHWZAIZD");
                                        if (res != null)
                                        {
                                           
                                          var result =  user.Exist_User(res[0]);
                                            if (result != null)
                                            {
                                                if (result.status == 1)
                                                {

                                                }
                                                else if (result.status == 2)
                                                {

                                                }
                                                else if (result.status == 3)
                                                {

                                                }
                                            }
                                            

                                        }

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

                            default:
                                callback = 0;
                                switch (counter)
                                {
                                    case 1:
                                        replymesge = correctSequence(str.intent, replymesge, 3);
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
                                        symb = luisresp.entities[0].entity;
                                        //check_entity(symb,luisresp);
                                        //if (counter == 5)
                                        //{
                                        //    check_status();
                                        //}
                                        flag = 1;
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
                                                   
                        }
                        if (callback == 1)
                        {
                            callback = 0;
                            goto back;
                        }
                        //if (symb != string.Empty)
                        //{
                        //    data.Add(symb);
                        //}

                        //phrase.ToLower();
                        //if (flag == 1 && (phrase == "yes" || phrase == "y"))
                        //{
                        //    if (data.Count == 4)
                        //    {
                        //        UsersInfo ui = new UsersInfo()
                        //        {
                        //            First_Name = data.ElementAt(0),
                        //            Last_Name = data.ElementAt(1),
                        //            User_age = Convert.ToInt32(data.ElementAt(2)),
                        //            Email = data.ElementAt(3),
                        //        };
                        //        user user1 = new user();
                        //        user1.Add_UserInfo(ui);
                        //        if (true)
                        //        {
                        //            replymesge = col.ElementAt(9);
                        //        }
                        //        flag = 0;
                        //        data.Clear();
                        //    }
                        //}
                    }
                    catch
                    {
                       // replymesge = luisresp.query;
                       // JobOptions(reply);
                        // replymesge = $"sorry";
                    }
                }
                else
                {
                    replymesge = $"sorry.I could not get as to what you say";
                }
                // reply = activity.CreateReply(replymesge);
                reply.Text = replymesge;
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
       
            else if (activity.Type == ActivityTypes.ConversationUpdate)
            {
                counter = 1;
                //IConversationUpdateActivity update = activity;
                //using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, activity))
                //{
                //    data.Clear();
                //    var client = scope.Resolve<IConnectorClient>();
                //    if (update.MembersAdded.Any())
                //    {
                //        var repli = activity.CreateReply();
                //        repli.Attachments = new List<Attachment>();
                //        var newMembers = update.MembersAdded?.Where(t => t.Id != activity.Recipient.Id);

                //        List<CardAction> cardButtons = new List<CardAction>();
                //        CardAction plButton = new CardAction()
                //        {
                //            Value = "what sort of position are you looking for ?",
                //            Type = "postBack",
                //            Title = "Get Started"
                //        };
                //        cardButtons.Add(plButton);
                //        // JobOptions(repli);
                //        HeroCard plCard = new HeroCard()
                //        {
                //            Buttons = cardButtons
                //        };
                //        Attachment plAttachment = plCard.ToAttachment();
                //        repli.Attachments.Add(plAttachment);

                //        foreach (var newMember in newMembers)
                //        {
                //            repli.Text = "Welcome";
                //            if (!string.IsNullOrEmpty(newMember.Name))
                //            {
                //                repli.Text += $" {newMember.Name}";
                //            }
                //            repli.Text += "! I'm Maz created by the people at PeopleHome to help you find the job right for you. So let's get-started ?";
                //            repli.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                //            await connector.Conversations.ReplyToActivityAsync(repli);
                //        }
                  //  }
              //  }


                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }


            else if (activity.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
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

        private void check_entity(string symb,LuisResponse luisresp)
        {
            symb = luisresp.entities[0].entity;
            if (symb != string.Empty)
            {
                data.Add(symb);
            }
        }

        private void check_status()
        {
            if (data.Count == 6)
            {
                UsersInfo ui = new UsersInfo()
                {
                    First_Name = data.ElementAt(0),
                    Last_Name = data.ElementAt(1),
                    User_age = Convert.ToInt32(data.ElementAt(2)),
                    Gender = data.ElementAt(3),
                    location = data.ElementAt(4),
                    Email = data.ElementAt(5),
                };
                user user1 = new user();
                user1.Add_UserInfo(ui);
                if (true)
                {
                   // replymesge = col.ElementAt(9);
                }
                flag = 0;
                data.Clear();
            }

        }

        private void startConv(Activity reply)
        {
            reply.Attachments = new List<Attachment>();
            List<CardAction> cardButtons = new List<CardAction>();
            CardAction plButton = new CardAction()
            {
                Value = "what sort of position are you looking for ?",
                Type = "postBack",
                Title = "Get Started"
            };
            cardButtons.Add(plButton);
            // JobOptions(repli);
            HeroCard plCard = new HeroCard()
            {
                Buttons = cardButtons
            };
            Attachment plAttachment = plCard.ToAttachment();
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