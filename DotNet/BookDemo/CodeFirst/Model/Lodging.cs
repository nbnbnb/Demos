using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Model
{
    public class Lodging
    {
        public int LodgingId { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }

        public Destination Destination { get; set; }

        public decimal MilesFromNearestAirport { get; set; }

        public int DestinationId { get; set; }

        public List<InternetSpecial> InternetSpecials { get; set; }

        public int? PrimaryContactId { get; set; }
        [InverseProperty("PrimaryContactFor")]
        public Person PrimaryContact { get; set; }

        public int? SecondaryContactId { get; set; }
        [InverseProperty("SecondaryContactFor")]
        public Person SecondaryContact { get; set; }
    }
}
