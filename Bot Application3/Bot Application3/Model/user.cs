using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bot_Application3.Model
{
    public class user
    {

        private static BotContext ec = new BotContext();
        private static BotContext ac = null;
        public static bool add_basic_info(basic_infos ui)
        {
            ac = new BotContext();
            ac.basic_infos.Add(ui);
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

        public static void add_edu_info(educational_infos[] edu)
        {           
            foreach (var record in edu)
            {
                ac = new BotContext();
                ac.educational_infos.Add(record);
                try
                {
                    ac.SaveChanges();
                   // return true;
                }
                catch
                {
                    //return false;
                }
            }
        }




        public static bool add_prof_info(professional_Infos pi)
        {
            ac = new BotContext();
            ac.professional_Infos.Add(pi);
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

        public static basic_infos exist_user(string id)
        {
            ac = new BotContext();
            // var res = ac.UsersInfoes.Any(x => x.User_ID == id);
            var query = (from exs in ac.basic_infos
                         where exs.user_id == id
                         select exs).SingleOrDefault();
            return query;
        }


        public static void upd_pro_info()
        {
                try
                {
                    ec.SaveChanges();
                    // return true;
                }
                catch
                {
                    //return false;
                }
            
        }


        public static  professional_Infos find_user_occu(string id)
        {
           // ac = new BotContext();
            var query = (from fin in ec.professional_Infos
                         where fin.user_id == id
                         select fin).SingleOrDefault();
            return query;
        }

        public static int get_skill_id(string skill)
        {
            ac = new BotContext();
            var query = (from ski in ac.skills
                         where ski.skill_name == skill
                         select ski.skill_id).SingleOrDefault();
            return query;
        }


    }

}