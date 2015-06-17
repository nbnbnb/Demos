using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chap9.Models
{
    public class DefaultContactRepository : IContactRepository
    {
        private static List<Contact> _contacts = new List<Contact>
        {
            new Contact{
                Id="001",
                Name="张三",
                PhoneNo="123",
                EmailAddress="zhangsan@gmail.com"
            },
            new Contact{
                Id="002",
                Name="李四",
                PhoneNo="456",
                EmailAddress="lisi@gmail.com"
            }
        };

        public List<Contact> GetAllContacts()
        {
            return _contacts;
        }

        public Contact GetContact(string id)
        {
            return _contacts.FirstOrDefault(m => m.Id == id);
        }
    }
}