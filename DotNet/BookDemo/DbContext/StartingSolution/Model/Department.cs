using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class  Department
    {
        public int DepartmentId { get; set; }
        public DepartmentNames Name { get; set; }
        public decimal Budget { get; set; }
    }
}
