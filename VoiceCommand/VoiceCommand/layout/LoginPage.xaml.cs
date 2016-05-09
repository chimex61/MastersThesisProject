using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VoiceCommand
{
    public partial class LoginPage : ContentPage
    {
        private Account oAccount;

        public LoginPage()
        {
            InitializeComponent();
        }

        bool CanLogIn()
        {
            bool isPass = false;
            bool isLogin = false;

            if ( !string.IsNullOrWhiteSpace( loginText.Text ) )
            {
                isLogin = true;
            }

            if ( !string.IsNullOrWhiteSpace( passwordText.Text ) )
            {
                isPass = true;
            }

            return isPass & isLogin;
        }

        async void OnLogin(object sender, EventArgs e)
        {
            bool log = CanLogIn(); //sprawdzenie, czy pola login i pass nie sa puste

            if (log)
            {
                loginInfo.Text = "Logging ...";
                loginInfo.IsVisible = true;
                progressIndicator.IsVisible = true;
                progressIndicator.IsRunning = true;

                oAccount = new Account( loginText.Text.Trim(), passwordText.Text );
                OortServ oServ = new OortServ( oAccount );
                await oServ.GetToken();

                if ( oServ.oAcc.Token != null )
                {
                    //loginInfo.Text = "Yours access token: " + oServ.oAcc.Token;
                    loginInfo.Text = "Logged";

                    await Navigation.PushModalAsync( new VoiceCommand.MainPage() );

                    loginInfo.IsVisible = false;
                    progressIndicator.IsVisible = false;
                }
                else
                {
                    loginInfo.Text = "Login or/and password is incorrect.";
                    progressIndicator.IsVisible = false;
                }
            }
            else
            {
                loginInfo.Text = "Put all credentials into proper fields.";
                loginInfo.IsVisible = true;
            }
        }
    }
}
