using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTHEL8.Menu
{
    public class Menu
    {
        private string[] options;
        private Action[] actions;

        public Menu(string[] options, Action[] actions)
        {
            this.options = options;
            this.actions = actions;
        }

        public void AddOption(string option, Action action)
        {
            int index = options.Length;
            Array.Resize(ref options, index + 1);
            Array.Resize(ref actions, index + 1);

            options[index] = $"{index + 1}. {option}";
            actions[index] = action;
        }

        public void Display()
        {
            bool shouldExit = false;

            do
            {
                Console.WriteLine("Choose an option:");
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine(options[i]);
                }

                int choice = GetChoice();

                if (choice >= 1 && choice <= actions.Length)
                {
                    actions[choice - 1]?.Invoke();
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }

                // Check if the chosen action is the "Back" action
                shouldExit = actions[choice - 1] == Back;
            } while (!shouldExit);
        }

        private static int GetChoice()
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
            return choice;
        }

        protected static void Back()
        {
            Console.WriteLine("Returning to the main menu");
        }
    }
}
