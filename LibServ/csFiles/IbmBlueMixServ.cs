﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.IO;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Authenticators;
using System.Collections.Specialized;

namespace LibServ
{
    public class IbmBlueMixServ
    {
       
        public void ExecuteItem( )
        {
            try
            {
                //string sFileName = "..//..//..//..//wav//record.wav";
                //FileStream fileStream = File.OpenRead("C:/Users/Tomek/Desktop/konwerter/probka1.wav");
                FileStream fileStream = File.OpenRead("..//..//..//..//wav//probka1.wav");
                //FileStream fileStream = File.OpenRead( sFileName );
                MemoryStream memoryStream = new MemoryStream();
                memoryStream.SetLength(fileStream.Length);
                fileStream.Read(memoryStream.GetBuffer(), 0, (int)fileStream.Length);
                byte[] BA_AudioFile = memoryStream.GetBuffer();
                HttpWebRequest _HWR_SpeechToText = null;
                _HWR_SpeechToText =
                                    (HttpWebRequest)HttpWebRequest.Create("https://stream.watsonplatform.net/speech-to-text/api/v1/recognize");
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

                HttpWebResponse HWR_Response = (HttpWebResponse)_HWR_SpeechToText.GetResponse();
                if (HWR_Response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader SR_Response = new StreamReader(HWR_Response.GetResponseStream());
                    var result = SR_Response.ReadToEnd();
                    Console.WriteLine(result); var JsonObject = Newtonsoft.Json.Linq.JObject.Parse(result);
                    double confidence = (double)JsonObject["results"][0]["alternatives"][0]["confidence"];
                    if (confidence <= 0.9)
                    {
                        Console.WriteLine("Watson knows what he has heard: " + (string)JsonObject["results"][0]["alternatives"][0]["transcript"]);
                        Console.WriteLine("Watson's confidence!: " + (string)JsonObject["results"][0]["alternatives"][0]["confidence"]);
                    }
                    else
                    {
                        Console.WriteLine("Watson thinks: " + (string)JsonObject["results"][0]["alternatives"][0]["transcript"]);
                        Console.WriteLine("Watson has this confidence: " + (string)JsonObject["results"][0]["alternatives"][0]["confidence"]);
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

        public ResponseContent ExecuteItem( byte[] ba_file )
        {
            var oResponseStruct = new ResponseContent();

            try
            {
                var oClient = new RestClient( m_sIbmSpeechUrl );
                oClient.Authenticator = new HttpBasicAuthenticator( "bbac0b5a-58c3-42e5-b21c-53b848b3d287", "VqKg1YBaWod5" );
                var oRequest = new RestRequest( Method.POST );
                oRequest.AddHeader( "--data-binary", "@record.wav" );
                oRequest.AddHeader( "content-type", "audio/wav" );
                oRequest.AddParameter( "audio/wav", ba_file, ParameterType.RequestBody );

                var oResponse = oClient.Execute( oRequest );
                Console.WriteLine( oResponse.Content );
               // oResponseStruct = Deserialize(oResponse);
            }
            catch ( Exception oException )
            {
                Console.WriteLine( "Error: " + oException.Message );
            }

            return oResponseStruct;
        }
    }
}
