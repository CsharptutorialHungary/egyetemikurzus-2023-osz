using System;

internal class AddTaskCommand : CommandBase
{
    private readonly TodoList todoList;

    public AddTaskCommand(IHost host, TodoList todoList) : base(host)
    {
        this.todoList = todoList;
    }

    public override string Name => "add";

    public override void Execute()
    {
        Host.WriteLine("Give the task you'd like to add.");
        Host.Write("Input:");
        string newTask = Host.ReadLine();
        todoList.AddTask(newTask);
    }
}


