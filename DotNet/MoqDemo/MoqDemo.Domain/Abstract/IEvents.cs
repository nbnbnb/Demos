using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoqDemo.Domain.Abstract
{
    public delegate void MyEventHandler(int i,bool b);
  
    public interface IEvents
    {
        event MyEventHandler Send;
        void Submit();
    }
}
