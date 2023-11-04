using System.Reflection;
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
            

            if (!XMLHandler.InitializeXMLData(PathConst.USERS_PATH) || !XMLHandler.InitializeXMLData(PathConst.TRANSACTIONS_PATH))
            {
                Console.Write(PathConst.USERS_PATH, "\n");
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

            XMLHandler.writeObjectsToXML(PathConst.USERS_PATH, users);
        }
    }
}