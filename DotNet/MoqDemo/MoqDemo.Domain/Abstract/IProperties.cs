using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoqDemo.Domain.Entities;

namespace MoqDemo.Domain.Abstract
{
    public interface IProperties
    {
        int ProductId { set; get; }

        string ProductName { get; set; }

        Address Address { get; set; }
    }
}
