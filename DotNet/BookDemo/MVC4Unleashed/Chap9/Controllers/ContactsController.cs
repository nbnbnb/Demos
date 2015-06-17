using Chap9.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Mvc;

namespace Chap9.Controllers
{
    public class ContactsController : ApiController
    {
        [Inject]
        public IContactRepository Repository { get; set; }

        public IEnumerable<Contact> Get()
        {
            return Repository.GetAllContacts();
        }

        public Contact Get(string id)
        {
            return Repository.GetContact(id);
        }

        public void Put(Contact contact)
        {
            contact.Id = Guid.NewGuid().ToString();
            Repository.GetAllContacts().Add(contact);
        }

        public void Post(Contact contact)
        {
            Delete(contact.Id);
            Repository.GetAllContacts().Add(contact);
        }

        public void Delete(string id)
        {
            Contact contact = Repository.GetContact(id);
            Repository.GetAllContacts().Remove(contact);
        }

        public Contact GetContact(string id)
        {
            return Repository.GetContact(id);
        } 
    }
}
