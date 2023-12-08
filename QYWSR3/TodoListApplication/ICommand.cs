using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApplication
{
    internal interface ICommand
    {
        IHost Host { get; }
        string Name { get; }
        void Execute();

    }

    internal abstract class CommandBase : ICommand
    {
        public IHost Host { get; }

        protected CommandBase(IHost host)
        {
            Host = host;
        }

        public abstract string Name { get; }

        public abstract void Execute();
    }
}