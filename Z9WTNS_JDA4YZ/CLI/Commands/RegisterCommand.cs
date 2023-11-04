namespace Z9WTNS_JDA4YZ.CLI.Commands
{
    internal class RegisterCommand : ICommand
    {
        public HashSet<string> Names => new HashSet<string> { "register", "r" };

        public object? Execute()
        {
            return AccountHandler.Register();
        }
    }
}
