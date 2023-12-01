using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTHEL8.Models
{
    public class ProjectMembers
    {
        public Project? ProjectName { get; set; }
        public List<Employee>? Employees { get; set; }
    }
}
