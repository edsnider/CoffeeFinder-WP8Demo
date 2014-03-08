using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CoffeeFinder.Services
{
    public class LocationService
    {
        public LocationService()
        {
        }

        public async Task<Windows.Devices.Geolocation.Geocoordinate> GetLocationAsync()
        {
            Windows.Devices.Geolocation.Geolocator geolocator = new Windows.Devices.Geolocation.Geolocator();
 
            Windows.Devices.Geolocation.Geoposition geoposition = await geolocator.GetGeopositionAsync(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10));

            return geoposition.Coordinate;
        }
    }
}
