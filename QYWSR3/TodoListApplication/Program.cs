using System;
using ToDoListApplication.Commands;

internal class Program
{
    static int Main(string[] args)
    {
        Console.WriteLine("|------------------------------------------------|");
        Console.WriteLine("|   Here are the options you can select from:    |");
        Console.WriteLine("|------------------------------------------------|");
        Console.WriteLine("|   hello - Sais hello to you                    |");
        Console.WriteLine("|   options - Shows the options to help you      |");
        Console.WriteLine("|   exit - Close the application                 |");
        Console.WriteLine("|   add - Add to the TODO list                   |");
        Console.WriteLine("|   view - View the TODO list                    |");
        Console.WriteLine("|   remove - Remove from the TODO list           |");
        Console.WriteLine("|   clear - Clear the consol                     |");
        Console.WriteLine("|   update - Update the tasks (save changes)     |");
        Console.WriteLine("|   delete - Delete the whole TODO list          |");
        Console.WriteLine("|   load - Load in your own TODO list            |");
        Console.WriteLine("|   viewDueTime - View the TODO-list by DueTimes |");
        Console.WriteLine("|------------------------------------------------|");
        new CommandRunner().Run();
        return 0;
    }
}