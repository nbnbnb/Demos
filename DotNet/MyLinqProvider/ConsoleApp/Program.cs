using LinqToTerraServerProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            QueryableTerraServerData<Place> terraPlaces = new QueryableTerraServerData<Place>();

            var query = from place in terraPlaces
                            where place.Name == GetName() && place.State == GetState("btking")
                        //where place.Age == 100 && place.Name == GetName()
                        select place.PlaceType;

            foreach (PlaceType placeType in query)
                Console.WriteLine(placeType);
        }

        private static string GetName()
        {
            return "001";
        }

        public static string GetState(string name)
        {
            return "A";
        }

        public static string GetState()
        {
            return "A";
        }
    }
}
