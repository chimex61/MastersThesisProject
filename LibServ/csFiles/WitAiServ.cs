using System;
using RestSharp;
using RestSharp.Deserializers;

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
                oResponseStruct.Action = oDeserializedResponse.entities.on_off[0].value;
                oResponseStruct.Location = oDeserializedResponse.entities.location[0].value;

                if ( oResponse.Content.Contains( "color" ) )
                {
                    oResponseStruct.Color = oDeserializedResponse.entities.color[0].value;
                }
                else if ( oResponse.Content.Contains( "number" ) )
                {
                    oResponseStruct.Number = oDeserializedResponse.entities.number[0].value;
                }
            }
            catch( Exception oException )
            {
                Console.WriteLine( "Error: " + oException.Message );
            }

            return oResponseStruct;
        }

        public static string ColorToRgb(string color)
        {
            string sReturnColor = "ffffff";

            if ( color == "red" )
            {
                sReturnColor = "ff0000";
            }
            else if ( color == "green" )
            {
                sReturnColor = "006600";
            }
            else if (color == "yellow")
            {
                sReturnColor = "ffd11a";
            }
            else if (color == "white")
            {
                sReturnColor = "ffffff";
            }
            else if (color == "orange")
            {
                sReturnColor = "ff7700";
            }

            return sReturnColor;
        }
    }
}

