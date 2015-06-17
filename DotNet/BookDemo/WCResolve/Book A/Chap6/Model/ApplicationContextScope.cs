using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ApplicationContextScope : IDisposable
    {
        private ApplicationContext originContext = ApplicationContext.Current;

        public ApplicationContextScope()
        {
            ApplicationContext.Current = new ApplicationContext();
        }

        public void Dispose()
        {
            ApplicationContext.Current = originContext;
        }
    }
}
