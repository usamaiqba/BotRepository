using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bot_Application3.Model
{
        public class MongoData
        {  
            public string _id { get; set; }
            public Basic basic { get; set; }
            public Skill skill { get; set; }
            public Test[] test { get; set; }
    //        public TotalQuest[] totalquest { get; set; }
            public Project project { get; set; }
            public Professional professional { get; set; }
            public Educational[] educational { get; set; }
          //  public ProjectDetail[] project { get; set; }
       }

        public class Basic
        {
          public string first_name { get; set; }
          public string last_name  { get; set; }
          public int    age        { get; set; }
          public string gender     { get; set; }
          public string location   { get; set; }
          public string email      { get; set; }
          public int    status     { get; set; }
        }


       public class Professional
       {
         public string occupation_type { get; set; }
         public string company_type    { get; set; }
         public string position_type   { get; set; }
         public int    no_of_projects  { get; set; }
       }

       public class Educational
       {
         public string uni_name    { get; set; }
         public int    pass_year   { get; set; }
         public string degree_name { get; set; } 
       }

    public class Skill
    {
        public int UXUI { get; set; }
        public int GraphicWeb { get; set; }
        public string FullStack { get; set; }
    }

    public class Project
    {
        public int no_of_projects { get; set; }
        public Detail[] details { get; set; }
       
    }

    public class Detail
    {
        public string title { get; set; }
        public string description { get; set; }
    }


    public class Test
    {
        public string technology { get; set; }
        public string score { get; set; }
        public string date { get; set; }
    }

    //public class TotalQuest
    //{
    //    public string Serial { get; set; }
    //    public string Statements { get; set; }
    //    public string Options { get; set; }
    //    public string Answers { get; set; }
    //    public string type { get; set; }
    //}


    public class ProjectDetail
    {
        public string title { get; set; }
        public string description { get; set; }
    }

}


