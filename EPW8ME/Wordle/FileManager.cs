using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class FileManager<T>
{
    private string filePath;
    private string fileName;
    public FileManager(string fileNameValue)
    {
        string appPath = AppContext.BaseDirectory;
        filePath = Path.Combine(appPath, fileNameValue);
        fileName = fileNameValue;
    }

    public async Task SaveDataAsync(List<T> data)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await File.WriteAllTextAsync(filePath, jsonString);
        }
        catch (IOException e)
        {
            Console.WriteLine($"Error saving data: {e.Message}");
        }
    }

    public async Task<List<T>> LoadDataAsync()
    {
        List<T> loadedData = new List<T>();
        try
        {
            string jsonString = await File.ReadAllTextAsync(filePath);
            loadedData = JsonSerializer.Deserialize<List<T>>(jsonString);
            Console.WriteLine($"Data loaded from {filePath}");
        }
        catch (IOException e)
        {
            Console.WriteLine($"ERROR while loading data from: {filePath}\n{e.Message}");
            Console.WriteLine($"A new File is being created as \"{fileName}\" with an empty ");
            Console.ReadKey();
            using (var fileCreater = File.CreateText(filePath))
            {
                string emptyJsonFileContent = "[]";
                fileCreater.Write(emptyJsonFileContent);
                fileCreater.Dispose();
            }
        }

        return loadedData;
    }
}
