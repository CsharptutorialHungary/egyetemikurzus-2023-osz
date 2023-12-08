using System;
using ToDoListApplication.TODOlist;

namespace ToDoListApplication.Commands
{
    internal class RemoveTaskCommand : CommandBase
    {
        private TodoList todoList;

        public RemoveTaskCommand(IHost host, TodoList todoList) : base(host)
        {
            this.todoList = todoList;
        }

        public override string Name => "remove";

        public override void Execute()
        {
            Host.WriteLine("Give task's ID to remove task!");
            Host.Write("Input:");

            if (int.TryParse(Host.ReadLine(), out int removeTaskWithThisID))
            {
                todoList.RemoveTask(removeTaskWithThisID);
            }
            else
            {
                Host.WriteLine("Invalid input. Please enter a valid ID.");
            }
        }
    }
}


