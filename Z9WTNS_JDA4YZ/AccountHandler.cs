using Z9WTNS_JDA4YZ.DataClasses;
using Z9WTNS_JDA4YZ.Xml;

namespace Z9WTNS_JDA4YZ
{
    internal static class AccountHandler
    {
        internal static User? Login()
        {
            List<User> users = XmlHandler.ReadObjectsFromXml<User>(PathConst.USERS_PATH);
            

            return null;
        }

        internal static User? Register()
        {
            List<User> users = XmlHandler.ReadObjectsFromXml<User>(PathConst.USERS_PATH);
            
            XmlHandler.AppendObjectsToXml<User>(PathConst.USERS_PATH, new List<User>() { new User(users.Count+1, "jeno", "123") });

            Console.WriteLine("regiszteáricó");



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
