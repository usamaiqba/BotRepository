using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace SmbiBotApp.Model
{

    public class webhoook
    {
        public Datum[] data { get; set; }
    }

    public class Datum
    {
        public Get_Started get_started { get; set; }
    }

    public class Get_Started
    {
        public string payload { get; set; }
    }

}