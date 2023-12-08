using System;
using ToDoListApplication.TODOlist;

namespace ToDoListApplication.Commands
{
    internal class ShowOptionsCommand : CommandBase
    {
        public ShowOptionsCommand(IHost host) : base(host)
        {
        }

        public override string Name => "options";

        public override void Execute()
        {
            Host.WriteLine("|------------------------------------------------|");
            Host.WriteLine("|   Here are the options you can select from:    |");
            Host.WriteLine("|------------------------------------------------|");
            Host.WriteLine("|   hello - Sais hello to you                    |");
            Host.WriteLine("|   options - Shows the options to help you      |");
            Host.WriteLine("|   exit - Close the application                 |");
            Host.WriteLine("|   add - Add to the TODO list                   |");
            Host.WriteLine("|   view - View the TODO list                    |");
            Host.WriteLine("|   remove - Remove from the TODO list           |");
            Host.WriteLine("|   clear - Clear the consol                     |");
            Host.WriteLine("|   update - Update the tasks (save changes)     |");
            Host.WriteLine("|   delete - Delete the whole TODO list          |");
            Host.WriteLine("|   load - Load in your own TODO list            |");
            Host.WriteLine("|   viewDueTime - View the TODO-list by DueTimes |");
            Host.WriteLine("|------------------------------------------------|");
        }
    }
}


