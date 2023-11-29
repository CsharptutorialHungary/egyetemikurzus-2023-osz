using System;
using System.Data.SQLite;

namespace FTHEL8.Data
{
    public class DatabaseInit
    {
        public DatabaseInit()
        {
        }

        private static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = new SQLiteConnection("Data Source=./Database/Database.db;");
            Console.WriteLine(Directory.GetCurrentDirectory());
            try
            {
                sqlite_conn.Open();
                Console.WriteLine("Connection worked!");
            }
            catch (Exception)
            {
                Console.WriteLine("Connection didn't work!");
            }
            return sqlite_conn;
        }

        public static void ReadEmployees()
        {
            using (var connection = CreateConnection())
            {
                using (var command = new SQLiteCommand("SELECT * FROM employees", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Employee Data:");
                        while (reader.Read())
                        {
                            Console.WriteLine($"Employee ID: {reader["employee_id"]}, Name: {reader["name"]}, Position: {reader["position"]}, Salary: {reader["salary"]}");
                        }
                    }
                }
            }
        }
    }
}
