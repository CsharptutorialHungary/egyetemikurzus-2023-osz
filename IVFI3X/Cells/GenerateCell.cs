namespace IVFI3X.Cells
{
    internal class GenerateCell : Cell
    {
        public int Value { get; set; }
        public List<int> ValidValues { get; set; }
        public GenerateCell(int x, int y, int value)
            : base(x, y)
        {
            Value = value;
            ValidValues = Enumerable.Range(1, 9).ToList();
        }


    }
}
