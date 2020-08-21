using System;

namespace TimHanewich.Bing.Maps
{
    public class BingMapsPushpinsImageryRequest : BingMapsImageryRequest
    {
        public ImageryType ImagerySet {get; set;}
        public Pushpin[] Pushpins {get; set;}
    }
}