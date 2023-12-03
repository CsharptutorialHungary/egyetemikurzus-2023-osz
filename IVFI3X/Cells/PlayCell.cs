namespace IVFI3X.Cells
{
    internal class PlayCell : Cell
    {
        public new int Value { get; }
        public new bool IsVisible { get; set; }
        public PlayCell(int x, int y, int value, bool isVisible) : base(x, y)
        {
            Value = value;
            IsVisible = isVisible;
        }
    }
}
