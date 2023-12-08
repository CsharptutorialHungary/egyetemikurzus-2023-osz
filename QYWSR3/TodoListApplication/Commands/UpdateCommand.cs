using System;
using ToDoListApplication.TODOlist;

namespace ToDoListApplication.Commands
{
    internal class UppdateCommand : CommandBase
    {
        private readonly TodoList todoList;

        public UppdateCommand(IHost host, TodoList todoList) : base(host)
        {
            this.todoList = todoList;
        }

        public override string Name => "update";

        public override async void Execute()
        {
            Host.WriteLine("Type in the filename of your TODO-list to update it!");
            Host.WriteLine("If your TODO-list isnt in a file yet, dont worry. I will save it.");
            Host.WriteLine("It must be .json! (example: alma.json)");
            Host.Write("Input:");
            string fileNAME = Host.ReadLine();
            await todoList.UpdateTODO(fileNAME);
        }
    }
}

