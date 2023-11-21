using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class GameController
{

    private string JsonFilePath = "";
    private List<string> words;

    public GameController()
    {
        words = LoadWords();
    }

    public void AddNewWord(string newWord)
    {
        if (newWord.Length == 5)
        {
            words.Add(newWord.ToUpper());
            SaveWords();
        }
        else
        {
            Console.WriteLine("The word must be 5 characters long.");
        }
    }

    public string GetAnAnswer()
    {
        if (words == null)
        {
            words = LoadWords();
        }

        if (words.Count > 0)
        {
            Random random = new Random();
            int index = random.Next(words.Count);
            return words[index];
        }
        else
        {
            return "No words available.";
        }
    }

    private List<string> LoadWords()
    {
        List<string> words = new List<string>();
        try
        {
            var appPath = AppContext.BaseDirectory;
            var fileName = "Answers.json";
            var JsonFilePath = Path.Combine(appPath, fileName);
            Console.WriteLine(JsonFilePath);

            using(var fileReader = File.OpenText(JsonFilePath))
            {
                string? line = null;
                do
                {
                    line=fileReader.ReadLine();
                    words.Add(line);
                }while (line != null);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e.Message}");
        }        
        return words;
        
    }

    private void SaveWords()
    {
        string json = JsonSerializer.Serialize(words);
        File.WriteAllText(JsonFilePath, json);
    }
}