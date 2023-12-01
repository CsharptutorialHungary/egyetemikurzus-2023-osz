using System;
using System.Collections.Generic;

internal class TodoList
{
    private List<TodoTask> tasks;
    private int nextTaskId = 1;

    public TodoList()
    {
        tasks = new List<TodoTask>();
    }

    public void AddTask(string taskDescription)
    {
        TodoTask newTask = new TodoTask(nextTaskId++, taskDescription);
        tasks.Add(newTask);
    }

    public void RemoveTask(int taskId)
    {
        TodoTask taskToRemove = tasks.Find(task => task.Id == taskId);

        if (taskToRemove != null && tasks.Remove(taskToRemove))
        {
            Console.WriteLine($"Task with ID {taskId} removed successfully.");
        }
        else
        {
            Console.WriteLine($"Task with ID {taskId} not found.");
        }
    }

    public void DisplayTasks()
    {
        Console.WriteLine("Tasks in the to-do list:");

        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks found.");
        }
        else
        {
            foreach (var task in tasks)
            {
                Console.WriteLine($"ID: {task.Id}, Task: {task.Description}");
            }
        }
    }

}