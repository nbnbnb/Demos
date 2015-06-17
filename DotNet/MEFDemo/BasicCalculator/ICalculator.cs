using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCalculator
{
    public interface ICalculator
    {
        Func<double, double, double> Add { get; set; }

        Func<double, double, double> Subtract { get; set; }
    }
}
