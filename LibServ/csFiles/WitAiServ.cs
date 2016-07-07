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
    public class WitAiServ
    {
        private string m_sAccessToken = "FJKWWROXJRT26AFUMVVI3C7JWZYRDESJ";
        private string m_sWitSpeechUrl = "https://api.wit.ai/speech?v=20160602";

        public async Task SendItem( byte[] ba_file )
        {

            try
            {
                var oClient = new RestClient( m_sWitSpeechUrl );
                var oRequest = new RestRequest( Method.POST );
                oRequest.AddHeader( "--data-binary", "@record.wav" );
                oRequest.AddHeader( "content-type", "audio/wav" );
                oRequest.AddHeader("authorization", "Bearer " + m_sAccessToken );
                oRequest.AddParameter( "audio/wav", ba_file, ParameterType.RequestBody );
 
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
    }
}
