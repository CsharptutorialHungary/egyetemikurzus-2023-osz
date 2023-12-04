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

        public virtual void Display()
        {
            try
            {
                while (true)
                {
                    for (int i = 0; i < Options.Count; i++)
                    {
                        Console.WriteLine("{0}. {1}", i + 1, Options[i].Name);
                    }
                    int choice = GetUserChoice();

                    Options[choice - 1].Callback();

                    Task.Delay(125).Wait();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Not available choice. Please try again!");
            }
        }

        private static int GetUserChoice()
        {
            Console.WriteLine();
            Console.Write("Enter your choice: ");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
            return choice;
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

        public bool Contains(string option)
        {
            return Options.FirstOrDefault((op) => op.Name.Equals(option)) != null;
        }

    }
}
