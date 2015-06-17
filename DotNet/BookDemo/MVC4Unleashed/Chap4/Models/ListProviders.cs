﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chap4.Models
{
    public static class ListProviders
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