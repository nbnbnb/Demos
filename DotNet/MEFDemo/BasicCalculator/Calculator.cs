using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCalculator
{
    public class Calculator : ICalculator
    {
        [Import("Add", typeof(Func<double, double, double>))]
        public Func<double, double, double> Add { get; set; }

        [Import("Subtract", typeof(Func<double, double, double>))]
        public Func<double, double, double> Subtract { get; set; }
    }
}
