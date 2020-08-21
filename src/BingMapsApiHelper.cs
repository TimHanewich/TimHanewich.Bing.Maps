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
    
    }
}