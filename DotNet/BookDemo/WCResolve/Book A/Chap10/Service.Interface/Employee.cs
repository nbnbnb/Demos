using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Service.Interface
{
    [DataContract(Namespace = "http://www.zhangjin.me")]
    public class Employee
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Department { get; set; }

        [DataMember]
        public string Grade { get; set; }

        public override string ToString()
        {
            return string.Format("ID: {0,-5}姓名: {1,-5}级别: {2,-4},部门: {3}",
                Id, Name, Grade, Department);
        }
    }
}
