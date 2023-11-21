using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class GameController
{
    private static string appPath = AppContext.BaseDirectory;
    private static string fileName = "Answers.json";
    private string JsonFilePath = Path.Combine(appPath, fileName);
    private List<string> words;

    public GameController()
    {
        words = LoadWords();
    }

    public void AddNewWord(string newWord)
    {
        if(words == null)
        {
            return;
        }
        string upperCaseWord=newWord.ToUpper();
        if (!words.Contains(newWord.ToUpper()))
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
        else
        {
            Console.WriteLine("This word is already on the list");
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
            using(var fileReader = File.OpenText(JsonFilePath))
            {
                string? line = null;
                do
                {
                    line=fileReader.ReadLine();
                    if (line != null) //belerakja a nullt is valamiert sooo....
                    {
                        words.Add(line);
                    }
                }while (line != null);
                fileReader.Dispose();
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e.Message}");
            Console.WriteLine("A new File is being created with a single answer: PLANE");
            using (var fileCreater = File.CreateText(JsonFilePath))
            {
                fileCreater.WriteLine("PLANE");
                fileCreater.Dispose();
            }
            
        }
        return words;
        
    }

    private void SaveWords()
    {
        try
        {
            // Serialize each word to a separate line.
            string[] lines = words.ToArray();
            File.WriteAllLines(JsonFilePath, lines);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving words: {e.Message}");
        }
    }

    public void getWords()
    {
        foreach (string word in words)
        {
            Console.WriteLine(word);
        }
    }
}