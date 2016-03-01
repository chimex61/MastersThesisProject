using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceCommand
{
    public class Account
    {
        public Account(string sLogin, string sPassword)
        {
            Username = sLogin;
            Password = sPassword;
        }

        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Token { get; set; }
    }
}
