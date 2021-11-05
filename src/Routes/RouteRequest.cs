using System;
using System.IO;
using System.Net.Http;

namespace TimHanewich.Bing.Maps.Routes
{
    public class RouteRequest
    {
        public Geolocation Departure {get; set;}
        public Geolocation Destination {get; set;}
    }
}