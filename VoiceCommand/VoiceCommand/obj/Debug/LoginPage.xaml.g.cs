//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VoiceCommand {
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    
    
    public partial class LoginPage : ContentPage {
        
        private Entry loginText;
        
        private Entry passwordText;
        
        private Button loginButton;
        
        private ActivityIndicator progressIndicator;
        
        private Label loginInfo;
        
        private void InitializeComponent() {
            this.LoadFromXaml(typeof(LoginPage));
            loginText = this.FindByName<Entry>("loginText");
            passwordText = this.FindByName<Entry>("passwordText");
            loginButton = this.FindByName<Button>("loginButton");
            progressIndicator = this.FindByName<ActivityIndicator>("progressIndicator");
            loginInfo = this.FindByName<Label>("loginInfo");
        }
    }
}
