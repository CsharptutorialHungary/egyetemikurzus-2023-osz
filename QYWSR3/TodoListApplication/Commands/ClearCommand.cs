using System;
using ToDoListApplication.TODOlist;

namespace ToDoListApplication.Commands
{
    internal class ClearCommand : CommandBase
    {
        public ClearCommand(IHost host) : base(host)
        {
        }

        public override string Name => "clear";

        public override void Execute()
        {
            Host.Clear();
        }
    }
}