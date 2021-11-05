using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TimHanewich.Bing.Maps.Locations
{
    public static class LocationsExtensions
    {
        public static async Task<LocationsResponse> GetLocationsAsync(this BingMapsApiHelper bmah, LocationsRequest req)
        {
            //Format the url
            string PostalCodePart = ""; //will be blank if there isn't one.
            if (req.PostalCode.HasValue)
            {
                PostalCodePart = "&postalCode=" + req.PostalCode.Value.ToString();
            }
            string url = "http://dev.virtualearth.net/REST/v1/Locations?addressLine=" + req.Address + PostalCodePart + "&key=ApCFwArsTxtHr9sct9Vq7gEIQ8aeP8oHaAwowqXkB7EqnYOUbGpDlMVWkgpSCCvJ";
        
            //Request
            HttpClient hc = new HttpClient();
            HttpResponseMessage resp = await hc.GetAsync(url);

            if (resp.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Response form Bing Maps API was " + resp.StatusCode.ToString());
            }

            //Get the content
            string content = await resp.Content.ReadAsStringAsync();
            JObject jo = JObject.Parse(content);

            //Get each
            JArray ja_resourceSets = JArray.Parse(jo.Property("resourceSets").Value.ToString());
            if (ja_resourceSets.Count == 0)
            {
                throw new Exception("resourceSets had 0 children.");
            }

            //Get resources
            JObject jo_firstResourceSet = JObject.Parse(ja_resourceSets[0].ToString());
            JArray ja_resources = JArray.Parse(jo_firstResourceSet.Property("resources").Value.ToString());

            //Parse each location
            List<LocationResult> ToReturnResults = new List<LocationResult>();
            foreach (JObject jo_location in ja_resources)
            {
                LocationResult lr = new LocationResult();

                //Get the points
                JObject jo_point = JObject.Parse(jo_location.Property("point").Value.ToString());
                JArray ja_coordinates = JArray.Parse(jo_point.Property("coordinates").Value.ToString());
                lr.Latitude = Convert.ToSingle(ja_coordinates[0].ToString());
                lr.Longitude = Convert.ToSingle(ja_coordinates[1].ToString());

                //Get the address components
                JObject jo_address = JObject.Parse(jo_location.Property("address").Value.ToString());
                lr.AddressLine = jo_address.Property("addressLine").Value.ToString();
                lr.AdminDistrict = jo_address.Property("adminDistrict").Value.ToString();
                JProperty prop_adminDistrict2 = jo_address.Property("adminDistrict2");
                if (prop_adminDistrict2 != null)
                {
                    if (prop_adminDistrict2.Value.Type != JTokenType.Null)
                    {
                        lr.AdminDistrict2 = jo_address.Property("adminDistrict2").Value.ToString();
                    }
                }
                lr.CountryRegion = jo_address.Property("countryRegion").Value.ToString();
                lr.FormattedAddress = jo_address.Property("formattedAddress").Value.ToString();
                lr.Locality = jo_address.Property("locality").Value.ToString();
                lr.PostalCode = Convert.ToInt32(jo_address.Property("postalCode").Value.ToString());

                ToReturnResults.Add(lr);
            }

            //Prepare and return response
            LocationsResponse ToReturn = new LocationsResponse();
            ToReturn.Results = ToReturnResults.ToArray();
            return ToReturn;
        }
    }
}