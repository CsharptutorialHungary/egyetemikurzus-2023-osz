using System;
using ToDoListApplication.TODOlist;

namespace ToDoListApplication.Commands
{
    internal class ViewDueTimeCommand : CommandBase
    {
        private readonly TodoList todoList;

        public ViewDueTimeCommand(IHost host, TodoList todoList) : base(host)
        {
            this.todoList = todoList;
        }

        public override string Name => "viewDueTime";

        public override void Execute()
        {
            todoList.DisplayTasksByDueTime();
        }
    }
}