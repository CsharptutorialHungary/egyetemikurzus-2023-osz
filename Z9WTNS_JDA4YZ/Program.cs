using Z9WTNS_JDA4YZ.CLI;
using Z9WTNS_JDA4YZ.CLI.Commands;
using Z9WTNS_JDA4YZ.DataClasses;
using Z9WTNS_JDA4YZ.Xml;

namespace Z9WTNS_JDA4YZ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (!XmlHandler.InitializeXmlData(PathConst.USERS_PATH) || !XmlHandler.InitializeXmlData(PathConst.TRANSACTIONS_PATH))
            {
                Console.WriteLine("The Program stops running due to an error with Xml initialization.");
                return;
            }

            CommandRunner loginRegisterRunner = new CommandRunner
            {
                Message = "Choose an option ('login' or 'register' or 'exit'): ",
                Commands = new ICommand[] { new ExitCommand(), new LoginCommand(), new RegisterCommand() }
            };

            User user = (User)loginRegisterRunner.Run();

            CommandRunner programRunner = new CommandRunner
            {
                Message = "Add a transaction or query the net income statistics ('add' or 'stats'): ",
                Commands = new ICommand[] { new ExitCommand(), new AddTransactionCommand(), new QueryStatisticsCommand() }
            };

            programRunner.Run(user);
        }
    }
}