using System;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TimHanewich.Bing.Maps;
using Newtonsoft.Json.Linq;

namespace TimHanewich.Bing.Maps.Routes
{
    public static class RoutesExtension
    {
        public static async Task<RouteResponse> RequestRouteAsync(this BingMapsApiHelper bmah, RouteRequest request)
        {
            //construct the url
            string url = "http://dev.virtualearth.net/REST/v1/Routes?wayPoint.1=" + request.Departure.Latitude.ToString() + "," + request.Departure.Longitude.ToString() + "&waypoint.2=" + request.Destination.Latitude.ToString() + "," + request.Destination.Longitude.ToString() + "&key=" + bmah.ApiKey;

            //Call
            HttpClient hc = new HttpClient();
            HttpResponseMessage resp = await hc.GetAsync(url);

            if (resp.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Bing Maps route request failed with code " + resp.StatusCode.ToString());
            }

            //Get the values
            string content = await resp.Content.ReadAsStringAsync();
            JObject jo = JObject.Parse(content);
            JArray ja_resourceSets = JArray.Parse(jo.Property("resourceSets").Value.ToString());
            if (ja_resourceSets.Count == 0)
            {
                throw new Exception("resourceSets had 0 contents in the response");
            }
            JObject jo_firstResourceSet = JObject.Parse(ja_resourceSets[0].ToString());
            JArray ja_resources = JArray.Parse(jo_firstResourceSet.Property("resources").Value.ToString());
            if (ja_resources.Count == 0)
            {
                throw new Exception("No resources found.");
            }
            JObject jo_firstResource = JObject.Parse(ja_resources[0].ToString());

            RouteResponse response = new RouteResponse();

            //Get travel distance
            JProperty property_travelDistance = jo_firstResource.Property("travelDistance");
            if (property_travelDistance != null)
            {
                if (property_travelDistance.Value.Type != JTokenType.Null)
                {
                    response.TravelDistance = Convert.ToSingle(property_travelDistance.Value.ToString());
                }
            }

            //Get travel duration
            JProperty property_travelDuration = jo_firstResource.Property("travelDuration");
            if (property_travelDuration != null)
            {
                if (property_travelDuration.Value.Type != JTokenType.Null)
                {
                    response.TravelDuration = Convert.ToInt32(property_travelDuration.Value.ToString());
                }
            }

            //Get travel duration traffic
            JProperty property_travelDurationTraffic = jo_firstResource.Property("travelDurationTraffic");
            if (property_travelDurationTraffic != null)
            {
                if (property_travelDurationTraffic.Value.Type != JTokenType.Null)
                {
                    response.TravelDurationTraffic = Convert.ToInt32(property_travelDurationTraffic.Value.ToString());
                }
            }

            return response;
            
        }
    }
}