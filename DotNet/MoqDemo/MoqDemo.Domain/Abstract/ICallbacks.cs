using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoqDemo.Domain.Abstract
{
    public interface ICallbacks
    {
        bool Execute(string msg);
        bool Execute(int i);
        bool Execute(int i, string msg);
    }
}
