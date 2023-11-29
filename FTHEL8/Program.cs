using System.Data.SQLite;
using System.Diagnostics;
using FTHEL8.Data;

namespace FTHEL8
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseInit.ReadEmployees();
        }
    }
}
