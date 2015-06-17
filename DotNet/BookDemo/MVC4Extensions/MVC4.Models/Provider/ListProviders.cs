using MVC4.Models.Abstract;
using MVC4.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVC4.Models.Provider
{
    public class ListProviders
    {
        public static IListProvider Current { get; private set; }

        static ListProviders()
        {
            Current = new DefaultListProvider();
        }

        public static void SetListProvider(Func<IListProvider> providerAccessor)
        {
            Current = providerAccessor();
        }
    }
}
