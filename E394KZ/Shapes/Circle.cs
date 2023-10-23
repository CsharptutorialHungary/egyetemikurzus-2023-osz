﻿namespace E394KZ.Shapes
{
    internal record Circle : BaseShape
    {
        public uint Radius { get; init; }
        public Circle(string name, uint x, uint y, ConsoleColor color, uint radius) : base(name, x, y, color)
        {
            Radius = radius;
        }
        public override void Draw(Canvas canvas)
        {
            for (uint y = 0; y < canvas.Height; y++)
            {
                for (uint x = 0; x < canvas.Width; x++)
                {
                    double distance = Math.Sqrt(Math.Pow(x - X, 2) + Math.Pow(y - Y, 2));
                    if (distance < Radius)
                    {
                        canvas[x, y] = Color;
                    }
                }
            }
        }
    }
}
