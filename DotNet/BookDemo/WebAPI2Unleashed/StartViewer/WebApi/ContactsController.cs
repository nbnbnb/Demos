using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace WebApi
{
    public class ContactsController : ApiController
    {
        private static List<Contact> _contacts;

        static ContactsController()
        {
            _contacts = new List<Contact>();

            _contacts.Add(new Contact { 
                Id="001",
                Name="张三",
                PhoneNo="123456789",
                EmailAddress="zhangsan@gmail.com",
                Address="Zhang San Street"
            });

            _contacts.Add(new Contact
            {
                Id = "002",
                Name = "李四",
                PhoneNo = "123456789",
                EmailAddress = "lisi@gmail.com",
                Address = "Li Si Street"
            });
        }

        public IEnumerable<Contact> Get(string id = null)
        {
            return from contact in _contacts
                   where contact.Id == id || string.IsNullOrEmpty(id)
                   select contact;
        }

        public void Post(Contact contact)
        {
            contact.Id = Counter.ToString();
            _contacts.Add(contact);
        }

        public void Put(Contact contact)
        {
            _contacts.Remove(_contacts.First(m => m.Id == contact.Id));
            _contacts.Add(contact);
        }

        public void Delete(string id)
        {
            _contacts.Remove(_contacts.First(m => m.Id == id));
        }

        private string Counter
        {
            get
            {
                return (_contacts.Count + 1).ToString("D3");
            }
        }
    }
}
