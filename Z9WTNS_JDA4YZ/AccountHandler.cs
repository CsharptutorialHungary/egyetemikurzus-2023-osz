using Z9WTNS_JDA4YZ.CLI;
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


            return null;
        }

        internal static void AddTransaction()
        {

        }

        internal static void QueryStatistics()
        {

        }
    }
}
