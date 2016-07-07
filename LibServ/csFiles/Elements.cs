using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace LibServ
{
    public class Device
    {
        // ID of the device
        public string DevID { get; set; }

        // Name of the device
        public string DevName { get; set; }

        // True if the device is in SmartHub range
        public bool InRange { get; set; }

        // Type of the device
        // In our set there are four types:
        // - OORT SmartFinder
        // - OORT SmartLED
        // - OORT SmartLED2
        // - OORT SmartSocket
        public string DevType { get; set; }

        /*
        * There is need to recognize the dev type when user wants to
        * perform some action - every device has a different panel number
        */
    }

    public class SmartLED : Device
    {
        /*id 3, panelType Boolean, state OFF / ON*/
        public string BoolanState { get; set; }

        /*id 7, panelType Color, rgb 0065ff */
        public string ColorValue { get; set; }
       
        /*id 7, panelType Slide, slideType Brightness, value 0-100 */
        public string BrightnessValue { get; set; }

//         functions:
//         turn on
//         turn off
//         set brightness
//         set color

        public void TurnOn( string sAccessToken )
        {
            string sUrl = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/boolean/3";

            try
            {
                var oClient = new RestClient( sUrl );
                var oRequest = new RestRequest( Method.POST );
                oRequest.AddHeader( "authorization", "Bearer " + sAccessToken );
                oRequest.AddHeader( "content-type", "application/json" );
                oRequest.AddParameter( "application/json", "{\"state\": \"ON\"}", ParameterType.RequestBody );
                IRestResponse oResponse = oClient.Execute( oRequest );
            }
            catch ( Exception oException )
            {
                Console.WriteLine( "Error: " + oException.Message ); 
            } 
        }

        public void TurnOff( string sAccessToken )
        {
            string sUrl = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/boolean/3";

            try
            {
                var oClient = new RestClient( sUrl );
                var oRequest = new RestRequest( Method.POST );
                oRequest.AddHeader( "authorization", "Bearer " + sAccessToken );
                oRequest.AddHeader( "content-type", "application/json" );
                oRequest.AddParameter( "application/json", "{\"state\": \"OFF\"}", ParameterType.RequestBody );
                IRestResponse oResponse = oClient.Execute( oRequest );
            }
            catch ( Exception oException )
            {
                Console.WriteLine( "Error: " + oException.Message );
            }
        }

        public void SetBrightness( string sAccessToken, string sValue )
        {
            string sUrl = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/slide/7";

            try
            {
                var oClient = new RestClient( sUrl );
                var oRequest = new RestRequest( Method.POST );
                oRequest.AddHeader( "authorization", "Bearer " + sAccessToken );
                oRequest.AddHeader( "content-type", "application/json" );
                oRequest.AddParameter( "application/json", "{\"value\": \"" + sValue + "\"}", ParameterType.RequestBody );
                IRestResponse oResponse = oClient.Execute( oRequest );
            }
            catch ( Exception oException )
            {
                Console.WriteLine( "Error: " + oException.Message );
            }
        }

        public void SetColor( string sAccessToken, string sValue )
        {
            string sUrl = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/color/7";

            try
            {
                var oClient = new RestClient( sUrl );
                var oRequest = new RestRequest( Method.POST );
                oRequest.AddHeader( "authorization", "Bearer " + sAccessToken );
                oRequest.AddHeader( "content-type", "application/json" );
                oRequest.AddParameter( "application/json", "{\"rgb\": \"" + sValue + "\"}", ParameterType.RequestBody );
                IRestResponse oResponse = oClient.Execute( oRequest );
            }
            catch ( Exception oException )
            {
                Console.WriteLine( "Error: " + oException.Message );
            }
        }
    }

	public class SmartLED2 : Device
	{
		/*id 4, panelType Boolean, state OFF / ON*/
		public string BoolanState { get; set; }

		/*id 7, panelType Color, rgb 0065ff */
		public string ColorValue { get; set; }

		/*id 6, panelType Slide, slideType Brightness, value 0-100 */
		public string BrightnessValue { get; set; }

		/*id 5, panelType Slide, slideType White Temperature, value 0-100 */
		public string WhiteTemperatureValue { get; set; }


		public void TurnOn( string sAccessToken )
		{
			string sUrl = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/boolean/4";

			try
			{
				var oClient = new RestClient( sUrl );
				var oRequest = new RestRequest(Method.POST);
				oRequest.AddHeader( "authorization", "Bearer " + sAccessToken );
				oRequest.AddHeader( "content-type", "application/json" );
				oRequest.AddParameter( "application/json", "{\"state\": \"ON\"}", ParameterType.RequestBody );
				IRestResponse oResponse = oClient.Execute( oRequest );
			}
			catch ( Exception oException )
			{
				Console.WriteLine( "Error: " + oException.Message ); 
			} 
		}

		public void TurnOff( string sAccessToken )
		{
			string sUrl = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/boolean/4";

			try
			{
				var oClient = new RestClient( sUrl );
				var oRequest = new RestRequest( Method.POST );
				oRequest.AddHeader( "authorization", "Bearer " + sAccessToken );
				oRequest.AddHeader( "content-type", "application/json" );
				oRequest.AddParameter( "application/json", "{\"state\": \"OFF\"}", ParameterType.RequestBody );
				IRestResponse oResponse = oClient.Execute( oRequest );
			}
			catch ( Exception oException )
			{
				Console.WriteLine( "Error: " + oException.Message );
			}
		}

		public void SetBrightness( string sAccessToken, string sValue )
		{
			string sUrl = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/slide/6";

			try
			{
				var oClient = new RestClient( sUrl );
				var oRequest = new RestRequest( Method.POST );
				oRequest.AddHeader( "authorization", "Bearer " + sAccessToken );
				oRequest.AddHeader( "content-type", "application/json" );
				oRequest.AddParameter( "application/json", "{\"value\": \"" + sValue + "\"}", ParameterType.RequestBody );
				IRestResponse oResponse = oClient.Execute( oRequest );
			}
			catch ( Exception oException )
			{
				Console.WriteLine( "Error: " + oException.Message );
			}
		}

		public void SetColor( string sAccessToken, string sValue )
		{
			string sUrl = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/color/7";

			try
			{
				var oClient = new RestClient( sUrl );
				var oRequest = new RestRequest( Method.POST );
				oRequest.AddHeader( "authorization", "Bearer " + sAccessToken );
				oRequest.AddHeader( "content-type", "application/json" );
				oRequest.AddParameter( "application/json", "{\"rgb\": \"" + sValue + "\"}", ParameterType.RequestBody );
				IRestResponse oResponse = oClient.Execute( oRequest );
			}
			catch ( Exception oException )
			{
				Console.WriteLine( "Error: " + oException.Message );
			}
		}

		public void SetWhiteTemperature( string sAccessToken, string sValue )
		{
			string sUrl = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/slide/5";

			try
			{
				var oClient = new RestClient( sUrl );
				var oRequest = new RestRequest( Method.POST );
				oRequest.AddHeader( "authorization", "Bearer " + sAccessToken );
				oRequest.AddHeader( "content-type", "application/json" );
				oRequest.AddParameter( "application/json", "{\"value\": \"" + sValue + "\"}", ParameterType.RequestBody );
				IRestResponse oResponse = oClient.Execute( oRequest );
			}
			catch ( Exception oException )
			{
				Console.WriteLine( "Error: " + oException.Message );
			}
		}
	}

    public class SmartFinder : Device
    {
        /*id 2, panelType Boolean, state OFF / ON*/
        public string BoolanState { get; set; }

        public void TurnOn( string sAccessToken )
        {
            string sUrl = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/boolean/2";

            try
            {
                var oClient = new RestClient( sUrl );
                var oRequest = new RestRequest( Method.POST );
                oRequest.AddHeader( "authorization", "Bearer " + sAccessToken );
                oRequest.AddHeader( "content-type", "application/json" );
                oRequest.AddParameter( "application/json", "{\"state\": \"ON\"}", ParameterType.RequestBody );
                IRestResponse oResponse = oClient.Execute( oRequest );
            }
            catch ( Exception oException )
            {
                Console.WriteLine( "Error: " + oException.Message ); 
            }
        }
    }

    public class SmartSocket : Device
    {
		/*id 3, panelType Boolean, state OFF / ON*/
		public string BoolanState { get; set; }

		public void TurnOn( string sAccessToken )
		{
			string sUrl = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/boolean/3";

			try
			{
				var oClient = new RestClient( sUrl );
				var oRequest = new RestRequest( Method.POST );
				oRequest.AddHeader( "authorization", "Bearer " + sAccessToken );
				oRequest.AddHeader( "content-type", "application/json" );
				oRequest.AddParameter( "application/json", "{\"state\": \"ON\"}", ParameterType.RequestBody );
				IRestResponse oResponse = oClient.Execute( oRequest );
			}
			catch ( Exception oException )
			{
				Console.WriteLine( "Error: " + oException.Message ); 
			}
		}

		public void TurnOff( string sAccessToken )
		{
			string sUrl = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/boolean/3";

			try
			{
				var oClient = new RestClient( sUrl );
				var oRequest = new RestRequest( Method.POST );
				oRequest.AddHeader( "authorization", "Bearer " + sAccessToken );
				oRequest.AddHeader( "content-type", "application/json" );
				oRequest.AddParameter( "application/json", "{\"state\": \"OFF\"}", ParameterType.RequestBody );
				IRestResponse oResponse = oClient.Execute( oRequest );
			}
			catch ( Exception oException )
			{
				Console.WriteLine( "Error: " + oException.Message ); 
			}
		}
    }

    public class Group
    {
        // ID of the group
        public string GroupID { get; set; }

        // Name of the group
        public string GroupName { get; set; }

        // List of the devices in the group
        // To perform any action you have to call
        // every device separately
        public List<string> DevList { get; set; }
    }
}
