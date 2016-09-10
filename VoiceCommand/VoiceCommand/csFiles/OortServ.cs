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

        // WARNING: HARDCODE
        public void MakeAction(ResponseContent oContent)
        {
            switch (oContent.Location)
            {
                case "kitchen":
                    // LED1 or SmartSocket
                    switch (oContent.Device)
                    {
                        case "light":
                            // LED1
                            var oLED1 = new SmartLED();
                            oLED1.DevID = "e95ff60e-bf28-4456-91d5-d8775a581ac9";
                            switch (oContent.Action)
                            {
                                case "on":
                                    oLED1.TurnOn(m_Account.Token);
                                    break;
                                case "off":
                                    oLED1.TurnOff(m_Account.Token);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "socket":
                            var oSocket = new SmartSocket();
                            oSocket.DevID = "a4009f2b-0014-4383-a34e-0d4192e4f139";
                            switch (oContent.Action)
                            {
                                case "on":
                                    oSocket.TurnOn(m_Account.Token);
                                    break;
                                case "off":
                                    oSocket.TurnOff(m_Account.Token);
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
                    switch (oContent.Action)
                    {
                        case "on":
                            oLED2.TurnOn(m_Account.Token);
                            break;
                        case "off":
                            oLED2.TurnOff(m_Account.Token);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    if (oContent.Device == "finder" &&
                        oContent.Action == "on")
                    {
                        var oFinder = new SmartFinder();
                        oFinder.DevID = "59e30ea0-d491-4d4e-9044-c58f4ccb1e05";
                        oFinder.TurnOn(m_Account.Token);
                    }
                    break;
            }

        }
    }
}
