using System;

namespace TimHanewich.Bing.Maps.Locations
{
    public class LocationResult
    {
        public float Latitude {get; set;}
        public float Longitude {get; set;}
        public string AddressLine {get; set;}
        public string AdminDistrict {get; set;} //i.e. "FL" (the state)
        public string AdminDistrict2 {get; set;} //i.e. "Sarasota County" (the county)
        public string CountryRegion {get; set;} //i.e. United States
        public string FormattedAddress {get; set;} //the FULL address
        public string Locality {get; set;} //the name of the city
        public int PostalCode {get; set;}
    }
}