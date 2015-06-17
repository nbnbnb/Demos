using MvcAppModelBind.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAppModelBind.Controllers
{
    public class NBBindController : BasicController
    {
        //
        // GET: /NBBind/

        public ActionResult Index()
        {
            return this.InvokeAction("DemoAction");
        }

        protected override IValueProvider CreateValueProvider()
        {
            return GetDictionarySource();
        }

        /*
        public ActionResult DemoAction(string foo, int bar, [Bind(Prefix = "qux")] double baz)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("foo", foo);
            parameters.Add("bar", bar);
            parameters.Add("baz", baz);
            return View("DemoAction", parameters);
        }
         * */

        /*
        public ActionResult DemoAction(Contact foo, Contact bar)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("foo.Name", foo.Name);
            parameters.Add("foo.PhotoNo", foo.PhotoNo);
            parameters.Add("foo.EmailAddress", foo.EmailAddress);
            Address address = foo.Address;
            parameters.Add("foo.Address", String.Format("{0}省{1}市{2}{3}", 
                address.Province, address.City, address.District, address.Street));

            parameters.Add("bar.Name", bar.Name);
            parameters.Add("bar.PhotoNo", bar.PhotoNo);
            parameters.Add("bar.EmailAddress", bar.EmailAddress);
            address = bar.Address;
            parameters.Add("bar.Address", String.Format("{0}省{1}市{2}{3}",
               address.Province, address.City, address.District, address.Street));

            return View("DemoAction", parameters);
        }
         * */

        /*
        public ActionResult DemoAction(string[] foo, int[] bar)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            for (int i = 0; i < foo.Length; i++)
            {
                parameters.Add(string.Format("foo[{0}]", i), foo[i]);
            }
            for (int i = 0; i < bar.Length; i++)
            {
                parameters.Add(string.Format("bar[{0}]", i), bar[i]);
            }
            return View("DemoAction", parameters);
        }
        */

        /*
        public ActionResult DemoAction(Contact[] contacts)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            for (int i = 0; i < contacts.Length; i++)
            {
                string name = contacts[i].Name;
                string photoNo = contacts[i].PhotoNo;
                string emailAddress = contacts[i].EmailAddress;
                string address = string.Format("{0}省{1}市{2}{3}",
                    contacts[i].Address.Province,
                    contacts[i].Address.City,
                    contacts[i].Address.District,
                    contacts[i].Address.Street);
                parameters.Add(string.Format(" [{0}].Name ", i), name);
                parameters.Add(string.Format("[{0}].PhotoNo", i), photoNo);
                parameters.Add(string.Format(" [{0}].EmailAddress", i), emailAddress);
                parameters.Add(string.Format("[{0}].Address", i), address);
            }
            return View("DemoAction", parameters);
        }
         * */

        public ActionResult DemoAction(IDictionary<string, Contact> contacts)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            var contactsArray = contacts.ToArray();
            for (int i = 0; i < contactsArray.Length; i++)
            {
                string name = contactsArray[i].Value.Name;
                string photoNo = contactsArray[i].Value.PhotoNo;
                string emailAddress = contactsArray[i].Value.EmailAddress;
                string address = string.Format("{0}省{1}市{2}{3}",
                    contactsArray[i].Value.Address.Province,
                    contactsArray[i].Value.Address.City,
                    contactsArray[i].Value.Address.District,
                    contactsArray[i].Value.Address.Street);
                parameters.Add(string.Format(" [{0}].Name ", i), name);
                parameters.Add(string.Format("[{0}].PhotoNo", i), photoNo);
                parameters.Add(string.Format(" [{0}].EmailAddress", i), emailAddress);
                parameters.Add(string.Format("[{0}].Address", i), address);
            }
            return View("DemoAction", parameters);
        }

        private IValueProvider GetSimpleSource()
        {
            NameValueCollection dataSource = new NameValueCollection();
            dataSource.Add("Foo", "ABC");
            dataSource.Add("Bar", "123");
            dataSource.Add("Baz", "456.01");
            dataSource.Add("Qux", "789.01");
            return new NameValueCollectionValueProvider(dataSource, CultureInfo.CurrentCulture);
        }

        private IValueProvider GetArraySource()
        {
            NameValueCollection dataSource = new NameValueCollection();
            dataSource.Add("foo", "abc");
            dataSource.Add("foo", "ijk");
            dataSource.Add("foo", "xyz");

            dataSource.Add("bar", "123");
            dataSource.Add("bar", "456");
            dataSource.Add("bar", "789");
            return new NameValueCollectionValueProvider(dataSource, CultureInfo.CurrentCulture);
        }

        private IValueProvider GetComplexSource1()
        {
            NameValueCollection dataSource = new NameValueCollection();
            dataSource.Add("foo.Name", "张三");
            dataSource.Add("foo.PhotoNo", "123456789");
            dataSource.Add("foo.EmailAddress", "zhangsan@gmail.com");
            dataSource.Add("foo.Address.Province", "江苏");
            dataSource.Add("foo.Address.City", "苏州");
            dataSource.Add("foo.Address.District", "工业园区");
            dataSource.Add("foo.Address.Street", "星湖街 328 号");

            dataSource.Add("bar.Name", "李四");
            dataSource.Add("bar.PhotoNo", "987654321");
            dataSource.Add("bar.EmailAddress", "lisi@gmail.com");
            dataSource.Add("bar.Address.Province", "江苏");
            dataSource.Add("bar.Address.City", "苏州");
            dataSource.Add("bar.Address.District", "工业园区");
            dataSource.Add("bar.Address.Street", "金鸡湖路 328 号");
            return new NameValueCollectionValueProvider(dataSource, CultureInfo.CurrentCulture);
        }

        private IValueProvider GetIndexSource1()
        {
            NameValueCollection dataSource = new NameValueCollection();
            dataSource.Add("contacts[0].Name", "张三");
            dataSource.Add("contacts[0].PhotoNo", "123456789");
            dataSource.Add("contacts[0].EmailAddress", "zhangsan@gmail.com");
            dataSource.Add("contacts[0].Address.Province", "江苏");
            dataSource.Add("contacts[0].Address.City", "苏州");
            dataSource.Add("contacts[0].Address.District", "工业园区");
            dataSource.Add("contacts[0].Address.Street", "星湖街 328 号");

            dataSource.Add("contacts[1].Name", "李四");
            dataSource.Add("contacts[1].PhotoNo", "987654321");
            dataSource.Add("contacts[1].EmailAddress", "lisi@gmail.com");
            dataSource.Add("contacts[1].Address.Province", "江苏");
            dataSource.Add("contacts[1].Address.City", "苏州");
            dataSource.Add("contacts[1].Address.District", "工业园区");
            dataSource.Add("contacts[1].Address.Street", "金鸡湖路 328 号");
            return new NameValueCollectionValueProvider(dataSource, CultureInfo.CurrentCulture);
        }


        private IValueProvider GetIndexSource2()
        {
            NameValueCollection dataSource = new NameValueCollection();

            dataSource.Add("contacts.index", "first");
            dataSource.Add("contacts.index", "second");

            dataSource.Add("contacts[first].Name", "张三");
            dataSource.Add("contacts[first].PhotoNo", "123456789");
            dataSource.Add("contacts[first].EmailAddress", "zhangsan@gmail.com");
            dataSource.Add("contacts[first].Address.Province", "江苏");
            dataSource.Add("contacts[first].Address.City", "苏州");
            dataSource.Add("contacts[first].Address.District", "工业园区");
            dataSource.Add("contacts[first].Address.Street", "星湖街 328 号");

            dataSource.Add("contacts[second].Name", "李四");
            dataSource.Add("contacts[second].PhotoNo", "987654321");
            dataSource.Add("contacts[second].EmailAddress", "lisi@gmail.com");
            dataSource.Add("contacts[second].Address.Province", "江苏");
            dataSource.Add("contacts[second].Address.City", "苏州");
            dataSource.Add("contacts[second].Address.District", "工业园区");
            dataSource.Add("contacts[second].Address.Street", "金鸡湖路 328 号");
            return new NameValueCollectionValueProvider(dataSource, CultureInfo.CurrentCulture);
        }

        private IValueProvider GetDictionarySource()
        {
            NameValueCollection dataSource = new NameValueCollection();

            dataSource.Add("contacts.index", "first");
            dataSource.Add("contacts.index", "second");

            dataSource.Add("contacts[first].Key", "张三");
            dataSource.Add("contacts[first].Value.Name", "张三");
            dataSource.Add("contacts[first].Value.PhotoNo", "123456789");
            dataSource.Add("contacts[first].Value.EmailAddress", "zhangsan@gmail.com");
            dataSource.Add("contacts[first].Value.Address.Province", "江苏");
            dataSource.Add("contacts[first].Value.Address.City", "苏州");
            dataSource.Add("contacts[first].Value.Address.District", "工业园区");
            dataSource.Add("contacts[first].Value.Address.Street", "星湖街 328 号");

            dataSource.Add("contacts[second].Key", "李四");
            dataSource.Add("contacts[second].Value.Name", "李四");
            dataSource.Add("contacts[second].Value.PhotoNo", "987654321");
            dataSource.Add("contacts[second].Value.EmailAddress", "lisi@gmail.com");
            dataSource.Add("contacts[second].Value.Address.Province", "江苏");
            dataSource.Add("contacts[second].Value.Address.City", "苏州");
            dataSource.Add("contacts[second].Value.Address.District", "工业园区");
            dataSource.Add("contacts[second].Value.Address.Street", "金鸡湖路 328 号");
            return new NameValueCollectionValueProvider(dataSource, CultureInfo.CurrentCulture);
        }
    }
}
