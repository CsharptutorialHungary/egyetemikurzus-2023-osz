using IVFI3X.Player;
using System.Xml.Serialization;

Console.WriteLine("Kerlek add meg a neved:");
string name = Console.ReadLine();
Player newPlayer = new Player(name);

XmlSerializer serializer = new XmlSerializer(typeof(List<Player>));
List<Player> existingPlayers = null;


string projectDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\"));
string path = Path.Combine(projectDirectory, "player_data.xml");

Console.WriteLine(path);
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




if (existingPlayers != null && existingPlayers.Any(p => p.Name == newPlayer.Name) && !empty)
{
    Player existingPlayer = existingPlayers.First(p => p.Name == newPlayer.Name);
    Console.WriteLine($"Player {existingPlayer.Name} is already in the database. Best score: {existingPlayer.BestScore}");
}
else
{
    existingPlayers.Add(newPlayer);

    using (StreamWriter writer = new StreamWriter(path))
    {
        serializer.Serialize(writer, existingPlayers);
    }

    Console.WriteLine("Saved your player in the database.");
}