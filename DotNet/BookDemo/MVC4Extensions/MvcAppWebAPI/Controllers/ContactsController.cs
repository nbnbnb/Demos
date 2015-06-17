using MvcAppWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MvcAppWebAPI.Controllers
{
    public class ContactsController : ApiController
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

        // GET api/contacts
        public IEnumerable<Contact> Get()
        {
            return _contacts;
        }

        // GET api/contacts/001
        public Contact Get(string id)
        {
            return _contacts.FirstOrDefault(m => m.Id == id);
        }

        // PUT api/contacts
        public void Put(Contact contact)
        {
            contact.Id = Guid.NewGuid().ToString();
            _contacts.Add(contact);
        }

        // DELETE api/contacts/001
        public void Delete(string id)
        {
            Contact contact = _contacts.FirstOrDefault(m => m.Id == id);
            if (contact != null)
            {
                _contacts.Remove(contact);
            }
        }

        // POST api/contacts
        public void Post(Contact contact)
        {
            Delete(contact.Id);
            _contacts.Add(contact);
        }
    }
}
