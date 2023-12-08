namespace IVFI3X.Player
{
    public class Player
    {
        public string Name { get; set; }
        public List<int> TopScores { get; set; }

        public int BestScore
        {
            get
            {
                if (TopScores.Count > 0)
                {
                    return TopScores.Max();

                }
                else
                {
                    return 0;
                }

            }
        }

        public Player()
        {
            TopScores = new List<int>();
        }

        public Player(string name) : this()
        {
            Name = name;
        }
        public void AddScore(int score)
        {
            TopScores.Add(score);
            TopScores = TopScores
                .OrderByDescending(s => s)
                .Take(10)
                .ToList();
        }


    }
}