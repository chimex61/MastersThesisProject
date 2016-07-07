using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.IO;
using RestSharp;
using System.Collections.Specialized;

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

                /*
                 * Trzeba przeszukać json'a i odnaleźć ID poszczególnych urządzeń.
                 * ID jest potrzebne, żeby później odwoływać się do określonego urządzenia i nim zarządzać.
                 */
            }
            catch ( Exception oException )
            {
                Console.WriteLine( "Error: " + oException.Message );
            }
        }
    }
}
