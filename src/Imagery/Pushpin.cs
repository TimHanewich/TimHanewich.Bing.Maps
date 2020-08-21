using System;

namespace TimHanewich.Bing.Maps
{
    public class Pushpin
    {
        public float Latitude {get; set;}
        public float Longitude {get; set;}
        public int Style {get; set;} //https://docs.microsoft.com/en-us/bingmaps/rest-services/common-parameters-and-types/pushpin-syntax-and-icon-styles
        public string Label {get; set;}

        public string AsUrlRequestComponent()
        {
            string ToReturn = "";
            ToReturn = Latitude.ToString() + "," + Longitude.ToString() + ";" + Style.ToString() + ";" + Label;
            return ToReturn;
        }
    }
}