﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap7.ProxyPattern.Model
{
    public interface ICustomerRepository
    {
        Customer FindBy(Guid id);
    }
}
