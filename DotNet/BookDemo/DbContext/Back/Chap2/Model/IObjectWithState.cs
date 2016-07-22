using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public interface IObjectWithState
    {
        State State { get; set; }
    }

}
