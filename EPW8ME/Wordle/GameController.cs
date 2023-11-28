using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class GameController
{
    private static string appPath = AppContext.BaseDirectory;
    private static string fileName = "Answers.json";
    private static string fileNamePlayers = "Players.json";
    private string JsonFilePathWords = Path.Combine(appPath, fileName);
    private string JsonFilePathPlayers = Path.Combine(appPath, fileNamePlayers);
    private List<string> words;
    private List<Player> players;

    public GameController()
    {
        words = LoadWords();
        players = LoadPlayers();
    }


    public string getInput()
    {
        string input = Console.ReadLine();       
        return input;
    }

    public void startGameController()
    {
        string input = "";
        loadMenu(false);
        input = getInput();   //elso input a jatekostol
        
        Console.WriteLine(input);
        while (input != "6")
        {
            if (input == "1" || input == "2" || input == "3" || input == "4" || input == "5")
            {
                loadMenu(false);
                switch (input)
                {
                    case "1":
                        newGame();
                        input = "";
                        break;
                    case "2":
                        showRules();
                        input = "";
                        break;
                    case "3":
                        showHighScore();
                        Console.ReadKey();
                        input = "";
                        break;
                    case "4":
                        showAnswerList();
                        input = "";
                        break;
                    case "5":
                        //showAddNewWord();
                        input = "";
                        break;
                }
            }
            else if (input == "")
            {
                loadMenu(false);
                input = getInput();
            }
            else
            {
                loadMenu(true);
                input = getInput();
            }
        }
        loadMenu(false);
        Console.WriteLine("Thanks for checking out my game!\n" +
                          "Press any key, to close the program");

        Console.ReadKey();
        return;
    }

    public void newGame()
    {
        
        logo();
        string currentanswer = GetAnAnswer();
        Console.WriteLine(currentanswer);
        //bool lost = false;
        int score=0, round=0;
        /*while (!lost)
        {
            currentanswer = GetAnAnswer();

        }*/
        saveGame(score, round, currentanswer);
        Console.ReadKey();

    }

    public void saveGame(int score, int round,string answer)
    {
        logo();
        Console.WriteLine("You couldn't guess the word: " + answer + "\nYou got " + score + "points in " + round + " rounds!" +
                            "\nGive us your name, to save your scores.");
        string name = "";
        name = getInput();
        while(name == "")
        {
            Console.WriteLine("You need to give us a name.");
            name=getInput();
        }
        Player currentplayer = new Player();
        players.Add(currentplayer);
        savePlayers(players);
        logo();
        Console.WriteLine("Your record is now saved "+name);
    }

    public void showCurrentGame(int currentround, int currentscore, string[] previousGuessesAndFeedback)
    {
        logo();
    }

    public void tries(int currentTry)
    {
        switch (currentTry)
        {
            case 1:
                Console.Write("1st try (100pts): ");
                break;
            case 2:
                Console.Write("2nd try  (80pts): ");
                break;
            case 3:
                Console.Write("3rd try  (60pts): ");
                break;
            case 4:
                Console.Write("4th try  (40pts): ");
                break;
            case 5:
                Console.Write("5th try  (20pts): ");
                break;
            case 6:
                Console.Write("6th try   (5pts): ");
                break;
        }
    }





    public void showRules()
    {
        logo();
        Console.Write("Objective:\n\tThe goal of Wordle is to guess a hidden 5 letter long Word within 6 guesses\n\n" +
                      "Feedback:\n\tPlayer make guesses by suggesting words that they think might be the hidden word.\n" +
                      "\tEach guess has to be 5 letter long.\n" +
                      "\tAfter the guess, the player get a line of feedback for each letter in the guesseg word\n" +
                      "\tExample: the hidden word is \"PLANE\", and the player guessed \"LEAPT\"\n" +
                      "\t\t|LEAPT|\n" +
                      "\t\t|▄▄█▄X|\n\n" +
                      "\tThe symbols meaning:\n" +
                      "\t\t▄ - the letter is in the word, but not in there (in the example \"L\" is in the 1st position, while in the hidden word it's the 2nd letter\n" +
                      "\t\t█ - the letter is in the word, AND in the right place(\"A\" is the 3rd letter in PLANE and in LEAPT)\n" +
                      "\t\tX - the letter is not part of the hidden word, try to guess a word that don't have this letter\n" +
                      "Points:\n" +
                      "\tThe player will have 6 guesses for each new word:\n" +
                      "\tPoints are taken only when you guessed the word correctly.\n" +
                      "\t\tif you guess the word the 1st time, you gain 100pts (sometimes you get lucky).\n" +
                      "\t\tfor each guess you get 20pts less.\n" +
                      "\t\t\t1.try: 100pts\n" +
                      "\t\t\t2.try: 80pts\n" +
                      "\t\t\t3.try: 60pts\n" +
                      "\t\t\t4.try: 40pts\n" +
                      "\t\t\t5.try: 20pts\n" +
                      "\t\t\t6.try: 5pts\n\n" +
                      "If a word you tried cant be a hidden word, you get a warning, and it wont count as a try, so you won't lose a try for it\n" +
                      "You can check the possible words in the \"Possible Words\" option in the menu\n" +
                      "If you can't guess the word in 6 tries, your points will be stored in the highscores, after you give a player name\n" +
                      "\n" +
                      "Press any key to get back to menu");
        Console.ReadKey();

    } //done

    public void showHighScore()
    {
        Console.WriteLine(players.Count);
        Console.ReadKey();
        Console.ReadKey();
    }

    public void showAnswerList()
    {
        logo();
        foreach (string word in words)
        {
            Console.WriteLine(word);
        }

        Console.WriteLine($"\nCurrent possible answers: {words.Count}\nPress any key to get back to menu");
        Console.ReadKey();

    } //done

    public void showAddNewWord()
    {

    }


    public void logo()
    {
        Console.Clear();
        Console.WriteLine(" __      __________ __________________  .____     ___________\r\n/  \\    /  \\_____  \\\\______   \\______ \\ |    |    \\_   _____/\r\n\\   \\/\\/   //   |   \\|       _/|    |  \\|    |     |    __)_ \r\n \\        //    |    \\    |   \\|    `   \\    |___  |        \\\r\n  \\__/\\  / \\_______  /____|_  /_______  /_______ \\/_______  /\r\n       \\/          \\/       \\/        \\/        \\/        \\/ \n");
    } //done
    public void loadMenu(bool isInputWrong)
    {
        logo();
        if (isInputWrong)
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
                          "4. Possible words\n" +
                          "5. Add word\n" +
                          "6. Exit Game\n");
    }   //done

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
        List<string> loadedWords = new List<string>();
        try
        {
            using (var fileReader = File.OpenText(JsonFilePathWords))
            {
                string? line = null;
                do
                {
                    line = fileReader.ReadLine();
                    if (line != null) //belerakja a nullt is valamiert sooo....
                    {
                        loadedWords.Add(line);
                    }
                } while (line != null);
                loadedWords.Sort();
                fileReader.Dispose();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e.Message}");
            Console.WriteLine("A new File is being created with a single answer: PLANE");
            using (var fileCreater = File.CreateText(JsonFilePathWords))
            {
                fileCreater.WriteLine("PLANE");
                fileCreater.Dispose();
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        return loadedWords;
    }

    private void SaveWords()
    {
        try
        {
            // Serialize each word to a separate line.
            string[] lines = words.ToArray();
            File.WriteAllLines(JsonFilePathWords, lines);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving words: {e.Message}\n Press any key to continue");
            Console.ReadKey();
        }
    }

    public void getWords()
    {
        foreach (string word in words)
        {
            Console.WriteLine(word);
        }
    }

    public void savePlayers(List<Player> players)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(players, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(JsonFilePathPlayers, jsonString);

            Console.WriteLine($"Players data saved to {JsonFilePathPlayers}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving players data: {e.Message}");
        }
    }

    public List<Player> LoadPlayers()
    {
        List<Player> loadedPlayers = new List<Player>();
        try
        {
            
        }
        catch (Exception e)
        {
            
        }
        return loadedPlayers;
    }
}
