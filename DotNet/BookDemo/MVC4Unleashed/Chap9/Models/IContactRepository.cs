using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chap9.Models
{
    public interface IContactRepository
    {
        List<Contact> GetAllContacts();

        Contact GetContact(string id);
    }
}