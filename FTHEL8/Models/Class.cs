using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTHEL8.Models
{
    public class Class
    {
        public required string Name { get; set; }
        public string? Task { get; set; }
        public Employee? ClassLeader {  get; set; }

    }
}
