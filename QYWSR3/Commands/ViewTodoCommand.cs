using System;

internal class ViewTodoCommand : CommandBase
{
    private readonly TodoList todoList;

    public ViewTodoCommand(IHost host, TodoList todoList) : base(host)
    {
        this.todoList = todoList;
    }

    public override string Name => "view";

    public override void Execute()
    {
        todoList.DisplayTasks();
    }
}



