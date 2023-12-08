using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApplication.Commands
{
    internal class CommandRunner : IHost
    {
        private CommandLoader _cmdloader;
        public CommandRunner()
        {
            _cmdloader = new CommandLoader(this);
        }
        public void Run()
        {
            RunOptionsCommandOnceAtStart();
            while (true)
            {
                Console.Write("Command:");
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

        private void RunOptionsCommandOnceAtStart()
        {
            var optionsCommand = _cmdloader.Commands
                .Where(command => command.Name == "options")
                .FirstOrDefault();

            if (optionsCommand != null)
            {
                optionsCommand.Execute();
            }
        }
    }
}