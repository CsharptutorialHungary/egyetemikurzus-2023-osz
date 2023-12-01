using System;

internal record TodoTask
{
    public int Id { get; init; }
    public string Description { get; init; }

    public TodoTask(int id, string description)
    {
        Id = id;
        Description = description;
    }
}

