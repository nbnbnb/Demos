using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chap4.Models
{
    public interface IListProvider
    {
        IEnumerable<ListItem> GetListItems(String listName);
    }
}