using System;
using ToDoListApplication.Commands;

internal class Program
{
    static int Main(string[] args)
    {   
        Console.WriteLine("|------------------------------------------------|");
        Console.WriteLine("|       TODO-list application WELCOMES YOU!      |");
        Console.WriteLine("|------------------------------------------------|");
        try
        {
            new CommandRunner().Run();
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine("|------------------------------------------------|");
            Console.WriteLine("|   The application has crashed for unknown      |");
            Console.WriteLine("|   reasons! Try to restart it! Details:         |");
            Console.WriteLine("|------------------------------------------------|");
            Console.WriteLine(ex.ToString());
            Console.Error.WriteLine("Hiba történt: " + ex.Message);
            return 1;
        }
    }
}