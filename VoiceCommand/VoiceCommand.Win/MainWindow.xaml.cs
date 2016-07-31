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
using System.Runtime.InteropServices; //P/Invoke
using LibServ;

namespace VoiceCommand.Win
{
    public partial class MainWindow : Window
    {
        private Account m_oAccount;

        public MainWindow()
        {
            InitializeComponent();
        }

        bool CanLogIn()
        {
            bool bPassword = false;
            bool bLogin = false;

            if ( !string.IsNullOrWhiteSpace(USER_LOGIN.Text) )
            {
                bLogin = true;
            }

            if (!string.IsNullOrWhiteSpace(USER_PASSWORD.Password))
            {
                bPassword = true;
            }

            return bPassword & bLogin;
        }

        private async void OnLogin( object sender, RoutedEventArgs e )
        {
            if ( CanLogIn() )
            {
                LOGIN_INFO.Content = "Logging ...";
                progressIndicator.Visibility = System.Windows.Visibility.Visible;

                m_oAccount = new Account( USER_LOGIN.Text, USER_PASSWORD.Password );
                OortServ oServ = new OortServ( m_oAccount );
                await oServ.GetToken();

                if ( oServ.Account.Token != null )
                {
                    LOGIN_INFO.Content = "Logged";
                    progressIndicator.Visibility = System.Windows.Visibility.Hidden;

                    oServ.GetAccess();
                    //oServ.GetDevices();
                    HideComponents();
                    _NavigationFrame.Navigate( new MainPage( oServ ));
                }
                else
                {
                    LOGIN_INFO.Content = "Login or/and password is incorrect.";
                    progressIndicator.Visibility = System.Windows.Visibility.Hidden;
                }
            }
            else
            {
                LOGIN_INFO.Content = "Put all credentials into proper fields.";
            }
        }

        private void HideComponents()
        {
            LOGIN_LABEL.Visibility = System.Windows.Visibility.Hidden;
            USER_LOGIN.Visibility = System.Windows.Visibility.Hidden;
            PASS_LABEL.Visibility = System.Windows.Visibility.Hidden;
            USER_PASSWORD.Visibility = System.Windows.Visibility.Hidden;
            LOG_IN_BUTTON.Visibility = System.Windows.Visibility.Hidden;
            progressIndicator.Visibility = System.Windows.Visibility.Hidden;
            LOGIN_INFO.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}