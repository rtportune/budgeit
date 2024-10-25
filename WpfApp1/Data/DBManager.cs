using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace BudgeIt.Data
{
    public class DBManager
    {
        private string? _currentDatabasePath;

        public DBManager() { }

        public void OpenDatabase(string fileName)
        {
            if (_currentDatabasePath != null)
                CloseDatabase();

            _currentDatabasePath = fileName;
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
