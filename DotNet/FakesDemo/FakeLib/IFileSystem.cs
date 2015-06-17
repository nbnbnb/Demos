using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakeLib
{
    public interface IFileSystem
    {
        string ReadAllText(string fileName);

        int MyMethod(string value);

        int MyAge { get; set; }

        event EventHandler MyChangeEvent;

        T GetValue<T>();

        int DoNothing();
    }
}
