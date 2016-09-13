using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using RestSharp;

namespace LibServ
{
    public class OortServ
    {
        public Account Account { get; private set; }
        
        private string m_sUrlAuth = "https://my.oort.in/auth-system/authz/password";
        private string m_sUrlRest = "https://my.oort.in/rest-service/";
        private string m_sUrlDev = "https://my.oort.in/rest-service/v1/devices";
        private string m_sUrlGroups = "https:/my.oort.in/rest-service/v1/groups";

        private Dictionary<string, string> m_oCurlParameters = new Dictionary<string, string>();        

        public OortServ( Account oAccount )
        {
            Account = oAccount;

            m_oCurlParameters.Add( "username", oAccount.Username );
            m_oCurlParameters.Add( "password", Account.Password );
            m_oCurlParameters.Add( "grant_type", "password" );
        }

        // Gets the access token from server
        public async Task GetToken()
        {
            try
            {
                HttpClient oClient = new HttpClient();

                var oContent = new FormUrlEncodedContent( m_oCurlParameters );
                var oResponse = await oClient.PostAsync( m_sUrlAuth, oContent );
                var oJson = await oResponse.Content.ReadAsStringAsync();

                Dictionary<string, string> oDeserializedObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(oJson);

                string sAccToken;

                if (oDeserializedObject.TryGetValue( "access_token", out sAccToken ))
                {
                    Account.Token = sAccToken;

                }
            }
            catch( Exception oException )
            {
                Console.WriteLine( "Error: " + oException.Message );
            }   
        }

        public async Task GetAccess()
        {
            try
            {
                var oClient = new RestClient( m_sUrlRest );
                var oRequest = new RestRequest();
                oRequest.AddHeader( "Authorization", "Bearer " + Account.Token );

                oClient.ExecuteAsync( oRequest, oResponse =>
                {
                    Console.WriteLine( oResponse.Content );
                });
            }
            catch( Exception oException )
            {
                Console.WriteLine( "Error: " + oException.Message );
            }
            
        }

        public async Task GetGroups()
        {
            try
            {
                var oClient = new RestClient( m_sUrlGroups );
                var oRequest = new RestRequest();
                oRequest.AddHeader( "Authorization", "Bearer " + Account.Token );

                oClient.ExecuteAsync( oRequest, oResponse =>
                {
                    Console.WriteLine(oResponse.Content );
                });
            }
            catch ( Exception oException )
            {
                Console.WriteLine( "Error: " + oException.Message );
            }
        }

        public async Task GetDevices()
        {
            try
            {
                var oClient = new RestClient( m_sUrlDev );
                var oRequest = new RestRequest();
                oRequest.AddHeader( "Authorization", "Bearer " + Account.Token );

                oClient.ExecuteAsync( oRequest, oResponse =>
                {
                    Console.WriteLine( oResponse.Content );
                });

            }
            catch ( Exception oException )
            {
                Console.WriteLine( "Error: " + oException.Message );
            }
        }

        // WARNING: HARDCODE
        public /*async Task*/ void MakeAction( ResponseContent oContent )
        {
            switch( oContent.Location )
            {
                case "kitchen":
                    // LED1 or SmartSocket
                    switch( oContent.Device )
                    {
                        case "light":
                            // LED1
                            var oLED1 = new SmartLED();
                            oLED1.DevID = "e95ff60e-bf28-4456-91d5-d8775a581ac9";
                            switch ( oContent.Action )
                            {
                                case "on":
                                    oLED1.TurnOn( Account.Token );

                                    if ( !( string.IsNullOrEmpty( oContent.Color ) ) )
                                    {
                                        oLED1.SetColor( Account.Token, WitAiServ.ColorToRgb( oContent.Color ) );
                                    }
                                    else if ( !( string.IsNullOrEmpty( oContent.Number.ToString() ) ) )
                                    {
                                        oLED1.SetBrightness( Account.Token, oContent.Number.ToString() );
                                    }

                                    break;
                                case "off":
                                    oLED1.TurnOff( Account.Token );
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "socket":
                            var oSocket = new SmartSocket();
                            oSocket.DevID = "a4009f2b-0014-4383-a34e-0d4192e4f139";
                            switch( oContent.Action )
                            {
                                case "on":
                                    oSocket.TurnOn( Account.Token );
                                    break;
                                case "off":
                                    oSocket.TurnOff( Account.Token );
                                    break;
                                default:
                                    break;
                            }
                            break;
                    }
                    break;
                case "living room":
                    // only LED2
                    var oLED2 = new SmartLED2();
                    oLED2.DevID = "dfc57b6a-0991-40aa-89e6-3ee598f7cb08";
                    switch( oContent.Action )
                    {
                        case "on":
                            oLED2.TurnOn( Account.Token );

                            if (!(string.IsNullOrEmpty(oContent.Color)))
                            {
                                oLED2.SetColor(Account.Token, WitAiServ.ColorToRgb(oContent.Color));
                            }
                            else if (!(string.IsNullOrEmpty(oContent.Number.ToString())))
                            {
                                oLED2.SetBrightness(Account.Token, oContent.Number.ToString());
                            }

                            break;
                        case "off":
                            oLED2.TurnOff( Account.Token );
                            break;
                        default:
                            break;
                    }
                  
                    break;
                default:
                    if( oContent.Device == "finder" &&
                        oContent.Action == "on" )
                    {
                        var oFinder = new SmartFinder();
                        oFinder.DevID = "59e30ea0-d491-4d4e-9044-c58f4ccb1e05";
                        oFinder.TurnOn( Account.Token );
                    }
                    break;
            }
                
        }
    }
}
