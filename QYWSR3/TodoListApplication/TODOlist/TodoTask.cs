using System;

namespace ToDoListApplication.TODOlist
{
    internal record TodoTask
    {
        public int Id { get; init; }
        public string Description { get; init; }
        public DateTime DueTime { get; set; }

        public TodoTask(int id, string description, DateTime dueTime)
        {
            Id = id;
            Description = description;
            DueTime = dueTime;
        }
    }
}
