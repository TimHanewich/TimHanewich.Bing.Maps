using System;
using System.IO;

namespace TimHanewich.Bing.Maps.Locations
{
    public class LocationsRequest
    {
        public string Address {get; set;}
        public int? PostalCode {get; set;}
    }
}