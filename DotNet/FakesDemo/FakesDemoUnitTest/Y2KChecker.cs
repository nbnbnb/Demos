using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakesDemoUnitTest
{
    public static class Y2KChecker
    {
        public static void Check()
        {
            if (DateTime.Now == new DateTime(2000, 1, 1))
            {
                throw new ApplicationException("y2kbug!");
            }
        }
    }
}
