using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToTerraServerProvider
{
    public class WebServiceHelper
    {
        internal static Place[] GetPlacesFromTerraServer(List<string> locations)
        {
            List<Place> temp = new List<Place>();

            temp.Add(new Place
            {
                Name = "001",
                PlaceType = PlaceType.BayGulf,
                State = "A"
            });

            temp.Add(new Place
            {
                Name = "002",
                PlaceType = PlaceType.OtherLandFeature,
                State = "B"
            });

            temp.Add(new Place
            {
                Name = "003",
                PlaceType = PlaceType.CityTown,
                State = "C"
            });

            temp.Add(new Place
            {
                Name = "004",
                PlaceType = PlaceType.River,
                State = "D"
            });

            temp.Add(new Place
            {
                Name = "005",
                PlaceType = PlaceType.Lake,
                State = "E"
            });

            return temp.ToArray();
        }
    }
}
