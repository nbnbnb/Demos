using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqToNHibernateSample
{
    public class Star
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Planet> Planets { get; set; }
        public virtual StarTypes Class { get; set; }
        public virtual SurfaceColor Color { get; set; }
        public virtual double Mass { get; set; }
    } 
}
