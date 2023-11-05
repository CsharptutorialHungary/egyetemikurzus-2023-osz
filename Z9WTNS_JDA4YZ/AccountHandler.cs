using Z9WTNS_JDA4YZ.DataClasses;
using Z9WTNS_JDA4YZ.Xml;

namespace Z9WTNS_JDA4YZ
{
    internal static class AccountHandler
    {
        internal static User? Login()
        {
            List<User> users = XmlHandler.ReadObjectsFromXml<User>(PathConst.USERS_PATH);
            Console.WriteLine("Bejelentkezés");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Kérem, írja be a Nevét: ");
            string name = Console.ReadLine();
            Console.WriteLine("Kérem, írja be a jelszavát ");
            string password = Console.ReadLine();
            foreach (var user in users)
            {
                if(user.Username.Equals(name) || user.HashedPassword.Equals(password))
                {
                    Console.WriteLine("Sikeresen bent vagy");
                    break;
                }
                else { Console.WriteLine("Valami nem jó"); break; }

            }

            return null;
        }

        internal static User? Register()
        {
            Console.WriteLine("Most a regisztrációs menü sorban vagy!");
            List<User> users = XmlHandler.ReadObjectsFromXml<User>(PathConst.USERS_PATH);
            Console.WriteLine("Kérem, írja be a Nevét: ");
            string name = Console.ReadLine();
            Console.WriteLine("Kérem, írja be a jelszavát ");
            string password = Console.ReadLine();
            if(XmlHandler.AppendObjectsToXml<User>(PathConst.USERS_PATH, new List<User>() { new User(users.Count + 1, name, password) }))
            {Console.WriteLine("Sikeres volt a regisztráció");
                Login();
            }
            else { Console.WriteLine("Valami nem jó"); }
            

            return null;
        }

        internal static void AddTransaction(User user)
        {

        }

        internal static void QueryStatistics(User user)
        {

        }

    }
}
