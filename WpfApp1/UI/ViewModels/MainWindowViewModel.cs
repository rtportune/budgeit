using BudgeIt.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Reflection;
using BudgeIt.Interface.UI;

namespace BudgeIt.UI.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private DBManager _dbManager;
        private RelayCommand _cmdOpenDatabase;
        private RelayCommand _cmdNewDatabase;

        private string _databaseName;

        public MainWindowViewModel()
        {
            _dbManager = new DBManager();
        }

        public ICommand CmdOpenDatabase => _cmdOpenDatabase ?? (_cmdOpenDatabase = new RelayCommand(
            f =>
            {
                try
                {
                    var dialog = new OpenFileDialog()
                    {
                        DefaultDirectory = Assembly.GetExecutingAssembly().Location
                    };

                    if (dialog.ShowDialog() == true)
                    {
                        _databaseName = dialog.FileName;

                        _dbManager.OpenDatabase(_databaseName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to open database" + ex.Message);           
                }
            },
            o => true));

        public ICommand CmdNewDatabase => _cmdNewDatabase ?? (_cmdNewDatabase = new RelayCommand(
            f =>
            {
                try
                {
                    var dialog = new SaveFileDialog()
                    {                     
                        DefaultDirectory = Assembly.GetExecutingAssembly().Location,
                        Filter = "Databases|*.db"
                    };

                    if (dialog.ShowDialog() == true)
                    {
                        _databaseName = dialog.FileName;

                        _dbManager.OpenDatabase(_databaseName);
                        _dbManager.InitializeNewDatabase();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to create database" + ex.Message);
                }
            },
            o => true));
    }
}
