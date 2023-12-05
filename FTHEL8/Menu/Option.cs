using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTHEL8.Menu
{
    public class Option
    {
        public string Name { get; private set; }
        public Action Callback { get; private set; }

        public Option(string name, Action callback)
        {
            Name = name;
            Callback = callback;
        }
    }
}
