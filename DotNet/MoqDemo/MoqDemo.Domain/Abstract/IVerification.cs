using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoqDemo.Domain.Abstract
{
    public interface IVerification
    {
        void Execute(string command);

        string UserName { get; set; }

        int UserAge { get; set; }
    }
}
