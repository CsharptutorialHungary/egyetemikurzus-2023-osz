using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTHEL8.Menu
{
    public abstract class Menu(string[] options, Action[] actions)
    {
        private string[] options = options;
        private Action[] actions = actions;

        protected void AddOption(string option, Action action)
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
                Console.WriteLine();
                Console.WriteLine("Choose an option:");
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine(options[i]);
                }
                Console.WriteLine();

                int choice = GetChoice();

                if (choice >= 1 && choice <= actions.Length)
                {
                    actions[choice - 1]?.Invoke();
                }

                try
                {
                    shouldExit = actions[choice - 1] == Back;
                }catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("There is no option with that number!");
                }
                
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
            Console.WriteLine("Going back to the previous menu.");
        }
    }
}
