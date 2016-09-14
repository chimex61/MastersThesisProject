using System;
using System.Text;
using System.Net;
using System.IO;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;

namespace LibServ
{
    public class IbmBlueMixServ
    {

        public void ExecuteItem()
        {
            try
            {
                //string sFileName = "..//..//..//..//wav//record.wav";
                //FileStream fileStream = File.OpenRead("C:/Users/Tomek/Desktop/konwerter/probka1.wav");
                FileStream fileStream = File.OpenRead("..//..//..//..//wav//probka1.wav");
                //FileStream fileStream = File.OpenRead( sFileName );
                MemoryStream memoryStream = new MemoryStream();
                memoryStream.SetLength(fileStream.Length);
                fileStream.Read(memoryStream.GetBuffer(), 0, (int) fileStream.Length);
                byte[] BA_AudioFile = memoryStream.GetBuffer();
                HttpWebRequest _HWR_SpeechToText = null;
                _HWR_SpeechToText =
                    (HttpWebRequest)
                    HttpWebRequest.Create("https://stream.watsonplatform.net/speech-to-text/api/v1/recognize");
                string auth = string.Format("{0}:{1}", "bbac0b5a-58c3-42e5-b21c-53b848b3d287", "VqKg1YBaWod5");
                string auth64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(auth));
                string credentials = string.Format("{0} {1}", "Basic", auth64);

                _HWR_SpeechToText.Headers[HttpRequestHeader.Authorization] = credentials;
                _HWR_SpeechToText.Method = "POST";
                //_HWR_SpeechToText.ContentType = "audio/flac; rate=44100; channels=2;";
                _HWR_SpeechToText.ContentType = "audio/wav;";
                _HWR_SpeechToText.ContentLength = BA_AudioFile.Length;
                Stream stream = _HWR_SpeechToText.GetRequestStream();
                stream.Write(BA_AudioFile, 0, BA_AudioFile.Length);
                stream.Close();

                HttpWebResponse HWR_Response = (HttpWebResponse) _HWR_SpeechToText.GetResponse();
                if (HWR_Response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader SR_Response = new StreamReader(HWR_Response.GetResponseStream());
                    var result = SR_Response.ReadToEnd();
                    Console.WriteLine(result);
                    var JsonObject = Newtonsoft.Json.Linq.JObject.Parse(result);
                    double confidence = (double) JsonObject["results"][0]["alternatives"][0]["confidence"];
                    if (confidence <= 0.9)
                    {
                        Console.WriteLine("Watson knows what he has heard: " +
                                          (string) JsonObject["results"][0]["alternatives"][0]["transcript"]);
                        Console.WriteLine("Watson's confidence!: " +
                                          (string) JsonObject["results"][0]["alternatives"][0]["confidence"]);
                    }
                    else
                    {
                        Console.WriteLine("Watson thinks: " +
                                          (string) JsonObject["results"][0]["alternatives"][0]["transcript"]);
                        Console.WriteLine("Watson has this confidence: " +
                                          (string) JsonObject["results"][0]["alternatives"][0]["confidence"]);
                    }
                }
                stream.Close();
            }
            catch (Exception oException)
            {
                Console.WriteLine("Error: " + oException.Message);
            }
        }




        private string m_sIbmSpeechUrl = "https://stream.watsonplatform.net/speech-to-text/api/v1/recognize";

        public ResponseContent ExecuteItem(byte[] ba_file)
        {
            var oResponseStruct = new ResponseContent();

            try
            {
                var oClient = new RestClient(m_sIbmSpeechUrl);
                oClient.Authenticator = new HttpBasicAuthenticator("3e519458-b315-42d4-9544-c9f797b07b74",
                    "MFwrZxljCcyr");
                var oRequest = new RestRequest(Method.POST);
                oRequest.AddHeader("--data-binary", "@record.wav");
                oRequest.AddHeader("content-type", "audio/wav");
                oRequest.AddParameter("audio/wav", ba_file, ParameterType.RequestBody);

                var oResponse = oClient.Execute(oRequest);
                Console.WriteLine(oResponse.Content);
                oResponseStruct = Deserialize(oResponse);
            }
            catch (Exception oException)
            {
                Console.WriteLine("Error: " + oException.Message);
            }

            return oResponseStruct;
        }

        public ResponseContent Deserialize( IRestResponse oResponse )
        {
            var oResponseStruct = new ResponseContent();

            try
            {
                //
                // we have only transcript so we need to parse it
                //

                //Location
                if ( oResponse.Content.Contains( "kitchen" ) )
                {
                    oResponseStruct.Location = "kitchen";
                }
                else if ( oResponse.Content.Contains( "living room" ) )
                {
                    oResponseStruct.Location = "living room";
                }

                //Device
                if ( oResponse.Content.Contains( "light" ) ||
                    oResponse.Content.Contains("lights") )
                {
                    oResponseStruct.Device = "light";
                }
                else if ( oResponse.Content.Contains( "socket" ) )
                {
                    oResponseStruct.Device = "socket";
                }
                else if ( oResponse.Content.Contains( "finder" ) )
                {
                    oResponseStruct.Device = "finder";
                }

                //Action
                oResponseStruct.Action = oResponse.Content.Contains( "on" ) ? "on" : "off";

                //Color
                if ( oResponse.Content.Contains( "green" ) )
                {
                    oResponseStruct.Color = "green";
                }
                else if ( oResponse.Content.Contains("yellow" ) )
                {
                    oResponseStruct.Color = "yellow";
                }
                else if ( oResponse.Content.Contains("white" ) )
                {
                    oResponseStruct.Color = "white";
                }
                else if ( oResponse.Content.Contains("orange" ) )
                {
                    oResponseStruct.Color = "orange";
                }
                else if ( oResponse.Content.Contains("red" ) )
                {
                    oResponseStruct.Color = "red";
                }

                // Number
                if ( oResponse.Content.Contains( "hundred" ) )
                {
                    oResponseStruct.Number = 100;
                }
                else if ( oResponse.Content.Contains( "ninety" ) )
                {
                    oResponseStruct.Number = 90;
                }
                else if ( oResponse.Content.Contains( "eighty" ) )
                {
                    oResponseStruct.Number = 80;
                }
                else if ( oResponse.Content.Contains( "seventy" ) )
                {
                    oResponseStruct.Number = 70;
                }
                else if ( oResponse.Content.Contains( "sixty") )
                {
                    oResponseStruct.Number = 60;
                }
                else if ( oResponse.Content.Contains( "fifty" ) )
                {
                    oResponseStruct.Number = 50;
                }
                else if ( oResponse.Content.Contains( "forty" ) )
                {
                    oResponseStruct.Number = 40;
                }
                else if ( oResponse.Content.Contains( "thirty" ) )
                {
                    oResponseStruct.Number = 30;
                }
                else if ( oResponse.Content.Contains( "twenty" ) )
                {
                    oResponseStruct.Number = 20;
                }
                else if ( oResponse.Content.Contains( "ten" ) )
                {
                    oResponseStruct.Number = 10;
                }
                else if ( oResponse.Content.Contains( "zero" ) )
                {
                    oResponseStruct.Number = 0;
                }
                
            }
            catch ( Exception ex )
            {
                Console.WriteLine( "Error: " + ex.Message );
            }

            return oResponseStruct;
        }
    }
}
