using System.Reflection;

namespace Z9WTNS_JDA4YZ.XML
{
    internal static class PathConst
    {
        public static string USERS_PATH { get => Path.Combine(new string[] {GetProgramPath(), "data", "users.xml" }); }

        public static string TRANSACTIONS_PATH { get => Path.Combine(new string[] { GetProgramPath(), "data", "transactions.xml" }); }

        private static string GetProgramPath()
        {
            return Path.GetDirectoryName(Assembly.GetAssembly(typeof(PathConst))?.Location)!;
        }
    }
}
