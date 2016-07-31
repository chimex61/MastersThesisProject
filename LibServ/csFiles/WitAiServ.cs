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
using System.Collections.Specialized;

namespace LibServ
{
    public class WitAiServ
    {
        private string m_sAccessToken = "FJKWWROXJRT26AFUMVVI3C7JWZYRDESJ";
        private string m_sWitSpeechUrl = "https://api.wit.ai/speech?v=20160602";

        public /*async Task<ResponseContent>*/ ResponseContent ExecuteItem(byte[] ba_file)
        {
            var oResponseStruct = new ResponseContent();

            try
            {
                var oClient = new RestClient( m_sWitSpeechUrl );
                var oRequest = new RestRequest( Method.POST );
                oRequest.AddHeader( "--data-binary", "@record.wav" );
                oRequest.AddHeader( "content-type", "audio/wav" );
                oRequest.AddHeader("authorization", "Bearer " + m_sAccessToken );
                oRequest.AddParameter( "audio/wav", ba_file, ParameterType.RequestBody );
 
//                 oClient.ExecuteAsync( oRequest, oResponse =>
//                 {
//                     //Console.WriteLine( oResponse.Content );
//                     oResponseStruct = Deserialize( oResponse );
//                 });
                var oResponse = oClient.Execute(oRequest);
                oResponseStruct = Deserialize(oResponse);
            }
            catch( Exception oException )
            {
                Console.WriteLine( "Error: " + oException.Message );
            }

            return oResponseStruct;
        }

        public ResponseContent Deserialize(IRestResponse oResponse)
        {
            var oResponseStruct = new ResponseContent();
            var oDeserializer = new JsonDeserializer();
            var oDeserializedResponse = new RootObject();

            try
            {
                oDeserializedResponse = oDeserializer.Deserialize<RootObject>( oResponse );
                oResponseStruct.Device = oDeserializedResponse.entities.device[0].value;
                oResponseStruct.Location = oDeserializedResponse.entities.location[0].value;
                oResponseStruct.Action = oDeserializedResponse.entities.on_off[0].value;
                Console.WriteLine( oDeserializedResponse.entities.device[0].value );
                Console.WriteLine( oDeserializedResponse.entities.location[0].value );
                Console.WriteLine( oDeserializedResponse.entities.on_off[0].value );
            }
            catch( Exception oException )
            {
                Console.WriteLine( "Error: " + oException.Message );
            }

            return oResponseStruct;
        }
    }
}

