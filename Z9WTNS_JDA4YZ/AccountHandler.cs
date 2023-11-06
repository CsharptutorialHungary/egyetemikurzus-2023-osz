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
            List<User> users = XmlHandler.ReadObjectsFromXml<User>(PathConst.USERS_PATH);

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
            List<User> users = XmlHandler.ReadObjectsFromXml<User>(PathConst.USERS_PATH);
            Console.Write("Kérem, írja be a Nevét: ");
            string name = Console.ReadLine()!;
            Console.Write("Kérem, írja be a jelszavát: ");
            string password = Console.ReadLine()!;
            string hashedPassword = HashPassword(password);

            Console.Clear();

            if (XmlHandler.AppendObjectsToXml<User>(PathConst.USERS_PATH, new List<User> { new User(users.Count + 1, name, hashedPassword) }))
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
            decimal amount = 0;

            if (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("Nem megfelelő mennyiség, sikertelen tranzakció hozzáadás!");
                return;
            }

            Console.Write("Adj meg egy tranzakciós üzenetet: ");
            string message = Console.ReadLine()!;

            var transactions = XmlHandler.ReadObjectsFromXml<Transaction>(PathConst.TRANSACTIONS_PATH);

            Transaction transaction = new Transaction
            {
                Id = transactions.Count,
                UserId = user.Id,
                Amount = amount,
                Message = message
            };
            
            if(XmlHandler.AppendObjectsToXml<Transaction>(PathConst.TRANSACTIONS_PATH, new List<Transaction> { transaction }))
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
            List<Transaction> transactions = XmlHandler.ReadObjectsFromXml<Transaction>(PathConst.TRANSACTIONS_PATH);

            var userTransactions = transactions.Where(t => t.UserId.Equals(user.Id)).ToList();

            var incomes = userTransactions.Where(t => t.Amount > 0);
            var expenses = userTransactions.Where(t => t.Amount < 0);

            decimal grossIncome = incomes.Sum(t => t.Amount);
            decimal grossExpense = expenses.Sum(t => t.Amount);
            decimal grossFlow = grossExpense + grossIncome;

            decimal netIncome = CalculateNetIncome(grossIncome);
            decimal netExpense = CalculateNetExpense(grossExpense);
            decimal netFlow = netIncome + netExpense;

            decimal realFlow = netIncome - grossExpense;

            var paddedValues = new string[2, 3];
            decimal[] values = new[] { grossIncome, grossExpense, grossFlow, netIncome, netExpense, netFlow };

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    paddedValues[i, j] = values[i * 3 + j].ToString("C").PadLeft(21);
                }
            }

            string paddedRealFlow = realFlow.ToString("C").PadLeft(44);

            Console.WriteLine($"""

                    =================================================================================
                    ||                            Tranzakciós Statisztikák                         ||
                    =================================================================================
                    |          |        Bevétel       |        Kiadás        |        Forgalom      |
                    ---------------------------------------------------------------------------------
                    |  Bruttó  |{paddedValues[0, 0]} |{paddedValues[0, 1]} |{paddedValues[0, 2]} |
                     --------------------------------------------------------------------------------
                    |  Nettó   |{paddedValues[1, 0]} |{paddedValues[1, 1]} |{paddedValues[1, 2]} |
                    ---------------------------------------------------------------------------------
                    |         Valódi Forgalom         |{paddedRealFlow} |
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
