using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Z9WTNS_JDA4YZ.XML
{
    internal static class PathConst
    {


        public static string USERS_PATH = Path.Combine(new string[] { Path.GetDirectoryName(Assembly.GetAssembly(typeof(PathConst))?.Location) , "users.xml" });//".\\", "data","users.xml "});
        public static string TRANSACTIONS_PATH = Path.Combine(new string[] { Assembly.GetAssembly(typeof(Program))?.Location ?? ".\\", "data", "transactions.xml " });

    }
}
