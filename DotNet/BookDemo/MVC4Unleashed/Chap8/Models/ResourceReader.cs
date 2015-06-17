using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chap8.Models
{
    public abstract class ResourceReader
    {
        public abstract string GetString(string name);
    }
}