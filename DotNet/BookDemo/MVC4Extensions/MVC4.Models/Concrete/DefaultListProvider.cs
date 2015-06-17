using MVC4.Models.Abstract;
using MVC4.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVC4.Models.Concrete
{
    public class DefaultListProvider : IListProvider
    {
        private static Dictionary<string, IEnumerable<NBListItem>> _listItems =
            new Dictionary<string, IEnumerable<NBListItem>>();

        static DefaultListProvider()
        {
            NBListItem[] items = new[]{ 
                new NBListItem{Text="男",Value="M"},
                new NBListItem{Text="女",Value="F"}
            };
            _listItems.Add("Gender", items);

            items = new[]{ 
                new NBListItem{Text="高中",Value="H"},
                new NBListItem{Text="大学本科",Value="B"},
                new NBListItem{Text="硕士",Value="M"},
                new NBListItem{Text="博士",Value="D"}
            };
            _listItems.Add("Education", items);

            items = new[]{ 
                new NBListItem{Text="人事部",Value="HR"},
                new NBListItem{Text="行政部",Value="AD"},
                new NBListItem{Text="IT 部",Value="IT"}
            };
            _listItems.Add("Department", items);

            items = new[]{ 
                new NBListItem{Text="C#",Value="CSharp"},
                new NBListItem{Text="ASP.NET",Value="AspNet"},
                new NBListItem{Text="ADO.NET",Value="AdoNet"},
            };
            _listItems.Add("Skill", items);
        }

        public IEnumerable<NBListItem> GetListItems(string listName)
        {
            IEnumerable<NBListItem> items;
            if (_listItems.TryGetValue(listName, out items))
            {
                return items;
            }

            return new NBListItem[0];
        }
    }
}
