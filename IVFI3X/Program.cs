using IVFI3X.Player;
using System.Xml.Serialization;
using IVFI3X.Cells;

Console.WriteLine("Kerlek add meg a neved:");
string name = Console.ReadLine();
Player player = new Player(name);

XmlSerializer serializer = new XmlSerializer(typeof(List<Player>));
List<Player> existingPlayers = null;

string projectDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\"));
string path = Path.Combine(projectDirectory, "player_data.xml");



bool empty = false;
try
{
    using (StreamReader reader = new StreamReader(path))
    {
        try
        {
            existingPlayers = (List<Player>)serializer.Deserialize(reader);
        }
        catch (InvalidOperationException e)
        {
            existingPlayers = new List<Player>();
            empty = true;

        }

    }
}catch (FileNotFoundException)
{
    throw new FileNotFoundException("The player_data.xml is missing!");
}




if (existingPlayers != null && existingPlayers.Any(p => p.Name == player.Name) && !empty)
{
    player = existingPlayers.First(p => p.Name == player.Name);
    Console.WriteLine($"Player {player.Name} is already in the database. Best score: {player.BestScore}");
}
else
{

    existingPlayers.Add(player);

    using (StreamWriter writer = new StreamWriter(path))
    {
        serializer.Serialize(writer, existingPlayers);
    }

    Console.WriteLine("Saved your player in the database.");
}


