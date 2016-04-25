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
        public string DevID { get; set; }
        public string DevName { get; set; }
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

        public void TurnOn( string AccessToken )
        {
            string url = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/boolean/3";

            try
            {
                var client = new RestClient( url );
                var request = new RestRequest(Method.POST);
                request.AddHeader( "authorization", "Bearer " + AccessToken );
                request.AddHeader( "content-type", "application/json" );
                request.AddParameter( "application/json", "{\"state\": \"ON\"}", ParameterType.RequestBody );
                IRestResponse response = client.Execute( request );
            }
            catch (Exception ex)
            {
                Console.WriteLine( "Error: " + ex.Message ); 
            } 
        }

        public void TurnOff( string AccessToken )
        {
            string url = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/boolean/3";

            try
            {
                var client = new RestClient( url );
                var request = new RestRequest( Method.POST );
                request.AddHeader( "authorization", "Bearer " + AccessToken );
                request.AddHeader( "content-type", "application/json" );
                request.AddParameter( "application/json", "{\"state\": \"OFF\"}", ParameterType.RequestBody );
                IRestResponse response = client.Execute( request );
            }
            catch ( Exception ex )
            {
                Console.WriteLine( "Error: " + ex.Message );
            }
        }

        public void SetBrightness( string AccessToken, string Value )
        {
            string url = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/slide/7";

            try
            {
                var client = new RestClient( url );
                var request = new RestRequest( Method.POST );
                request.AddHeader( "authorization", "Bearer " + AccessToken );
                request.AddHeader( "content-type", "application/json" );
                request.AddParameter( "application/json", "{\"value\": \"" + Value + "\"}", ParameterType.RequestBody );
                IRestResponse response = client.Execute( request );
            }
            catch ( Exception ex )
            {
                Console.WriteLine( "Error: " + ex.Message );
            }
        }

        public void SetColor( string AccessToken, string Value )
        {
            string url = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/color/7";

            try
            {
                var client = new RestClient( url );
                var request = new RestRequest( Method.POST );
                request.AddHeader( "authorization", "Bearer " + AccessToken );
                request.AddHeader( "content-type", "application/json" );
                request.AddParameter( "application/json", "{\"rgb\": \"" + Value + "\"}", ParameterType.RequestBody );
                IRestResponse response = client.Execute( request );
            }
            catch ( Exception ex )
            {
                Console.WriteLine( "Error: " + ex.Message );
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


		public void TurnOn( string AccessToken )
		{
			string url = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/boolean/4";

			try
			{
				var client = new RestClient( url );
				var request = new RestRequest(Method.POST);
				request.AddHeader( "authorization", "Bearer " + AccessToken );
				request.AddHeader( "content-type", "application/json" );
				request.AddParameter( "application/json", "{\"state\": \"ON\"}", ParameterType.RequestBody );
				IRestResponse response = client.Execute( request );
			}
			catch (Exception ex)
			{
				Console.WriteLine( "Error: " + ex.Message ); 
			} 
		}

		public void TurnOff( string AccessToken )
		{
			string url = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/boolean/4";

			try
			{
				var client = new RestClient( url );
				var request = new RestRequest( Method.POST );
				request.AddHeader( "authorization", "Bearer " + AccessToken );
				request.AddHeader( "content-type", "application/json" );
				request.AddParameter( "application/json", "{\"state\": \"OFF\"}", ParameterType.RequestBody );
				IRestResponse response = client.Execute( request );
			}
			catch ( Exception ex )
			{
				Console.WriteLine( "Error: " + ex.Message );
			}
		}

		public void SetBrightness( string AccessToken, string Value )
		{
			string url = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/slide/6";

			try
			{
				var client = new RestClient( url );
				var request = new RestRequest( Method.POST );
				request.AddHeader( "authorization", "Bearer " + AccessToken );
				request.AddHeader( "content-type", "application/json" );
				request.AddParameter( "application/json", "{\"value\": \"" + Value + "\"}", ParameterType.RequestBody );
				IRestResponse response = client.Execute( request );
			}
			catch ( Exception ex )
			{
				Console.WriteLine( "Error: " + ex.Message );
			}
		}

		public void SetColor( string AccessToken, string Value )
		{
			string url = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/color/7";

			try
			{
				var client = new RestClient( url );
				var request = new RestRequest( Method.POST );
				request.AddHeader( "authorization", "Bearer " + AccessToken );
				request.AddHeader( "content-type", "application/json" );
				request.AddParameter( "application/json", "{\"rgb\": \"" + Value + "\"}", ParameterType.RequestBody );
				IRestResponse response = client.Execute( request );
			}
			catch ( Exception ex )
			{
				Console.WriteLine( "Error: " + ex.Message );
			}
		}

		public void SetWhiteTemperature( string AccessToken, string Value )
		{
			string url = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/slide/5";

			try
			{
				var client = new RestClient( url );
				var request = new RestRequest( Method.POST );
				request.AddHeader( "authorization", "Bearer " + AccessToken );
				request.AddHeader( "content-type", "application/json" );
				request.AddParameter( "application/json", "{\"value\": \"" + Value + "\"}", ParameterType.RequestBody );
				IRestResponse response = client.Execute( request );
			}
			catch ( Exception ex )
			{
				Console.WriteLine( "Error: " + ex.Message );
			}
		}
	}

    public class SmartFinder : Device
    {
        /*id 2, panelType Boolean, state OFF / ON*/
        public string BoolanState { get; set; }

        public void TurnOn( string AccessToken )
        {
            string url = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/boolean/2";

            try
            {
                var client = new RestClient( url );
                var request = new RestRequest( Method.POST );
                request.AddHeader( "authorization", "Bearer " + AccessToken );
                request.AddHeader( "content-type", "application/json" );
                request.AddParameter( "application/json", "{\"state\": \"ON\"}", ParameterType.RequestBody );
                IRestResponse response = client.Execute( request );
            }
            catch ( Exception ex )
            {
                Console.WriteLine( "Error: " + ex.Message ); 
            }
        }
    }

    public class SmartSocket : Device
    {
		/*id 3, panelType Boolean, state OFF / ON*/
		public string BoolanState { get; set; }

		public void TurnOn( string AccessToken )
		{
			string url = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/boolean/3";

			try
			{
				var client = new RestClient( url );
				var request = new RestRequest( Method.POST );
				request.AddHeader( "authorization", "Bearer " + AccessToken );
				request.AddHeader( "content-type", "application/json" );
				request.AddParameter( "application/json", "{\"state\": \"ON\"}", ParameterType.RequestBody );
				IRestResponse response = client.Execute( request );
			}
			catch ( Exception ex )
			{
				Console.WriteLine( "Error: " + ex.Message ); 
			}
		}

		public void TurnOff( string AccessToken )
		{
			string url = "https://my.oort.in/rest-service/v1/devices/" + DevID + "/panels/boolean/3";

			try
			{
				var client = new RestClient( url );
				var request = new RestRequest( Method.POST );
				request.AddHeader( "authorization", "Bearer " + AccessToken );
				request.AddHeader( "content-type", "application/json" );
				request.AddParameter( "application/json", "{\"state\": \"OFF\"}", ParameterType.RequestBody );
				IRestResponse response = client.Execute( request );
			}
			catch ( Exception ex )
			{
				Console.WriteLine( "Error: " + ex.Message ); 
			}
		}
    }

    public class Beacon : Device
    {

    }
}
