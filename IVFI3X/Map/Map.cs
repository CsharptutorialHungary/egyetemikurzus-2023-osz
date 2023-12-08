using IVFI3X.Cells;

namespace IVFI3X.Map
{
    class Map
    {
        public static int NonVisibleCount(PlayCell[,] playMap)
        {
            return playMap.Cast<PlayCell>().AsParallel().Count(cell => !cell.IsVisible);
        }

        public static PlayCell[,] GeneratePlayingMap(string difficulty)
        {

            GenerateCell[,] myMap = new GenerateCell[9, 9];
            GenerateMap(myMap);

            PlayCell[,] playMap = new PlayCell[9, 9];



            switch (difficulty)
            {
                case "easy":
                    FillPlayMap(playMap, myMap, 0.95);
                    break;
                case "medium":
                    FillPlayMap(playMap, myMap, 0.7);
                    break;
                case "hard":
                    FillPlayMap(playMap, myMap, 0.5);
                    break;
            }

            return playMap;
        }

        static void FillPlayMap(PlayCell[,] playMap, GenerateCell[,] myMap, double v)
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

        static void GenerateMap(GenerateCell[,] myArray)
        {
            Random random = new Random();
            int filled = 0;

            Parallel.For(0, myArray.GetLength(0), i =>
            {
                Parallel.For(0, myArray.GetLength(0), j =>
                {
                    myArray[i, j] = new GenerateCell(i, j, 0);
                });
            });




            while (filled != 81)
            {
                for (int i = 0; i < myArray.GetLength(0); i++)
                {
                    for (int j = 0; j < myArray.GetLength(1); j++)
                    {
                        GenerateCell actualCell = myArray[i, j];
                        if (actualCell.ValidValues.Count == 0)
                        {
                            filled -= CrossDeleteValue(myArray, i, j);
                            UpdateCells(myArray);
                        }
                        else if (actualCell.Value == 0)
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


        static int CrossDeleteValue(GenerateCell[,] myArray, int i, int j)
        {

            int count = 0;

            for (int x = 0; x < myArray.GetLength(0); x++)
            {
                if (myArray[x, j].Value == 0) continue;
                myArray[x, j].Value = 0;
                count++;

            }

            for (int y = 0; y < myArray.GetLength(1); y++)
            {
                if (myArray[i, y].Value == 0) continue;
                myArray[i, y].Value = 0;
                count++;
            }

            return count;
        }


        static void UpdateCells(GenerateCell[,] myArray)
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

                    myArray[i, j].ValidValues = myValidValues;

                }
            }


        }

    }
}
