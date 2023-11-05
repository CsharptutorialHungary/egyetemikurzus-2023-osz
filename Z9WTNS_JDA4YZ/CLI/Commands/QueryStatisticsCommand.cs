namespace Z9WTNS_JDA4YZ.CLI.Commands
{
    internal class QueryStatisticsCommand : ICommand
    {
        public HashSet<string> Names => new HashSet<string> { "query", "stats", "quers stats", "statistics", "s" };

        public object? Execute(params object[] inputs)
        {
            AccountHandler.QueryStatistics();

            return null;
        }
    }
}
