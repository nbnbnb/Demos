using FakeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakesDemoUnitTest
{
    public static class FileHelper
    {
        public static bool IsEmpty(IFileSystem fs, string f)
        {
            string content = fs.ReadAllText(f);
            return String.IsNullOrEmpty(content);
        }
    }
}
