using System;
using System.IO;

namespace TimHanewich.Bing.Maps.Routes
{
    public class RouteResponse
    {
        public float TravelDistance {get; set;} //Travel distance, default in kilometers
        public int TravelDuration {get; set;} //Travel time in seconds if there was no traffic
        public int TravelDurationTraffic {get; set;} //Travel time in seconds, considering current travel levels
    }
}