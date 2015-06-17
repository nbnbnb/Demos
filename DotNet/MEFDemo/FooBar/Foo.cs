using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooBar
{
    [Export("MEFDemo.FooBar.Foo")]
    public class Foo
    {
        public string Bar()
        {
            return "Foo.Bar";
        }
    }
}
