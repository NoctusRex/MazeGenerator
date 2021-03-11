using System.Windows;

namespace Develop
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public int MazeHeight;
        public int MazeWidth;
        public int? MazeSeed;

        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (!int.TryParse(Height.Text, out MazeHeight)) return;
            if (!int.TryParse(Width.Text, out MazeWidth)) return;

            if (int.TryParse(Seed.Text, out int seed))
                MazeSeed = seed;
            else
                MazeSeed = null;

            DialogResult = true;
        }
    }
}
