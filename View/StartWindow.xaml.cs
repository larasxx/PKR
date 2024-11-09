using System.Windows;

namespace PKR.View
{
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the MainWindow (game window)
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            // Close the StartWindow
            this.Close();
        }
    }
}
