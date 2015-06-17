using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Hostel : Lodging
    {
        public int MaxPersonsPerRoom { get; set; }

        public bool PrivateRoomsAvailable { get; set; }
    }
}
