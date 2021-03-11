using System;

namespace MazeGenerator
{
    public abstract class AbstractMaze
    {
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public long TotalCells => Width * Height;
        public int Seed { get; protected set; }
        protected Random Random { get; }

        public AbstractMaze(int width, int height, int? seed = null)
        {
            if (width <= 0) throw new ArgumentException("Width can not be small then 1");
            if (height <= 0) throw new ArgumentException("Height can not be small then 1");

            Width = width;
            Height = height;

            if (seed is null) Seed = DateTime.Now.Millisecond;
            Random = new Random(Seed);

            Generate();
        }

        public abstract void Generate();
    }
}
