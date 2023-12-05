using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FTHEL8.Menu
{
    public abstract class Menu
    {
        private List<Option> Options { get; set; }

        public Menu()
        {
            Options = new List<Option>();
        }

        public void Display()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine();
                    for (int i = 0; i < Options.Count; i++)
                    {
                        Console.WriteLine("{0}. {1}", i + 1, Options[i].Name);
                    }
                    int choice = GetUserChoice();

                    if(choice != -1)
                    {
                        Options[choice - 1]?.Callback();
                    }

                    Task.Delay(125).Wait();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private int GetUserChoice()
        {
            Console.WriteLine();
            Console.Write("Enter your choice: ");
            string input = Console.ReadLine()!;

            if (int.TryParse(input, out int choice))
            {
                if (choice >= 1 && choice <= Options.Count)
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("There is no option with that number!");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }

            return -1;
        }

        public Menu Add(string option, Action callback)
        {
            return Add(new Option(option, callback));
        }

        public Menu Add(Option option)
        {
            Options.Add(option);
            return this;
        }

    }
}
