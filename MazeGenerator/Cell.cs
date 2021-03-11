using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public class Cell
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public bool HasAllWalls => Walls[0] && Walls[1] && Walls[2] && Walls[3];
        public bool HasNorthWall => Walls[0];
        public bool HasEastWall => Walls[1];
        public bool HasSouthWall => Walls[2];
        public bool HasWestWall => Walls[3];

        public List<bool> Walls { get; set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;

            Walls = new List<bool>() { true, true, true, true };
        }

    }
}
