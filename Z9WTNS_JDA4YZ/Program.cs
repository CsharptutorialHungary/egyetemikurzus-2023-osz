using Z9WTNS_JDA4YZ.CLI;
using Z9WTNS_JDA4YZ.CLI.Commands;
using Z9WTNS_JDA4YZ.DataClasses;
using Z9WTNS_JDA4YZ.XML;

namespace Z9WTNS_JDA4YZ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string USERS_PATH = @".\data\users.xml";
            const string TRANSACTIONS_PATH = @".\data\transactions.xml";

            if (!XMLHandler.InitializeXMLData(USERS_PATH) || !XMLHandler.InitializeXMLData(TRANSACTIONS_PATH))
            {
                Console.WriteLine("The Program stops running due to an error with xml initialization.");
                return;
            }

            /*CommandRunner commandRunner = new CommandRunner
            {
                Message = "\rChoose an option ('login' or 'register' or 'exit'): ",
                Commands = new ICommand[] {new ExitCommand(), new LoginCommand(), new RegisterCommand()}
            };
            
            commandRunner.Run();
             */

            List<User> users = new List<User>()
            {
                new User(0, "Jenő", "asdasdasdasdasd")
            };

            XMLHandler.writeObjectsToXML(USERS_PATH, users);
        }
    }
}