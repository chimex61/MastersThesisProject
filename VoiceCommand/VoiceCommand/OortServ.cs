using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace VoiceCommand
{
    public class OortServ
    {
        public Account oAcc { get; private set; }
        private string sUrl = "https://my.oort.in/auth-system/authz/password";
        private Dictionary<string, string> oCurlParameters = new Dictionary<string, string>();


        public OortServ(Account oAccount)
        {
            oAcc = oAccount;

            oCurlParameters.Add("username", oAccount.Username);
            oCurlParameters.Add("password", oAcc.Password);
            oCurlParameters.Add("grant_type", "password");
        }

        public async Task GetToken()
        {
            HttpClient client = new HttpClient();

            var content = new FormUrlEncodedContent(oCurlParameters);

            var resp = await client.PostAsync(sUrl, content);
            var json = await resp.Content.ReadAsStringAsync();

            Dictionary<string, string> m = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            string accToken;

            if (m.TryGetValue("access_token", out accToken))
            {
                oAcc.Token = accToken;
            }
        }
    }
}
