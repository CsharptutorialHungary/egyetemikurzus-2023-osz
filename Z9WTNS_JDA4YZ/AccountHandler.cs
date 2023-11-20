using System.Security.Cryptography;
using System.Text;
using Z9WTNS_JDA4YZ.DataClasses;
using Z9WTNS_JDA4YZ.Xml;

namespace Z9WTNS_JDA4YZ
{
    internal static class AccountHandler
    {
        internal static User? Login()
        {
            List<User> users = XmlHandler.ReadObjectsFromXml<User>(PathConst.UsersPath);

            Console.Clear();
            Console.WriteLine("Kérem jelentkezen be!");
            Console.WriteLine("---------------------------------------");

            Console.Write("Kérem, írja be a Nevét: ");
            string name = Console.ReadLine()!;

            Console.Write("Kérem, írja be a jelszavát: ");
            string password = Console.ReadLine()!;

            Console.Clear();

            foreach (var user in users)
            {
                if (user.Username.Equals(name) && VerifyPassword(password, user.HashedPassword))
                {
                    Console.WriteLine("Sikeresen bejelentkezés!");
                    return user;
                }
            }

            Console.WriteLine("Belépés sikertelen!");
            Console.WriteLine();
            return null;
        }

        internal static User? Register()
        {
            Console.Clear();
            Console.WriteLine("Most a regisztrációs menü sorban vagy!");

            List<User> users = XmlHandler.ReadObjectsFromXml<User>(PathConst.UsersPath);

            Console.Write("Kérem, írja be a Nevét: ");
            string name = Console.ReadLine()!;

            Console.Write("Kérem, írja be a jelszavát: ");
            string password = Console.ReadLine()!;
            string hashedPassword = HashPassword(password);

            Console.Clear();

            if (XmlHandler.AppendObjectToXml(PathConst.UsersPath, new User(users.Count, name, hashedPassword)))
            {
                Console.WriteLine("Sikeres regisztráció!");
                return Login();
            }
            else
            {
                Console.WriteLine("Regisztráció sikertelen!");
            }

            Console.WriteLine();
            return null;
        }

        internal static void AddTransaction(User user)
        {
            Console.WriteLine();
            Console.Write("Add meg a tranzakció mennyiségét: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Nem megfelelő mennyiség, sikertelen tranzakció hozzáadás!");
                return;
            }

            Console.Write("Adj meg egy tranzakciós üzenetet: ");
            string message = Console.ReadLine()!;

            var transactions = XmlHandler.ReadObjectsFromXml<Transaction>(PathConst.TransactionsPath);

            Transaction transaction = new Transaction
            {
                Id = transactions.Count,
                UserId = user.Id,
                Amount = amount,
                Message = message
            };
            
            if(XmlHandler.AppendObjectToXml(PathConst.TransactionsPath, transaction))
            {
                Console.WriteLine("Tranzakció sikeresen hozzáadva!");
            }
            else
            {
                Console.WriteLine("A tranzakció hozzáadása sikertelen volt!");
            }

            Console.WriteLine();
        }

        internal static void QueryStatistics(User user)
        {
            List<Transaction> transactions = XmlHandler.ReadObjectsFromXml<Transaction>(PathConst.TransactionsPath);

            var userTransactions = transactions.Where(t => t.UserId.Equals(user.Id)).AsParallel().ToList();

            var incomes = userTransactions.Where(t => t.Amount > 0).AsParallel();
            var expenses = userTransactions.Where(t => t.Amount < 0).AsParallel();

            decimal grossIncome = incomes.Sum(t => t.Amount);
            decimal grossExpense = expenses.Sum(t => t.Amount);
            decimal grossFlow = grossExpense + grossIncome;

            decimal netIncome = CalculateNetIncome(grossIncome);
            decimal netExpense = CalculateNetExpense(grossExpense);
            decimal netFlow = netIncome + netExpense;

            decimal realFlow = netIncome + grossExpense;

            Console.WriteLine($"""

                    =================================================================================
                    ||                            Tranzakciós Statisztikák                         ||
                    =================================================================================
                    |          |        Bevétel       |        Kiadás        |        Forgalom      |
                    ---------------------------------------------------------------------------------
                    |  Bruttó  |{grossIncome, 21:C} |{grossExpense, 21:C} |{grossFlow, 21:C} |
                     --------------------------------------------------------------------------------
                    |  Nettó   |{netIncome, 21:C} |{netExpense, 21:C} |{netFlow, 21:C} |
                    ---------------------------------------------------------------------------------
                    |         Valódi Forgalom         |{realFlow, 44:C} |
                    =================================================================================
                    
                    """);
        }

        private static decimal CalculateNetIncome(decimal grossIncome)
        {
            return grossIncome / 1.15m;
        }

        private static decimal CalculateNetExpense(decimal grossExpense)
        {
            return grossExpense / 1.27m;
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
