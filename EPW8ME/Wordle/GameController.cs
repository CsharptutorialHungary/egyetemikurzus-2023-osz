using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class GameController
{
    private FileManager<string> answerFileManager;
    private FileManager<Player> playerFileManager;
    private List<string> words;
    private List<Player> players;

    public GameController()
    {
        answerFileManager = new FileManager<string>("Answers.json");
        playerFileManager = new FileManager<Player>("Players.json");
        words = new List<string>();
        players = new List<Player>();
    }

    public string getInput()
    {
        string input = Console.ReadLine();
        if (input == null)
        {
            input = "";
        }
        return input.ToUpper();
    }   //done

    public async Task startGameController()
    {
        words = await answerFileManager.LoadDataAsync();
        players = await playerFileManager.LoadDataAsync();
        string input = "";
        loadMenu(false);
        input = getInput();

        Console.WriteLine(input);
        while (input != "6")
        {
            if (input == "1" || input == "2" || input == "3" || input == "4" || input == "5")
            {
                loadMenu(false);
                await handleMenuInputAsync(input);
                input = "";
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
    }   //done

    public async Task handleMenuInputAsync(string input)
    {
        switch (input)
        {
            case "1":
                await newGameAsync();                
                break;
            case "2":
                showRules();                
                break;
            case "3":
                showHighScore();                
                break;
            case "4":
                showAnswerList();                
                break;
            case "5":
                await showAddNewWordAsync();                
                break;
        }
    }   //done

    public async Task newGameAsync()
    {
        bool testing = false;
        testing = true;

        logo();
        string currentAnswer = await GetAnAnswerAsync();
        Console.WriteLine(currentAnswer);
        bool lost = false;
        int collectedScore = 0, reachedRound = 0;
        while (!lost)
        {
            logo();
            Console.WriteLine("Round " + reachedRound + ", score: " + collectedScore + (testing ? $"[TESTING] the answer is {currentAnswer}" : ""));
            Guess[] guessFeedbacks = new Guess[6];
            string[] allMyGuesses = new string[6];
            for (int i = 0; i < 6; i++) //6-ot tippelhetunk
            {
                tries(i + 1);
                string currentGuess = getInput();

                while (!isInValidInput(currentGuess))
                {
                    reprintPreviousLines(guessFeedbacks, allMyGuesses, i + 1, reachedRound, collectedScore, currentAnswer, testing, true);
                    currentGuess = getInput();
                }
                allMyGuesses[i] = currentGuess;
                if (currentGuess == currentAnswer)
                {
                    collectedScore=handlePointCollection(collectedScore, i);
                    reachedRound++;
                    currentAnswer = await GetAnAnswerAsync();
                    break;
                }
                else
                {
                    guessFeedbacks[i] = new Guess(currentAnswer, currentGuess);
                    Console.WriteLine("\t\t    " + guessFeedbacks[i].getFeedback());
                    if (i == 5)
                    {
                        lost = true;
                    }
                }
            }

        }
        await saveGameAsync(collectedScore, reachedRound, currentAnswer);
        Console.ReadKey();

    }   //done

    public int handlePointCollection(int score,int index)
    {
        switch (index)
        {
            case 0:
                score += 100;
                break;
            case 1:
                score += 80;
                break;
            case 2:
                score += 60;
                break;
            case 3:
                score += 40;
                break;
            case 4:
                score += 20;
                break;
            case 5:
                score += 5;
                break;
        }
        return score;
    }   //done

    public bool isInValidInput(string input)
    {
        if (input == null)
        {
            return false;
        }
        if (input.Length != 5)
        {
            return false;
        }
        return input.All(char.IsLetter);
    }   //done

    public void reprintPreviousLines(Guess[] guesses, string[] feedbacks, int piece, int round, int score, string answer, bool testing, bool inputError)
    {
        logo();
        if (inputError)
        {
            Console.WriteLine("Wrong input, the guess should be a 5 letter long word!");
        }
        else
        {
            Console.WriteLine();
        }
        Console.WriteLine("Round " + round + ", score: " + score + (testing ? $"[TESTING] the answer is {answer}" : ""));
        for (int i = 0; i < piece; i++)
        {
            if (i != piece - 1)
            {
                tries(i + 1);
                Console.WriteLine(feedbacks[i]);
            }
            else
            {
                tries(i + 1);
                //Console.Write(guesses[i].getFeedback());
            }
            if (i != piece - 1)
            {
                Console.WriteLine("\t\t    " + guesses[i].getFeedback());
            }
        }
    }   //done

    public async Task saveGameAsync(int score, int round, string answer)
    {
        logo();
        Console.WriteLine("You couldn't guess the word: " + answer + "\nYou got " + score + "points in " + round + " rounds!" +
                            "\nGive us your name, to save your scores. (note: you can't use the name \'EXIT\')");
        string name = "";
        name = getInput();
        while (name == "" || name=="EXIT")
        {
            Console.WriteLine("You need to give us a name.");
            name = getInput();
        }
        Player currentplayer = new Player
        {
            name = name.ToUpper(),
            score = score,
            round = round,
        };
        players.Add(currentplayer);
        await playerFileManager.SaveDataAsync(players);
        logo();
        Console.WriteLine("Your record is now saved " + name);
    }   //done

    public void tries(int currentTry)
    {
        switch (currentTry)
        {
            case 1:
                Console.Write("1st guess (100pts): ");
                break;
            case 2:
                Console.Write("2nd guess  (80pts): ");
                break;
            case 3:
                Console.Write("3rd guess  (60pts): ");
                break;
            case 4:
                Console.Write("4th guess  (40pts): ");
                break;
            case 5:
                Console.Write("5th guess  (20pts): ");
                break;
            case 6:
                Console.Write("6th guess   (5pts): ");
                break;
        }
    }   //done

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

    }   //done

    public void showHighScore()
    {
        logo();
        var top10Players = players
            .GroupBy(player => player.name)
            .Select(group => group.OrderByDescending(player => player.score).First())
            .OrderByDescending(player => player.score)
            .ThenBy(player => player.name)
            .Take(10)
            .ToList();
        Console.WriteLine("Top 10 (or less) Players by reached score:");
        int place = 1;
        foreach (var player in top10Players)
        {
            Console.WriteLine($"{place}. {player.name} - with {player.score} points in {player.round} round.");
            place++;
        }

        Console.WriteLine("\nYou can check for any players records, if you give us it's username (or write \"exit\") to get back to menu:");
        string input = getInput();
        while (input != "EXIT")
        {
            searchPlayer(players, input);
            input = getInput();
        }
        Console.WriteLine("You are getting back to the menu. Press any key to continue!");
        Console.ReadKey();
    }   //done

    public void searchPlayer(List<Player> playerRecords, string playerName)
    {
        var playerDetails = playerRecords
            .Where(player => player.name == playerName)
            .ToList();

        if (playerDetails.Any())
        {
            int highestScore = playerDetails.Max(player => player.score);
            double averageScore = playerDetails.Average(player => player.score);
            int highestRound = playerDetails.Max(player => player.round);
            double averageRound = playerDetails.Average(player => player.round);

            logo();
            Console.WriteLine($"{playerName} details:");
            Console.WriteLine($"   Highest score: {highestScore}  Average score: {averageScore:F2}");
            Console.WriteLine($"   Highest round: {highestRound}  Average round: {averageRound:F2}");
            Console.WriteLine("\nYou can check for any players records, if you give us it's username (or write \"exit\") to get back to menu:");
        }
        else
        {
            logo();
            Console.WriteLine($"Player with {playerName} name does not played yet.");
            Console.WriteLine("\nYou can check for any players records, if you give us it's username (or write \"exit\") to get back to menu:");
        }
    }   //done

    public void showAnswerList()
    {
        logo();
        words.Sort();
        foreach (string word in words)
        {
            Console.WriteLine(word);
        }

        Console.WriteLine($"\nCurrent possible answers: {words.Count}\nPress any key to get back to menu");
        Console.ReadKey();

    }   //done

    public async Task showAddNewWordAsync()
    {
        logo();
        Console.WriteLine("Give us a new 5 letter word:");
        string input = getInput();
        while (!isInValidInput(input))
        {
            logo();
            Console.WriteLine("The Word you gave should not be an answer!\n(possible errors: not 5 letter long, using numbers or special characters)");
            Console.WriteLine("Give us a new 5 letter word:");
            input = getInput();
        }
        logo();

        if (!words.Contains(input))
        {
            words.Add(input.ToUpper());
            await answerFileManager.SaveDataAsync(words);
            Console.WriteLine($"{input} is now added to the answer list!\n");
        }
        else
        {
            Console.WriteLine($"{input} is already on the list!\n");
        }
        Console.WriteLine("Press any key to get back to the menu");
        Console.ReadKey();
    }   //done

    public void logo()
    {
        Console.Clear();
        Console.WriteLine(" __      __________ __________________  .____     ___________\r\n/  \\    /  \\_____  \\\\______   \\______ \\ |    |    \\_   _____/\r\n\\   \\/\\/   //   |   \\|       _/|    |  \\|    |     |    __)_ \r\n \\        //    |    \\    |   \\|    `   \\    |___  |        \\\r\n  \\__/\\  / \\_______  /____|_  /_______  /_______ \\/_______  /\r\n       \\/          \\/       \\/        \\/        \\/        \\/ \n");
    }   //done

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

    public async Task AddNewWordAsync(string newWord)
    {
        string upperCaseWord = newWord.ToUpper();
        if (!words.Contains(upperCaseWord))
        {
            if (upperCaseWord.Length == 5)
            {
                words.Add(upperCaseWord.ToUpper());
                await answerFileManager.SaveDataAsync(words);
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
    }   //done

    public async Task<string> GetAnAnswerAsync()
    {
        if (words == null)
        {
            words = await answerFileManager.LoadDataAsync();
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
    }   //done

    public void getWords()
    {
        foreach (string word in words)
        {
            Console.WriteLine(word);
        }
    }   //done
}
