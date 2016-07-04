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
        private string sAccessToken = "FRJSRRJ3MLAYXAO2M6KGFCUPV43SLFLI";
        private string sWitSpeechUrl = "https://api.wit.ai/speech?v=20160602";

        public async Task SendItem( byte[] file )
        {

            try
            {
                var oClient = new RestClient( sWitSpeechUrl );
                var oRequest = new RestRequest( Method.POST );
                oRequest.AddHeader( "--data-binary", "@record.wav" );
                oRequest.AddHeader( "content-type", "audio/wav" );
                oRequest.AddHeader( "authorization", "Bearer " + sAccessToken );

                oRequest.AddParameter( "audio/wav", file, ParameterType.RequestBody );

//                 var response = client.Execute( request );
//                 //var json = await response.Content.ReadAsStringAsync();
//                 Console.WriteLine( response.Content );

                oClient.ExecuteAsync(oRequest, response =>
                {
                    Console.WriteLine(response.Content);
                });
            }
            catch( Exception ex )
            {
                Console.WriteLine( "Error: " + ex.Message );
            }           
        }
    }
}
