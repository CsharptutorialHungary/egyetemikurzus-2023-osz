using System;

internal class ClearCommand : CommandBase
{
    public ClearCommand(IHost host) : base(host)
    {
    }

    public override string Name => "clear";

    public override void Execute()
    {
        Host.Clear();
    }
}


