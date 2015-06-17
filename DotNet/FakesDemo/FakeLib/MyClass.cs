using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakeLib
{
    public class MyClass
    {
        public MyClass() { }

        public MyClass(string name)
        {
            this.Name = name;
        }

        public int MyMethod()
        {
            return 0;
        }

        public string Name { get; set; }
    }
}
