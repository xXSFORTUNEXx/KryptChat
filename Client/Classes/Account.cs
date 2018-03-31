using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace Client
{
    public class Account
    {
        public int ID;
        public string Name;
        public string Password;
        public string EmailAddress;
        public string LastLogin;
        public string AccountKey;
        public NetConnection netConnection;

        public Account() { }

        public Account(string name, string pass, string emailAddress)
        {
            Name = name;
            Password = pass;
            EmailAddress = emailAddress;
        }

        public Account(string name, string pass, string emailAddress, string lastLogin, string accountKey)
        {
            Name = name;
            Password = pass;
            EmailAddress = emailAddress;
            LastLogin = lastLogin;
            AccountKey = accountKey;
        }
    }
}
