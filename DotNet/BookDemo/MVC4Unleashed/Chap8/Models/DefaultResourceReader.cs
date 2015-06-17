using Chap8.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace Chap8.Models
{
    public class DefaultResourceReader:ResourceReader
    {
        public override string GetString(string name)
        {
            return Resources.ResourceManager.GetString(name);
        }
    }
}