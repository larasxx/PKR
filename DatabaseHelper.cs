using System.Data.SQLite;
using System.IO;

namespace PKR
{
    public class DatabaseHelper
    {
        private const string DatabaseFile = "PokerHandHistory.db";

        public static void InitializeDatabase()
        {
            // Check if the database file exists; if not, create it
            if (!File.Exists(DatabaseFile))
            {
                SQLiteConnection.CreateFile(DatabaseFile);
            }

            // Connect to the database and ensure the HandHistory table exists
            using (var connection = new SQLiteConnection($"Data Source={DatabaseFile};Version=3;"))
            {
                connection.Open();
                string createTableQuery = @"CREATE TABLE IF NOT EXISTS HandHistory (
                                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                Date TEXT,
                                                PlayerCount INTEGER,
                                                BigBlind INTEGER,
                                                SmallBlind INTEGER,
                                                Ante INTEGER,
                                                Pot INTEGER,
                                                Winner TEXT)";
                var command = new SQLiteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
