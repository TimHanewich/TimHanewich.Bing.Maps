using System;

namespace TimHanewich.Bing.Maps
{
    public class BingMapsImageryRequest
    {
        public int ZoomLevel {get; set;} //Zoom level from 1 (far away) to 20 (up close)
        public int Width {get; set;}
        public int Height {get; set;}
    }
}