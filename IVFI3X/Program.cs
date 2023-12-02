using IVFI3X.Player;
using System.Xml.Serialization;

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
        if (num == 1)
        {
            Console.WriteLine("Start a new game");
            //impement new game 
        }
        else if (num == 2)
        {
            Console.WriteLine("Continue the last game");
            //impement continue game 
        }
        else if (num == 3)
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