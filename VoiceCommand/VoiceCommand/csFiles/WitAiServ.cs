﻿using System;
using RestSharp.Portable.Deserializers;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System.Threading.Tasks;
using System.IO;

namespace VoiceCommand
{
    public class WitAiServ
    {
        private string m_sAccessToken = "FJKWWROXJRT26AFUMVVI3C7JWZYRDESJ";
        private string m_sWitSpeechUrl = "https://api.wit.ai/speech?v=20160602";

        public async Task<ResponseContent> ExecuteItem(byte[] ba_file)
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

                var oResponse = await oClient.Execute(oRequest);
                oResponseStruct = await Deserialize(oResponse);
            }
            catch( Exception oException )
            {
                //Console.WriteLine( "Error: " + oException.Message );
            }

            return oResponseStruct;
        }

        public async Task<ResponseContent> Deserialize(IRestResponse oResponse)
        {
            var oResponseStruct = new ResponseContent();
            var oDeserializer = new JsonDeserializer();
            var oDeserializedResponse = new RootObject();
            string tmp = oResponse.Content;
            try
            {
                oDeserializedResponse = oDeserializer.Deserialize<RootObject>( oResponse );
                oResponseStruct.Device = oDeserializedResponse.entities.device[0].value;
                oResponseStruct.Location = oDeserializedResponse.entities.location[0].value;
                oResponseStruct.Action = oDeserializedResponse.entities.on_off[0].value;
            }
            catch( Exception oException )
            {
                //Console.WriteLine( "Error: " + oException.Message );
            }

            return oResponseStruct;
        }
    }
}

