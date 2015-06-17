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
        private static List<Contact> contacts;
        private int counter = 3;

        static ContactsController()
        {
            contacts = new List<Contact>();
            contacts.Add(new Contact
            {
                Id = "001",
                Name = "张三",
                PhoneNo = "0512-12345678",
                EmailAddress = "zhangsan@gmail.com",
                Address = "江苏省苏州市星湖街 328 号"
            });
            contacts.Add(new Contact
            {
                Id = "002",
                Name = "李四",
                PhoneNo = "0512-23456789",
                EmailAddress = "lisi@gmail.com",
                Address = "讲述省苏州市金鸡湖大道 328 号"
            });
        }

        public IEnumerable<Contact> Get(string id = null)
        {
            return from contact in contacts
                   where contact.Id == id || string.IsNullOrEmpty(id)
                   select contact;
        }
        public void Post(Contact contact)
        {
            contact.Id = counter.ToString("D3");
            contacts.Add(contact);
        }
        public void Put(Contact contact)
        {
            contacts.Remove(contacts.First(c => c.Id == contact.Id));
            contacts.Add(contact);
        }
        public void Delete(string id)
        {
            contacts.Remove(contacts.First(c => c.Id == id));
        }
    }
}
