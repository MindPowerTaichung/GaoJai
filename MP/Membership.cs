using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPERP2015.MP
{
    public class Membership {
        
        MembershipModelContainer db = new MembershipModelContainer();

        public IEnumerable<User> GetUsers()
        {
            return db.Users;
        }
        public User GetUser(string userName )
        {
            return db.Users.Find(userName);
        }

        public bool CreateUser(string userName, string password, out string exceptionInfo)
        {
            bool succeed = false;
            exceptionInfo = "";

            User user = db.Users.Find(userName);
            if (user==null)
            {
                user = new User { UserName = userName, Password = password };
                db.Users.Add(user);
                try
                {
                    db.SaveChanges();
                    succeed = true;
                }
                catch (Exception ex)
                {
                    exceptionInfo = ex.Message;
                }
            }
            return succeed;
        }
        
        public bool ValidUser(string userName,string password)
        {
            bool isValid = false;
            
            var user = db.Users.Find(userName);
            if (user!=null && user.Password==password)
            {
                isValid = true;
            }
            return isValid;
        }
    }
}