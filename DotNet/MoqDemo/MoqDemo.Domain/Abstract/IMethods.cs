using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoqDemo.Domain.Abstract
{
    public interface IMethods
    {
        bool DoSomething(string msg);
        bool TryParse(string aq, out string msg);
        int GetCountThing();
    }
}
