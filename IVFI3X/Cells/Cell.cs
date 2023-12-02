using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IVFI3X.Cells
{
    internal class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public bool IsVisible { get; set; }
        public Cell(int x, int y, int value, bool isVisible)
        {
            X = x;
            Y = y;
            Value = value;
            IsVisible = isVisible;
        }
    }
}