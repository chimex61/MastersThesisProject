using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;

namespace VoiceCommand
{
    public class OortServ
    {
        public Account m_Account { get; private set; }

        private string m_sUrlAuth = "https://my.oort.in/auth-system/authz/password";
        private string m_sUrlRest = "https://my.oort.in/rest-service/";
        private string m_sUrlDev = "https://my.oort.in/rest-service/v1/devices";
        private string m_sUrlGroups = "https:/my.oort.in/rest-service/v1/groups";

        private Dictionary<string, string> oCurlParameters = new Dictionary<string, string>();


        public OortServ(Account oAccount)
        {
            m_Account = oAccount;

            oCurlParameters.Add("username", oAccount.Username);
            oCurlParameters.Add("password", m_Account.Password);
            oCurlParameters.Add("grant_type", "password");
        }

        public async Task GetToken()
        {
            HttpClient client = new HttpClient();

            var content = new FormUrlEncodedContent(oCurlParameters);

            var resp = await client.PostAsync(m_sUrlAuth, content);
            var json = await resp.Content.ReadAsStringAsync();

            Dictionary<string, string> m = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            string accToken;

            if (m.TryGetValue("access_token", out accToken))
            {
                m_Account.Token = accToken;
            }
        }

        public async Task GetAccess()
        {
            try
            {
                var oClient = new RestClient(m_sUrlRest);
                var oRequest = new RestRequest();
                oRequest.AddHeader("Authorization", "Bearer " + m_Account.Token);

                await oClient.Execute(oRequest);
            }
            catch (Exception oException)
            {
                //Console.WriteLine("Error: " + oException.Message);
            }

        }

        public async Task GetDevices()
        {
            try
            {
                var oClient = new RestClient(m_sUrlDev);
                var oRequest = new RestRequest();
                oRequest.AddHeader("Authorization", "Bearer " + m_Account.Token);

                await oClient.Execute(oRequest);

            }
            catch (Exception oException)
            {
                //Console.WriteLine("Error: " + oException.Message);
            }
        }

    }
}
