using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Globalization;

namespace ToDoListApplication.TODOlist
{
    internal class TodoList
    {
        private List<TodoTask> tasks;

        private int nextTaskId = 1;

        public TodoList()
        {
            tasks = new List<TodoTask>();
        }

        public void AddTask(string taskDescription, string dueTime)
        {
            if (TryParseDateTime(dueTime, out DateTime taskDateTime))
            {
                if (IsDueTimeAfterNow(taskDateTime))
                {
                    TodoTask newTask = new TodoTask(nextTaskId++, taskDescription, taskDateTime);
                    tasks.Add(newTask);
                }
                else
                {
                    Console.WriteLine("Couldn't add task. The due time should be after the actual time!");
                }
            }
            else
            {
                Console.WriteLine("Couldn't add task due to incorrect date format!");
            }
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
                    Console.WriteLine($"ID: {task.Id}, Task: {task.Description} DueTime: {task.DueTime}");
                }
            }
        }

        public async Task UpdateTODO(string fileName)
        {
            try
            {
                string path = await combineDesktopPathToFileName(fileName);
                string jsonString = JsonSerializer.Serialize(tasks);

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
            string relativePath = Path.Combine("TODO list", "egyetemikurzus-2023-osz", "QYWSR3", "TodoListApplication");
            string filePath = Path.Combine(desktopPath, relativePath, fileName);
            return Task.FromResult(filePath); ;
        }

        public async Task LoadTODO(string fileName)
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
            }
            else
            {
                Console.WriteLine("Delete, canceled!");
            }
        }

        private bool TryParseDateTime(string datetimeString, out DateTime parsedDateTime)
        {
            string format = "yyyy-MM-dd HH";
            if (DateTime.TryParseExact(datetimeString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDateTime))
            {
                return true;
            }
            else
            {
                parsedDateTime = DateTime.MinValue;
                return false;
            }
        }

        private bool IsDueTimeAfterNow(DateTime dueTime)
        {
            DateTime now = DateTime.Now;
            return dueTime > now;
        }

        public void DisplayTasksByDueTime()
        {
            Console.WriteLine("Tasks in the to-do list sorted by DueTime:");

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                var groupedTasks = tasks
                    .OrderBy(task => task.DueTime)
                    .GroupBy(task => task.DueTime.Date);

                foreach (var group in groupedTasks)
                {
                    Console.WriteLine($"DueTimeDay: {group.Key:yyyy. MM. dd.}");

                    foreach (var task in group)
                    {
                        Console.WriteLine($"  ID: {task.Id}, Task: {task.Description}   DueHour: {task.DueTime:HH}h");
                    }
                }
            }
        }
    }
}