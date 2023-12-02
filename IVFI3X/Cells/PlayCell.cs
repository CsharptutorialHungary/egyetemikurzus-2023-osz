using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVFI3X.Cells
{
    internal class ImmutableCell : Cell
    {
        public new int X { get; }
        public new int Y { get; }
        public new int Value { get; }
        public new bool IsVisible { get; }
        public ImmutableCell(int x, int y, int value, bool isVisible) : base(x, y, value, isVisible)
        {
            X = x;
            Y = y;
            Value = value;
            IsVisible = isVisible;
        }
    }
}
