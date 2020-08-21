using System;
using TimHanewich.Bing.Maps;
using System.Collections.Generic;
using System.IO;

namespace FunctionalTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            BingMapsApiHelper apiHelper = new BingMapsApiHelper("");

            BingMapsPushpinsImageryRequest req = new BingMapsPushpinsImageryRequest();
            req.Width = 800;
            req.Height = 800;
            req.ImagerySet = ImageryType.Aerial;

            List<Pushpin> pps = new List<Pushpin>();
            pps.Add(new Pushpin(47.493266f, -122.215553f, 1, "SEA"));
            pps.Add(new Pushpin(47.908243f, -122.285577f, 1, "PAI"));
            req.Pushpins = pps.ToArray();

            Stream s = apiHelper.DownloadPushpinsImageryAsync(req).Result;

            string path = "C:\\Users\\tihanewi\\Downloads\\img.jpg";
            Stream towrite = System.IO.File.OpenWrite(path);
            s.Position = 0;
            s.CopyTo(towrite);
            towrite.Dispose();
            s.Dispose();
            
        }
    }
}
