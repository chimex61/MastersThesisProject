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
        public Account oAcc { get; private set; }
        private string sUrlAuth = "https://my.oort.in/auth-system/authz/password";
        private string sUrlRest = "https://my.oort.in/rest-service/";
        private Dictionary<string, string> oCurlParameters = new Dictionary<string, string>();
        private Dictionary<string, string> oCurlParams = new Dictionary<string, string>();

        

        public OortServ(Account oAccount)
        {
            oAcc = oAccount;

            oCurlParameters.Add("username", oAccount.Username);
            oCurlParameters.Add("password", oAcc.Password);
            oCurlParameters.Add("grant_type", "password");
        }

        // Gets the access token from server
        public async Task GetToken()
        {
            HttpClient oClient = new HttpClient();

            var content = new FormUrlEncodedContent(oCurlParameters);

            var resp = await oClient.PostAsync(sUrlAuth, content);
            var json = await resp.Content.ReadAsStringAsync();

            Dictionary<string, string> m = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            string accToken;

            if (m.TryGetValue("access_token", out accToken))
            {
                oAcc.Token = accToken;
            }
        }

        public async Task GetAccess()
        {
            try
            {
                /*
                HttpClient oClient = new HttpClient();
                var content = new FormUrlEncodedContent(oCurlParams);

                oClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + oAcc.Token);

                var resp = await oClient.PostAsync(sUrlRest, content);

                var json = await resp.Content.ReadAsStringAsync();
                */
                var oClient = new RestClient(sUrlRest);
                var request = new RestRequest();
                request.AddHeader("Authorization", "Bearer " + oAcc.Token);

                //var response = oClient.Execute(request);
                //var content = response.Content;

                oClient.ExecuteAsync(request, response =>
                {
                    Console.WriteLine(response.Content);
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            
        }
    }

}
