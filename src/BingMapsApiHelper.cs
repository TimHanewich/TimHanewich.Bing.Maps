using System;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http;


namespace TimHanewich.Bing.Maps
{
    public class BingMapsApiHelper
    {
        private string ApiKey;

        public BingMapsApiHelper(string api_key)
        {
            ApiKey = api_key;
        }
        
        #region "Imagery"

        public async Task<Stream> DownloadPushpinsImageryAsync(BingMapsPushpinsImageryRequest request)
        {
            string url = "https://dev.virtualearth.net/REST/v1/Imagery/Map/";

            //The imagery type
            url = url + request.ImagerySet.ToString().ToLower() + "?";

            //Add in all the pushpins
            foreach (Pushpin pp in request.Pushpins)
            {
                url = url + "pushpin=" + pp.AsUrlRequestComponent() + "&";
            }

            //Size of the image
            url = url + "mapsize=" + request.Width.ToString() + "," + request.Height.ToString() + "&";

            //Key
            url = url + "key=" + ApiKey;


            //Make the request and get the Stream
            HttpClient hc = new HttpClient();
            HttpResponseMessage hrm = await hc.GetAsync(url);
            if (hrm.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Request to the Bing Maps API failed.");
            }
            Stream s = await hrm.Content.ReadAsStreamAsync();
            return s;
        }

        #endregion

        #region "Misc Tools"
        /// <summary>
        /// Calculates the distance in meters between two points on Earth.
        /// </summary>
        static float HaversineDistance(float lat1, float lon1, float lat2, float lon2)
        {
            double earth_radius = 6371000;
            double n1 = lat1 * (Math.PI / 180);
            double n2 = lat2 * (Math.PI / 180);
            double d1 = (lat2 - lat1) * (Math.PI / 180);
            double d2 = (lon2 - lon1) * (Math.PI / 180);

            double a = (Math.Sin(d1/2) * Math.Sin(d1/2)) + (Math.Cos(n1) * Math.Cos(n2) * Math.Sin(d2/2) * Math.Sin(d2 / 2));
            double c = 2 * (Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a)));
            double d = earth_radius * c;
            
            return Convert.ToSingle(d);
        }
        #endregion

    }
}