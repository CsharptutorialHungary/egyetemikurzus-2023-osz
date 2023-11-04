namespace Z9WTNS_JDA4YZ.DataClasses
{
    [Serializable]
    internal sealed record class Transaction(int Id, int UserId, long Amount, string Message);
}
