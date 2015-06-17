using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleContract
{
    public interface ICalculatorExtension
    {
        string Title { get; }

        string Description { get; }

        //FrameworkElement GetUI();
    }
}
