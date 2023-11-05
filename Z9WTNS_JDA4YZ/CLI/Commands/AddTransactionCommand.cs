namespace Z9WTNS_JDA4YZ.CLI.Commands
{
    internal class AddTransactionCommand : ICommand
    {
        public HashSet<string> Names => new HashSet<string> { "add", "a", "add transaction", "transaction", "t" };

        public object? Execute(params object[] inputs)
        {
            AccountHandler.AddTransaction();

            return null;
        }
    }
}
