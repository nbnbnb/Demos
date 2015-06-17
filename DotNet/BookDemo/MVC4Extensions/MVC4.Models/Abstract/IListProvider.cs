using MVC4.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVC4.Models.Abstract
{
    public interface IListProvider
    {
        IEnumerable<NBListItem> GetListItems(string listName);
    }
}
