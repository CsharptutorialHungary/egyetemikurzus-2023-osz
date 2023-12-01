using System;

internal class TodoTask
{
    public int Id { get; }
    public string Description { get; }

    public TodoTask(int id, string description)
    {
        Id = id;
        Description = description;
    }
}
