using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToTerraServerProvider
{
    public class Place
    {
        public string Name { get; set; }

        public string State { get; set; }

        public PlaceType PlaceType { get; set; }

        public int Age { get; set; }
    }
}
