using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public class Maze<CellType> : AbstractMaze where CellType : Cell
    {
        public CellType[,] Cells { get; protected set; }

        public Maze(int width, int height, int? seed = null) : base(width, height, seed) { }


        public override void Generate()
        {
            Initialize();

            List<CellType> visitedCells = new List<CellType>();
            Stack<CellType> cellStack = new Stack<CellType>();
            CellType currentCell = Cells[Random.Next(0, Width), Random.Next(0, Height)];

            visitedCells.Add(currentCell);
            cellStack.Push(currentCell);

            while (cellStack.Any())
            {
                currentCell = cellStack.Pop();
                List<CellType> neighbours = GetUnvisitedNeighbours(currentCell, visitedCells);

                if (neighbours.Any())
                {
                    cellStack.Push(currentCell);
                    CellType targetCell = neighbours[Random.Next(0, neighbours.Count())];
                    RemoveWall(currentCell, targetCell);
                    visitedCells.Add(targetCell);
                    cellStack.Push(targetCell);
                }

            }
        }

        private List<CellType> GetUnvisitedNeighbours(CellType currentCell, List<CellType> visitedCells)
        {
            List<CellType> cells = new List<CellType>();

            if (currentCell.X + 1 < Width && !visitedCells.Contains(Cells[currentCell.X + 1, currentCell.Y])) 
                cells.Add(Cells[currentCell.X + 1, currentCell.Y]);
            if (currentCell.X - 1 >= 0 && !visitedCells.Contains(Cells[currentCell.X - 1, currentCell.Y])) 
                cells.Add(Cells[currentCell.X - 1, currentCell.Y]);
            if (currentCell.Y + 1 < Height && !visitedCells.Contains(Cells[currentCell.X, currentCell.Y + 1])) 
                cells.Add(Cells[currentCell.X, currentCell.Y + 1]);
            if (currentCell.Y - 1 >= 0 && !visitedCells.Contains(Cells[currentCell.X, currentCell.Y - 1]))
                cells.Add(Cells[currentCell.X, currentCell.Y - 1]);

            return cells;
        }

        public void RemoveWall(CellType currentCell ,CellType targetCell)
        {
            // if target cell is to the right
            if (targetCell.X > currentCell.X)
            {
                currentCell.Walls[1] = false;
                targetCell.Walls[3] = false;
            }
            // if target cell is to the left
            if (targetCell.X < currentCell.X)
            {
                currentCell.Walls[3] = false;
                targetCell.Walls[1] = false;
            }
            // if target cell is to the bottom
            if (targetCell.Y > currentCell.Y)
            {
                currentCell.Walls[2] = false;
                targetCell.Walls[0] = false;
            }
            // if target cell is to the top
            if (targetCell.Y < currentCell.Y)
            {
                currentCell.Walls[0] = false;
                targetCell.Walls[2] = false;
            }
        }

        private void Initialize()
        {
            Cells = new CellType[Width, Height];

            for (int column = 0; column < Width; column++)
                for (int row = 0; row < Height; row++)
                    Cells[column, row] = (CellType)Activator.CreateInstance(typeof(CellType), column, row);
        }

    }
}
