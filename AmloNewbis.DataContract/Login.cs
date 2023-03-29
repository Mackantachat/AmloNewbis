using System;
using System.Collections.Generic;
using System.Text;

namespace AmloNewbis.DataContract
{
    public class Login
    {
        public string accessToken { get; set; }
        public string userID { get; set; }
        //public string password { get; set; }
        public string fullname { get; set; }
        public string startCommand { get; set; }
        public string applicationID { get; set; }
        public string rnd { get; set; }
    }

}
