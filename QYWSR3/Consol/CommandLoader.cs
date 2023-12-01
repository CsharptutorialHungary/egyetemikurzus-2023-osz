using System;
using System.Collections.Generic;

internal class CommandLoader
{
    private readonly IHost _host;
    private readonly TodoList _todoList;

    public CommandLoader(IHost host, TodoList? todoList = null)
    {
        _host = host;
        _todoList = todoList ?? new TodoList();
        LoadCommands();
    }

    public List<ICommand> Commands { get; private set; } = new List<ICommand>();

    private void LoadCommands()
    {
        Commands.Add(new HelloCommand(_host));
        Commands.Add(new ExitCommand(_host));
        Commands.Add(new ShowOptionsCommand(_host));
        Commands.Add(new ClearCommand(_host));
        Commands.Add(new AddTaskCommand(_host, _todoList));
        Commands.Add(new ViewTodoCommand(_host, _todoList));
        Commands.Add(new RemoveTaskCommand(_host, _todoList));
        Commands.Add(new UppdateCommand(_host, _todoList));
        Commands.Add(new DeleteAllTODOCommand(_host, _todoList));
        Commands.Add(new LoadTODOCommand(_host, _todoList));
    }
}
