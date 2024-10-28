using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace BudgeIt.Data
{
    public class DBManager
    {
        private string? _currentDatabasePath;
        private SQLiteConnection _connection;

        public DBManager() { }

        public void OpenDatabase(string fileName)
        {
            if (_currentDatabasePath != null)
                CloseDatabase();

            _currentDatabasePath = fileName;

            var connectionString = new SQLiteConnectionStringBuilder()
            {
                DataSource = _currentDatabasePath
            };

            _connection = new SQLiteConnection(connectionString.ToString());

            _connection.Open();
        }

        public void InitializeNewDatabase()
        {
            var command = _connection.CreateCommand();
            command.CommandText = "CREATE TABLE transactions(ID TEXT PRIMARY KEY NOT NULL, Month INT, Day INT, Year INT, Vendor TEXT, Type INT, Amount REAL)";
            command.ExecuteNonQuery();

            command = _connection.CreateCommand();
            command.CommandText = "CREATE TABLE transactionTypes(ID INT PRIMARY KEY, Name TEXT); INSERT INTO tranactionTypes VALUES(0,'Unknown');";
            command.ExecuteNonQuery();
        }

        private void CloseDatabase()
        {
            _currentDatabasePath = null;
        }

        public void Init()
        {
        }

        public void Terminate()
        {
        }
    }
}
