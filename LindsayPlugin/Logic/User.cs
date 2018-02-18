using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LindsayPlugin.Logic
{
    public class User
    {
        public string ID { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public User(string id, string user, string mail)
        {
            ID = id;
            Username = user;
            Email = mail;
        }
    }
}