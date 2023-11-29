using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTHEL8.Models
{
    public class Department
    {
        public string Name { get; set; }
        public Class Class { get; set; }
        public string Description { get; set; }
        public Employee DepartmentLeader {  get; set; }

    }
}
