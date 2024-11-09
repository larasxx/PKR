using System.Data;
using System.Data.SQLite;
using System.Windows;

namespace PKR.View
{
    public partial class GameHistoryWindow : Window
    {
        public GameHistoryWindow()
        {
            InitializeComponent();
            DatabaseHelper.InitializeDatabase();  // Ensure database and table are created
            LoadGameHistory();
        }


        private void LoadGameHistory()
        {
            using (var connection = new SQLiteConnection("Data Source=PokerHandHistory.db;Version=3;"))
            {
                connection.Open();
                string query = "SELECT * FROM HandHistory";
                using (var command = new SQLiteCommand(query, connection))
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    HistoryDataGrid.ItemsSource = dataTable.DefaultView;
                }
            }
        }
    }
}
