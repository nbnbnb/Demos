﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap7.QueryObject.Infrastructure
{
    public class OrderByClause
    {
        public string PropertyName { get; set; }
        public bool Desc { get; set; }
    }
}
