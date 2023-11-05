using System;
using System.Collections.Generic;
using Z9WTNS_JDA4YZ.DataClasses;
using Z9WTNS_JDA4YZ.Xml;
using System.Security.Cryptography;
using System.Text;

namespace Z9WTNS_JDA4YZ
{
    internal static class AccountHandler
    {
        internal static User? Login()
        {
            List<User> users = XmlHandler.ReadObjectsFromXml<User>(PathConst.USERS_PATH);
            Console.WriteLine("Kérem jelentkezen be!");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Kérem, írja be a Nevét: ");
            string name = Console.ReadLine();
            Console.WriteLine("Kérem, írja be a jelszavát ");
            string password = Console.ReadLine();

            foreach (var user in users)
            {
                if (user.Username.Equals(name) && VerifyPassword(password, user.HashedPassword))
                {
                    Console.WriteLine("Sikeresen bent vagy");
                    return user;
                }
            }

            Console.WriteLine("Belépés sikertelen");
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
            string hashedPassword = HashPassword(password);

            if (XmlHandler.AppendObjectsToXml<User>(PathConst.USERS_PATH, new List<User> { new User(users.Count + 1, name, hashedPassword) }))
            {
                Console.WriteLine("Sikeres volt a regisztráció");
                return Login();
            }
            else
            {
                Console.WriteLine("Regisztráció sikertelen");
            }

            return null;
        }

        internal static void AddTransaction(User user)
        {
            // Itt adhatsz hozzá tranzakciókat a felhasználóhoz
        }

        internal static void QueryStatistics(User user)
        {
            // Itt lekérdezheted a felhasználó statisztikáit
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            }
        }

        private static bool VerifyPassword(string password, string hashedPassword)
        {
            string newPasswordHash = HashPassword(password);
            return newPasswordHash == hashedPassword;
        }
    }
}
