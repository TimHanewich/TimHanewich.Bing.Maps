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

            float ele = apiHelper.GetElevationMetersAsync(46.861387f, -121.714906f).Result;
            Console.WriteLine(ele.ToString());
            
        }
    }
}