while (true)
{
    Console.WriteLine();
    Console.WriteLine("Enter the number of what you want to do:");
    Console.WriteLine("1 => Start a new game");
    Console.WriteLine("2 => Look at my top scores");
    Console.WriteLine("3 => Exit the app");

    string numInput = Console.ReadLine(); 
    int num; 

    if (int.TryParse(numInput, out num))
    {
        if (num == 1) // new game
        {
            Console.WriteLine("Start a new game");
            Console.WriteLine("Choose the difficulty: easy,medium,hard");
            PlayCell[,] playMap = GeneratePlayingMap(GetDifficuiltyInput());
            int score= PlayGame(playMap);

            Player foundPlayer = existingPlayers.Find(p => p.Name == player.Name);
            foundPlayer?.TopScores.Add(score);

            using StreamWriter writer = new StreamWriter(path);
            serializer.Serialize(writer, existingPlayers);

        }
        else if (num == 2) // show scores
        {
            Console.WriteLine("Look at my top scores");
            ShowTopScores(player);
        }
        else if (num == 3) // exit
        {
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("The number you imputed is not in the list");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid integer.");
    }

}




int PlayGame(PlayCell[,] playMap)
{

    int score = NonVisibleCount(playMap);

    while (NonVisibleCount(playMap)!=0)
    {
        Console.WriteLine();
        Console.WriteLine("You have to guess the cells marked with X");
        PrintPlayMap(playMap);
        int[] nextStep = GetNextStep();

        if (playMap[nextStep[0], nextStep[1]].Value == nextStep[2])
        {
            playMap[nextStep[0], nextStep[1]].IsVisible = true;
            Console.WriteLine("Good guess ^^");
        }
        else
        {
            Console.WriteLine("Wrong guess :p");
            score--;
        }
    }

    Console.WriteLine("Congratulations! You have finished the game.");
    Console.WriteLine("Your score was: " + score);
    return score;

}

int NonVisibleCount(PlayCell[,] playMap)
{
    int count = 0;
    foreach (PlayCell cell in playMap)
    {
        if (!cell.IsVisible)
        {
            count++;
        }
    }

    return count;
}


int[] GetNextStep()
{
    int[] nextStep = new int[3];
    Console.WriteLine("Enter your next step:");
    Console.Write("X coordinate (1-9)(sor): ");
    string xInput = Console.ReadLine();
    int x;
    if (int.TryParse(xInput, out x) && x >= 1 && x <= 9)
    {
        nextStep[0] = x-1;
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid integer for X coordinate.");
        return null;
    }
    Console.Write("Y coordinate (1-9)(oszlop): ");
    string yInput = Console.ReadLine();
    int y;
    if (int.TryParse(yInput, out y) && y >= 1 && y <= 9)
    {
        nextStep[1] = y - 1;
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid integer for Y coordinate.");
        return null;
    }
    Console.Write("Value (1-9): ");
    string valueInput = Console.ReadLine();
    int value;
    if (int.TryParse(valueInput, out value) && value >= 1 && value <= 9)
    {
        nextStep[2] = value;
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid integer between 1 and 9 for the value.");
        return null;
    }
    return nextStep;
}


string GetDifficuiltyInput()
{
    string difficulty;
    while (true)
    {
        difficulty = Console.ReadLine();
        if (difficulty == "easy" || difficulty == "medium" || difficulty == "hard")
        {
            break;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid difficulty level: easy, medium, or hard.");
        }
    }

    return difficulty;
}

PlayCell[,] GeneratePlayingMap(string difficulty)
{

    GenerateCell[,] myMap = new GenerateCell[9,9];
    GenerateMap(myMap);

    PlayCell[,] playMap = new PlayCell[9, 9];



    if (difficulty == "easy")
    {
        FillPlayMap(playMap, myMap, 0.95);
        
    }
    else if (difficulty == "medium") 
    {
        FillPlayMap(playMap, myMap, 0.7);
        
    }
    else if (difficulty == "hard")
    {
        FillPlayMap(playMap, myMap, 0.5);
        
    }
    
    return playMap;
}

void FillPlayMap(PlayCell[,] playMap, GenerateCell[,] myMap, double v)
{
    Random random = new Random();
    for (int i = 0; i < playMap.GetLength(0); i++)
    {
        for (int j = 0; j < playMap.GetLength(1); j++)
        {
            bool isVisible = random.NextDouble() < v;
            playMap[i, j] = new PlayCell(i, j, myMap[i, j].Value, isVisible);
        }
    }
}

void GenerateMap(GenerateCell[,] myArray)
{
    Random random = new Random();
    int filled = 0;

    for (int i = 0; i < myArray.GetLength(0); i++)
    {
        for (int j = 0; j < myArray.GetLength(1); j++)
        {
            myArray[i, j] = new GenerateCell(i, j, 0);
        }
    }


    
    while (filled!=81)
    {
        for (int i = 0; i < myArray.GetLength(0); i++)
        {
            for (int j = 0; j < myArray.GetLength(1); j++)
            {
                GenerateCell actualCell = myArray[i, j];
                if (actualCell.ValidValues.Count==0 )
                {
                    filled -= CrossDeleteValue(myArray, i, j);
                    UpdateCells(myArray);
                }
                else if (actualCell.Value==0)
                {
                    int randomIndex = random.Next(0, actualCell.ValidValues.Count);
                    int randomNumber = actualCell.ValidValues[randomIndex];

                    actualCell.Value = randomNumber;
                    UpdateCells(myArray);
                    filled++;
                    
                }
            }
        }
    }
    
}

void PrintGenerateMap(GenerateCell[,] myArray)
{
    for (int i = 0; i < myArray.GetLength(0); i++)
    {
        for (int j = 0; j < myArray.GetLength(1); j++)
        {
            Console.Write(myArray[i, j].Value + " ");
        }
        Console.WriteLine();
    }
}

void PrintPlayMap(PlayCell[,] myArray)
{
    for (int i = 0; i < myArray.GetLength(0); i++)
    {
        for (int j = 0; j < myArray.GetLength(1); j++)
        {
            if (myArray[i, j].IsVisible)
            {
                Console.Write(myArray[i, j].Value + " ");
            }
            else
            {
                Console.Write("X" + " ");
            }
            
        }
        Console.WriteLine();
    }
}

int CrossDeleteValue(GenerateCell[,] myArray, int i, int j)
{

    int count = 0;
    
    for (int x = 0; x < myArray.GetLength(0); x++)
    {
        if (myArray[x, j].Value!=0)
        {
            myArray[x, j].Value = 0;
            count++;
        }
        
    }
    
    for (int y = 0; y < myArray.GetLength(1); y++)
    {
        if (myArray[i, y].Value != 0)
        {
            myArray[i, y].Value = 0;
            count++;
        }
    }

    return count;
}

void UpdateCells(GenerateCell[,] myArray)
{
    

    for (int i = 0; i < myArray.GetLength(0); i++)
    {
        for (int j = 0; j < myArray.GetLength(1); j++)
        {
            List<int> myValidValues = Enumerable.Range(1, 9).ToList();

            //row
            for (int x = 0; x < myArray.GetLength(0); x++)
            {
                if (x != i)
                {
                    myValidValues.Remove(myArray[x, j].Value);
                }
            }
            //column
            for (int y = 0; y < myArray.GetLength(1); y++)
            {
                if (y != j)
                {
                    myValidValues.Remove(myArray[i, y].Value);
                }
            }

            myArray[i,j].ValidValues= myValidValues;

        }
    }

    
}

void ShowTopScores(Player player)
{
    Console.WriteLine("Your best scores:");

    bool noScores = true;

    for (var index = 0; index < player.TopScores.Count; index++)
    {
        var score = player.TopScores[index];
        Console.WriteLine(index+1 + ". " + score);
        noScores = false;
    }

    if (noScores)
    {
        Console.WriteLine("You don't have saved scores yet");
    }
}