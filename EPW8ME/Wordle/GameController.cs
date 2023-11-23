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

    private List<string> players;
    private Player currentplayer;


    public GameController()
    {
        words = LoadWords();
    }


    public string getInput()
    {
        string input = Console.ReadLine();
        return input;
    }

    public void startGameController()
    {
        string input ="";
        loadMenu(false);
        input=getInput();   //elso input a jatekostol
        Console.WriteLine(input);
        while (input != "5")
        {
            if(input=="1" || input == "2" || input == "3" || input == "4")
            {
                loadMenu(false);
                switch (input)
                {
                    case "1":
                        Console.WriteLine("1 as input");
                        input = getInput();
                        break;
                    case "2":
                        Console.WriteLine("2 as input");
                        input = getInput();
                        break;
                    case "3":
                        Console.WriteLine("3 as input");
                        input = getInput();
                        break;
                    case "4":
                        Console.WriteLine("4 as input");
                        input = getInput();
                        break;
                }                
            }
            else
            {
                loadMenu(true);
                input = getInput();
            }
        }
        loadMenu(false);
        Console.WriteLine("Thanks for Checking out my game!");
        Console.ReadKey();
        return;
    }

    public void newGame()
    {

    }

    public void loadMenu(bool hiba)
    {
        Console.Clear();
        Console.WriteLine(" __      __________ __________________  .____     ___________\r\n/  \\    /  \\_____  \\\\______   \\______ \\ |    |    \\_   _____/\r\n\\   \\/\\/   //   |   \\|       _/|    |  \\|    |     |    __)_ \r\n \\        //    |    \\    |   \\|    `   \\    |___  |        \\\r\n  \\__/\\  / \\_______  /____|_  /_______  /_______ \\/_______  /\r\n       \\/          \\/       \\/        \\/        \\/        \\/ \n");

        if (hiba)
        {
            Console.WriteLine("Press a number to navigate!\n!! A number from the list below !!\n");
            
        }
        else
        {
            Console.WriteLine("Press a number to navigate!\n\n");
        }



        Console.WriteLine("1. New Game\n" +
                          "2. Rules\n" +
                          "3. High-Score\n" +
                          "4. Add a new Word\n" +
                          "5. Exit Game\n");

        
    }

    public void AddNewWord(string newWord)
    {
        if (words == null)
        {
            return;
        }
        string upperCaseWord = newWord.ToUpper();
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
            using (var fileReader = File.OpenText(JsonFilePath))
            {
                string? line = null;
                do
                {
                    line = fileReader.ReadLine();
                    if (line != null) //belerakja a nullt is valamiert sooo....
                    {
                        words.Add(line);
                    }
                } while (line != null);
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