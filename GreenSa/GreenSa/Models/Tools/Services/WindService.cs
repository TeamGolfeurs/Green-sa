using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace GreenSa.Models.Tools.Services
{
    public class WindService : Service
    {
        public const string latStJacques = "48.067";
        public const string lonStJacques = "-1.748";
        private const string url = "http://api.openweathermap.org/data/2.5/weather?APPID=";
        private const string appid = "1845b50acc4f9dabca4feb5b3d55fc68";
        /**
         * Methode allant chercher des infos sur une API web pour retrouver des infos concernant le vent.
         *
         */
        /*async*/
        public async Task<WindInfo> getCurrentWindInfo()
        {
            if (!isAvaible()) throw new NotAvaibleException();
            string fullUrl = url + appid + "&lat=" + latStJacques + "&lon=" + lonStJacques;

            WindJson wind = await FetchData(fullUrl);

            ImageSource img = ImageSource.FromResource("GreenSa.Ressources.Images.left-arrow.png");
            return new WindInfo(wind.windSpeed, wind.windDirection, img);
        }

        private async Task<WindJson> FetchData(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        string json = streamReader.ReadToEnd();
                        Debug.WriteLine("JSON : "+json);
                        JsonTextReader reader = new JsonTextReader(new StringReader(json));
                        WindJson wJson = new WindJson();
                        while (reader.Read())
                        {
                            if(reader.Value != null && reader.TokenType.ToString() == "PropertyName")
                            {   
                                //if(reader.TokenType.ToString() == "String")
                                Debug.WriteLine("Token : "+reader.Value);
                                switch(((string) reader.Value)) {
                                    case "speed":
                                        reader.Read();
                                        Debug.WriteLine("Value : " + reader.Value);
                                        wJson.windSpeed = (Convert.ToDouble(reader.Value));
                                        break;
                                    case "deg":
                                        reader.Read();
                                        Debug.WriteLine("Value : " + reader.Value);
                                        wJson.windDirection = (Convert.ToDouble(reader.Value));
                                        break;
                                }
                            }
                        }
                        return wJson;
                        
                    }
                }
            }
            return new WindJson();
        }



        public static bool isAvaible()
        {
            return true;
            throw new NotImplementedException();
        }


    }
}
