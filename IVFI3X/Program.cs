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
using (StreamReader reader = new StreamReader(path))
{
    try
    {
        existingPlayers = (List<Player>)serializer.Deserialize(reader);
    }
    catch (Exception e)
    {
        existingPlayers = new List<Player>();
        empty =true;

    }
    
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
    Console.WriteLine("2 => Continue the last game");
    Console.WriteLine("3 => Look at my top scores");

    string numInput = Console.ReadLine(); 
    int num; 

    if (int.TryParse(numInput, out num))
    {
        if (num == 1) // new game
        {
            Console.WriteLine("Start a new game");
            Console.WriteLine("Choose the difficulty: easy,medium,hard");
            string difficuilty = getDifficuiltyInput();

        }
        else if (num == 2) // continue game
        {
            Console.WriteLine("Continue the last game");
            //impement continue game 
        }
        else if (num == 3) // show scores
        {
            Console.WriteLine("Look at my top scores");
            showTopScores(player);
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

string getDifficuiltyInput()
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

void generatePlayingMap(string difficulty)
{

    GenerateCell[,] myMap = new GenerateCell[9,9];
    generateMap(myMap);

    PlayCell[,] playMap = new PlayCell[9, 9];



    if (difficulty == "easy")
    {
        
    }
    else if (difficulty == "medium") 
    {
            
    }
    else if (difficulty == "hard")
    {

    }
    
    
}

void generateMap(GenerateCell[,] myArray)
{
    Random random = new Random();
    int filled = 0;

    for (int i = 0; i < myArray.GetLength(0); i++)
    {
        for (int j = 0; j < myArray.GetLength(1); j++)
        {
            myArray[i, j] = new GenerateCell(i, j, 0, true);
        }
    }


    //int oldFilled = 0;
    while (filled!=81)
    {
        //oldFilled = filled;
        for (int i = 0; i < myArray.GetLength(0); i++)
        {
            for (int j = 0; j < myArray.GetLength(1); j++)
            {
                GenerateCell actualCell = myArray[i, j];
                if (actualCell.ValidValues.Count==0 )
                {
                    filled -= crossDeleteValue(myArray, i, j);
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

        Console.WriteLine(filled);
    }
    
}

void printMap(GenerateCell[,] myArray)
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

int crossDeleteValue(GenerateCell[,] myArray, int i, int j)
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



void showTopScores(Player player)
{
    Console.WriteLine("Your best scores:");

    bool noScores = true;

    for (var index = 0; index < player.TopScores.Count; index++)
    {
        var score = player.TopScores[index];
        Console.WriteLine(index + ". " + score);
        noScores = false;
    }

    if (noScores)
    {
        Console.WriteLine("You don't have saved scores yet");
    }
}