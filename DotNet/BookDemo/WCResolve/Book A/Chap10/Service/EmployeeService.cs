using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Text;

namespace Service
{
    public class EmployeeService : IEmployees
    {
        public static IList<Employee> employees = new List<Employee>
        {
            new Employee{Id="001",Name="张三",Department="开发部",Grade="G7"},
            new Employee{Id="002",Name="李四",Department="人事部",Grade="G6"}
        };

        public IEnumerable<Employee> GetAll()
        {
            return employees;
        }

        public Employee Get(string id)
        {
            Employee employee = employees.FirstOrDefault(m => m.Id == id);
            if (null == employee)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode =
                    HttpStatusCode.NotFound;
            }
            return employee;
        }

        public void Create(Employee employee)
        {
            employees.Add(employee);
        }

        public void Update(Employee employee)
        {
            this.Delete(employee.Id);
            employees.Add(employee);
        }

        public void Delete(string id)
        {
            Employee employee = this.Get(id);
            if (null != employee)
            {
                employees.Remove(employee);
            }
        }
    }
}
