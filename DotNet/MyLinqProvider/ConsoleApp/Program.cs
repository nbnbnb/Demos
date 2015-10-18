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

        static bool isSay = false;
        static void Main(string[] args)
        {
            SayName("a");
            //Start();
        }

        private static void Start()
        {
            QueryableTerraServerData<Place> terraPlaces = new QueryableTerraServerData<Place>();

            var query = from place in terraPlaces
                            //    where place.Name == GetName() && place.State == GetState("btking")
                            //where place.Age == 100 && place.Name == GetName()
                        where place.Name == "ABC"
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

        private static void SayName(string name)
        {
            Console.WriteLine(name);
            if (name == "a")
            {
                SayName("b");

            }

            if (name == "b")
            {
                isSay = true;
            }

            Console.WriteLine(isSay);
        }
    }
}
