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
using RestSharp.Deserializers;
using RestSharp.Authenticators;
using System.Collections.Specialized;

namespace LibServ
{
    public class IbmBlueMixServ
    {
        private string m_sIbmSpeechUrl = "https://stream.watsonplatform.net/speech-to-text/api/v1/recognize";

        public /*async Task<ResponseContent>*/ ResponseContent ExecuteItem(byte[] ba_file)
        {
            var oResponseStruct = new ResponseContent();

            try
            {
                var oClient = new RestClient( m_sIbmSpeechUrl );
                var oRequest = new RestRequest( Method.POST );
                oClient.Authenticator = new HttpBasicAuthenticator( "krzysztof.sommerrey@gmail.com", "oort2015" );
                oRequest.AddHeader( "--data-binary", "@record.wav" );
                oRequest.AddHeader( "content-type", "audio/wav" );
                oRequest.AddParameter( "audio/wav", ba_file, ParameterType.RequestBody );

                //                 oClient.ExecuteAsync( oRequest, oResponse =>
                //                 {
                //                     //Console.WriteLine( oResponse.Content );
                //                     oResponseStruct = Deserialize( oResponse );
                //                 });
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
