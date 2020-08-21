using System;
using TimHanewich.Bing.Maps;

namespace FunctionalTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            BingMapsPushpinsImageryRequest req = new BingMapsPushpinsImageryRequest();
            req.Width = 300;
            req.Height = 200;
            req.ImagerySet = ImageryType.Aerial;

            
        }
    }
}
