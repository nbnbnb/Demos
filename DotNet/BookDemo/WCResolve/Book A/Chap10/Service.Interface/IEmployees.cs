using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Service.Interface
{
    [ServiceContract(Namespace = "http://www.zhangjin.me")]
    public interface IEmployees
    {
        [WebGet(UriTemplate = "all")]
        IEnumerable<Employee> GetAll();

        [WebGet(UriTemplate = "{id}")]
        Employee Get(string id);

        [WebInvoke(UriTemplate = "/", Method = "POST")]
        void Create(Employee employee);

        [WebInvoke(UriTemplate = "/", Method = "PUT")]
        void Update(Employee employee);

        [WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        void Delete(string id);
    }
}
