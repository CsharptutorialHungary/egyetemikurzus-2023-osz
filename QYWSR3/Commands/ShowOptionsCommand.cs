using System;

internal class ShowOptionsCommand : CommandBase
{
    public ShowOptionsCommand(IHost host) : base(host)
    {
    }

    public override string Name => "options";

    public override void Execute()
    {
        Host.WriteLine("Here are the options you can select from:");
        Host.WriteLine("hello - Sais hello to you");
        Host.WriteLine("options - Shows the options to help you");
        Host.WriteLine("exit - Close the application");
        Host.WriteLine("add - Add to the TODO list");
        Host.WriteLine("view - View the TODO list");
        Host.WriteLine("remove - Remove from the TODO list");
        Host.WriteLine("clear - Clear the consol");
    }
}



