using System;
using System.Collections.Generic;
using System.Text.Json;


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

    public async void UpdateTODO(string fileName)
    {
        try
        {
            string path = await combineDesktopPathToFileName(fileName);
            string jsonString = JsonSerializer.Serialize(tasks);

            // Create the file if it doesn't exist, overwrite if it does
            using (FileStream fs = File.Open(path, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(jsonString);
                }
            }

            Console.WriteLine($"Tasks updated!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating tasks to file: {ex.Message}");
        }
    }
    private Task<string> combineDesktopPathToFileName(string fileName)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        string relativePath = Path.Combine("TODO list", "egyetemikurzus-2023-osz", "QYWSR3", "TODO list");
        string filePath = Path.Combine(desktopPath, relativePath, fileName);
        return Task.FromResult(filePath); ;
    }

    public async void LoadTODO(string fileName)
    {
        try
        {
            string path = await combineDesktopPathToFileName(fileName);
            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);
                List<TodoTask> loadedTasks = JsonSerializer.Deserialize<List<TodoTask>>(jsonString);

                if (loadedTasks != null)
                {
                    tasks = loadedTasks;
                    Console.WriteLine($"Tasks loaded successfully from {path}!");
                }
                else
                {
                    Console.WriteLine($"Error loading tasks from {path}. JSON data is invalid.");
                }
            }
            else
            {
                Console.WriteLine($"File {fileName} not found. No tasks loaded.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading tasks from file: {ex.Message}");
        }
    }

public void DeleteAllTODO(string confirmation)
    {
        if (confirmation == "yes")
        {
            tasks.Clear();
            Console.WriteLine("TodoList deleted!");
        } else
        {
            Console.WriteLine("Delete, canceled!");
        }
    }
}