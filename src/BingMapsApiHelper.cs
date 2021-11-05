using System;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http;


namespace TimHanewich.Bing.Maps
{
    public class BingMapsApiHelper
    {
        private string _ApiKey;

        public BingMapsApiHelper(string api_key)
        {
            _ApiKey = api_key;
        }
        
        public string ApiKey
        {
            get
            {
                return _ApiKey;
            }
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
            url = url + "key=" + _ApiKey;


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
        public static float HaversineDistance(float lat1, float lon1, float lat2, float lon2)
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

        #region "Elevation"

        public async Task<float> GetElevationMetersAsync(float latitude, float longitude)
        {
            string url = "http://dev.virtualearth.net/REST/v1/Elevation/List?points=" + latitude.ToString() + "," + longitude.ToString() + "&key=" + _ApiKey;
            HttpClient hc = new HttpClient();
            HttpResponseMessage hrm = await hc.GetAsync(url);
            string web = await hrm.Content.ReadAsStringAsync();

            //Strip out the elevation
            int loc1 = web.IndexOf("elevations");
            if (loc1 == -1)
            {
                throw new Exception("Fatal failure while acquiring elevation data for point " + latitude.ToString() + ", " + longitude.ToString());
            }
            loc1 = web.IndexOf("[", loc1 + 1);
            int loc2 = web.IndexOf("]", loc1 + 1);
            string num = web.Substring(loc1 + 1, loc2 - loc1 - 1);
            float val = Convert.ToSingle(num);

            return val;
        }

        #endregion
    }
}