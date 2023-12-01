using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class CommandRunner : IHost
{
    private CommandLoader _cmdloader;
    public CommandRunner()
    {
        _cmdloader = new CommandLoader(this);
    }
    public void Run()
    {
        while (true)
        {
            string? userInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(userInput))
            {
                var command = _cmdloader.Commands
                    .Where(command => command.Name == userInput)
                    .FirstOrDefault();
                if (command != null)
                {
                    command.Execute();
                }
                else
                {
                    Console.WriteLine($"This command doesn't exist: {userInput}");
                }
            }
        }
    }
}
