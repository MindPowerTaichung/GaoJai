using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPERP2015.MP
{
    public class UserPasswordViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string TimestampString { get; set; }
    }
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string TimestampString { get; set; }
    }
}