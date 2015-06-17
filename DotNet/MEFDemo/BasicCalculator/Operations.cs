using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCalculator
{
    public class Operations
    {

        [SpeedExport("Add", typeof(Func<double, double, double>), Speed = Speed.Fast)]
        public double Add(double x, double y)
        {
            return x + y;
        }

        [Export("Subtract", typeof(Func<double, double, double>))]
        public double Subtract(double x, double y)
        {
            return x - y;
        }

        [ImportMany("Add", typeof(Func<double, double, double>))]
        public Lazy<Func<double, double, double>, ISpeedCapabilities>[]
            AddMethods { get; set; }
    }
}
