using System;
using System.IO;

namespace TimHanewich.Bing.Maps.Locations
{
    public class LocationsRequest
    {
        //Point to address
        public float? Latitude {get; set;}
        public float? Longitude {get; set;}

        //Address to point
        public string Address {get; set;}
        public int? PostalCode {get; set;}
    }
}