
namespace LibServ
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
