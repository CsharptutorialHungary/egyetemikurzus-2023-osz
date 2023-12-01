using System;

internal class Program
{
    static int Main(string[] args)
    {
        Console.WriteLine("Welcom to my TODO list application :D");
        Console.WriteLine("Here are the options you can select from:");
        Console.WriteLine("hello - Sais hello to you");
        Console.WriteLine("options - Shows the options to help you");
        Console.WriteLine("exit - Close the application");
        Console.WriteLine("add - Add to the TODO list");
        Console.WriteLine("view - View the TODO list");
        Console.WriteLine("remove - Remove from the TODO list");
        Console.WriteLine("clear - Clear the consol");
        new CommandRunner().Run();
        return 0;
    }
}