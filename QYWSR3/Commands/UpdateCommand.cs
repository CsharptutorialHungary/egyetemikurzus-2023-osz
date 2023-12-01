using System;

internal class UppdateCommand : CommandBase
{
    private readonly TodoList todoList;

    public UppdateCommand(IHost host, TodoList todoList) : base(host)
    {
        this.todoList = todoList;
    }

    public override string Name => "update";

    public override void Execute()
    {
        Host.WriteLine("Type in the name of the TODO list you want to update! It must be .json!");
        Host.Write("Input:");
        string fileNAME = Host.ReadLine();
        todoList.UpdateTODO(fileNAME);
    }
}


