using MazeGenerator;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Develop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Maze<Cell> Maze { get; set; }
        SolidColorBrush Brush { get; set; } = new SolidColorBrush(Colors.Black);
        Ellipse Location { get; set; }
        int LocationX { get; set; }
        int LocationY { get; set; }

        int CellHeight => (int)(this.ActualHeight / Maze.Height) - 2;
        int CellWidth => (int)(this.ActualWidth / Maze.Width) - 2;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Window1 startDialog = new Window1();
            if (startDialog.ShowDialog() == true)
            {
                Maze = new Maze<Cell>(startDialog.MazeWidth, startDialog.MazeHeight, startDialog.MazeSeed);
                DrawMaze();
            }
            else return;

            Location = new Ellipse()
            {
                Width = CellWidth / 2,
                Height = CellHeight / 2,
                Fill = new SolidColorBrush(Colors.Red)
            };
            MazeCanvas.Children.Add(Location);

            UpdateLocation(LocationX, LocationY);
        }

        private void DrawMaze()
        {
            for (int width = 0; width < Maze.Width; width++)
                for (int height = 0; height < Maze.Height; height++)
                    DrawCell(Maze.Cells[width, height]);
        }

        private void DrawCell(Cell cell)
        {
            if (cell.HasNorthWall)
            {
                MazeCanvas.Children.Add(new Line()
                {
                    X1 = Math.Max(cell.X * CellWidth, 1),
                    Y1 = Math.Max(cell.Y * CellHeight, 1),
                    X2 = Math.Max(cell.X * CellWidth + CellWidth, 1),
                    Y2 = Math.Max(cell.Y * CellHeight, 1),
                    Stroke = Brush,
                    StrokeThickness = 1
                });
            }

            if (cell.HasEastWall)
            {
                MazeCanvas.Children.Add(new Line()
                {
                    X1 = Math.Max(cell.X * CellWidth + CellWidth, 1),
                    Y1 = Math.Max(cell.Y * CellHeight, 1),
                    X2 = Math.Max(cell.X * CellWidth + CellWidth, 1),
                    Y2 = Math.Max(cell.Y * CellHeight + CellHeight, 1),
                    Stroke = Brush,
                    StrokeThickness = 1
                });
            }

            if (cell.HasSouthWall)
            {
                MazeCanvas.Children.Add(new Line()
                {
                    X1 = Math.Max(cell.X * CellWidth, 1),
                    Y1 = Math.Max(cell.Y * CellHeight + CellHeight, 1),
                    X2 = Math.Max(cell.X * CellWidth + CellWidth, 1),
                    Y2 = Math.Max(cell.Y * CellHeight + CellHeight, 1),
                    Stroke = Brush,
                    StrokeThickness = 1
                });
            }

            if (cell.HasWestWall)
            {
                MazeCanvas.Children.Add(new Line()
                {
                    X1 = Math.Max(cell.X * CellWidth, 1),
                    Y1 = Math.Max(cell.Y * CellHeight, 1),
                    X2 = Math.Max(cell.X * CellWidth, 1),
                    Y2 = Math.Max(cell.Y * CellHeight + CellHeight, 1),
                    Stroke = Brush,
                    StrokeThickness = 1
                });
            }

        }

        private void UpdateLocation(int x, int y)
        {

            Location.SetValue(Canvas.LeftProperty, (double)((x * CellWidth) + (CellWidth / 4)));
            Location.SetValue(Canvas.TopProperty, (double)((y * CellHeight) + (CellHeight / 4)));
        }

        private void MazeCanvas_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                LocationY--;
            }
            if (e.Key == Key.A)
            {
                LocationX--;
            }
            if (e.Key == Key.S)
            {
                LocationY++;
            }
            if (e.Key == Key.D)
            {
                LocationX++;
            }

            UpdateLocation(LocationX, LocationY);
        }

    }
}
