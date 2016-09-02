using System;
using RestSharp.Portable;
using RestSharp.Portable.Authenticators;
using RestSharp.Portable.HttpClient;

namespace VoiceCommand
{
    public class IbmBlueMixServ
    {

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
                //Console.WriteLine( oResponse.Content );
               // oResponseStruct = Deserialize(oResponse);
            }
            catch ( Exception oException )
            {
                //Console.WriteLine( "Error: " + oException.Message );
            }

            return oResponseStruct;
        }
    }
}
