using MVC4.Extensions.ModelExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MvcAppModelMetadataWithTemplate.Models
{
    public class Employee
    {
        [DisplayName("姓名")]
        public string Name { get; set; }

        [DisplayName("性别")]
        [RadioButtonList("Gender")]
        public string Gender { get; set; }

        [DisplayName("学历")]
        [DropdownList("Education")]
        public string Education { get; set; }

        [DisplayName("所在部门")]
        [ListBox("Department")]
        public IEnumerable<string> Departments { get; set; }

        [DisplayName("擅长技能")]
        [CheckBoxList("Skill")]
        public IEnumerable<string> Skills { get; set; }
    }
}