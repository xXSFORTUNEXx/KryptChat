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

        public Account(int id, string name, NetConnection connection)
        {
            ID = id;
            Name = name;
            netConnection = connection;
        }

        public Account(int id, string name, string pass, string emailAddress, NetConnection connection)
        {
            ID = id;
            Name = name;
            Password = pass;
            EmailAddress = emailAddress;
            netConnection = connection;
        }

        public Account(int id, string name, string pass, string emailAddress, string lastLogin, string accountKey, NetConnection connection)
        {
            ID = id;
            Name = name;
            Password = pass;
            EmailAddress = emailAddress;
            LastLogin = lastLogin;
            AccountKey = accountKey;
            netConnection = connection;
        }
    }
}
