using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqToNHibernateSample
{
    public class Planet
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsHabitable { get; set; }
        public virtual Star Sun { get; set; }
    } 
}
