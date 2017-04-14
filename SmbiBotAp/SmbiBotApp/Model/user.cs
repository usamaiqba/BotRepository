using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmbiBotApp.Services;


namespace SmbiBotApp.Model
{
    public class user
    {
        
        BotContext ec = new BotContext();
        BotContext ac = null;    
        public bool Add_UserInfo(UsersInfo ui)
        {
            ac = new BotContext();        
            ac.UsersInfoes.Add(ui);
            try
            {
                ac.SaveChanges();
                return true;
            }
            catch
            {
                return false;            
            }

        }
    }  
}