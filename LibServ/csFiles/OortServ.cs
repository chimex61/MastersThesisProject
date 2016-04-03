using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace LibServ
{
    public class OortServ
    {
        public Account oAcc { get; private set; }
        private string sUrlAuth = "https://my.oort.in/auth-system/authz/password";
        private string sUrlRest = "https://my.oort.in/rest-service/";
        private Dictionary<string, string> oCurlParameters = new Dictionary<string, string>();
        private Dictionary<string, string> oCurlParams = new Dictionary<string, string>();

        HttpClient oClient = new HttpClient();

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

        // Do poprawki, zła metoda dla HttpClient
        public async Task GetAccess()
        {
            oCurlParams.Add("Authorization", "Bearer " + oAcc.Token);

            var content = new FormUrlEncodedContent(oCurlParams);

            var resp = await oClient.PostAsync(sUrlRest, content);
            var json = await resp.Content.ReadAsStringAsync();

            Dictionary<string, string> m = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}
