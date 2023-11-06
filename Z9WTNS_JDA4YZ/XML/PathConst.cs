using System.Reflection;

namespace Z9WTNS_JDA4YZ.Xml
{
    internal static class PathConst
    {
        private static string PROGRAM_PATH { get => Path.GetDirectoryName(Assembly.GetAssembly(typeof(PathConst))?.Location)!; }

        public static string USERS_PATH { get => Path.Combine(new string[] { PROGRAM_PATH, "data", "users.Xml" }); }

        public static string TRANSACTIONS_PATH { get => Path.Combine(new string[] { PROGRAM_PATH, "data", "transactions.Xml" }); }
    }
}
