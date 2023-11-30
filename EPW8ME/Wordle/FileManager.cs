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

    public void SaveData(List<T> data)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            File.WriteAllText(filePath, jsonString);
        }
        catch (IOException e)
        {
            Console.WriteLine($"Error saving data: {e.Message}");
        }
    }

    public List<T> LoadData()
    {
        List<T> loadedData = new List<T>();
        try
        {
            // Read the JSON content from the file
            string jsonString = File.ReadAllText(filePath);

            // Deserialize the JSON array into a list of players
            loadedData = JsonSerializer.Deserialize<List<T>>(jsonString);

            Console.WriteLine($"Data loaded from {filePath}");
        }
        catch (IOException e)
        {
            Console.WriteLine($"ERROR while loading data from: {filePath}\n{e.Message}");
            Console.WriteLine($"A new File is beeing created as \"{fileName}\" with an empty ");
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
