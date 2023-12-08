using System;
using ToDoListApplication.Commands;

internal class Program
{
    static int Main(string[] args)
    {
        new CommandRunner().Run();
        return 0;
    }
}