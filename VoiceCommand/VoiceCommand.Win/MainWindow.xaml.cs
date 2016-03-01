using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VoiceCommand;

namespace VoiceCommand.Win
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Account oAccount;

        public MainWindow()
        {
            InitializeComponent();
        }

        bool CanLogIn()
        {
            bool isPass = false;
            bool isLogin = false;

            if (!string.IsNullOrWhiteSpace(USER_LOGIN.Text))
            {
                isLogin = true;
            }

            if (!string.IsNullOrWhiteSpace(USER_PASSWORD.Password))
            {
                isPass = true;
            }

            return isPass & isLogin;
        }

        private async void OnLogin(object sender, RoutedEventArgs e)
        {
            bool log = CanLogIn();

            if (log)
            {
                LOGIN_INFO.Content = "Logging ...";

                oAccount = new Account(USER_LOGIN.Text, USER_PASSWORD.Password);
                OortServ oServ = new OortServ(oAccount);
                await oServ.GetToken();

                if (oServ.oAcc.Token != null)
                {
                    LOGIN_INFO.Content = "Yours access token: " + oServ.oAcc.Token;
                }
                else
                {
                    LOGIN_INFO.Content = "Login or/and password is incorrect.";
                }
            }
            else
            {
                LOGIN_INFO.Content = "Put all credentials into proper fields.";
            }
        }
    }
}