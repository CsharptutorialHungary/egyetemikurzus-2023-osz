using System;

internal class LoadTODOCommand : CommandBase
{
    private readonly TodoList todoList;

    public LoadTODOCommand(IHost host, TodoList todoList) : base(host)
    {
        this.todoList = todoList;
    }

    public override string Name => "load";

    public override void Execute()
    {
        Host.WriteLine("Load your own TODO list in! Give me the name of your file, it must be .json!");
        Host.Write("Input:");
        string fileNAME = Host.ReadLine();
        todoList.LoadTODO(fileNAME);
    }
}


